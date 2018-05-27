using Shop.Model.Abstract;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Shop.Model.Models
{
    [Table("Pages")]
    public class Page : ISeoable, ISwitchable
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }

        [MaxLength(250)]
        public string Name { get; set; }

        public string Content { get; set; }

        [MaxLength(250)]
        public string MetaDescription
        {
            get; set;
        }

        [MaxLength(250)]
        public string MetaKeywork
        {
            get; set;
        }

        [Required]
        public bool Status
        {
            get; set;
        }
    }
}
