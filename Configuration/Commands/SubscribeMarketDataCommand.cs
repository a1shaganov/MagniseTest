using System;
using System.Collections.Generic;

namespace Configuration.Commands
{
    /// <summary>
    /// Class keep command data for subscribe
    /// </summary>
    public class SubscribeMarketDataCommand
    {
        #region Properties
        /// <summary>
        /// Subscribe type
        /// </summary>
        public String type
        { get; set; }

        /// <summary>
        /// Identificator
        /// </summary>
        public String id
        { get; set; }

        /// <summary>
        /// Instrument identificator
        /// </summary>
        public String instrumentId
        { get; set; }

        /// <summary>
        /// Provider
        /// </summary>
        public String provider
        { get; set; }

        /// <summary>
        /// Flag subscribe
        /// </summary>
        public Boolean subscribe
        { get; set; }

        /// <summary>
        /// Kinds
        /// </summary>
        public List<String> kinds
        { get; set; }
        #endregion

        #region Constructor
        /// <summary>
        /// Constructor
        /// </summary>
        public SubscribeMarketDataCommand()
        {
            type = "l1-subscription";
            subscribe = true;
            kinds = new List<String>()
            { "ask", "bid", "last" };
        }
        #endregion
    }
}