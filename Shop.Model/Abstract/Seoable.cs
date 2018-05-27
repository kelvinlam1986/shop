using System.ComponentModel.DataAnnotations;

namespace Shop.Model.Abstract
{
    public class Seoable : ISeoable
    {
        [MaxLength(256)]
        public string MetaKeywork { get; set; }
        [MaxLength(256)]
        public string MetaDescription { get; set; }
    }
}
