using AutoMapper;
using RadwaMintaWebAPI.Contracts;
using RadwaMintaWebAPI.DTOs.Reviews;
using RadwaMintaWebAPI.Interfaces;
using RadwaMintaWebAPI.Models.Entities;

namespace RadwaMintaWebAPI.Services
{
    public class ReviewService(IUnitOfWork _unitOfWork, IMapper _mapper) : IReviewService
    {
        public async Task<IEnumerable<ReviewReadDto>> GetAllReviewsAsync(string lang)
        {
            var reviews = await _unitOfWork.GetRepository<Review, int>().GetAllAsync();
            var reviewDtos = new List<ReviewReadDto>();

            foreach (var review in reviews)
            {
                reviewDtos.Add(new ReviewReadDto
                {
                    Id = review.Id,
                    FirstName = lang == "ar" ? review.FirstNameAr : review.FirstName,
                    LastName = lang == "ar" ? review.LastNameAr : review.LastName,
                    JobTitle = lang == "ar" ? review.JobTitleAr : review.JobTitle,
                    Email = review.Email,
                    Description = lang == "ar" ? review.DescriptionAr : review.Description,
                    Rating = review.Rating,
                    ReviewDate = review.ReviewDate
                });
            }

            return reviewDtos.OrderByDescending(r => r.ReviewDate);
        }

        public async Task<ReviewReadDto> GetReviewByIdAsync(int id)
        {
            var review = await _unitOfWork.GetRepository<Review, int>().GetByIdAsync(id);
            if (review == null)
                throw new KeyNotFoundException($"Review with ID {id} not found.");

            return _mapper.Map<ReviewReadDto>(review);
        }

        public async Task<ReviewReadDto> CreateReviewAsync(ReviewCreateDto reviewRequest)
        {
            var review = _mapper.Map<Review>(reviewRequest);
            review.ReviewDate = DateTime.Now;

            await _unitOfWork.GetRepository<Review, int>().AddAsync(review);
            await _unitOfWork.SaveChangesAsync();

            return _mapper.Map<ReviewReadDto>(review);
        }

        public async Task DeleteReviewAsync(int id)
        {
            var review = await _unitOfWork.GetRepository<Review, int>().GetByIdAsync(id);
            if (review == null)
                throw new KeyNotFoundException($"Review with ID {id} not found.");

            _unitOfWork.GetRepository<Review, int>().Delete(review);
            await _unitOfWork.SaveChangesAsync();
        }
        //public async Task<IEnumerable<ReviewResponseDTo>> GetAllReviewsAsync(string lang)
        //{
        //    var reviews = await _unitOfWork.GetRepository<Review, int>().GetAllAsync();

        //    var reviewDtos = new List<ReviewResponseDTo>();
        //    foreach (var review in reviews)
        //    {
        //        reviewDtos.Add(new ReviewResponseDTo
        //        {
        //            Id = review.Id,
        //            FirstName = lang == "ar" ? review.FirstNameAr : review.FirstName, 
        //            LastName = lang == "ar" ? review.LastNameAr : review.LastName,   
        //            JobTitle = lang == "ar" ? review.JobTitleAr : review.JobTitle,
        //            Email = review.Email,
        //            Description = lang == "ar" ? review.DescriptionAr : review.Description,
        //            Rating = review.Rating,
        //        });
        //    }
        //    return reviewDtos;
        //}


        //public async Task<ReviewResponseDTo> CreateReviewAsync(ReviewRequestDTo reviewRequest)
        //{
        //    var review = _mapper.Map<ReviewRequestDTo, Review>(reviewRequest);

        //    await _unitOfWork.GetRepository<Review, int>().AddAsync(review);
        //    await _unitOfWork.SaveChangesAsync();

        //    return _mapper.Map<Review, ReviewResponseDTo>(review);
        //}


        //public async Task<ReviewResponseDTo> GetReviewByIdAsync(int id)
        //{
        //    var review = await _unitOfWork.GetRepository<Review, int>().GetByIdAsync(id);
        //    if (review == null)
        //    {
        //        throw new KeyNotFoundException($"Review with ID {id} not found.");
        //    }
        //    return _mapper.Map<Review, ReviewResponseDTo>(review);
        //}



        //// For Admin
        //public async Task DeleteReviewAsync(int id)
        //{
        //    var review = await _unitOfWork.GetRepository<Review, int>().GetByIdAsync(id);
        //    if (review == null)
        //    {
        //        throw new KeyNotFoundException($"Review with ID {id} not found.");
        //    }
        //    _unitOfWork.GetRepository<Review, int>().Delete(review);
        //    await _unitOfWork.SaveChangesAsync();
        //}
    }
}
