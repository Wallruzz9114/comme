namespace Core.ViewModels
{
    public class NewOrderViewModel
    {
        public string CartId { get; set; }
        public int DeliveryMethodId { get; set; }
        public AddressViewModel ShippingAddress { get; set; }
    }
}