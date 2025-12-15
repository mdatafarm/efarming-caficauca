using EFarming.Common;
using System;

namespace EFarming.DTO.APIModule
{
    /// <summary>
    /// FarmsSearchAPIDTO EntityDTO
    /// </summary>
    public class FarmSearchAPIDTO : EntityDTO
    {
        /// <summary>
        /// Gets or sets the department identifier.
        /// </summary>
        /// <value>
        /// The department identifier.
        /// </value>
        public Guid DepartmentId { get; set; }

        /// <summary>
        /// Gets or sets the municipality identifier.
        /// </summary>
        /// <value>
        /// The municipality identifier.
        /// </value>
        public Guid MunicipalityId { get; set; }

        /// <summary>
        /// Gets or sets the village identifier.
        /// </summary>
        /// <value>
        /// The village identifier.
        /// </value>
        public Guid VillageId { get; set; }

        /// <summary>
        /// Gets or sets the code.
        /// </summary>
        /// <value>
        /// The code.
        /// </value>
        public string Code { get; set; }

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the user identifier.
        /// </summary>
        /// <value>
        /// The user identifier.
        /// </value>
        public Guid UserId { get; set; }
    }
}
