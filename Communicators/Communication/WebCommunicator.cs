using System;
using System.Net.Http;
using System.Net;
using System.Threading.Tasks;
using Configuration.Authorization;
using System.Net.Http.Headers;
using System.Collections.Specialized;
using System.Reflection;
using System.Web;

namespace Communicators.Communication
{
    /// <summary>
    /// Class that communicate with REST service 
    /// </summary>
    public class WebCommunicator
    {
        #region Fields
        /// <summary>
        /// Client that communicate with server
        /// </summary>
        private HttpClient Client;

        /// <summary>
        /// String host for connection
        /// </summary>
        private String Host;
        #endregion

        #region Constructor
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="host">Web host for connection</param>
        public WebCommunicator(String host)
        {
            if (String.IsNullOrEmpty(host))
                throw new ArgumentNullException("Variable host is empty");

            Host = host;
            Client = new HttpClient();
        }
        #endregion

        #region Methods
        /// <summary>
        /// Get token informations
        /// </summary>
        /// <param name="credentials">Credentials information</param>
        /// <param name="endpoint">Endpoint</param>
        /// <returns>Token informations</returns>
        public async Task<TokenInformation> GetTokenInformation(CredentialsInformation credentials, String endpoint)
        {
            if (credentials == null)
                throw new ArgumentNullException("Credentials data is null");

            if (String.IsNullOrEmpty(endpoint))
                throw new ArgumentNullException("Endpoint is null");

            HttpResponseMessage taskResponse = await Client.PostAsync(Host + endpoint, credentials.GetCredentialsContent());
            String result = await taskResponse.Content.ReadAsStringAsync();

            if (taskResponse.StatusCode != HttpStatusCode.OK)
                return null;

            TokenInformation token = System.Text.Json.JsonSerializer.Deserialize<TokenInformation>(result);
            Client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(token.token_type, token.access_token);
            return token;
        }

        /// <summary>
        /// Get value information
        /// </summary>
        /// <typeparam name="T">Type of response</typeparam>
        /// <param name="valueParameters">Value parameters</param>
        /// <param name="endpoint">Endpoint</param>
        /// <returns>Value informations</returns>
        public async Task<T> GetValueInformation<T, TParam>(TParam parameters, String endpoint) where T : class where TParam : class
        {
            String url = Host + endpoint;
            if (parameters != null)
                url = BuildQueryString<TParam>(url, parameters);

            HttpResponseMessage taskResponse = await Client.GetAsync(url);
            string result = await taskResponse.Content.ReadAsStringAsync();

            if (taskResponse.StatusCode != HttpStatusCode.OK)
                return null;

            return System.Text.Json.JsonSerializer.Deserialize<T>(result); ;
        }

        /// <summary>
        /// Build 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="url"></param>
        /// <param name="queryObject"></param>
        /// <returns></returns>
        private String BuildQueryString<T>(String url, T queryObject) where T : class
        {
            UriBuilder uriBuilder = new UriBuilder(url);
            NameValueCollection query = HttpUtility.ParseQueryString(uriBuilder.Query);

            PropertyInfo[] properties = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);
            foreach (PropertyInfo property in properties)
            {
                String name = property.Name;
                String value = Convert.ToString(property.GetValue(queryObject, null));
                if (!String.IsNullOrWhiteSpace(value))
                {
                    query[name] = HttpUtility.UrlEncode(value);
                }
            }

            uriBuilder.Query = query.ToString();
            return uriBuilder.ToString();
        }
        #endregion
    }
}