using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFarming.Common.Consts
{
    /// <summary>
    /// Quality Atrributes Types
    /// </summary>
    public static class QualityAttributeTypes
    {
        /// <summary>
        /// The range
        /// </summary>
        public static string RANGE = "RANGE";
        /// <summary>
        /// The options
        /// </summary>
        public static string OPTIONS = "OPTIONS";
        /// <summary>
        /// The ope n_ text
        /// </summary>
        public static string OPEN_TEXT = "OPEN_TEXT";
        /// <summary>
        /// The types
        /// </summary>
        public static List<string> TYPES = new List<string> { RANGE, OPTIONS, OPEN_TEXT };
    }
}
