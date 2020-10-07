using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Core.ViewModels
{
    public class CartViewModel
    {
        [Required]
        public string Id { get; set; }
        public List<CartItemViewModel> CartItems { get; set; } = new List<CartItemViewModel>();
    }
}