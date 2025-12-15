using System;
using System.Collections.Generic;

namespace EFarming.Common
{
    /// <summary>
    /// The custom exception for validation errors
    /// </summary>
    public class ApplicationValidationErrorsException : Exception
    {
        #region Properties

        /// <summary>
        /// The _validation errors
        /// </summary>
        IEnumerable<string> _validationErrors;
        /// <summary>
        /// Get or set the validation errors messages
        /// </summary>
        /// <value>
        /// The validation errors.
        /// </value>
        public IEnumerable<string> ValidationErrors
        {
            get
            {
                return _validationErrors;
            }
        }

        #endregion

        #region Constructor

        /// <summary>
        /// Create new instance of Application validation errors exception
        /// </summary>
        /// <param name="validationErrors">The collection of validation errors</param>
        public ApplicationValidationErrorsException(IEnumerable<string> validationErrors)
            : base("Invalid type, expected is RegisterTypesMapConfigurationElement")
        {
            _validationErrors = validationErrors;
        }

        #endregion
    }
}
