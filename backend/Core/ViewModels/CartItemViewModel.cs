using System.ComponentModel.DataAnnotations;

namespace Core.ViewModels
{
    public class CartItemViewModel
    {
        [Required]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        [Range(0.1, double.MaxValue, ErrorMessage = "Price must be greater than 0")]
        public decimal Price { get; set; }

        [Required]
        [Range(1, double.MaxValue, ErrorMessage = "Quantity needs to be more than 1")]
        public int Quantity { get; set; }

        [Required]
        public string PictureURL { get; set; }

        [Required]
        public string ProductBrand { get; set; }

        [Required]
        public string ProductType { get; set; }
    }
}