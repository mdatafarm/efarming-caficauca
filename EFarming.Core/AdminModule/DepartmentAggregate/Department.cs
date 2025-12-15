using EFarming.Common;
using EFarming.Core.AdminModule.MunicipalityAggregate;
using EFarming.Core.AdminModule.PlantationTypeAggregate;
using EFarming.Core.ImpactModule.IndicatorAggregate;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace EFarming.Core.AdminModule.DepartmentAggregate
{
    /// <summary>
    /// Department Enty
    /// </summary>
    public class Department : Entity
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Department"/> class.
        /// </summary>
        public Department()
        {
            _municipalities = new List<Municipality>();
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
        /// 
        public int? Code { get; set; }

        

        [Required]
        [MaxLength(32)]
        public string Name
        {
            get { return _name; }
            set { _name = SanitizeString(value); }
        }

        /// <summary>
        /// The _municipalities
        /// </summary>
        private ICollection<Municipality> _municipalities;
        /// <summary>
        /// Gets or sets the municipalities.
        /// </summary>
        /// <value>
        /// The municipalities.
        /// </value>
        
        public ICollection<Municipality> Municipalities
        {
            get
            {
                return _municipalities;
            }
            set
            {
                _municipalities = value;
            }
        }

        #region public methods
        /// <summary>
        /// Counts the farms.
        /// </summary>
        /// <returns></returns>
        public int CountFarms()
        {
            return Municipalities.Sum(m => m.CountFarms());
        }

        /// <summary>
        /// Gets the plantation types.
        /// </summary>
        /// <returns></returns>
        public IEnumerable<PlantationType> GetPlantationTypes()
        {
            IEnumerable<PlantationType> plantationTypes = new List<PlantationType>();
            Municipalities.All(m =>
            {
                plantationTypes = plantationTypes.Concat(m.GetPlantationTypes());
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
            return Municipalities.Where(f => f.GetImpactAnswers() != null)
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
