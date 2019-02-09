using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Shop.Web.Models
{
    public class OrderViewModel
    {
        public int ID { get; set; }

        [Required]
        [MaxLength(250)]
        public string CustomerName { get; set; }

        [Required]
        [MaxLength(250)]
        public string CustomerAddress { get; set; }

        [Required]
        [MaxLength(50)]
        public string CustomerMobile { get; set; }

        [Required]
        [MaxLength(256)]
        public string CustomerEmail { get; set; }

        [MaxLength(250)]
        public string CustomerMessage { get; set; }

        [MaxLength(250)]
        public string PaymentMethod { get; set; }

        [MaxLength(50)]
        public string CreatedBy { get; set; }

        [MaxLength(50)]
        [Required]
        public string PaymentStatus { get; set; }

        [MaxLength(128)]
        public string CustomerId { get; set; }

        public IEnumerable<OrderDetailViewModel> OrderDetails { get; set; }
    }
}