using System;
using System.Collections.Generic;
using System.Text;

namespace Configuration.Answers
{
    /// <summary>
    /// Abstract class keep type of answer
    /// </summary>
    public abstract class AbstractAnswer
    {
        #region Properties
        /// <summary>
        /// Session type
        /// </summary>
        public String type
        { get; set; }
        #endregion
    }
}
