using TaskManagementSol.Domain.Model;

namespace TaskManagementSol.Application.Interface.Task
{
    //Contratos especificos para la logica de negocio.
    public interface ITaskService
    {
        //Metodo para obtener una tarea segun su estatus.
        //Task<string> GetTaskByStatus(string status); //Deberia de devolver un Response.
        Task<Result> GetAllTasksAsync();
        Task<Result> GetTaskByIdAsync(int id);
        Task<Result> CreateTaskAsync(TaskModel task);
        Task<Result> UpdateTaskAsync(TaskModel task);
        Task<Result> DeleteTaskByIdAsync(int id);

    }
}
