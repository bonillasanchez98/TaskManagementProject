using System.Linq.Expressions;

namespace TaskManagementSol.Application.Interface
{
    //Contrato global para operaciones CRUD.
    public interface IBaseRepo<T> where T : class
    {
        Task<Result> GetAllAsync(Expression<Func<T, bool>> filter); //Aplicando genericos
        Task<Result> GetByIdAsync(int id);
        Task<Result> CreateAsync(T type);
        Task<Result> UpdateAsync(T type);
        Task<Result> DeleteAsync(int id);
        Task<Result> ExistAsync(Expression<Func<T, bool>> filter);
    }
}
