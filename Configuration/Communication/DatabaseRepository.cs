using System;

namespace Configuration.Communication
{
    /// <summary>
    /// Class keep simulation information
    /// </summary>
    public class ExCryptoquoteInformation : CryptoquoteInformation
    {
        #region Properties
        /// <summary>
        /// Default order size
        /// </summary>
        public UInt32 defaultOrderSize
        { get; set; }
        #endregion
    }
}