using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFarming.Core.FarmModule.FarmAggregate
{
    /// <summary>
    /// FarmRepository Interface
    /// </summary>
    public interface IFarmRepository : IRepository<Farm>
    {
        /// <summary>
        /// Farms the by code.
        /// </summary>
        /// <param name="code">The code.</param>
        /// <returns></returns>
        List<Farm> FarmByCode(string code);
        /// <summary>
        /// Gets the farm by code.
        /// </summary>
        /// <param name="code">The code.</param>
        /// <returns></returns>
        Farm GetFarmByCode(string code);
    }
}
