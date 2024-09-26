using System.ComponentModel.DataAnnotations;

namespace app_version.Model.DTO
{
    public class AppVersionDTO
    {
        public string ApplicationId { get; set; }
        public string Version { get; set; }

        public string Message { get; set; }

        public bool IsLate { get; set; }

        public string AndroidUrl { get; set; }
        public string IosUrl { get; set; }
    }
}
