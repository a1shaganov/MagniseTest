using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Configuration.Communication
{
    /// <summary>
    /// Class keep provider information
    /// </summary>
    public class ExchangesInformation
    {
        #region Properties
        /// <summary>
        /// List of oanda providers
        /// </summary>
        public List<String> oanda
        { get; set; }

        /// <summary>
        /// List of dxfeed providers
        /// </summary>
        public List<String> dxfeed
        { get; set; }

        /// <summary>
        /// List of simulation providers
        /// </summary>
        public List<String> simulation
        { get; set; }

        /// <summary>
        /// List of alpaca providers
        /// </summary>
        public List<String> alpaca
        { get; set; }

        /// <summary>
        /// List of cryptoquote providers
        /// </summary>
        public List<String> cryptoquote
        { get; set; }

        /// <summary>
        /// List of active-tick providers
        /// </summary>
        [JsonPropertyName("active-tick")]
        public List<String> activetick
        { get; set; }
        #endregion

        #region Constructor
        /// <summary>
        /// Constructor
        /// </summary>
        public ExchangesInformation()
        {
            oanda = new List<String>();
            dxfeed = new List<String>();
            simulation = new List<String>();
            alpaca = new List<String>();
            cryptoquote = new List<String>();
            activetick = new List<String>();
        }
        #endregion
    }
}