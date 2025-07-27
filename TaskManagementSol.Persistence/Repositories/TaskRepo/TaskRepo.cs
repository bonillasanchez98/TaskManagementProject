using TaskManagementSol.Application.Interface.Repos;
using TaskManagementSol.Domain.Model;
using TaskManagementSol.Persistence.Context;

namespace TaskManagementSol.Persistence.Repositories.TaskRepo
{
    //Implementacion generica + Implementacion especifica
    public sealed class TaskRepo : BaseRepository<TaskModel>, ITaskRepo
    {
        public TaskRepo(TaskManagementContext dbContext) : base(dbContext)
        {
        }

        public override Task<(bool IsSuccess, string Message)> CreateAsync(TaskModel type)
        {
            return base.CreateAsync(type);
        }

        public override Task<IEnumerable<TaskModel>> GetAllAsync()
        {
            return base.GetAllAsync();
        }

        public override Task<TaskModel> GetByIdAsync(int id)
        {
            return base.GetByIdAsync(id);
        }

        public override Task<(bool IsSuccess, string Message)> UpdateAsync(TaskModel type)
        {
            return base.UpdateAsync(type);
        }

        public override Task<(bool IsSuccess, string Message)> DeleteAsync(int id)
        {
            return base.DeleteAsync(id);
        }
    }
}
