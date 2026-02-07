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

        public string Soup { get; set; }

        [Column("dish_a")]
        public string DishA { get; set; }

        [Column("dish_b")]
        public string DishB { get; set; }
    }
}
