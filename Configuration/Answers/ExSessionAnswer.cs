using System;

namespace Configuration.Answers
{
    /// <summary>
    /// Class keep additional information about session
    /// </summary>
    public class ExSessionAnswer : SessionAnswer
    {
        /// <summary>
        /// Class keep session answer
        /// </summary>
        public class SessionAnswer
        {
            /// <summary>
            /// Flag keep opened session
            /// </summary>
            public Boolean opened
            { get; set; }

            /// <summary>
            /// Date time
            /// </summary>
            public DateTime timestamp
            { get; set; }
        }
    }
}