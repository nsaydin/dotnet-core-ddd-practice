using Core.Domain.Entity;

namespace Core.Domain.Product
{
    public class Product : AggregateRoot
    {
        public string Code { get; private set; }

        public decimal Price { get; private set; }

        public int Stock { get; private set; }

        public Result SetCode(string code)
        {
            if (string.IsNullOrEmpty(code))
                return Result.Fail(ProductError.CodeCannotBeEmpty);
            Code = code;

            return Result.Ok();
        }

        public Result SetPrice(decimal price)
        {
            if (price <= 0)
                return Result.Fail(ProductError.PriceShouldBeGreaterThanOrEqualToZero);
            Price = price;

            return Result.Ok();
        }

        public Result SetStock(int stock)
        {
            if (stock < 0)
                return Result.Fail(ProductError.StockShouldBeGreaterThanOrEqualToZero);
            Stock = stock;
            return Result.Ok();
        }

        public void Sell(int quantity)
        {
            Stock -= quantity;
        }
    }
}