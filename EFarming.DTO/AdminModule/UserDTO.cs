using EFarming.Common;
using EFarming.Common.Encription;
using EFarming.Common.Resources;
using EFarming.DTO.FarmModule;
using EFarming.DTO.ProjectModule;
using EFarming.DTO.QualityModule;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace EFarming.DTO.AdminModule
{
    /// <summary>
    /// UserDTO EntityDTO
    /// </summary>
    public class UserDTO : EntityDTO
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="UserDTO"/> class.
        /// </summary>
        public UserDTO()
        {
            Roles = new List<RoleDTO>();
            Projects = new List<ProjectDTO>();
        }

        /// <summary>
        /// Gets or sets the username.
        /// </summary>
        /// <value>
        /// The username.
        /// </value>
        public string Username { get; set; }

        /// <summary>
        /// Gets or sets the email.
        /// </summary>
        /// <value>
        /// The email.
        /// </value>
        public string Email { get; set; }

        /// <summary>
        /// Gets or sets the password.
        /// </summary>
        /// <value>
        /// The password.
        /// </value>
        [DataType(DataType.Password)]
        public string Password { get; set; }

        /// <summary>
        /// Gets or sets the old password.
        /// </summary>
        /// <value>
        /// The old password.
        /// </value>
        [DataType(DataType.Password)]
        public string OldPassword { get; set; }
        
        /// <summary>
        /// Gets or sets the new password.
        /// </summary>
        /// <value>
        /// The new password.
        /// </value>
        [DataType(DataType.Password)]
        public string NewPassword { get; set; }
        
        /// <summary>
        /// Gets or sets the confirm password.
        /// </summary>
        /// <value>
        /// The confirm password.
        /// </value>
        [DataType(DataType.Password)]
        public string ConfirmPassword { get; set; }

        /// <summary>
        /// Gets or sets the salt.
        /// </summary>
        /// <value>
        /// The salt.
        /// </value>
        public string Salt { get; set; }

        /// <summary>
        /// Gets or sets the first name.
        /// </summary>
        /// <value>
        /// The first name.
        /// </value>
        public string FirstName { get; set; }
        
        /// <summary>
        /// Gets or sets the last name.
        /// </summary>
        /// <value>
        /// The last name.
        /// </value>
        public string LastName { get; set; }
        
        /// <summary>
        /// Gets the full name.
        /// </summary>
        /// <value>
        /// The full name.
        /// </value>
        public string FullName { get { return string.Concat(FirstName, " ", LastName); } }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is active.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is active; otherwise, <c>false</c>.
        /// </value>
        public bool IsActive { get; set; }
        
        /// <summary>
        /// Gets or sets the role.
        /// </summary>
        /// <value>
        /// The role.
        /// </value>
        public Guid Role { get; set; }

        /// <summary>
        /// Gets or sets the roles.
        /// </summary>
        /// <value>
        /// The roles.
        /// </value>
        public List<RoleDTO> Roles { get; set; }
        /// <summary>
        /// Gets or sets the farms.
        /// </summary>
        /// <value>
        /// The farms.
        /// </value>
        public List<FarmDTO> Farms { get; set; }

        /// <summary>
        /// Gets or sets the sensory profile assessments.
        /// </summary>
        /// <value>
        /// The sensory profile assessments.
        /// </value>
        public List<SensoryProfileAssessmentDTO> SensoryProfileAssessments { get; set; }

        /// <summary>
        /// Gets or sets the projects.
        /// </summary>
        /// <value>
        /// The projects.
        /// </value>
        public virtual List<ProjectDTO> Projects { get; set; }

        /// <summary>
        /// Determines whether the specified object is valid.
        /// </summary>
        /// <param name="validationContext">The validation context.</param>
        /// <returns>
        /// A collection that holds failed-validation information.
        /// </returns>
        public override IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var validations = new List<ValidationResult>();
            if (!string.IsNullOrEmpty(OldPassword))
            {
                if (!EncriptorFactory.CreateEncriptor().HashPassword(OldPassword, Salt).Equals(Password))
                {
                    validations.Add(new ValidationResult(ExceptionMessage.Password_Incorrect, new List<string> { "OldPassword" }));
                }
                else if (!string.IsNullOrEmpty(NewPassword) && !string.IsNullOrEmpty(ConfirmPassword) && !NewPassword.Equals(ConfirmPassword))
                {
                    validations.Add(new ValidationResult(ExceptionMessage.Confirmation_Not_Match, new List<string> { "ConfirmPassword" }));
                }
            }
            validations.AddRange(base.Validate(validationContext));
            return validations;
        }
    }
}
