using EFarming.Common;
using EFarming.Core.FarmModule.FarmAggregate;
using EFarming.Core.ProjectModule.ProjectAggregate;
using EFarming.Core.QualityModule.ChecklistAggregate;
using EFarming.Core.QualityModule.SensoryProfileAggregate;
using EFarming.Core.SustainabilityModule.ContactAggregate;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace EFarming.Core.AuthenticationModule.AutenticationAggregate
{
    /// <summary>
    /// 
    /// </summary>
    public class User : Entity
    {
        /// <summary>
        /// The system
        /// </summary>
        public static Guid SYSTEM = Guid.Parse("E70C1282-8021-4F3A-8DF2-30B29B2BAB6C");

        /// <summary>
        /// Gets or sets the username.
        /// </summary>
        /// <value>
        /// The username.
        /// </value>
        [Required]
        public string Username { get; set; }

        /// <summary> Gets or sets the email.</summary>
        /// <value> The email.</value>
        [Required]
        public string Email { get; set; }

        /// <summary>
        /// Gets or sets the password.
        /// </summary>
        /// <value>
        /// The password.
        /// </value>
        [Required]
        public string Password { get; set; }

        /// <summary>
        /// Gets or sets the salt.
        /// </summary>
        /// <value>
        /// The salt.
        /// </value>
        [MaxLength(32)]
        [Required]
        public string Salt { get; set; }

        /// <summary>
        /// Gets or sets the first name.
        /// </summary>
        /// <value>
        /// The first name.
        /// </value>
        [Required]
        [MaxLength(64)]
        public string FirstName { get; set; }

        /// <summary>
        /// Gets or sets the last name.
        /// </summary>
        /// <value>
        /// The last name.
        /// </value>
        [Required]
        [MaxLength(64)]
        public string LastName { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is active.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is active; otherwise, <c>false</c>.
        /// </value>
        public bool IsActive { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether [on install].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [on install]; otherwise, <c>false</c>.
        /// </value>
        [DefaultValue(false)]
        public bool OnInstall { get; set; }

        /// <summary>
        /// Gets or sets the roles.
        /// </summary>
        /// <value>
        /// The roles.
        /// </value>
        public virtual ICollection<Role> Roles { get; set; }

        /// <summary>
        /// Gets or sets the farms.
        /// </summary>
        /// <value>
        /// The farms.
        /// </value>
        public virtual ICollection<Farm> Farms { get; set; }

        /// <summary>
        /// Gets or sets the sensory profile assessments.
        /// </summary>
        /// <value>
        /// The sensory profile assessments.
        /// </value>
        public virtual ICollection<SensoryProfileAssessment> SensoryProfileAssessments { get; set; }

        /// <summary>
        /// Gets or sets the checklists.
        /// </summary>
        /// <value>
        /// The checklists.
        /// </value>
        public virtual ICollection<Checklist> Checklists { get; set; }

        /// <summary>
        /// Gets or sets the contacts.
        /// </summary>
        /// <value>
        /// The contacts.
        /// </value>
        public virtual ICollection<Contact> Contacts { get; set; }

        /// <summary>
        /// Gets or sets the projects.
        /// </summary>
        /// <value>
        /// The projects.
        /// </value>
        public virtual ICollection<Project> Projects { get; set; }
    }
}
