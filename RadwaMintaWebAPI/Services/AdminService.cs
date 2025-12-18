using AutoMapper;
using Microsoft.EntityFrameworkCore;
using RadwaMintaWebAPI.Contracts;
using RadwaMintaWebAPI.DTOs.Media;
using RadwaMintaWebAPI.Interfaces;
using RadwaMintaWebAPI.Models.DbContexts;
using RadwaMintaWebAPI.Models.Entities;

namespace RadwaMintaWebAPI.Services
{
    public class AdminService(IUnitOfWork _unitOfWork, IMapper _mapper) : IAdminService
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

        public async Task<bool> UpdateSocialLinksAsync(SocialMediaDTo socialLinksDto)
        {
            var socialMediaList = await _unitOfWork.GetRepository<SocialMedia, int>().GetAllAsync();
            var existingLinks = socialMediaList.FirstOrDefault();

            if (existingLinks == null)
                return false;

            existingLinks.Facebook = socialLinksDto.Facebook;
            existingLinks.WhatsApp = socialLinksDto.WhatsApp;
            existingLinks.Youtube = socialLinksDto.Youtube;
            existingLinks.Instagram = socialLinksDto.Instagram;
            existingLinks.X = socialLinksDto.X;
            existingLinks.Pinterest = socialLinksDto.Pinterest;
            existingLinks.LinkedIn = socialLinksDto.LinkedIn;

            _unitOfWork.GetRepository<SocialMedia, int>().Update(existingLinks);

            await _unitOfWork.SaveChangesAsync();

            return true;
        }
    }
}
