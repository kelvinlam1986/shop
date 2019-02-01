using System.ComponentModel.DataAnnotations;

namespace Shop.Web.Models
{
    public class ContactDetailViewModel
    {
        public int ID { get; set; }
        
        [Required(ErrorMessage = "Bạn quên nhập tên của mình")]
        public string Name { get; set; }

        [MaxLength(50, ErrorMessage = "Số điện thoại không vượt quá 50 ký tự")]
        public string Phone { get; set; }

        [MaxLength(250, ErrorMessage = "Email không vượt quá 250 ký tự")]
        public string Email { get; set; }

        [MaxLength(250, ErrorMessage = "Website không vượt quá 250 ký tự")]
        public string Website { get; set; }

        [MaxLength(250, ErrorMessage = "Địa chỉ không vượt quá 250 ký tự")]
        public string Address { get; set; }
        public string Other { get; set; }
        public bool Status { get; set; }
        public double? Lat { get; set; }
        public double? Lng { get; set; }
    }
}