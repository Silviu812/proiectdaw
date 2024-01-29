using System.ComponentModel.DataAnnotations;

namespace web_api.Models
{
    public class Product
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; } = String.Empty;
        public int Price { get; set; }
        public string Details { get; set; } = String.Empty;
        public List<Review> Reviews { get; set; } = null!;
    }

}
