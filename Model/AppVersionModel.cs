using System.ComponentModel.DataAnnotations;

namespace app_version.Model
{
    public class AppVersionModel
    {
        [Key]
        public string ApplicationId { get; set; }

        [Required]
        [StringLength(100)]
        public string Version { get; set; }

        [Required]
        [StringLength(500)]
        public string Message { get; set; }

        public bool IsLate { get; set; }

        [Required]
        [StringLength(200)]
        [Url]
        public string AndroidUrl { get; set; }

        [Required]
        [StringLength(200)]
        [Url]
        public string IosUrl { get; set; }
    }
}
