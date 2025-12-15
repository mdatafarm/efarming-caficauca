using System.Collections.Generic;

namespace EFarming.Common
{
    /// <summary>
    /// EntityCompare
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class EntityComparer<T> : IEqualityComparer<T> where T : Entity
    {
        /// <summary>
        /// Determines whether the specified objects are equal.
        /// </summary>
        /// <param name="x">The first object of type <paramref name="T" /> to compare.</param>
        /// <param name="y">The second object of type <paramref name="T" /> to compare.</param>
        /// <returns>
        /// true if the specified objects are equal; otherwise, false.
        /// </returns>
        public bool Equals(T x, T y)
        {
            return x.Id.Equals(y.Id);
        }

        /// <summary>
        /// Returns a hash code for this instance.
        /// </summary>
        /// <param name="obj">The object.</param>
        /// <returns>
        /// A hash code for this instance, suitable for use in hashing algorithms and data structures like a hash table. 
        /// </returns>
        public int GetHashCode(T obj)
        {
            return obj.Id.GetHashCode();
        }
    }

    /// <summary>
    /// EntityDTOCompare
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class EntityDTOComparer<T> : IEqualityComparer<T> where T : EntityDTO
    {
        /// <summary>
        /// Determines whether the specified objects are equal.
        /// </summary>
        /// <param name="x">The first object of type to compare.</param>
        /// <param name="y">The second object of type  to compare.</param>
        /// <returns>
        /// true if the specified objects are equal; otherwise, false.
        /// </returns>
        public bool Equals(T x, T y)
        {
            return x.Id.Equals(y.Id);
        }

        /// <summary>
        /// Returns a hash code for this instance.
        /// </summary>
        /// <param name="obj">The object.</param>
        /// <returns>
        /// A hash code for this instance, suitable for use in hashing algorithms and data structures like a hash table. 
        /// </returns>
        public int GetHashCode(T obj)
        {
            return obj.Id.GetHashCode();
        }
    }
}
