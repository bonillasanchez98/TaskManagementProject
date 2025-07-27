using TaskManagementSol.Domain.Model;

namespace TaskManagementSol.Application.Interface.Repos
{
    //Implementacion del contrato global CRUD + contrato especifico de la entidad TaskModel
    public interface ITaskRepo : IBaseRepo<TaskModel>
    {
    }
}
