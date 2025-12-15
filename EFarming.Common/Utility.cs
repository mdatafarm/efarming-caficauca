using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EFarming.Common
{
    /// <summary>
    /// Utility class
    /// </summary>
    static class Utility
    {

        // utilty function to convert string to byte[]        
        /// <summary>
        /// Gets the bytes.
        /// </summary>
        /// <param name="str">The string.</param>
        /// <returns></returns>
        public static byte[] GetBytes(string str)
        {
            byte[] bytes = ASCIIEncoding.ASCII.GetBytes(str);
            return bytes;
        }

        // utilty function to convert byte[] to string        
        /// <summary>
        /// Gets the string.
        /// </summary>
        /// <param name="bytes">The bytes.</param>
        /// <returns></returns>
        public static string GetString(byte[] bytes)
        {
            char[] chars = ASCIIEncoding.ASCII.GetChars(bytes);
            return new string(chars);
        }
    }
}
