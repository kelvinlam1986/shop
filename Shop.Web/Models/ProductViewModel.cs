using System;
using System.ComponentModel.DataAnnotations;

namespace Shop.Web.Models
{
    public class ProductViewModel
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Alias { get; set; }
        public string Description { get; set; }
        public int CategoryID { get; set; }
        public virtual ProductCategoryViewModel Category { get; set; }
        public string Image { get; set; }
        public string MoreImages { get; set; }
        public int? DisplayOrder { get; set; }
        public decimal Price { get; set; }
        public decimal? Promotion { get; set; }
        public int? Waranty { get; set; }
        public string Content { get; set; }
        public bool? HomeFlag { get; set; }
        public bool? HotFlag { get; set; }
        public int? ViewCount { get; set; }
        [Required]
        public bool Status { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public string UpdatedBy { get; set; }
    }
}