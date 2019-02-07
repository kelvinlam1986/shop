using System;
using System.ComponentModel.DataAnnotations;

namespace Shop.Web.Models
{
    [Serializable]
    public class ProductViewModel
    {
        public int ID { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Alias { get; set; }
        public string Description { get; set; }
        [Required]
        public int CategoryID { get; set; }
        public virtual ProductCategoryViewModel Category { get; set; }
        public string Image { get; set; }
        public string MoreImages { get; set; }
        [Required]
        public int? DisplayOrder { get; set; }
        [Required]
        public decimal Price { get; set; }
        public decimal? Promotion { get; set; }
        public int? Waranty { get; set; }
        public string Content { get; set; }
        public bool? HomeFlag { get; set; }
        public bool? HotFlag { get; set; }
        public int? ViewCount { get; set; }
        public string MetaKeywork { get; set; }
        public string MetaDescription { get; set; }
        [Required]
        public bool Status { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public string UpdatedBy { get; set; }
        public string Tags { get; set; }
        public int Quantity { get; set; }
    }
}