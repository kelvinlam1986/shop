using System.ComponentModel.DataAnnotations.Schema;

namespace Shop.Model.Models
{
    [Table("PostTags")]
    public class PostTag
    {
        public int PostID { get; set; }
        public int TagID { get; set; }
        public Post Post { get; set; }
        public Tag Tag { get; set; }
    }
}
