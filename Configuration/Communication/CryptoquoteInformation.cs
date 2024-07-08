using System;

namespace Configuration.Communication
{
    /// <summary>
    /// Class keep cryptoquote information
    /// </summary>
    public class CryptoquoteInformation
    {
        #region Properties
        /// <summary>
        /// Symbol value
        /// </summary>
        public String symbol
        { get; set; }

        /// <summary>
        /// Name exchange
        /// </summary>
        public String exchange
        { get; set; }
        #endregion
    }
}