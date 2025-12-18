using RadwaMintaWebAPI.DTOs.Reviews;

namespace RadwaMintaWebAPI.Interfaces
{
    public interface IReviewService
    {
        //Task<IEnumerable<ReviewResponseDTo>> GetAllReviewsAsync(string lang);
        //Task<ReviewResponseDTo> CreateReviewAsync(ReviewRequestDTo reviewRequest);
        //Task<ReviewResponseDTo> GetReviewByIdAsync(int id);

        //// For Admin
        //Task DeleteReviewAsync(int id);

        Task<IEnumerable<ReviewReadDto>> GetAllReviewsAsync(string lang);
        Task<ReviewReadDto> GetReviewByIdAsync(int id);
        Task<ReviewReadDto> CreateReviewAsync(ReviewCreateDto dto);
        Task DeleteReviewAsync(int id);
    }
}
