using System;
using System.Runtime.Serialization;

namespace EFarming.Core.DashboardModule
{
    /// <summary>
    /// Data Item
    /// </summary>
    /// <typeparam name="T"></typeparam>
    [Serializable]
    [DataContract]
    public class DataItem<T>
    {
        /// <summary>
        /// Gets or sets the value.
        /// </summary>
        /// <value>
        /// The value.
        /// </value>
        [DataMember(Name = "Value")]
        public T Value { get; set; }
    }
}
