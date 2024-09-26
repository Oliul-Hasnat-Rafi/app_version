using app_version.Model;
using System.Drawing;

namespace API_Task.Data.Interface
{
    public interface IUnitOfWork : IDisposable
    {
      //  IGenericRepository<DepartmentModel> Departments { get; }
        IGenericRepository<AppVersionModel> GetEmpolyees { get; }
     

        Task SaveAsync();
        Task ExecuteProcedureWithoutResult(string query);
    }
}
