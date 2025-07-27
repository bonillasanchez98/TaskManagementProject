namespace TaskManagementSol.Application.Interface
{
    //Contrato global para operaciones CRUD.
    public interface IBaseRepo<T> where T : class
    {
        Task<IEnumerable<T>> GetAllAsync();
        Task<T> GetByIdAsync(int id);
        Task<(bool IsSuccess, string Message)> CreateAsync(T type);
        Task<(bool IsSuccess, string Message)> UpdateAsync(T type);
        Task<(bool IsSuccess, string Message)> DeleteAsync(int id);
    }
}
