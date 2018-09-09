using System;

namespace Shop.Web.Models
{
    public class PostTagViewModel
    {
        public int PostID { get; set; }
        public string TagID { get; set; }
        public virtual PostViewModel Post { get; set; }
        public virtual TagViewModel Tag { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public string UpdatedBy { get; set; }
        public string MetaKeywork { get; set; }
        public string MetaDescription { get; set; }
        public bool Status { get; set; }
    }
}