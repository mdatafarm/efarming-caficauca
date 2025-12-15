using EFarming.Core.AdminModule.PlantationTypeAggregate;
using EFarming.Core.AdminModule.PlantationVarietyAggregate;
using EFarming.DAL;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace EFarming.Repository.AdminModule
{
    /// <summary>
    /// PlantationType Repository
    /// </summary>
    public class PlantationTypeRepository : Repository<PlantationType>, IPlantationTypeRepository
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PlantationTypeRepository"/> class.
        /// </summary>
        /// <param name="unitOfWork">The unit of work.</param>
        public PlantationTypeRepository(UnitOfWork unitOfWork)
            : base(unitOfWork) { }

        public class PlantationsToMovil
        {
            public List<PlantationType> Tipo_Plantacion { get; set; }
            public List<PlantationVariety> Variedades { get; set; }

        }

        public List<PlantationType> GetFullData()
        {
            UnitOfWork db = new UnitOfWork();

            var Plantations1 = db.PlantationType.ToList();

            List<PlantationsToMovil> Plantations = new List<PlantationsToMovil>();

            int count = 1;
            foreach (var item in Plantations1)
            {
                PlantationsToMovil pt = new PlantationsToMovil();

                if (count == 1)
                {
                    pt.Tipo_Plantacion = db.PlantationType.OrderBy(x=> x.Name).ToList();

                    foreach (var item2 in pt.Tipo_Plantacion)
                    {
                        pt.Variedades = db.PlantationVarieties.OrderBy(x => x.Name).ToList();
                    }
                }

                Plantations.Add(pt);
            }

            var set2 = db.PlantationType.Where(x => x.DeletedAt == null);
            return set2.Include(x => x.PlantationVarieties).OrderBy(x => x.Name).ToList();
        }
    }
}
