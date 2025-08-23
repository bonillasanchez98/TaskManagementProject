using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using TaskManagementSol.Application;
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

        public BaseRepository(TaskManagementContext dbContext)
        {
            _dbContext = dbContext;
            _dbSet = dbContext.Set<T>();
        }

        public virtual async Task<Result> CreateAsync(T type)
        {
            Result result = new Result();
            try
            {
                _dbSet.Add(type);
                await _dbContext.SaveChangesAsync();
                result = Result.Success($"Entity created successfully.", type);
                return result;
            }
            catch (Exception)
            {
                result = Result.Failure($"Error saving in DB");
                return result;
            }   
        }

        public virtual async Task<Result> GetAllAsync(Expression<Func<T, bool>> filter)
        {
            Result result = new Result();
            try
            {
                var entities = await _dbSet.Where(filter).ToListAsync();
                result = Result.Success($"Entities returned successfully", entities);
                return result;
            }
            catch (Exception ex)
            {
                result = Result.Failure($"Error returning entities: {ex.Message}");
                return result;
            }

        }

        public virtual async Task<Result> GetByIdAsync(int id)
        {
            Result result = new Result();
            try
            {
                var entity = await _dbSet.FindAsync(id);
                if (entity is null)
                {
                    result = Result.Failure("Entity not found in DB");
                    return result;
                }
                result = Result.Success($"Entity found successfully", entity);
                return result;

            }
            catch (Exception)
            {
                result = Result.Failure($"Error finding entity ID {id} in DB");
                return result;
            }
        }

        public virtual async Task<Result> UpdateAsync(T type)
        {
            Result result = new Result();
            try
            {
                _dbSet.Update(type);
                await _dbContext.SaveChangesAsync();
                result = Result.Success($"Task created successfullly.", type);
                return result;
            }
            catch (Exception)
            {
                result = Result.Failure($"Error updating ");
                return result;
            }
        }

        public virtual async Task<Result> DeleteAsync(int id)
        {
            Result result = new Result();
            try
            {
                var taskExist = await _dbSet.FindAsync(id);
                if (taskExist != null)
                {
                    _dbSet.Remove(taskExist);
                    await _dbContext.SaveChangesAsync();
                    result = Result.Success($"Task removed successfully.");
                    return result;
                }
                else
                {
                    result = Result.Failure($"Task {id} Not found!");
                    return result;
                }

            }
            catch (Exception)
            {
                result = Result.Failure("Error deleting Task.");
                return result;
            }
        }
    }
}
