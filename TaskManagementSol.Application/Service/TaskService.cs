using System.Linq.Expressions;
using TaskManagementSol.Application.Interface.Repos;
using TaskManagementSol.Application.Interface.Task;
using TaskManagementSol.Domain.Model;

namespace TaskManagementSol.Application.Service
{
    //Cabecera del delegate
    public delegate Result ValidateTask(TaskModel task);

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
                ValidateTask validateTask = new ValidateTask(ValidateTaskBody); //Case: ValidandoTask
                if (!validateTask(taskModel).IsSuccess)
                {
                    response = Result.Failure($"Task invalid format");
                    return response;
                }

                //Case: TaskCreationNotify
                Action<string> taskCreationNotify = tcn => 
                Console.Write($"Task created: {taskModel.Description}\n" +
                                   $"DueTime: {taskModel.DueTime}");
                Console.WriteLine(taskCreationNotify);

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
            Action<string> taskModifyNotify = tcn =>
                Console.Write($"Task modify: {taskModel.Description}\n" +
                                   $"DueTime: {taskModel.DueTime}\n" +
                                   $"Status: {taskModel.Status}");
            Console.WriteLine(taskModifyNotify);
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

        //Cuerpo del ValidateTaskBody
        private Result ValidateTaskBody(TaskModel task)
        {
            if (!String.IsNullOrWhiteSpace(task.Description) && task.DueTime > DateTime.Now)
            {
                return Result.Success($"Task: {task}");
            }
            return Result.Failure("Description or Duetime invalid");
        }
        
    }
}
