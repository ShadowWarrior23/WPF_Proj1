using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPF_Proj1
{
    class DailyMenu
    {
        [Key]
        public DateOnly Day { get; set; }

        [Required]
        [MaxLength(100)]
        public string Soup { get; set; } = string.Empty;

        [Required]
        [MaxLength(150)]
        [Column("dish_a")]
        public string DishA { get; set; } = string.Empty;

        [Required]
        [MaxLength(150)]
        [Column("dish_b")]
        public string DishB { get; set; } = string.Empty;

        public ICollection<Order> Orders { get; set; } = new List<Order>();
    }
}
