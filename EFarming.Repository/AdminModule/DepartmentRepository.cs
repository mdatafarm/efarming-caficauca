using EFarming.Common;
using EFarming.Common.Logging;
using EFarming.Core;
using EFarming.Core.AdminModule.DepartmentAggregate;
using EFarming.Core.AdminModule.MunicipalityAggregate;
using EFarming.Core.AdminModule.VillageAggregate;
using EFarming.Core.Specification.Contract;
using EFarming.DAL;
using EFarming.DAL.Contract;
using EFarming.DAL.Resources;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;

namespace EFarming.Repository.AdminModule
{
    /// <summary>
    /// Department Repository
    /// </summary>
    public class DepartmentRepository : Repository<Department>, IDepartmentRepository
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DepartmentRepository"/> class.
        /// </summary>
        /// <param name="unitOfWork">The unit of work.</param>
        public DepartmentRepository(UnitOfWork unitOfWork): base(unitOfWork)
        { }

        public class DepartmensToMovil
        {
            public List<Department> departments { get; set; }
            public List<Municipality> municipalities { get; set; }
            public List<Village> villages { get; set; }

        }

        /// <summary>
        /// Gets the full data.
        /// </summary>
        /// <returns>
        /// List of Deparment
        /// </returns>
        public List<Department> GetFullData()
        {
            UnitOfWork db = new UnitOfWork();

            var departments1 = db.Departments.ToList();

            List<DepartmensToMovil> departments = new List<DepartmensToMovil>();

            int count = 1;
            foreach (var item in departments1)
            {
                DepartmensToMovil dm = new DepartmensToMovil();

                if (count == 1)
                {
                    dm.departments = db.Departments.ToList();

                    foreach (var item2 in dm.departments)
                    {
                        dm.municipalities = db.Municipalities.OrderBy(x => x.Name).ToList();

                        foreach (var item3 in dm.municipalities)
                        {
                            dm.villages = db.Villages.Where(x => x.MunicipalityId == item3.Id).OrderBy(x => x.Name).ToList();
                        }
                    }
                }

                departments.Add(dm);
            }
            
            var set2 = db.Departments.Where(x => x.DeletedAt == null);
            return set2.Include(x => x.Municipalities.Select(m => m.Villages)).OrderBy(x => x.Name).ToList();
        }
    }
}
