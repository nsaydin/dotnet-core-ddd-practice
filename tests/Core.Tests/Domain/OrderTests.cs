using Core.Domain.Order;
using Xunit;

namespace Core.Tests.Domain
{
    public class OrderTests
    {
        [Fact]
        public void should_success_when_set_order_code()
        {
            // given 
            const string code = "apple";
            var order = new Order();

            // when
            var result = order.SetCode(code);

            // then
            Assert.True(result.IsSuccess);
            Assert.Equal(code, order.ProductCode);
        }

        [Fact]
        public void should_fail_when_set_order_code_with_empty()
        {
            // given 
            const string code = "";
            var order = new Order();

            // when
            var result = order.SetCode(code);

            // then
            Assert.False(result.IsSuccess);
            Assert.Contains(result.Errors, x => x == OrderError.CodeCannotBeEmpty);
        }
    }
}