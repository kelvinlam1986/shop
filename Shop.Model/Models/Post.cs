using Shop.Model.Abstract;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Xml;

namespace Shop.Model.Models
{
    [Table("Posts")]
    public class Post : Auditable
    {
        [Key]
        public int ID { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Alias { get; set; }
        public string Description { get; set; }
        public int? CategoryID { get; set; }
        [ForeignKey("CategoryID")]
        public virtual PostCategory Category { get; set; }
        public string Image { get; set; }
        public int? DisplayOrder { get; set; }
        public string Content { get; set; }
        public bool? HomeFlag { get; set; }
        public bool? HotFlag { get; set; }
        public int? ViewCount { get; set; }
    }
}
