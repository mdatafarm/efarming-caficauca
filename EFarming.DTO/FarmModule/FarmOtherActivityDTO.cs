using EFarming.Common;
using EFarming.DTO.AdminModule;
using System;

namespace EFarming.DTO.FarmModule
{
    /// <summary>
    /// FarmOtherActivityDTO EntityDTO
    /// </summary>
    public class FarmOtherActivityDTO : EntityDTO
    {
        /// <summary>
        /// Gets or sets the percentage.
        /// </summary>
        /// <value>
        /// The percentage.
        /// </value>
        public double Percentage { get; set; }

        /// <summary>
        /// Gets or sets the input percentage.
        /// </summary>
        /// <value>
        /// The input percentage.
        /// </value>
        public int InputPercentage
        {
            get
            {
                return Convert.ToInt32(Percentage * 100);
            }
            set
            {
                Percentage = value / 100d;
            }
        }

        /// <summary>
        /// Gets or sets the farm identifier.
        /// </summary>
        /// <value>
        /// The farm identifier.
        /// </value>
        public Guid FarmId { get; set; }

        /// <summary>
        /// Gets or sets the other activity identifier.
        /// </summary>
        /// <value>
        /// The other activity identifier.
        /// </value>
        public Guid OtherActivityId { get; set; }

        /// <summary>
        /// Gets or sets the farm.
        /// </summary>
        /// <value>
        /// The farm.
        /// </value>
        public FarmDTO Farm { get; set; }

        /// <summary>
        /// Gets or sets the other activity.
        /// </summary>
        /// <value>
        /// The other activity.
        /// </value>
        public OtherActivityDTO OtherActivity { get; set; }

        /// <summary>
        /// Gets the formated percentage.
        /// </summary>
        /// <value>
        /// The formated percentage.
        /// </value>
        public string FormatedPercentage
        {
            get
            {
                return string.Format("{0} %", InputPercentage);
            }
        }
    }
}
