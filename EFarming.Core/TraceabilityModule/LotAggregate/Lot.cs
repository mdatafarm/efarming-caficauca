using EFarming.Common;
using EFarming.Core.TraceabilityModule.InvoicesAggregate;
using System.Collections.Generic;

namespace EFarming.Core.TraceabilityModule.LotAggregate
{
    /// <summary>
    /// Lot Entity
    /// </summary>
    public class Lot : Entity
    {
        /// <summary>
        /// The _code
        /// </summary>
        private string _code;
        /// <summary>
        /// Gets or sets the code.
        /// </summary>
        /// <value>
        /// The code.
        /// </value>
        public string Code
        {
            get { return _code; }
            set { _code = SanitizeString(value); }
        }
    }
}
