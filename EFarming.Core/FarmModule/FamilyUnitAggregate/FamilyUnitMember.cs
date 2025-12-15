using EFarming.Common;
using EFarming.Core.FarmModule.FarmAggregate;
using EFarming.Core.Resources;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFarming.Core.FarmModule.FamilyUnitAggregate
{
    /// <summary>
    /// FamilyUnitMember Entity
    /// </summary>
    public class FamilyUnitMember : Entity
    {
        #region Properties
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
        [MaxLength(32)]
        public string LastName { get; set; }

        /// <summary>
        /// Gets or sets the age.
        /// </summary>
        /// <value>
        /// The age.
        /// </value>
        public DateTime Age { get; set; }

        /// <summary>
        /// Gets or sets the identification.
        /// </summary>
        /// <value>
        /// The identification.
        /// </value>
        [MaxLength(16)]
        public string Identification { get; set; }

        /// <summary>
        /// Gets or sets the education.
        /// </summary>
        /// <value>
        /// The education.
        /// </value>
        public string Education { get; set; }

        /// <summary>
        /// Gets or sets the phone number.
        /// </summary>
        /// <value>
        /// The phone number.
        /// </value>
        public string PhoneNumber { get; set; }

        /// <summary>
        /// Gets or sets the relationship.
        /// </summary>
        /// <value>
        /// The relationship.
        /// </value>
        [MaxLength(32)]
        public string Relationship { get; set; }

        /// <summary>
        /// Gets or sets the marital status.
        /// </summary>
        /// <value>
        /// The marital status.
        /// </value>
        [MaxLength(32)]
        public string MaritalStatus { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is owner.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is owner; otherwise, <c>false</c>.
        /// </value>
        public bool IsOwner { get; set; }

        /// <summary>
        /// Gets or sets the farm identifier.
        /// </summary>
        /// <value>
        /// The farm identifier.
        /// </value>
        [Required]
        public Guid FarmId { get; set; }

        /// <summary>
        /// Gets or sets the farm.
        /// </summary>
        /// <value>
        /// The farm.
        /// </value>
        public virtual Farm Farm { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int? IDProductor { get; set; }
        #endregion

        #region Static data
        /// <summary>
        /// The educatio n_ list
        /// </summary>
        public static string EDUCATION_LIST = "EducationList";
        /// <summary>
        /// The marita l_ statu s_ list
        /// </summary>
        public static string MARITAL_STATUS_LIST = "MaritalStatusList";
        /// <summary>
        /// The relationshi p_ list
        /// </summary>
        public static string RELATIONSHIP_LIST = "RelationshipList";

        /// <summary>
        /// Initializes the list.
        /// </summary>
        /// <returns></returns>
        public static Dictionary<string, List<string>> InitializeList()
        {
            var initilizedValues = new Dictionary<string, List<string>>();

            ////initilize education list
            //var educationList = new List<string> 
            //{ 
            //    Messages.Elementary, 
            //    Messages.High, 
            //    Messages.Profesional,
            //    Messages.Technician
            //};
            //initilizedValues.Add(EDUCATION_LIST, educationList);

            var educationList = new List<string>
            {
                "Especialista",
                "Magister",
                "Ninguna",
                "Postgrado",
                "Primaria sin terminar",
                "Primaria",
                "Profesional en curso",
                "Profesional",
                "Secundaria",
                "Tecnico",
                "Tecnologo"
            };
            initilizedValues.Add(EDUCATION_LIST, educationList);

            // Initialize Marital Status
            var maritalStatusList = new List<string>
            {
                "Casado(a)",
                "Divorciado(a)",
                "Ninguno",
                "Soltero(a)",
                "Union Libre",
                "Viudo(a)"
            };
            initilizedValues.Add(MARITAL_STATUS_LIST, maritalStatusList);

            // Initialize relationship
            var relationshipList = new List<string>
            {
                "Abuelo(a)",
                "Bisnieto(a)",
                "Conyuge",
                "Cuñado(a)",
                "Hermano(a)",
                "Hijastro(a)",
                "Hijo(a) adoptado(a)",
                "Hijo(a)",
                "Madre",
                "Nieto(a)",
                "Ninguno",
                "Nuera",
                "Padre",
                "Primo(a)",
                "Sobrino(a)",
                "Suegro(a)",
                "Tio(a)",
                "Yerno"
            };
            initilizedValues.Add(RELATIONSHIP_LIST, relationshipList);

            return initilizedValues;
        }
        #endregion
    }
}
