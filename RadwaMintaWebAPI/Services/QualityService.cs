using AutoMapper;
using Microsoft.Extensions.Configuration;
using RadwaMintaWebAPI.Contracts;
using RadwaMintaWebAPI.DTOs.Quality;
using RadwaMintaWebAPI.Interfaces;
using RadwaMintaWebAPI.Models.Entities;

namespace RadwaMintaWebAPI.Services
{
    public class QualityService(IUnitOfWork _unitOfWork, IMapper _mapper, IConfiguration _configuration) : IQualityService
    {
        private string GetFullPictureUrl(string relativeUrl)
        {
            if (string.IsNullOrEmpty(relativeUrl))
                return string.Empty;
            else
            {
                var baseUrl = _configuration.GetSection("URLs")["BaseUrl"];
                return $"{baseUrl}{relativeUrl}";
            }
        }

        public async Task<IEnumerable<QualityDTo>> GetAllQualitiesAsync(string lang)
        {
            var qualities = await _unitOfWork.GetRepository<Quality, int>().GetAllAsync();

            var qualityDtos = new List<QualityDTo>();
            foreach (var quality in qualities)
            {
                qualityDtos.Add(new QualityDTo
                {
                    Id = quality.Id,
                    Title = lang == "ar" ? quality.TitleAr : quality.Title,
                    PictureUrl = GetFullPictureUrl(quality.PictureUrl),
                    Content = lang == "ar" ? quality.ContentAr : quality.Content
                });
            }
            return qualityDtos;
        }
    }
}
