using Core.Domain.Product;
using Xunit;

namespace Core.Tests.Domain
{
    public class ProductTests
    {
        [Fact]
        public void should_success_when_set_product_code()
        {
            // given 
            const string code = "apple";
            var product = new Product();

            // when
            var result = product.SetCode(code);

            // then
            Assert.True(result.IsSuccess);
            Assert.Equal(code, product.Code);
        }

        [Fact]
        public void should_fail_when_set_product_code_with_empty()
        {
            // given 
            const string code = "";
            var product = new Product();

            // when
            var result = product.SetCode(code);

            // then
            Assert.False(result.IsSuccess);
            Assert.Contains(result.Errors, x => x == ProductError.CodeCannotBeEmpty);
        }

        [Fact]
        public void should_success_when_set_product_price()
        {
            // given 
            const decimal price = 100;
            var product = new Product();

            // when
            var result = product.SetPrice(price);

            // then
            Assert.True(result.IsSuccess);
            Assert.Equal(price, product.Price);
        }

        [Fact]
        public void should_fail_when_set_product_price_less_zero()
        {
            const decimal price = 0;
            var product = new Product();

            // when
            var result = product.SetPrice(price);

            // then
            Assert.False(result.IsSuccess);
            Assert.Contains(result.Errors, x => x == ProductError.PriceShouldBeGreaterThanOrEqualToZero);
        }

        [Fact]
        public void should_success_when_set_product_stock()
        {
            // given 
            const int stock = 100;
            var product = new Product();

            // when
            var result = product.SetStock(stock);

            // then
            Assert.True(result.IsSuccess);
            Assert.Equal(stock, product.Stock);
        }

        [Fact]
        public void should_fail_when_set_product_stock_less_zero()
        {
            // given 
            const int stock = -1;
            var product = new Product();

            // when
            var result = product.SetStock(stock);

            // then
            Assert.False(result.IsSuccess);
            Assert.Contains(result.Errors, x => x == ProductError.StockShouldBeGreaterThanOrEqualToZero);
        }

        [Fact]
        public void should_success_when_sell_product()
        {
            // given 
            const int sellQuantity = 10;
            var product = new Product();
            product.SetStock(100);

            // when
            product.Sell(sellQuantity);

            // then
            Assert.Equal(90, product.Stock);
        }
    }
}