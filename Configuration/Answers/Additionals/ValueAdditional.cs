using System;

namespace Configuration.Answers.Additionals
{
    /// <summary>
    /// Class keep value additional for answer
    /// </summary>
    public class ValueAdditional
    {
        /// <summary>
        /// Timestamp update infor for value
        /// </summary>
        public String timestamp
        { get; set; }

        /// <summary>
        /// Price of value
        /// </summary>
        public Double price
        { get; set; }

        /// <summary>
        /// volume of value
        /// </summary>
        public Int32 volume
        { get; set; }

        /// <summary>
        /// Change of value
        /// </summary>
        public Double change
        { get; set; }

        /// <summary>
        /// Change of value
        /// </summary>
        public Double changePct
        { get; set; }
    }
}