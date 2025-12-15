using EFarming.Common;
using System;

namespace EFarming.DTO.FarmModule
{
    /// <summary>
    /// SoilAnalysisDTO HistoricalDTO
    /// </summary>
    public class SoilAnalysisDTO : HistoricalDTO
    {
        /// <summary>
        /// Gets or sets the comment.
        /// </summary>
        /// <value>
        /// The comment.
        /// </value>
        public string Comment { get; set; }

        /// <summary>
        /// Gets or sets the depth.
        /// </summary>
        /// <value>
        /// The depth.
        /// </value>
        public int Depth { get; set; }

        /// <summary>
        /// Gets or sets the farm identifier.
        /// </summary>
        /// <value>
        /// The farm identifier.
        /// </value>
        public Guid FarmId { get; set; }

        /// <summary>
        /// Gets or sets the farm.
        /// </summary>
        /// <value>
        /// The farm.
        /// </value>
        public virtual FarmDTO Farm { get; set; }
    }
}
