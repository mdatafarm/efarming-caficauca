using EFarming.Common;

namespace EFarming.DTO.FarmModule
{
    /// <summary>
    /// WorkerDTO EntityDTO
    /// </summary>
    public class WorkerDTO : EntityDTO
    {
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
        public FarmDTO Farm { get; set; }
    }
}
