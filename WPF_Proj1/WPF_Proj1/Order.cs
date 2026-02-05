using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPF_Proj1
{
    class Order
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public DateTime Day { get; set; }
        public bool WantsSoup { get; set; }
        public char? DishChoice { get; set; } // 'a' or 'b' or 'n'
    }
}
