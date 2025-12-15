using EFarming.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFarming.DTO.AdminModule
{
    /// <summary>
    /// MunicipalityDTO EntityDTO
    /// </summary>
    public class MunicipalityDTO : EntityDTO
    {
        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the department identifier.
        /// </summary>
        /// <value>
        /// The department identifier.
        /// </value>
        public Guid DepartmentId { get; set; }

        /// <summary>
        /// Gets or sets the code.
        /// </summary>
        /// <value>
        /// The code.
        /// </value>
        public int Code { get; set; }

        /// <summary>
        /// Gets or sets the department.
        /// </summary>
        /// <value>
        /// The department.
        /// </value>
        public DepartmentDTO Department { get; set; }

        /// <summary>
        /// Gets or sets the villages.
        /// </summary>
        /// <value>
        /// The villages.
        /// </value>
        public ICollection<VillageDTO> Villages { get; set; }
    }
}
