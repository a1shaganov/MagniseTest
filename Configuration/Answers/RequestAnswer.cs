using Configuration.Answers.Additionals;
using System;

namespace Configuration.Answers
{
    /// <summary>
    /// Class keep answer on request
    /// </summary>
    public class RequestAnswer : AbstractAnswer
    {
        /// <summary>
        /// Instrument identificator
        /// </summary>
        public String instrumentId
        { get; set; }

        /// <summary>
        /// provider
        /// </summary>
        public String provider
        { get; set; }

        /// <summary>
        /// Last information
        /// </summary>
        public ValueAdditional last
        { get; set; }

        /// <summary>
        /// Bid information
        /// </summary>
        public ValueAdditional bid
        { get; set; }

        /// <summary>
        /// Bid information
        /// </summary>
        public ValueAdditional ask
        { get; set; }
    }
}