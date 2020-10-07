using System.ComponentModel.DataAnnotations;

namespace Core.ViewModels
{
    public class AddressViewModel
    {
        [Required]
        public string Street { get; set; }

        [Required]
        public string City { get; set; }

        [Required]
        public string Province { get; set; }

        [Required]
        public string PostalCode { get; set; }

        [Required]
        public string Country { get; set; }

        [Required]
        public string AppUserId { get; set; }
    }
}