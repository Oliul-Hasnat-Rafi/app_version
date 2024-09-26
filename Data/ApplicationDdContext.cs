using app_version.Model;
using Microsoft.EntityFrameworkCore;

namespace API_Task.Data
{
   

        public class ApplicationDbContext : DbContext
        {

            public ApplicationDbContext(DbContextOptions options) : base(options)
            {

            }
            public DbSet<AppVersionModel> GetAppVersions { get; set; }
        }
    


}
