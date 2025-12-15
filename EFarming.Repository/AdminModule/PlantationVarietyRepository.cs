using EFarming.Core.AdminModule.PlantationVarietyAggregate;
using EFarming.DAL;
using System.Collections.Generic;
using System.Linq;

namespace EFarming.Repository.AdminModule
{
    /// <summary>
    /// PlantationVariety Repository
    /// </summary>
    public class PlantationVarietyRepository : Repository<PlantationVariety>, IPlantationVarietyRepository
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PlantationVarietyRepository"/> class.
        /// </summary>
        /// <param name="unitOfWork">The unit of work.</param>
        public PlantationVarietyRepository(UnitOfWork unitOfWork)
            : base(unitOfWork) { }

        public class PlantationsVToMovil
        {
            public List<PlantationVariety> Variedades { get; set; }

        }

        public List<PlantationVariety> GetFullData()
        {
            UnitOfWork db = new UnitOfWork();

            var Plantations1 = db.PlantationVarieties.ToList();

            List<PlantationsVToMovil> PlantationsV = new List<PlantationsVToMovil>();

            int count = 1;
            foreach (var item in Plantations1)
            {
                PlantationsVToMovil pt = new PlantationsVToMovil();

                if (count == 1)
                {
                    pt.Variedades = db.PlantationVarieties.OrderBy(x => x.Name).ToList();
                }

                PlantationsV.Add(pt);
            }

            var set2 = db.PlantationVarieties.Where(x => x.DeletedAt == null);
            return set2.OrderBy(x => x.Name).ToList();
        }
    }
}
