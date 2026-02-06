using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPF_Proj1
{
    class Order
    {
        [Key]
        public int Id { get; set; }

        [Column("user_id")]
        public int UserId { get; set; }

        public DateTime Day { get; set; }

        [Column("wants_soup")]
        public bool WantsSoup { get; set; }

        [Column("dish_choice")]
        public char? DishChoice { get; set; } // 'a' / 'b' / 'n'
    }
}
