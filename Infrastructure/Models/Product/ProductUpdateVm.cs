namespace Infrastructure.Models.Product
{
    public class ProductUpdateVm
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Image { get; set; }
        public string Article { get; set; }
        public decimal Price { get; set; }
        public string Size { get; set; }
    }
}
