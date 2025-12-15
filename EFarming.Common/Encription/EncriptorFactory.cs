using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFarming.Common.Encription
{
    /// <summary>
    /// Encription Factory
    /// </summary>
    public static class EncriptorFactory
    {
        #region Members

        /// <summary>
        /// The _current encriptor factory
        /// </summary>
        static IEncriptorFactory _currentEncriptorFactory = null;

        #endregion

        #region Public Methods

        /// <summary>
        /// Set the  log factory to use
        /// </summary>
        /// <param name="encriptorFactory">Log factory to use</param>
        public static void SetCurrent(IEncriptorFactory encriptorFactory)
        {
            _currentEncriptorFactory = encriptorFactory;
        }

        /// <summary>
        /// Createt a new Log
        /// </summary>
        /// <returns>
        /// Created ILog
        /// </returns>
        public static IEncriptor CreateEncriptor()
        {
            return (_currentEncriptorFactory != null) ? _currentEncriptorFactory.Create() : null;
        }

        #endregion
    }
}
