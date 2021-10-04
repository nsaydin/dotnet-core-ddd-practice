namespace Core.Domain.Product
{
    public struct ProductError
    {
        public const string CodeCannotBeEmpty = nameof(CodeCannotBeEmpty);
        public const string CodeIsAlreadyExist = nameof(CodeIsAlreadyExist);
        
        public const string PriceShouldBeGreaterThanOrEqualToZero = nameof(PriceShouldBeGreaterThanOrEqualToZero);
        public const string StockShouldBeGreaterThanOrEqualToZero = nameof(StockShouldBeGreaterThanOrEqualToZero);
    }
}