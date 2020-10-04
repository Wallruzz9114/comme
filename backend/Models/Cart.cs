using System.Collections.Generic;

namespace Models
{
    public class Cart
    {
        public Cart() { }

        public Cart(string id)
        {
            Id = id;
        }

        public string Id { get; set; }
        public List<CartItem> CartItems { get; set; } = new List<CartItem>();
    }
}