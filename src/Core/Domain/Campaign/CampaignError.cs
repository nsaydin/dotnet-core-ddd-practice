namespace Core.Domain.Campaign
{
    public struct CampaignError
    {
        public const string NameCannotBeEmpty = nameof(NameCannotBeEmpty);
        public const string IsAlreadyExist = nameof(IsAlreadyExist);
        public const string DurationShouldBeGreaterThanZero = nameof(DurationShouldBeGreaterThanZero);
        public const string ProductNotFound = nameof(ProductNotFound);
        public const string TargetSalesCountShouldBeLessThanProductStock = nameof(TargetSalesCountShouldBeLessThanProductStock);
    }
}