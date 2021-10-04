namespace Application.Dtos
{
    public class ProductDto
    {
        public string Code { get; set; }
        public decimal Price { get; set; }
        public int Stock { get; set; }

        public override string ToString()
        {
            return $"Product created; {nameof(Code)} {Code}, {nameof(Price)} {Price}, {nameof(Stock)} {Stock}";
        }
    }
}