using SampleAPI.Entities;
using System.ComponentModel.DataAnnotations;

namespace SampleAPI.Models
{
    public class ProductViewModel
    {
        [Required]
        public string Title { get; set; }

        [Required]
        public TypeEnum Type { get; set; }
        
        [Required]
        public double Price { get; set; }
        
        [Required]
        public ColorEnum Color { get; set; }

    }
}
