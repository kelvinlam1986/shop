using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Shop.Model.Models
{
    [Table("Footers")]
    public class Footer
    {
        [Key]
        [MaxLength(250)]
        [Column(TypeName = "varchar")]
        public string ID { get; set; }

        [Required]
        public string Content { get; set; }
    }
}
