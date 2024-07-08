using Configuration.Answers;
using Configuration.Authorization;
using Configuration.Commands;
using System;
using System.Net.WebSockets;
using System.Text;
using System.Text.Json;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;

namespace Communicators.Communication
{
    /// <summary>
    /// Class that communicate with web socket service 
    /// </summary>
    public class WebSocketCommunicator
    {
        #region Properties
        /// <summary>
        /// Web socket client
        /// </summary>
        private ClientWebSocket Client;

        /// <summary>
        /// String host for connection
        /// </summary>
        private String Host;

        /// <summary>
        /// Token for authorization
        /// </summary>
        private TokenInformation Token;

        /// <summary>
        /// Cancel token
        /// </summary>
        private CancellationTokenSource CancelToken;

        /// <summary>
        /// Communicator thread that recive data from web socket
        /// </summary>
        private Thread CommunicatorThread;

        /// <summary>
        /// Flag show that web socket connected
        /// </summary>
        private Boolean IsConnected;
        #endregion

        #region Events
        /// <summary>
        /// Answer recived
        /// </summary>
        public event EventHandler<AbstractAnswer> AnswerRecived;

        /// <summary>
        /// Socket error
        /// </summary>
        public event EventHandler SocketError;
        #endregion

        #region Constructor
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="uri">URI for connection to web socket</param>
        /// <param name="token">Token for authorization</param>
        public WebSocketCommunicator(String host)
        {
            if (String.IsNullOrEmpty(host))
                throw new ArgumentNullException("Host is null or empty");

            Host = host;

            CancelToken = new CancellationTokenSource();
            Client = new ClientWebSocket();
        }
        #endregion

        #region Methods
        /// <summary>
        /// Connect to web socket
        /// </summary>
        /// <returns>Flag mean that connection was success</returns>
        public async Task<Boolean> Connect(TokenInformation token)
        {
            if (token == null)
                throw new ArgumentNullException("Token is null or empty");

            Token = token;
            await Client.ConnectAsync(new Uri(Host + Token.access_token), CancelToken.Token);

            IsConnected = Client.State == WebSocketState.Open;
            if (IsConnected)
            {
                CommunicatorThread = new Thread(new ParameterizedThreadStart(ReciveDataFromSocket));
                CommunicatorThread.Start();
            }

            return IsConnected;
        }

        /// <summary>
        /// Disconnect from web socket
        /// </summary>
        /// <returns>Result task</returns>
        public async Task Disconnect()
        {
            try
            {
                IsConnected = false;
                Client.Dispose();
                await Task.Delay(100);
                CommunicatorThread.Abort();
            }
            catch { }
        }

        /// <summary>
        /// Method for thread that recive data from web socket
        /// </summary>
        /// <param name="obj">Parameter</param>
        private async void ReciveDataFromSocket(object obj)
        {
            try
            {
                while (true)
                {
                    AbstractAnswer answer = null;

                    Byte[] buffer = new Byte[1024];
                    WebSocketReceiveResult result = await Client.ReceiveAsync(new ArraySegment<byte>(buffer), CancellationToken.None);
                    String str = Encoding.ASCII.GetString(buffer);

                    Int32 cnt = Regex.Matches(str, "\0").Count;
                    if (cnt > 0)
                        str = str.Substring(0, str.Length - cnt);

                    if (str.Contains("session"))
                        answer = JsonSerializer.Deserialize<SessionAnswer>(str);
                    else if (str.Contains("multi-session"))
                        answer = JsonSerializer.Deserialize<ExSessionAnswer>(str);
                    else if (str.Contains("requestId"))
                        answer = JsonSerializer.Deserialize<ResponseAnswer>(str);
                    else if (str.Contains("instrumentId"))
                        answer = JsonSerializer.Deserialize<RequestAnswer>(str);

                    if (AnswerRecived != null)
                        AnswerRecived(this, answer);
                }
            }
            catch
            {
                if (SocketError != null)
                    SocketError(this, EventArgs.Empty);
            }
        }

        /// <summary>
        /// Send command
        /// </summary>
        /// <param name="command">Command</param>
        /// <returns>Task result</returns>
        public async Task SendCommand(SubscribeMarketDataCommand command)
        {
            if (command == null)
                throw new Exception("Command is null or empty");

            String jsonString = JsonSerializer.Serialize(command);
            Byte[] bytes = Encoding.ASCII.GetBytes(jsonString);
            await Client.SendAsync(new ArraySegment<byte>(bytes), WebSocketMessageType.Text, true, CancellationToken.None);
        }
        #endregion
    }
}