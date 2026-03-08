using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WPF_Proj1
{
    class Order
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [Column("user_id")]
        public int UserId { get; set; }

        [Required]
        public DateOnly Day { get; set; }

        [Column("wants_soup")]
        public bool WantsSoup { get; set; }

        [MaxLength(1)]
        [Column("dish_choice")]
        public string? DishChoice { get; set; } // "A", "B", or null

        [ForeignKey(nameof(UserId))]
        public User User { get; set; } = null!;

        [ForeignKey(nameof(Day))]
        public DailyMenu DailyMenu { get; set; } = null!;
    }
}
