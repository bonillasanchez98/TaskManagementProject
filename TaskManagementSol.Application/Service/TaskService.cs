using System.Linq.Expressions;
using TaskManagementSol.Application.Interface.Repos;
using TaskManagementSol.Application.Interface.Task;
using TaskManagementSol.Domain.Model;

namespace TaskManagementSol.Application.Service
{
    public class TaskService : ITaskService
    {
        //ATRIBUTOS
        private readonly ITaskRepo _repo;

        //CONSTRUCTOR
        public TaskService(ITaskRepo repo)
        {
            _repo = repo;
        }

        //METODOS
        public async Task<Result> CreateTaskAsync(TaskModel taskModel)
        {
            Result response = new Result();
            try
            {
                if (taskModel is null)
                {
                    response = Result.Failure("Task cannot be null");
                    return response;
                }
                return await _repo.CreateAsync(taskModel);
            }
            catch (Exception e)
            {
                response = Result.Failure($"CreateAsyncError: {e.Message}");
            }
            return response;
        }

        public async Task<Result> GetAllTasksAsync()
        {
            Result response = new Result();
            try
            {
                var taskResult = await _repo.GetAllAsync(t => t.Status == "Enabled" || t.Status == "Pending" || t.Status == "Completed");
                if (taskResult.IsSuccess)
                {
                    response = Result.Success("Tasks returned successfully", taskResult.Data);
                }
                else
                {
                    response = Result.Failure("Failure try getting Tasks.");
                }
            }
            catch (Exception e)
            {
                response = Result.Failure($"GetAllAsync Error: {e.Message}");
            }
            return response;
        }
        
        public async Task<Result> GetTaskByIdAsync(int id)
        {
            Result response = new Result();
            try
            {
                var IdExist = await _repo.GetByIdAsync(id);
                if (IdExist is null)
                {
                    response = Result.Failure($"Task {id} not found.");
                }
                response = Result.Success("Task returned successfully", IdExist.Data);
                
            }
            catch (Exception e)
            {
                response = Result.Failure($"GetByIdAsync Error: {e.Message}");
            }

            return response;
        }

        public async Task<Result> UpdateTaskAsync(TaskModel taskModel)
        {
            Result response = new Result();
            try
            {
                response = await _repo.UpdateAsync(taskModel);
                return response;

            }
            catch (Exception e)
            {
                response = Result.Failure($"UpdateAsync Error: {e.Message}");
            }
            return response;
        }

        public async Task<Result> DeleteTaskByIdAsync(int id)
        {
            Result response = new Result();
            try
            {
                var idExist = await _repo.GetByIdAsync(id);
                if (idExist != null)
                {
                    var result = await _repo.DeleteAsync(id);
                    return Result.Success($"Task ID[{id}] deleted successfully.");
                }
                else
                {
                    return Result.Failure($"ID[{id}] not found.");
                }
            }
            catch (Exception e)
            {
                response = Result.Failure($"DeleteAsync Error: {e.Message}");
            }
            return response;
        }

    }
}
