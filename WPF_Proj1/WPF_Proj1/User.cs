using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPF_Proj1
{
    class User
    {
        [Key]
        public int Id { get; set; }
        public string Username { get; set; }

        [Column("full_name")]
        public string FullName { get; set; }
        public int Balance { get; set; }
    }
}
