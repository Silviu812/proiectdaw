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
        public async Task<Review> CreateReview(Review review)
        {
            _userDbContext.Reviews.Add(review);
            await _userDbContext.SaveChangesAsync();
            return review;
        }

        public async Task<Review?> UpdateReview(int reviewId, Review updatedReview)
        {
            var review = await _userDbContext.Reviews.FindAsync(reviewId);
            if (review == null) return null;

            review.Content = updatedReview.Content;
            review.Rating = updatedReview.Rating;

            await _userDbContext.SaveChangesAsync();
            return review;
        }

        public async Task<bool> DeleteReview(int reviewId)
        {
            var review = await _userDbContext.Reviews.FindAsync(reviewId);
            if (review == null) return false;

            _userDbContext.Reviews.Remove(review);
            await _userDbContext.SaveChangesAsync();
            return true;
        }
    }
}