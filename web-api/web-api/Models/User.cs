using System.ComponentModel.DataAnnotations;

namespace web_api.Models
{
    public class User
    {
        [Key]
        public int Id { get; set; }
        public string Username { get; set; } = String.Empty;
        public string Email { get; set; } = String.Empty;
        public string Password { get; set; } = String.Empty;
        public string Role { get; set; } = WebRoles.Member;
        public List<Review> Reviews { get; set; } = null!;
        public Information Information { get; set; } = null!; 
    }
    public static class WebRoles
    {
        public const string Member = "Membru";
        public const string Admin = "Admin";
    }
}
