namespace Models
{
    public class CartItem
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
        public string PictureURL { get; set; }
        public string ProductBrand { get; set; }
        public string ProductType { get; set; }
    }
}