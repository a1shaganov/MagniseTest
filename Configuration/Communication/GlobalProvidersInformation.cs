using System;
using System.Collections.Generic;

namespace Configuration.Communication
{
    /// <summary>
    /// Class keep providers infromation
    /// </summary>
    public class GlobalProvidersInformation
    {
        #region Properties
        /// <summary>
        /// List of providers
        /// </summary>
        public List<String> data
        { get; set; }
        #endregion

        #region Constrictor
        /// <summary>
        /// Constructor
        /// </summary>
        public GlobalProvidersInformation()
        {
            data = new List<String>();
        }
        #endregion
    }
}