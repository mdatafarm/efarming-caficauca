using EFarming.Common;
using System.Collections.Generic;
using System.Linq;

namespace EFarming.Web.Helpers
{
    /// <summary>
    /// CheckBox Helper
    /// </summary>
    public static class CheckBoxHelper
    {
        /// <summary>
        /// Determines whether the specified entity is checked.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <param name="entities">The entities.</param>
        /// <returns></returns>
        public static bool IsChecked(EntityDTO entity, IEnumerable<EntityDTO> entities){
            return entities.Contains(entity, new EntityDTOComparer<EntityDTO>());
        }
    }
}