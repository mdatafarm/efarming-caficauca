using EFarming.Common;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EFarming.Core.QualityModule.ChecklistAggregate
{
    /// <summary>
    /// Almacenamient entity
    /// </summary>
    public class Almacenamiento : Entity
    {
        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        /// <value>
        /// The identifier.
        /// </value>
        [Key, ForeignKey("Checklist")]
        public override Guid Id
        {
            get { return base.Id; }
            set { base.Id = value; }
        }

        /// <summary>
        /// Gets or sets a value indicating whether [almacenamiento exclusivo cafe].
        /// </summary>
        /// <value>
        /// <c>true</c> if [almacenamiento exclusivo cafe]; otherwise, <c>false</c>.
        /// </value>
        public bool AlmacenamientoExclusivoCafe { get; set; }

        /// <summary>
        /// Gets or sets the tipo empaque cafe nespresso.
        /// </summary>
        /// <value>
        /// The tipo empaque cafe nespresso.
        /// </value>
        public string TipoEmpaqueCafeNespresso { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether [almacena productos contaminantes].
        /// </summary>
        /// <value>
        /// <c>true</c> if [almacena productos contaminantes]; otherwise, <c>false</c>.
        /// </value>
        public bool AlmacenaProductosContaminantes { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether [condiciones minimas].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [condiciones minimas]; otherwise, <c>false</c>.
        /// </value>
        public bool CondicionesMinimas { get; set; }

        /// <summary>
        /// Gets or sets the lugar adecuado.
        /// </summary>
        /// <value>
        /// The lugar adecuado.
        /// </value>
        public string LugarAdecuado { get; set; }

        /// <summary>
        /// Gets or sets the tipo estopa.
        /// </summary>
        /// <value>
        /// The tipo estopa.
        /// </value>
        public string TipoEstopa { get; set; }

        /// <summary>
        /// Gets or sets the observaciones.
        /// </summary>
        /// <value>
        /// The observaciones.
        /// </value>
        public string Observaciones { get; set; }

        /// <summary>
        /// Gets or sets the checklist.
        /// </summary>
        /// <value>
        /// The checklist.
        /// </value>
        public virtual Checklist Checklist { get; set; }
    }
}
