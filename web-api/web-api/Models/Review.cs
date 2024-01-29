using System.ComponentModel.DataAnnotations;
namespace web_api.Models
{
    public class Review
    {
        [Key]
        public int Id { get; set; }
        public User User { get; set; } = null!;
        public Product Product { get; set; } = null!;  
        public int Rating { get; set; }
        public string Content { get; set; } = String.Empty;

    }

}
