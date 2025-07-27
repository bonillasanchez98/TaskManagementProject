using TaskManagementSol.Domain.Model;

namespace TaskManagementSol.Application.Interface.Task
{
    //Contratos especificos para la logica de negocio.
    public interface ITaskService : IBaseRepo<TaskModel>
    {
        //Metodo para obtener una tarea segun su estatus.
        //Task<string> GetTaskByStatus(string status); //Deberia de devolver un Response.
        
    }
}
