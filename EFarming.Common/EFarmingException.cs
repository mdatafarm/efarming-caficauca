using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFarming.Common
{
    /// <summary>
    /// Efarming Exeption tool
    /// </summary>
    public class EFarmingException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="EFarmingException"/> class.
        /// </summary>
        public EFarmingException() { }
        /// <summary>
        /// Initializes a new instance of the <see cref="EFarmingException"/> class.
        /// </summary>
        /// <param name="message">The message that describes the error.</param>
        public EFarmingException(string message) : base(message) { }
        /// <summary>
        /// Initializes a new instance of the <see cref="EFarmingException"/> class.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="inner">The inner.</param>
        public EFarmingException(string message, Exception inner) : base(message, inner) { }

        /// <summary>
        /// Raises the exception.
        /// </summary>
        /// <param name="e">The Exception</param>
        /// <returns></returns>
        public static EFarmingException RaiseException(Exception e)
        {
            return new EFarmingException(GetInnerMessage(e), e);
        }

        /// <summary>
        /// Raises the exception.
        /// </summary>
        /// <param name="title">The title.</param>
        /// <param name="e">The Exception</param>
        /// <returns></returns>
        public static EFarmingException RaiseException(string title, Exception e)
        {
            return new EFarmingException(string.Concat(title, ": ", GetInnerMessage(e)), e);
        }

        /// <summary>
        /// Gets the inner message.
        /// </summary>
        /// <param name="e">The Exception</param>
        /// <returns></returns>
        public static string GetInnerMessage(Exception e)
        {
            if (e.InnerException != null)
                GetInnerMessage(e.InnerException);
            return e.Message;
        }
    }
}