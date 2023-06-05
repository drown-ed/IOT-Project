using System.ComponentModel.DataAnnotations;

namespace PortfolioWeb.Models
{
    public class PortfolioModel
    {
        [Key]
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string? Url { get; set; }
        [FileExtensions(Extensions =".png, .jpg, .jpeg, .gif", ErrorMessage ="이미지 파일 선택")]
        public string? FileName { get; set; }
    }
}
