using API_Task.Data;
using API_Task.Data.Interface;
using app_version.Model;
using app_version.Model.DTO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;

namespace app_version.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AppVersionController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ApplicationDbContext _db;

        public AppVersionController(IUnitOfWork unitOfWork, ApplicationDbContext db)
        {
            _unitOfWork = unitOfWork;
            _db = db;
        }

        [HttpPost("create")]
        public async Task<IActionResult> CreateAppVersion(AppVersionDTO appVersionDto)
        {
            if (appVersionDto == null || !ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var appVersion = new AppVersionModel
                {
                    ApplicationId = appVersionDto.ApplicationId,
                    Version = appVersionDto.Version,
                    IsLate = appVersionDto.IsLate,
                    Message = appVersionDto.Message,
                    IosUrl = appVersionDto.IosUrl,
                    AndroidUrl = appVersionDto.AndroidUrl
                };

                await _unitOfWork.getAppVersion.CreateAsync(appVersion);
                await _unitOfWork.SaveAsync();

                return CreatedAtAction(nameof(GetAppVersion), new { applicationId = appVersion.ApplicationId }, appVersion);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred while creating the app version: {ex.Message}");
            }
        }

        [HttpGet("{applicationId}")]
        public async Task<IActionResult> GetAppVersion(string applicationId)
        {
            try
            {
                var appVersion = await _unitOfWork.getAppVersion.GetAsync(av => av.ApplicationId == applicationId);

                if (appVersion == null)
                {
                    return NotFound($"No app version found for ApplicationId: {applicationId}");
                }

                return Ok(appVersion);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred while retrieving the app version: {ex.Message}");
            }
        }

        [HttpPut("{applicationId}")]
        public async Task<IActionResult> UpdateAppVersion(string applicationId, AppVersionDTO appVersionDto)
        {
            if (appVersionDto == null || !ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var existingAppVersion = await _unitOfWork.getAppVersion.GetAsync(av => av.ApplicationId == applicationId);

                if (existingAppVersion == null)
                {
                    return NotFound($"No app version found for ApplicationId: {applicationId}");
                }

                existingAppVersion.Version = appVersionDto.Version;
                existingAppVersion.IsLate = appVersionDto.IsLate;
                existingAppVersion.Message = appVersionDto.Message;
                existingAppVersion.IosUrl = appVersionDto.IosUrl;
                existingAppVersion.AndroidUrl = appVersionDto.AndroidUrl;

                await _unitOfWork.getAppVersion.UpdateAsync(existingAppVersion);
                await _unitOfWork.SaveAsync();

                return Ok(existingAppVersion);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred while updating the app version: {ex.Message}");
            }
        }

        [HttpDelete("{applicationId}")]
        public async Task<IActionResult> DeleteAppVersion(string applicationId)
        {
            try
            {
                var appVersion = await _unitOfWork.getAppVersion.GetAsync(av => av.ApplicationId == applicationId);

                if (appVersion == null)
                {
                    return NotFound($"No app version found for ApplicationId: {applicationId}");
                }

                 _unitOfWork.getAppVersion.DeleteAsync(appVersion);
                await _unitOfWork.SaveAsync();

                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred while deleting the app version: {ex.Message}");
            }
        }
    }
}