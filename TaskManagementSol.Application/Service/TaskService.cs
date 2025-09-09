using TaskManagementSol.Application.Interface.Repos;
using TaskManagementSol.Application.Interface.Task;
using TaskManagementSol.Domain.Factory;
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
                var exist = await _repo.ExistAsync(t => t.Id == taskModel.Id); //Case: TaskIdExist
                if (exist.IsSuccess)
                {
                    response = Result.Failure($"ID [{taskModel.Id}] already exist");
                    return response;
                }

                ValidateTask validateTask = new ValidateTask(ValidateTaskBody); //Case: ValidandoTask
                if (!validateTask(taskModel).IsSuccess)
                {
                    response = Result.Failure($"Task invalid format");
                    return response;
                }

                TaskCreationNotify(taskModel); //Case: TaskCreationNotify

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
                var exist = await _repo.ExistAsync(t => t.Id == id); //Case: TaskIdExist
                if (exist.IsSuccess)
                {
                    response = Result.Success("Task returned successfully", exist.Data);

                    //Case: CalculateDaysLeft
                    Func<TaskModel, int> calculateDays = task => (task.DueTime.Day - DateTime.Now.Day);
                    int daysLeft = calculateDays(exist.Data);
                    Console.WriteLine($"Task: {id} \n" +
                                              $"Days left: {daysLeft}");

                    return response;
                }
                else
                {
                    response = Result.Failure($"Task ID [{id}] not found.");
                    return response;
                }
                
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

        //Case: Crear tarea de alta prioridad
        public async Task<Result> CreateHighPriorityTaskAsync(string description)
        {
            Result response = new Result();
            try
            {
                if (String.IsNullOrWhiteSpace(description))
                {
                    response = Result.Failure("The description cannot be null");
                }
                var taskHigPriority = TaskModelFactory.CreateHighPrioriyTask(description);
                return await _repo.CreateAsync(taskHigPriority);
            }
            catch (Exception e)
            {
                response = Result.Failure($"CreateHighPrioriyTask Error: {e.Message}");
            }
            return response;
        }


        //Case: Obtener todas las tareas con estado pendiente
        public async Task<Result> GetPendingTasks()
        {
            Result response = new Result();
            try
            {
                var taskResult = await _repo.GetAllAsync(t => t.Status == "Pending");
                if (taskResult.IsSuccess)
                {
                    response = Result.Success("Tasks pending", taskResult.Data);
                }
                else
                {
                    response = Result.Failure("Failure try getting pending Tasks.");
                }
            }
            catch (Exception e)
            {
                response = Result.Failure($"GetPendingTasks Error: {e.Message}");
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

        //Delegado para notificar la creacion de una Task
        private Action<TaskModel> TaskCreationNotify = tcn =>
                Console.Write($"Task created: {tcn.Description}\n" +
                                   $"DueTime: {tcn.DueTime}");
    }
}
