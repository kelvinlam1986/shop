using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Shop.Model.Abstract
{
    public abstract class Auditable : IAuditable, ISeoable, ISwitchable
    {
        public DateTime? CreatedDate { get; set; }

        [MaxLength(50)]
        [Column(TypeName = "varchar")]
        public string CreatedBy { get; set; }

        public DateTime? UpdatedDate { get; set; }

        [MaxLength(50)]
        [Column(TypeName = "varchar")]
        public string UpdatedBy { get; set; }

        [MaxLength(256)]
        public string MetaKeywork { get; set; }
        [MaxLength(256)]
        public string MetaDescription { get; set; }

        [Required]
        public bool Status { get; set; }
    }
}
