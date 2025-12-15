using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFarming.Common.Encription
{
    /// <summary>
    /// Entriptor Create
    /// </summary>
    public class SHA256EncriptorFactory : IEncriptorFactory
    {
        /// <summary>
        /// Creates this instance.
        /// </summary>
        /// <returns></returns>
        public IEncriptor Create()
        {
            return new SHA256Encriptor();
        }
    }
}
