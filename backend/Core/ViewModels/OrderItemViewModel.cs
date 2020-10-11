namespace Core.ViewModels
{
    public class OrderItemViewModel
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public string PictureURL { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
    }
}