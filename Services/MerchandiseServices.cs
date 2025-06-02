using Microsoft.AspNetCore.Http;
using VtuberMerchHub.DTOs;
using VtuberMerchHub.Models;
using VtuberMerchHub.Data;

namespace VtuberMerchHub.Services
{
    public interface IMerchandiseService
    {
        Task<List<MerchandiseDTO>> GetAllMerchandisesAsync();
        Task<List<MerchandiseDTO>> GetMerchandisesByUserIdAsync(int userId);

        Task<MerchandiseDTO> GetMerchandiseByIdAsync(int id);
        Task<MerchandiseDTO> CreateMerchandiseAsync(int vtuberId, string merchandiseName, IFormFile imageUrl, DateTime? startDate, DateTime? endDate, string? description);
        Task<MerchandiseDTO> UpdateMerchandiseAsync(int id, string merchandiseName, IFormFile imageUrl, DateTime? startDate, DateTime? endDate, string? description, int? vtuberId);
        Task<bool> DeleteMerchandiseAsync(int id);
    }

    public class MerchandiseService : IMerchandiseService
    {
        private readonly IMerchandiseRepository _repository;
        private readonly ICloudinaryService _cloudinary;

        public MerchandiseService(IMerchandiseRepository repository, ICloudinaryService cloudinary)
        {
            _repository = repository;
            _cloudinary = cloudinary;
        }

        public async Task<List<MerchandiseDTO>> GetAllMerchandisesAsync()
        {
            return await _repository.GetAllMerchandisesAsync();
        }

        public async Task<List<MerchandiseDTO>> GetMerchandisesByUserIdAsync(int userId)
        {
            return await _repository.GetMerchandisesByUserIdAsync(userId);
        }


        public async Task<MerchandiseDTO> GetMerchandiseByIdAsync(int id)
        {
            return await _repository.GetMerchandiseByIdAsync(id)
                ?? throw new Exception("Merchandise không tồn tại");
        }

        public async Task<MerchandiseDTO> CreateMerchandiseAsync(int vtuberId, string merchandiseName, IFormFile imageUrl, DateTime? startDate, DateTime? endDate, string? description)
        {
            var imageUrlStr = await _cloudinary.UploadImageAsync(imageUrl);

            var merchandise = new Merchandise
            {
                VtuberId = vtuberId,
                MerchandiseName = merchandiseName,
                ImageUrl = imageUrlStr,
                StartDate = startDate,
                EndDate = endDate,
                Description = description
            };

            return await _repository.CreateMerchandiseAsync(merchandise);
        }

        public async Task<MerchandiseDTO> UpdateMerchandiseAsync(int id, string merchandiseName, IFormFile imageUrl, DateTime? startDate, DateTime? endDate, string? description, int? vtuberId)
        {
            var merchandise = await _repository.FindEntityByIdAsync(id)
                ?? throw new Exception("Merchandise không tồn tại");

            merchandise.MerchandiseName = merchandiseName ?? merchandise.MerchandiseName;
            merchandise.StartDate = startDate ?? merchandise.StartDate;
            merchandise.EndDate = endDate ?? merchandise.EndDate;
            merchandise.Description = description ?? merchandise.Description;
            merchandise.VtuberId = vtuberId ?? merchandise.VtuberId;

            if (imageUrl != null)
            {
                merchandise.ImageUrl = await _cloudinary.UploadImageAsync(imageUrl);
            }

            return await _repository.UpdateMerchandiseAsync(merchandise);
        }

        public async Task<bool> DeleteMerchandiseAsync(int id)
        {
            return await _repository.DeleteMerchandiseAsync(id);
        }
    }
}
