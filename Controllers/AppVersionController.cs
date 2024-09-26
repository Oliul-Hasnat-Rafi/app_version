using API_Task.Data;
using API_Task.Data.Interface;
using app_version.Model;
using app_version.Model.DTO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace app_version.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AppVersionController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ApplicationDbContext db;

        public AppVersionController(IUnitOfWork unitOfWork,ApplicationDbContext db)
        {
            _unitOfWork = unitOfWork;
            this.db = db;
        }

        // POST: Create App Version
        [HttpPost("create-app-version")]
        public async Task<IActionResult> CreateAppVersion(AppVersionDTO appversiondto)
        {
            if (appversiondto == null || !ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var appVersion = new AppVersionModel
                {
                    ApplicationId = appversiondto.ApplicationId,
                    Version = appversiondto.Version,
                    IsLate = appversiondto.IsLate,
                    Message = appversiondto.Message,
                    IosUrl = appversiondto.IosUrl,
                    AndroidUrl = appversiondto.AndroidUrl
                };

                await _unitOfWork.GetEmpolyees.CreateAsync(appVersion);
                await _unitOfWork.SaveAsync();

                return Ok("App version created successfully.");
            }
            catch (Exception)
            {
                return StatusCode(500, "An error occurred while creating the app version.");
            }
        }

        [HttpGet("{applicationId}")]
        public async Task<IActionResult> GetAppVersion(string applicationId)
        {
            try
            {
                //var appVersion = await _unitOfWork.GetEmpolyees.GetAsync(av => av.ApplicationId == applicationId);
                var appVersion = await db.GetAppVersions.Where(x=>x.ApplicationId==applicationId).Select(x=>x.Version).SingleOrDefaultAsync();
                if (appVersion == null)
                {
                    return NotFound($"No app version found for ApplicationId: {applicationId}");
                }

                return Ok(appVersion);
            }
            catch (Exception)
            {
                return StatusCode(500, "An error occurred while retrieving the app version.");
            }
        }
    }
}
