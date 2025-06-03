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
    public interface IVtuberService
    {
        Task<VtuberDTO> GetVtuberByIdAsync(int id);
        Task<List<VtuberDTO>> GetAllVtubersAsync();
        Task<Vtuber> CreateVtuberAsync(int userId, string vtuberName, string realName, DateTime? debutDate, string channel, string description, int? vtuberGender, int? speciesId, int? companyId, IFormFile modelFile);
        Task<VtuberDTO> UpdateVtuberAsync(int id, string vtuberName, string realName, DateTime? debutDate, string channel, string description, int? vtuberGender, int? speciesId, int? companyId, IFormFile modelFile);
        Task<bool> DeleteVtuberAsync(int id);
    }

    // VtuberService
    public class VtuberService : IVtuberService
    {
        private readonly IVtuberRepository _vtuberRepository;
        private readonly ICloudinaryService _cloudinaryService;

        public VtuberService(IVtuberRepository vtuberRepository, ICloudinaryService cloudinaryService)
        {
            _vtuberRepository = vtuberRepository;
            _cloudinaryService = cloudinaryService;
        }

        public async Task<VtuberDTO> GetVtuberByIdAsync(int id)
        {
            return await _vtuberRepository.GetVtuberByIdAsync(id) ?? throw new Exception("Vtuber không tìm thấy");
        }

        public async Task<List<VtuberDTO>> GetAllVtubersAsync()
        {
            return await _vtuberRepository.GetAllVtubersAsync();
        }

        public async Task<Vtuber> CreateVtuberAsync(int userId, string vtuberName, string realName, DateTime? debutDate, string channel, string description, int? vtuberGender, int? speciesId, int? companyId, IFormFile modelFile)
        {
            var vtuber = new Vtuber
            {
                UserId = userId,
                VtuberName = vtuberName,
                RealName = realName,
                DebutDate = debutDate,
                Channel = channel,
                Description = description,
                VtuberGender = vtuberGender,
                SpeciesId = speciesId,
                CompanyId = companyId,
                ModelUrl = await _cloudinaryService.UploadImageAsync(modelFile)
            };
            return await _vtuberRepository.CreateVtuberAsync(vtuber);
        }

        public async Task<VtuberDTO> UpdateVtuberAsync(int id, string vtuberName, string realName, DateTime? debutDate, string channel, string description, int? vtuberGender, int? speciesId, int? companyId, IFormFile modelFile)
        {
            var vtuber = await _vtuberRepository.GetEntityByIdAsync(id) ?? throw new Exception("Vtuber không tìm thấy");
            // var vtuberUpdate = new Vtuber
            // {
            //     // VtuberId = id,
            //     VtuberName = vtuberName,
            //     RealName = realName,
            //     DebutDate = debutDate,
            //     Channel = channel,
            //     Description = description,
            //     VtuberGender = vtuberGender,
            //     SpeciesId = speciesId,
            //     CompanyId = companyId
            // };
            // vtuber.VtuberName = vtuberName ?? vtuber.VtuberName;
            vtuber.VtuberName = vtuberName ?? vtuber.VtuberName;
            vtuber.RealName = realName ?? vtuber.RealName;
            vtuber.DebutDate = debutDate ?? vtuber.DebutDate;
            vtuber.Channel = channel ?? vtuber.Channel;
            vtuber.Description = description ?? vtuber.Description;
            vtuber.VtuberGender = vtuberGender ?? vtuber.VtuberGender;
            vtuber.SpeciesId = speciesId ?? vtuber.SpeciesId;
            vtuber.CompanyId = companyId ?? vtuber.CompanyId;
            if (modelFile != null)
            {
                vtuber.ModelUrl = await _cloudinaryService.UploadImageAsync(modelFile);
            }
            return await _vtuberRepository.UpdateVtuberAsync(vtuber);
        }

        public async Task<bool> DeleteVtuberAsync(int id)
        {
            return await _vtuberRepository.DeleteVtuberAsync(id);
        }
    }
}