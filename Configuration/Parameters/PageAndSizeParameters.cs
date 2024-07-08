using System;
using System.Collections.Generic;

namespace Configuration.Parameters
{
    /// <summary>
    /// Class keep parameters for page and size
    /// </summary>
    public class PageAndSizeParameters : DefaultParameters
    {
        #region Properties
        /// <summary>
        /// Page
        /// </summary>
        public UInt32 page
        { get; set; }

        /// <summary>
        /// Size
        /// </summary>
        public UInt32 size
        { get; set; }
        #endregion
    }
}