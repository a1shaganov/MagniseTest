using System.Collections.Generic;

namespace Configuration.Communication
{
    /// <summary>
    /// Class keep global information
    /// </summary>
    public class GlobalInformation
    {
        #region Properties
        /// <summary>
        /// Paging informations
        /// </summary>
        public PagingInformation paging
        { get; set; }

        /// <summary>
        /// List of value informations
        /// </summary>
        public List<ValueInformation> data
        { get; set; }
        #endregion

        #region Constructor
        /// <summary>
        /// Constructor
        /// </summary>
        public GlobalInformation()
        {
            data = new List<ValueInformation>();
        }
        #endregion
    }
}