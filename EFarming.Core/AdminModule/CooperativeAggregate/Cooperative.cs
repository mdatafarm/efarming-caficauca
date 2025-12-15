using EFarming.Common;

namespace EFarming.Core.AdminModule.CooperativeAggregate
{
    /// <summary>
    /// Cooperative Entity
    /// 
    /// Use for save the information about the principal centers to buy coffee
    /// </summary>
    public class Cooperative : Entity
    {
        /// <summary>
        /// The _name
        /// </summary>
        private string _name;
        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        public string Name 
        {
            get { return _name; }
            set { _name = SanitizeString(value); }
        }
    }
}
