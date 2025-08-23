using System.Linq.Expressions;
using TaskManagementSol.Application;
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

        public override Task<Result> CreateAsync(TaskModel taskModel)
        {
            Result result = new Result();
            if (taskModel.Description.Length < 5)
            {
                result = Result.Failure("The description cannot be to short");
            }
            return base.CreateAsync(taskModel);
        }

        public override Task<Result> GetAllAsync(Expression<Func<TaskModel, bool>> filter)
        {
            return base.GetAllAsync(filter);
        }

        public override Task<Result> GetByIdAsync(int id)
        {
            return base.GetByIdAsync(id);
        }

        public override Task<Result> UpdateAsync(TaskModel type)
        {
            return base.UpdateAsync(type);
        }

        public override Task<Result> DeleteAsync(int id)
        {
            return base.DeleteAsync(id);
        }
    }
}
