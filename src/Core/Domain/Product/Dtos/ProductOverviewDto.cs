namespace Core.Domain.Product.Dtos
{
    public class ProductOverviewDto
    {
        public string Code { get; set; }
        public decimal Price { get; set; }
        public decimal? DiscountedPrice { get; set; }
        public int Stock { get; set; }
    }
}