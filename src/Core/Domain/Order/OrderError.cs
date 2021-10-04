namespace Core.Domain.Order
{
    public struct OrderError
    {
        public const string CodeCannotBeEmpty = nameof(CodeCannotBeEmpty);
        public const string PriceShouldBeGreaterThanOrEqualToZero = nameof(PriceShouldBeGreaterThanOrEqualToZero);
        public const string QuantityShouldBeGreaterThanOrEqualToZero = nameof(QuantityShouldBeGreaterThanOrEqualToZero);
        public const string NotEnoughStock = nameof(NotEnoughStock);
    }
}