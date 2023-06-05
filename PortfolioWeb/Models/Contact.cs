using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace PortfolioWeb.Models
{
    public class Contact
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "필수 입력")]
        [MaxLength(30)]
        public string Name { get; set; }
        [Required(ErrorMessage = "필수 입력")]
        [MaxLength(100)]
        public string Email { get; set; }
        [Required(ErrorMessage = "필수 입력")]
        [MaxLength(1000)]
        public string Message { get; set; }
    }
}
