using System.ComponentModel.DataAnnotations.Schema;

namespace Shop.Model.Models
{
    [Table("ProductTags")]
    public class ProductTag
    {
        public int ProductID { get; set; }
        public int TagID { get; set; }
        public Product Product { get; set; }
        public Tag Tag { get; set; }
    }
}
