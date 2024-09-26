using API_Task.Data.Interface;
using app_version.Model;
using Microsoft.EntityFrameworkCore;
using System.Drawing;

namespace API_Task.Data.Implementation
{
    public class UnitOfWork : IUnitOfWork
    {
        private ApplicationDbContext _context;
        GenericRepository<AppVersionModel> _AppVersion;
       


       
        public UnitOfWork(ApplicationDbContext context)
        {
            _context = context;
        }

        public IGenericRepository<AppVersionModel> GetEmpolyees => _AppVersion ??= new GenericRepository<AppVersionModel>(_context);

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        private void Dispose(bool dispose)
        {
            if (dispose)
            {
                _context.Dispose();
            }
        }
        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }

        public async Task ExecuteProcedureWithoutResult(string query)
        {
            await _context.Database.ExecuteSqlRawAsync(query);
        }
    }
}
