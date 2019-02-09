using Shop.Model.Abstract;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Shop.Model.Models
{
    [Table("Orders")]
    public class Order : Switchable
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }

        [Required]
        [MaxLength(250)]
        public string CustomerName { get; set; }

        [Required]
        [MaxLength(250)]
        public string CustomerAddress { get; set; }

        [Required]
        [MaxLength(50)]
        [Column(TypeName = "varchar")]
        public string CustomerMobile { get; set; }

        [Required]
        [MaxLength(256)]
        public string CustomerEmail { get; set; }

        [MaxLength(250)]
        public string CustomerMessage { get; set; }

        [MaxLength(128)]
        public string CustomerId { get; set; }

        [ForeignKey("CustomerId")]
        public virtual ApplicationUser User { get; set; }

        public DateTime? CreatedDate { get; set; }

        [MaxLength(50)]
        [Column(TypeName = "varchar")]
        public string CreatedBy { get; set; }

        [MaxLength(250)]
        public string PaymentMethod { get; set; }

        [MaxLength(50)]
        [Required]
        public string PaymentStatus { get; set; }

        public virtual IEnumerable<OrderDetail> OrderDetails { get; set; }

    }
}
