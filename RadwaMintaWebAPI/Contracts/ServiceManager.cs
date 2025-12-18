using AutoMapper;
using MailKit;
using Microsoft.Extensions.Options;
using RadwaMintaWebAPI.Interfaces;
using RadwaMintaWebAPI.Models.DbContexts;
using RadwaMintaWebAPI.Models.Entities;
using RadwaMintaWebAPI.Services;

namespace RadwaMintaWebAPI.Contracts
{
    public class ServiceManager : IServiceManager
    {
        private readonly IProductService _productService;
        private readonly IMediaService _mediaService;
        private readonly IReviewService _reviewService;
        private readonly IQualityService _qualityService;
        private readonly IExperienceService _experienceService;
        private readonly ITokenService _tokenService;
        private readonly IAuthService _authService;
        private readonly IAdminService _adminService;


        public ServiceManager(
            IProductService productService,
            IMediaService mediaService,
            IReviewService reviewService,
            IQualityService qualityService,
            IExperienceService experienceService,
            ITokenService tokenService,
            IAuthService authService,
            IAdminService adminService
            )
        {
            _productService = productService;
            _mediaService = mediaService;
            _reviewService = reviewService;
            _qualityService = qualityService;
            _experienceService = experienceService;
            _tokenService = tokenService;
            _authService = authService;
            _adminService = adminService;
        }

        public IProductService ProductService => _productService;
        public IMediaService MediaService => _mediaService;
        public IReviewService ReviewService => _reviewService;
        public IQualityService QualityService => _qualityService;
        public IExperienceService ExperienceService => _experienceService;
        public ITokenService TokenService => _tokenService;
        public IAuthService AuthService => _authService;

        IAdminService IServiceManager.AdminService => _adminService;
    }
}
