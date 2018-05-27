using System.ComponentModel.DataAnnotations;

namespace Shop.Model.Abstract
{
    public class Switchable : ISwitchable
    {
        [Required]
        public bool Status { get; set; }
    }
}
