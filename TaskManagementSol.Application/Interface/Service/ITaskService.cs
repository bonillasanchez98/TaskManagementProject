using TaskManagementSol.Domain.Model;

namespace TaskManagementSol.Application.Interface.Task
{
    //Contratos especificos para la logica de negocio.
    public interface ITaskService
    {
        //Metodo para obtener una tarea segun su estatus.
        Task<Result> GetPendingTasks(); //Case: PendingTasks
        Task<Result> GetAllTasksAsync();
        Task<Result> GetTaskByIdAsync(int id);
        Task<Result> CreateTaskAsync(TaskModel task);
        Task<Result> UpdateTaskAsync(TaskModel task);
        Task<Result> DeleteTaskByIdAsync(int id);

        //Case: Crear tarea de alta prioridad
        Task<Result> CreateHighPriorityTaskAsync(string description);
    }
}
