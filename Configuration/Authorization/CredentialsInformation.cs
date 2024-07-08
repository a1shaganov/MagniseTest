using System;
using System.Collections.Generic;
using System.Net.Http;

namespace Configuration.Authorization
{
    /// <summary>
    /// Class keep credentials for work with servers
    /// </summary>
    public class CredentialsInformation
    {
        #region Properties
        /// <summary>
        /// Grand type
        /// </summary>
        public String grant_type
        { get; set; }

        /// <summary>
        /// Client ID
        /// </summary>
        public String client_id
        { get; set; }

        /// <summary>
        /// Username
        /// </summary>
        public String username
        { get; set; }

        /// <summary>
        /// Password
        /// </summary>
        public String password
        { get; set; }
        #endregion

        #region Methods
        /// <summary>
        /// Get credentials content
        /// </summary>
        /// <returns>Content</returns>
        public FormUrlEncodedContent GetCredentialsContent()
        {
            return new FormUrlEncodedContent(new Dictionary<string, string>
            {
                { nameof(grant_type), grant_type },
                { nameof(client_id), client_id },
                { nameof(username), username },
                { nameof(password), password },
            });
        }
        #endregion
    }
}