using Microsoft.EntityFrameworkCore;
using TaskManagementSol.Application.Interface;
using TaskManagementSol.Domain.Model;
using TaskManagementSol.Persistence.Context;

namespace TaskManagementSol.Persistence.Repositories
{
    //Implementacion generica del contrato global CRUD.
    public abstract class BaseRepository<T> : IBaseRepo<T> where T : class
    {
        private readonly TaskManagementContext _dbContext;
        private readonly DbSet<T> _dbSet;

        protected BaseRepository(TaskManagementContext dbContext)
        {
            _dbContext = dbContext;
            _dbSet = dbContext.Set<T>();
        }

        public virtual async Task<(bool IsSuccess, string Message)> CreateAsync(T type)
        {
            try
            {
                await _dbSet.AddAsync(type);
                await _dbContext.SaveChangesAsync();
                return (true, $"Task {typeof(T)} created successfullly.");
            }
            catch (Exception)
            {
                return (false, "Error creating Task.");
            }
        }

        public virtual async Task<IEnumerable<T>> GetAllAsync()
            => (IEnumerable<T>)await _dbContext.Task_tb.ToListAsync();


        public virtual async Task<T> GetByIdAsync(int id)
            => await _dbSet.FindAsync(id);

        public virtual async Task<(bool IsSuccess, string Message)> UpdateAsync(T type)
        {
            try
            {
                _dbSet.Update(type);
                await _dbContext.SaveChangesAsync();
                return (true, $"Task {typeof(T)} created successfullly.");
            }
            catch (Exception)
            {
                return (false, "Error updating Task.");
            }
        }

        public virtual async Task<(bool IsSuccess, string Message)> DeleteAsync(int id)
        {
            try
            {
                var taskExist = await _dbSet.FindAsync(id);
                if (taskExist != null)
                {
                    _dbSet.Remove(taskExist);
                    await _dbContext.SaveChangesAsync();
                    return (true, $"Task removed successfully.");
                }
                else
                {
                    return (false, $"Task {id} Not found!");
                }

            }
            catch (Exception)
            {
                return (false, "Error deleting Task.");
            }
        }
    }
}
