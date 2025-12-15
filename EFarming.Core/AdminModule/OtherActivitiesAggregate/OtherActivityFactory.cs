namespace EFarming.Core.AdminModule.OtherActivitiesAggregate
{
    /// <summary>
    /// Other Activity Factory
    /// </summary>
    public static class OtherActivityFactory
    {
        /// <summary>
        /// Others the activity.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <returns></returns>
        public static OtherActivity OtherActivity(string name)
        {
            return new OtherActivity { Name = name };
        }
    }
}
