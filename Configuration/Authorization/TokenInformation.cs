using System;

namespace Configuration.Authorization
{
    /// <summary>
    /// Class keep token informations for work with servers
    /// </summary>
    public class TokenInformation
    {
        #region Properties
        /// <summary>
        /// Access token
        /// </summary>
        public string access_token
        { get; set; }

        /// <summary>
        /// Refresh token
        /// </summary>
        public string refresh_token
        { get; set; }

        /// <summary>
        /// Expires time
        /// </summary>
        public Int32 expires_in
        { get; set; }

        /// <summary>
        /// Expires time for refresh
        /// </summary>
        public Int32 refresh_expires_in
        { get; set; }

        /// <summary>
        /// Type of token
        /// </summary>
        public String token_type
        { get; set; }

        /// <summary>
        /// Session state
        /// </summary>
        public String session_state
        { get; set; }
        #endregion
    }
}