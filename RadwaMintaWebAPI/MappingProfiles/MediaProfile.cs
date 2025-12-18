using AutoMapper;
using RadwaMintaWebAPI.DTOs.Media;
using RadwaMintaWebAPI.DTOs.Products;
using RadwaMintaWebAPI.DTOs.Reviews;
using RadwaMintaWebAPI.Models.Entities;

namespace RadwaMintaWebAPI.MappingProfiles
{
    public class MediaProfile : Profile
    {
        public MediaProfile()
        {
            CreateMap<SocialMedia, SocialMediaDTo>();
            CreateMap<SocialMediaDTo, SocialMedia>();

            CreateMap<SocialMedia, WhatsAppDTo>();
            CreateMap<SocialMedia, PlatformDTo>();

            CreateMap<Product, ProductDTo>();

            CreateMap<ReviewCreateDto, Review>();
            CreateMap<Review, ReviewReadDto>();


        }
    }
}
