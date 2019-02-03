using System;
using System.ComponentModel.DataAnnotations;

namespace Shop.Web.Models
{
    public class FeedbackViewModel
    {
        public int ID { get; set; }

        [Required(ErrorMessage = "Bạn phải nhập tên.")]
        [MaxLength(250, ErrorMessage = "Tên không vượt quá 250 ký tự.")]
        public string Name { get; set; }

        [StringLength(250, ErrorMessage = "Email không vượt quá 250 ký tự")]
        public string Email { get; set; }

        [StringLength(500, ErrorMessage = "Tin nhắn không vượt quá 500 ký tự")]
        public string Message { get; set; }

        public DateTime CreatedDate { get; set; }

        [Required(ErrorMessage = "Bạn phải chọn trạng thái")]
        public bool Status { get; set; }

        public ContactDetailViewModel ContactViewModel { get; set; }
    }
}