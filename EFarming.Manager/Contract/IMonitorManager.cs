using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFarming.Manager.Contract
{
    /// <summary>
    /// Monitor Manager
    /// </summary>
    public interface IMonitorManager
    {
        /// <summary>
        /// Determines whether [is database up].
        /// </summary>
        /// <returns>boot with the state of the database</returns>
        bool IsDatabaseUp();
    }
}
