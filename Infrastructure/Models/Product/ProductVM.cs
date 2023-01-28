namespace Infrastructure.Models.Product
{
    public class ProductVM
    {
        public ProductVM()
        {
            Categories = new List<string>();
        }

        public string Id { get; set; }
        public string Name { get; set; }
        public string Image { get; set; }
        public string Article { get; set; }
        public decimal Price { get; set; }
        public string Size { get; set; }

        public virtual IEnumerable<string> Categories { get; set; }
    }
}
