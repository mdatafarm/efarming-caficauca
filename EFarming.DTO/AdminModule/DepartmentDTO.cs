using EFarming.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFarming.DTO.AdminModule
{
    /// <summary>
    /// DepartmentDTO EntityDTO
    /// </summary>
    public class DepartmentDTO : EntityDTO
    {
        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        public string Name { get; set; }
        public int? Code { get; set; }

        /// <summary>
        /// Gets or sets the municipalities.
        /// </summary>
        /// <value>
        /// The municipalities.
        /// </value>
        public ICollection<MunicipalityDTO> Municipalities { get; set; }
    }
}
