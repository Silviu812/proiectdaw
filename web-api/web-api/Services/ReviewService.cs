using Microsoft.EntityFrameworkCore;
using web_api.Data;
using web_api.Models;

namespace web_api.Services
{
    public class ReviewService
    {
        private readonly UserDbContext _userDbContext;
        private readonly UserService _userService;

        public ReviewService(UserDbContext userDbContext, UserService userService)
        {
            _userDbContext = userDbContext;
            _userService = userService;
        }

        public async Task<List<Review>> GetAllReviews()
        {
            return await _userDbContext.Reviews.ToListAsync();
        }

        public async Task<List<Review>> GetUserReviewsFromId(int Id)
        {
            User? user = await _userService.GetUserById(Id);

            if (user == null) return null!;

            return user.Reviews.ToList();
        }

        public async Task<List<Review>> GetUserReviewsFromUsername(string username)
        {
            User? user = await _userService.GetUserByUserName(username);

            if (user == null) return null!;

            return user.Reviews.ToList();
        }
    }
}