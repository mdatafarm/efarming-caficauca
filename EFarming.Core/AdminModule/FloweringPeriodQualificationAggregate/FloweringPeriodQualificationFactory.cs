namespace EFarming.Core.AdminModule.FloweringPeriodQualificationAggregate
{
    /// <summary>
    /// FloweringPeriodQualification Factory
    /// </summary>
    public static class FloweringPeriodQualificationFactory
    {
        public static FloweringPeriodQualification FloweringPeriodQualification(string name)
        {
            return new FloweringPeriodQualification { Name = name };
        }
    }
}
