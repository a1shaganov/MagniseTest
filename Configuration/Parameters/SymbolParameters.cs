using System;
using System.Collections.Generic;

namespace Configuration.Parameters
{
    /// <summary>
    /// Class keep parameters for symbol value
    /// </summary>
    public class SymbolParameters : DefaultParameters
    {
        #region Properties
        /// <summary>
        /// Type of value
        /// </summary>
        public String symbol
        { get; set; }
        #endregion
    }
}
