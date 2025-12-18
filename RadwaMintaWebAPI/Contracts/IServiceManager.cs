using RadwaMintaWebAPI.Interfaces;

namespace RadwaMintaWebAPI.Contracts
{
    public interface IServiceManager
    {
        // Property for Every Service
        public IProductService ProductService { get; }
        public IMediaService MediaService { get; }
        public IReviewService ReviewService { get; }
        public IQualityService QualityService { get; }
        public IExperienceService ExperienceService { get; }
        public ITokenService TokenService { get; }
        public IAuthService AuthService { get; }
        public IAdminService AdminService { get; }
    }
}
