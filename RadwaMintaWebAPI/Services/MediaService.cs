using AutoMapper;
using Microsoft.EntityFrameworkCore;
using RadwaMintaWebAPI.Contracts;
using RadwaMintaWebAPI.DTOs.Media;
using RadwaMintaWebAPI.Interfaces;
using RadwaMintaWebAPI.Models.DbContexts;
using RadwaMintaWebAPI.Models.Entities;
using System.Net;

namespace RadwaMintaWebAPI.Services
{
    public class MediaService(IUnitOfWork _unitOfWork, IMapper _mapper, MintaDbContext _dbContext) : IMediaService
    {
        public async Task<SocialMediaDTo> GetSocialMediaLinksAsync()
        {
            var socialMedia = await _unitOfWork.GetRepository<SocialMedia, int>().GetAllAsync();
            var firstEntry = socialMedia.FirstOrDefault();

            if (firstEntry == null)
            {
                return new SocialMediaDTo();
            }

            return _mapper.Map<SocialMedia, SocialMediaDTo>(firstEntry);
        }

        public async Task<WhatsAppDTo> GetWhatsAppLinkAsync()
        {
            var socialMedia = await _unitOfWork.GetRepository<SocialMedia, int>().GetAllAsync();
            var firstEntry = socialMedia.FirstOrDefault();

            if (firstEntry == null || string.IsNullOrEmpty(firstEntry.WhatsApp))
            {
                return new WhatsAppDTo();
            }

            return _mapper.Map<SocialMedia, WhatsAppDTo>(firstEntry);
        }

        public async Task<PlatformDTo> GetPlatformLinkAsync()
        {
            var socialMedia = await _unitOfWork.GetRepository<SocialMedia, int>().GetAllAsync();
            var firstEntry = socialMedia.FirstOrDefault();

            if (firstEntry == null || string.IsNullOrEmpty(firstEntry.WhatsApp))
            {
                return new PlatformDTo();
            }

            return _mapper.Map<SocialMedia, PlatformDTo>(firstEntry);
        }


   








    }
}
