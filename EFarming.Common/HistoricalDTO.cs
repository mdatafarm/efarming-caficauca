using System;
using System.ComponentModel.DataAnnotations;

namespace EFarming.Common
{
    /// <summary>
    /// Historical DTO EntityDTO
    /// </summary>
    public class HistoricalDTO : EntityDTO
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="HistoricalDTO"/> class.
        /// </summary>
        public HistoricalDTO()
        {
            Date = DateTime.Now;
        }

        /// <summary>
        /// Gets or sets the date.
        /// </summary>
        /// <value>
        /// The date.
        /// </value>
        [DataType(DataType.Date)]
        public DateTime Date
        {
            get;
            set;
        }

        /// <summary>
        /// Gets the formated date.
        /// </summary>
        /// <value>
        /// The formated date.
        /// </value>
        public string FormatedDate
        {
            get
            {
                return Date.ToShortDateString();
            }
        }

        /// <summary>
        /// Gets the input date formated.
        /// </summary>
        /// <value>
        /// The input date formated.
        /// </value>
        public string InputDateFormated
        {
            get
            {
                return string.Format("{0:yyyy-MM-dd hh:mm}", Date);
            }
        }
    }
}
