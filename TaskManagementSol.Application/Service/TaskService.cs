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

        public async Task<(bool IsSuccess, string Message)> CreateAsync(TaskModel taskModel)
        {
            var response = new Response<TaskModel>();
            try
            {
                var result = await _repo.CreateAsync(taskModel);
                response.Message = result.Message;
                response.Success = result.IsSuccess;
                return (true, $"Task created successfully.");
            }
            catch (Exception e)
            {
                response.Errors.Add($"CreateAsync Error: {e.Message}");
                return (false, e.Message);
            }
        }


        public async Task<IEnumerable<TaskModel>> GetAllAsync()
        {
            var response = new Response<TaskModel>();
            try
            {
                response.DataList = await _repo.GetAllAsync();
                response.Success = true;
            }
            catch (Exception e)
            {
                response.Errors.Add($"GetAllAsync Error: {e.Message}");
            }
            return response.DataList;
        }

        public async Task<TaskModel> GetByIdAsync(int id)
        {
            var response = new Response<TaskModel>();
            try
            {
                var IdExist = await _repo.GetByIdAsync(id);
                if(IdExist != null)
                {
                    response.SingleData= IdExist;
                    response.Success = true;
                }
                else
                {
                    response.Success = false;
                    response.Message = $"Task {id} not found.";
                }
            }
            catch (Exception e)
            {
                response.Errors.Add($"GetByIdAsync Error: {e.Message}");
            }

            return response.SingleData;
        }

        public async Task<(bool IsSuccess, string Message)> UpdateAsync(TaskModel taskModel)
        {
            var response = new Response<TaskModel>();
            try
            {
                var result = await _repo.UpdateAsync(taskModel);
                response.Message = result.Message;
                response.Success = result.IsSuccess;
                return (true, $"Task updated successfully.");
            }
            catch (Exception e)
            {
                response.Errors.Add($"UpdateAsync Error: {e.Message}");
                return (false, e.Message);
            }
        }
        public async Task<(bool IsSuccess, string Message)> DeleteAsync(int id)
        {
            var response = new Response<TaskModel>();
            try
            {
                var result = await _repo.DeleteAsync(id);
                response.Message = result.Message;
                response.Success = result.IsSuccess;
                return (true, $"Task deleted successfully.");
            }
            catch (Exception e)
            {
                response.Errors.Add($"DeleteAsync Error: {e.Message}");
                return (false, e.Message);
            }
        }
    }
}
