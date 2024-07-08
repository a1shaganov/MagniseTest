using Communicators.Communication;
using Configuration.Answers;
using Configuration.Authorization;
using Configuration.Commands;
using Configuration.Communication;
using Database;
using Database.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MagniseWebAPI.Storages
{
    /// <summary>
    /// Class keep storage for all informations
    /// </summary>
    public class DataStorage : IDisposable
    {
        #region Fields
        /// <summary>
        /// A single instanceof the data storage class
        /// </summary>
        private static DataStorage _instance = null;

        /// <summary>
        /// Object for lock
        /// </summary>
        private Object Locker;

        /// <summary>
        /// Currency repository
        /// </summary>
        private CurrencyRepository Database;

        /// <summary>
        /// Web communicator that connected to REST API
        /// </summary>
        private WebCommunicator webCommunicator;

        /// <summary>
        /// Web socket communicator that connected to server with realtime values
        /// </summary>
        private WebSocketCommunicator webSocketCommunicator;

        /// <summary>
        /// Endpoint for API
        /// </summary>
        private String EndpointApi;

        /// <summary>
        /// Endpoint for socket
        /// </summary>
        private String EndpointSocket;

        /// <summary>
        /// Paths for API
        /// </summary>
        private IDictionary<String, String> ApiPaths;

        /// <summary>
        /// Token information
        /// </summary>
        private TokenInformation Token;

        /// <summary>
        /// Subscribed values
        /// </summary>
        private List<SubscribeMarketDataCommand> SubscribedValues;

        /// <summary>
        /// Identificator command
        /// </summary>
        private Int32 idCommand;

        /// <summary>
        /// Currency values
        /// </summary>
        private List<ValueInformation> InternalCurrencyValues;
        #endregion

        #region Properties
        /// <summary>
        /// A single instanceof the data storage class
        /// </summary>
        public static DataStorage Instance
        { get { return _instance ?? (_instance = new DataStorage()); } }
        #endregion

        #region Constructor
        /// <summary>
        /// Constructor
        /// </summary>
        private DataStorage()
        {
            Locker = new Object();
            idCommand = 0;
            InternalCurrencyValues = new List<ValueInformation>();
            SubscribedValues = new List<SubscribeMarketDataCommand>();
            Database = new CurrencyRepository();
        }
        #endregion

        #region Methods
        /// <summary>
        /// Initialize endpoints and API paths
        /// </summary>
        /// <param name="endpointApi">Endpoint for API</param>
        /// <param name="endpointSocket">Endpoint for socket</param>
        /// <param name="apiPaths">Paths for API</param>
        /// <param name="user">User</param>
        /// <param name="password">Password</param>
        public void Initialize(String endpointApi, String endpointSocket,
            IDictionary<String, String> apiPaths, String user, String password)
        {
            if (String.IsNullOrEmpty(endpointApi))
                throw new ArgumentNullException("Endpoint for API is null or empty");

            if (String.IsNullOrEmpty(endpointSocket))
                throw new ArgumentNullException("Endpoint for socket is null or empty");

            if (apiPaths == null)
                throw new ArgumentNullException("Paths for API is null");

            EndpointApi = endpointApi;
            EndpointSocket = endpointSocket;
            ApiPaths = apiPaths;

            Task task = ConfigureCommunicators(user, password);
            task.Wait();

            task = LoadAndCheckValues();
            task.Wait();
        }

        /// <summary>
        /// Method for configure communicators
        /// </summary>
        /// <param name="user">User</param>
        /// <param name="password">Password</param>
        /// <returns>Resulkt task</returns>
        private async Task ConfigureCommunicators(String user, String password)
        {
            if (!ApiPaths.ContainsKey("Token"))
                throw new Exception("Path for token API not found");

            CredentialsInformation credentials = new CredentialsInformation()
            {
                grant_type = "password",
                client_id = "app-cli",
                username = user,
                password = password
            };

            webCommunicator = new WebCommunicator(EndpointApi);
            Token = await webCommunicator.GetTokenInformation(credentials, ApiPaths["Token"]);

            if (Token == null)
                throw new Exception("Token is null");

            webSocketCommunicator = new WebSocketCommunicator("wss://platform.fintacharts.com/api/streaming/ws/v1/realtime?token=");
            webSocketCommunicator.AnswerRecived += WebSocketCommunicator_AnswerRecived;
            webSocketCommunicator.SocketError += WebSocketCommunicator_SocketError;
            await webSocketCommunicator.Connect(Token);
        }

        /// <summary>
        /// Method load and check values
        /// </summary>
        /// <returns>Result task</returns>
        public async Task LoadAndCheckValues()
        {
            if (!ApiPaths.ContainsKey("Values"))
                throw new Exception("Path for values API not found");

            Configuration.Parameters.PageAndSizeParameters parameters = new Configuration.Parameters.PageAndSizeParameters()
            {
                page = 1,
                size = 100
            };

            InternalCurrencyValues.Clear();

            GlobalInformation globalInformation = await webCommunicator.GetValueInformation<GlobalInformation, Configuration.Parameters.PageAndSizeParameters>(parameters, ApiPaths["Values"]);
            InternalCurrencyValues.AddRange(globalInformation.data);
            await CheckGlobalInformation(globalInformation);

            if (globalInformation.paging.pages == 1)
                return;

            parameters.page++;
            while (parameters.page <= globalInformation.paging.pages)
            {
                globalInformation = await webCommunicator.GetValueInformation<GlobalInformation, Configuration.Parameters.PageAndSizeParameters>(parameters, ApiPaths["Values"]);
                InternalCurrencyValues.AddRange(globalInformation.data);
                await CheckGlobalInformation(globalInformation);

                parameters.page++;
            }
        }

        /// <summary>
        /// Check global information in database
        /// </summary>
        /// <param name="globalInformation">Global information</param>
        private async Task CheckGlobalInformation(GlobalInformation globalInformation)
        {
            foreach (ValueInformation value in globalInformation.data)
            {
                Boolean check = await CheckValueInDatabase(value, value.mappings.alpaca, nameof(value.mappings.alpaca));
                check |= await CheckValueInDatabase(value, value.mappings.simulation, nameof(value.mappings.simulation));
                check |= await CheckValueInDatabase(value, value.mappings.activetick, nameof(value.mappings.activetick));
                check |= await CheckValueInDatabase(value, value.mappings.cryptoquote, nameof(value.mappings.cryptoquote));
                check |= await CheckValueInDatabase(value, value.mappings.dxfeed, nameof(value.mappings.dxfeed));
                check |= await CheckValueInDatabase(value, value.mappings.oanda, nameof(value.mappings.oanda));

                if (check)
                    Database.SaveDataToDatabase();
            }
        }

        /// <summary>
        /// Check and save value to database
        /// </summary>
        /// <param name="value">Value</param>
        /// <param name="cryptoInformation">Crypro information</param>
        /// <param name="map">Map for value</param>
        /// <returns>Task result</returns>
        private async Task<Boolean> CheckValueInDatabase(ValueInformation value, CryptoquoteInformation cryptoInformation, String map)
        {
            if (cryptoInformation == null)
                return false;

            IEnumerable<CurrencyEntity> entities = await Database.GetEntityItemBySpecialsIdAsync(value.id);
            if (entities.FirstOrDefault(item => item.Map == map) == null)
            {
                Database.AddEntityItem(new CurrencyEntity()
                {
                    IdentificatorValue = value.id,
                    Currency = value.currency,
                    Description = value.description,
                    Exchange = value.exchange,
                    Kind = value.kind,
                    Symbol = value.symbol,
                    Map = map,
                    AskValue = 0.0,
                    BidValue = 0.0,
                    LastValue = 0.0
                });

                return true;
            }

            return false;
        }

        /// <summary>
        /// Get all currency values
        /// </summary>
        /// <returns>List of all currencies</returns>
        public async Task<IEnumerable<CurrencyEntity>> GetAllCurrencyValues()
        {
            return await Database.GetAllEntityItemsAsync();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="identificators"></param>
        /// <param name="providers"></param>
        /// <returns></returns>
        public async Task<IEnumerable<CurrencyEntity>> GetSelectedCurrencyValues(IEnumerable<String> identificators, IEnumerable<String> providers)
        {
            List<CurrencyEntity> result = new List<CurrencyEntity>();
            foreach(String identificator in identificators)
            {
                IEnumerable<CurrencyEntity> entities = null;
                lock (Locker)
                    entities = Database.GetEntityItemBySpeciaslId(identificator);

                if (entities.Count() == 0)
                    continue;

                foreach (CurrencyEntity entity in entities)
                {
                    if ((providers != null) && !providers.Contains(entity.Map))
                        continue;

                    SubscribeMarketDataCommand cmd = SubscribedValues.FirstOrDefault(item =>
                        (item.instrumentId == identificator) && (item.provider == entity.Map));
                    if (cmd != null)
                        continue;

                    idCommand++;
                    cmd = new SubscribeMarketDataCommand()
                    {
                        id = idCommand.ToString(),
                        instrumentId = identificator,
                        provider = entity.Map
                    };

                    SubscribedValues.Add(cmd);
                    await webSocketCommunicator.SendCommand(cmd);
                }

                result.AddRange(entities);
            }

            return result;
        }

        /// <summary>
        /// Method for event that notify answer recived
        /// </summary>
        /// <param name="sender">Object that execute event</param>
        /// <param name="e">Parameters</param>
        private void WebSocketCommunicator_AnswerRecived(Object sender, AbstractAnswer e)
        {
            if (e is RequestAnswer)
            {
                IEnumerable<CurrencyEntity> entities = null;
                lock (Locker)
                {
                    entities = Database.GetEntityItemBySpeciaslId((e as RequestAnswer).instrumentId);
                    CurrencyEntity entity = entities.FirstOrDefault(item => item.Map == (e as RequestAnswer).provider);
                    if (entity != null)
                    {
                        if ((e as RequestAnswer).ask != null)
                        {
                            entity.AskValue = (e as RequestAnswer).ask.price;
                            entity.AskDatetime = (e as RequestAnswer).ask.timestamp;
                        }

                        if ((e as RequestAnswer).bid != null)
                        {
                            entity.BidValue = (e as RequestAnswer).bid.price;
                            entity.BidDatetime = (e as RequestAnswer).bid.timestamp;
                        }

                        if ((e as RequestAnswer).last != null)
                        {
                            entity.LastValue = (e as RequestAnswer).last.price;
                            entity.LastDatetime = (e as RequestAnswer).last.timestamp;
                        }

                        Database.SaveDataToDatabase();
                    }
                }
            }
        }

        /// <summary>
        /// Method for event that notify socket has error
        /// </summary>
        /// <param name="sender">Object that execute event</param>
        /// <param name="e">Parameters</param>
        private void WebSocketCommunicator_SocketError(object sender, EventArgs e)
        {
        }

        /// <summary>
        /// Dispose this class and all variables in class
        /// </summary>
        public void Dispose()
        {
            Task task = webSocketCommunicator.Disconnect();
            task.Wait();

            Database.Dispose();
        }
        #endregion
    }
}
