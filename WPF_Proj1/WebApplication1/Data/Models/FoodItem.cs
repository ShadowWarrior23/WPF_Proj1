using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace WebApplication1.Data.Models
{
    public class FoodItem
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(50)]
        public string Name { get; set; } = string.Empty;

        [Required]
        [MaxLength(4)]
        public string Categ { get; set; } = string.Empty; //"Soup" || "Main"

        public string[] Ingreds { get; set; } = Array.Empty<string>();
        public string[] Allergens { get; set; } = Array.Empty<string>();
        public string[] Tags { get; set; } = Array.Empty<string>();
        public int Rating {  get; set; }
    }
}
