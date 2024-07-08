using System;
using System.Collections.Generic;
using System.Text;

namespace Configuration.Communication
{
    /// <summary>
    /// Class keep value information
    /// </summary>
    public class ValueInformation
    {
        #region Fields
        /// <summary>
        /// Identificator
        /// </summary>
        public String id
        { get; set; }

        /// <summary>
        /// Symbol calue
        /// </summary>
        public String symbol
        { get; set; }

        /// <summary>
        /// Kind value
        /// </summary>
        public String kind
        { get; set; }

        /// <summary>
        /// Exchange name
        /// </summary>
        public String exchange
        { get; set; }

        /// <summary>
        /// Description
        /// </summary>
        public String description
        { get; set; }

        /// <summary>
        /// Tick size
        /// </summary>
        public Double tickSize
        { get; set; }

        /// <summary>
        /// Currency name
        /// </summary>
        public String currency
        { get; set; }

        /// <summary>
        /// Base currency name
        /// </summary>
        public String baseCurrency
        { get; set; }

        /// <summary>
        /// Value mapping
        /// </summary>
        public MappingInformation mappings
        { get; set; }
        #endregion
    }
}