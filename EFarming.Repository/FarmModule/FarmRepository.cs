using EFarming.Core.FarmModule.FarmAggregate;
using EFarming.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFarming.Repository.FarmModule
{
    /// <summary>
    /// Farm Repository
    /// </summary>
    public class FarmRepository : Repository<Farm>, IFarmRepository
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="FarmRepository"/> class.
        /// </summary>
        /// <param name="unitOfWork">The unit of work.</param>
        public FarmRepository(UnitOfWork unitOfWork) : base(unitOfWork) { }

        /// <summary>
        /// Farms the by code.
        /// </summary>
        /// <param name="code">The code.</param>
        /// <returns></returns>
        public List<Farm> FarmByCode(string code)
        {
            var set = GetSet().Where(f => f.Code == code);
            return set.ToList();
        }

        /// <summary>
        /// Gets the farm by code.
        /// </summary>
        /// <param name="code">The code.</param>
        /// <returns></returns>
        public Farm GetFarmByCode(string code)
        {
            Farm Farm = new Farm();
            try
            {
                Farm = GetSet().Where(f => f.Code == code).First();
            }catch(Exception e)
            {

            }
                
            return Farm;
        }
    }
}
