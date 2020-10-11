namespace Models
{
    public class ShippingAddress
    {
        public ShippingAddress() { }

        public ShippingAddress(string street, string city, string province, string postalCode, string country)
        {
            Street = street;
            City = city;
            Province = province;
            PostalCode = postalCode;
            Country = country;
        }

        public string Street { get; set; }
        public string City { get; set; }
        public string Province { get; set; }
        public string PostalCode { get; set; }
        public string Country { get; set; }
    }
}