using EFarming.Common;
using EFarming.Core.AdminModule.MunicipalityAggregate;
using EFarming.Core.AdminModule.PlantationTypeAggregate;
using EFarming.Core.FarmModule.FarmAggregate;
using EFarming.Core.ImpactModule.IndicatorAggregate;
using System;
using System.Collections.Generic;
using System.Linq;

namespace EFarming.Core.AdminModule.VillageAggregate
{
    /// <summary>
    /// Village Entity
    /// </summary>
    public class Village : Entity
    {
        /// <summary>
        /// The _name
        /// </summary>
        private string _name;
        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        public string Name
        {
            get { return _name; }
            set { _name = SanitizeString(value); }
        }

        public int Code { get; set; }

        /// <summary>
        /// Gets or sets the municipality identifier.
        /// </summary>
        /// <value>
        /// The municipality identifier.
        /// </value>
        public Guid MunicipalityId { get; set; }

        /// <summary>
        /// Gets or sets the municipality.
        /// </summary>
        /// <value>
        /// The municipality.
        /// </value>
        public virtual Municipality Municipality { get; set; }

        /// <summary>
        /// Gets or sets the farms.
        /// </summary>
        /// <value>
        /// The farms.
        /// </value>
        public virtual ICollection<Farm> Farms { get; set; }


        #region public methods
        /// <summary>
        /// Counts the farms.
        /// </summary>
        /// <returns>Count of Farms</returns>
        public int CountFarms()
        {
            return Farms.Count();
        }

        /// <summary>
        /// Gets the plantation types.
        /// </summary>
        /// <returns>Typo of Plantation</returns>
        public IEnumerable<PlantationType> GetPlantationTypes()
        {
            IEnumerable<PlantationType> plantationTypes = new List<PlantationType>();
            Farms.All(f =>
            {
                plantationTypes = plantationTypes.Concat(f.Productivity.GetPlantationTypes());
                return true;
            });
            return plantationTypes;
        }

        /// <summary>
        /// Gets the impact answers.
        /// </summary>
        /// <returns></returns>
        public IDictionary<Indicator, int> GetImpactAnswers()
        {
            return Farms.Where(f => f.GetImpactAnswers() != null)
                .SelectMany(f => f.GetImpactAnswers())
                .GroupBy(a => a.Key, new EntityComparer<Indicator>())
                .Select(group =>
                new
                {
                    Indicator = group.Key,
                    Sum = group.Sum(g => g.Value) / group.Count()
                }).ToDictionary(a => a.Indicator, a => a.Sum);
        }
        #endregion
    }
}
