using EFarming.Common;
using EFarming.Core.AdminModule.DepartmentAggregate;
using EFarming.Core.AdminModule.PlantationTypeAggregate;
using EFarming.Core.AdminModule.VillageAggregate;
using EFarming.Core.ImpactModule.IndicatorAggregate;
using System;
using System.Collections.Generic;
using System.Linq;

namespace EFarming.Core.AdminModule.MunicipalityAggregate
{
    /// <summary>
    /// Municipality Entity
    /// </summary>
    public class Municipality : Entity
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Municipality"/> class.
        /// </summary>
        public Municipality()
        {
            _villages = new List<Village>();
        }
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

        /// <summary>
        /// Gets or sets the code.
        /// </summary>
        /// <value>
        /// The code.
        /// </value>
        public int Code { get; set; }

        /// <summary>
        /// Gets or sets the department identifier.
        /// </summary>
        /// <value>
        /// The department identifier.
        /// </value>
        public Guid DepartmentId { get; set; }
        /// <summary>
        /// Gets or sets the department.
        /// </summary>
        /// <value>
        /// The department.
        /// </value>
        public virtual Department Department { get; set; }

        /// <summary>
        /// The _villages
        /// </summary>
        private ICollection<Village> _villages;
        /// <summary>
        /// Gets or sets the villages.
        /// </summary>
        /// <value>
        /// The villages.
        /// </value>
        public ICollection<Village> Villages
        {
            get
            {
                return _villages;
            }
            set
            {
                _villages = value;
            }
        }

        #region public methods
        /// <summary>
        /// Counts the farms.
        /// </summary>
        /// <returns></returns>
        public int CountFarms()
        {
            return Villages.Sum(v => v.CountFarms());
        }

        /// <summary>
        /// Gets the plantation types.
        /// </summary>
        /// <returns></returns>
        public IEnumerable<PlantationType> GetPlantationTypes()
        {
            IEnumerable<PlantationType> plantationTypes = new List<PlantationType>();
            Villages.All(v =>
            {
                plantationTypes = plantationTypes.Concat(v.GetPlantationTypes());
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
            return Villages.Where(f => f.GetImpactAnswers() != null)
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
