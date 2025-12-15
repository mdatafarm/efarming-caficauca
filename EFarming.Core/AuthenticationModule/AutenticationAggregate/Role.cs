using EFarming.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace EFarming.Core.AuthenticationModule.AutenticationAggregate
{
    /// <summary>
    /// Role Entity
    /// </summary>
    public class Role : Entity
    {
        /// <summary>
        /// The admin identifier
        /// </summary>
        public static Guid AdminId = Guid.Parse("8689b5b1-129d-424f-a77d-86ee0319fcb6");
        public static Guid TasterId = Guid.Parse("17C9B448-0481-4364-B439-CA729D4AD001");
        /// <summary>
        /// The technician identifier
        /// </summary>
        public static Guid TechnicianId = Guid.Parse("d3d50a4d-b63a-406f-ba90-0a56f06afad6");
        /// <summary>
        /// The sustentability identifier
        /// </summary>
        public static Guid SustainabilityId = Guid.Parse("1164AF91-ECAE-4A9F-A394-E5DAF87A693E");
        /// <summary>
        /// The manager identifier
        /// </summary>
        public static Guid ReportsId = Guid.Parse("0584A7C8-5D41-4D37-9CB3-FEF5394314D1");
        /// <summary>
        /// The project identifier
        /// </summary>
        public static Guid ProjectId = Guid.Parse("27a27f5a-08c4-4606-8efa-dbe27d20e14f");

        /// <summary>
        /// Gets or sets the name of the role.
        /// </summary>
        /// <value>
        /// The name of the role.
        /// </value>
        [Required]
        public string RoleName { get; set; }

        /// <summary>
        /// Gets or sets the description.
        /// </summary>
        /// <value>
        /// The description.
        /// </value>
        public string Description { get; set; }

        /// <summary>
        /// Gets or sets the users.
        /// </summary>
        /// <value>
        /// The users.
        /// </value>
        public virtual ICollection<User> Users { get; set; }
    }
}
