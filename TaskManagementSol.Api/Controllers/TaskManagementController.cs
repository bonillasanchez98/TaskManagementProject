using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TaskManagementSol.Application;
using TaskManagementSol.Application.Interface.Task;
using TaskManagementSol.Domain.Model;

namespace TaskManagementSol.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TaskManagementController : ControllerBase
    {
        private readonly ITaskService _service;

        public TaskManagementController(ITaskService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllTaskAsync()
        {
            var result = await _service.GetAllAsync();
            if(result == null)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetTaskByIdAsync(int id)
        {
            var result = await _service.GetByIdAsync(id);
            if (result is null)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> AddTaskAsync(TaskModel taskModel)
        {
            var result = await _service.CreateAsync(taskModel);
            if (!result.IsSuccess)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateTaskAsync(TaskModel taskModel)
        {
            var result = await _service.UpdateAsync(taskModel);
            if (!result.IsSuccess)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTaskByIdAsync(int id)
        {
            var result = await _service.DeleteAsync(id);
            if (!result.IsSuccess)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }
    }
}
