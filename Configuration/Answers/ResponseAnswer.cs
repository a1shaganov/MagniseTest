using System;
using System.Collections.Generic;
using System.Text;

namespace Configuration.Answers
{
    /// <summary>
    /// Class keep response answer
    /// </summary>
    public class ResponseAnswer : AbstractAnswer
    {
        /// <summary>
        /// Session identificator
        /// </summary>
        public String requestId
        { get; set; }
    }
}
