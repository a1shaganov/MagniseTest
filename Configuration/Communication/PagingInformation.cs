using System;

namespace Configuration.Communication
{
    /// <summary>
    /// Class for paging informations
    /// </summary>
    public class PagingInformation
    {
        #region Properties
        /// <summary>
        /// Page number
        /// </summary>
        public UInt32 page
        { get; set; }

        /// <summary>
        /// Number of pages
        /// </summary>
        public UInt32 pages
        { get; set; }

        /// <summary>
        /// Number of items
        /// </summary>
        public UInt32 items
        { get; set; }
        #endregion
    }
}