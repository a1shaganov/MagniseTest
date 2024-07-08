using System.Runtime.Serialization;
using System.Text.Json.Serialization;

namespace Configuration.Communication
{
    /// <summary>
    /// Class kepp mappings information
    /// </summary>
    [DataContract]
    public class MappingInformation
    {
        /// <summary>
        /// Cryptoquote information
        /// </summary>
        public CryptoquoteInformation cryptoquote
        { get; set; }

        /// <summary>
        /// Simulation information
        /// </summary>
        public ExCryptoquoteInformation simulation
        { get; set; }

        /// <summary>
        /// Alpace information
        /// </summary>
        public ExCryptoquoteInformation alpaca
        { get; set; }

        /// <summary>
        /// Alpace information
        /// </summary>
        public ExCryptoquoteInformation dxfeed
        { get; set; }

        /// <summary>
        /// Oanda information
        /// </summary>
        public ExCryptoquoteInformation oanda
        { get; set; }

        /// <summary>
        /// Oanda information
        /// </summary>
        [JsonPropertyName("active-tick")]
        public ExCryptoquoteInformation activetick
        { get; set; }
    }
}