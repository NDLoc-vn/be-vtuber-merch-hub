using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using VtuberMerchHub.Data;
using VtuberMerchHub.Models;
using Microsoft.AspNetCore.Http;
using VtuberMerchHub.DTOs;

namespace VtuberMerchHub.Services
{
    public interface IMerchandiseService
    {
        Task<Merchandise> GetMerchandiseByIdAsync(int id);
        Task<List<Merchandise>> GetAllMerchandisesAsync();
        Task<Merchandise> CreateMerchandiseAsync(int vtuberId, string merchandiseName, IFormFile imageUrl, DateTime? startDate, DateTime? endDate, string? description);
        Task<Merchandise> UpdateMerchandiseAsync(int merchandiseId, string merchandiseName, IFormFile imageUrl, DateTime? startDate, DateTime? endDate, string newDescription, int? vtuberId);
        Task<bool> DeleteMerchandiseAsync(int id);
    }

    // MerchandiseService
    public class MerchandiseService : IMerchandiseService
    {
        private readonly IMerchandiseRepository _merchandiseRepository;
        private readonly ICloudinaryService _cloudinaryService;

        public MerchandiseService(IMerchandiseRepository merchandiseRepository, ICloudinaryService cloudinaryService)
        {
            _merchandiseRepository = merchandiseRepository;
            _cloudinaryService = cloudinaryService;
        }

        public async Task<Merchandise> GetMerchandiseByIdAsync(int id)
        {
            return await _merchandiseRepository.GetMerchandiseByIdAsync(id) ?? throw new Exception("Merchandise không tìm thấy");
        }

        public async Task<List<Merchandise>> GetAllMerchandisesAsync()
        {
            return await _merchandiseRepository.GetAllMerchandisesAsync();
        }

        public async Task<Merchandise> CreateMerchandiseAsync(int vtuberId, string merchandiseName, IFormFile imageUrl, DateTime? startDate, DateTime? endDate, string? description)
        {
            var merchandise = new Merchandise
            {
                VtuberId = vtuberId,
                MerchandiseName = merchandiseName,
                ImageUrl = await _cloudinaryService.UploadImageAsync(imageUrl),
                StartDate = startDate,
                EndDate = endDate,
                Description = description
            };
            return await _merchandiseRepository.CreateMerchandiseAsync(merchandise);
        }

        public async Task<Merchandise> UpdateMerchandiseAsync(int merchandiseId, string merchandiseName, IFormFile imageUrl, DateTime? startDate, DateTime? endDate, string newDescription, int? vtuberId)
        {
            var merchandise = await _merchandiseRepository.GetMerchandiseByIdAsync(merchandiseId) ?? throw new Exception("Merchandise không tìm thấy");
            merchandise.MerchandiseName = merchandiseName ?? merchandise.MerchandiseName;
            merchandise.StartDate = startDate != default ? startDate : merchandise.StartDate;
            merchandise.EndDate = endDate != default ? endDate : merchandise.EndDate;
            merchandise.Description = newDescription ?? merchandise.Description;
            merchandise.VtuberId = vtuberId ?? merchandise.VtuberId;
            if (imageUrl != null)
            {
                merchandise.ImageUrl = await _cloudinaryService.UploadImageAsync(imageUrl);
            }
            return await _merchandiseRepository.UpdateMerchandiseAsync(merchandise);
        }

        public async Task<bool> DeleteMerchandiseAsync(int id)
        {
            return await _merchandiseRepository.DeleteMerchandiseAsync(id);
        }
    }
}