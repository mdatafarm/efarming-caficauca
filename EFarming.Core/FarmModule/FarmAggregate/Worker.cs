using EFarming.Common;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EFarming.Core.FarmModule.FarmAggregate
{
    /// <summary>
    /// Worker Entity
    /// </summary>
    public class Worker : Entity
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Worker"/> class.
        /// </summary>
        public Worker()
        {
        }

        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        /// <value>
        /// The identifier.
        /// </value>
        [Key, ForeignKey("Farm")]
        public override Guid Id
        {
            get { return base.Id; }
            set { base.Id = value; }
        }

        /// <summary>
        /// Gets or sets the permanent women.
        /// </summary>
        /// <value>
        /// The permanent women.
        /// </value>
        public int PermanentWomen { get; set; }

        /// <summary>
        /// Gets or sets the permanent men.
        /// </summary>
        /// <value>
        /// The permanent men.
        /// </value>
        public int PermanentMen { get; set; }

        /// <summary>
        /// Gets or sets the temporary women.
        /// </summary>
        /// <value>
        /// The temporary women.
        /// </value>
        public int TemporaryWomen { get; set; }

        /// <summary>
        /// Gets or sets the temporary men.
        /// </summary>
        /// <value>
        /// The temporary men.
        /// </value>
        public int TemporaryMen { get; set; }

        /// <summary>
        /// Gets or sets the farm.
        /// </summary>
        /// <value>
        /// The farm.
        /// </value>
        public virtual Farm Farm { get; set; }
    }
}
