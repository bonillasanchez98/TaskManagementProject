using Moq;
using TaskManagementSol.Application;
using TaskManagementSol.Application.Interface.Repos;
using TaskManagementSol.Application.Service;

namespace TaskManagementSol.Test
{
    public class TaskModelTest
    {
        private readonly Mock<ITaskRepo> _iTaskRepoMock;
        private readonly TaskService _taskService;

        public TaskModelTest()
        {
            _iTaskRepoMock = new Mock<ITaskRepo>();
            _taskService = new TaskService(_iTaskRepoMock.Object);
        }

        //Caso: Obtener Task por su id y retornar not null
        [Fact]
        public async Task GetTaskByIdAsync_TaskExist_ReturnNotNull()
        {
            //Arrage
            var taskTest = new Domain.Model.TaskModel
            {
                Id = 1,
                Description = "Test Task #1",
                DueTime = DateTime.Now,
                Status = "Completed",
                AditionalData = "Description modified"
            };
            _iTaskRepoMock.Setup(t => t.GetByIdAsync(1)).ReturnsAsync(new Result(true, "Success", taskTest));

            //Act
            var result = await _taskService.GetTaskByIdAsync(1);
            //Assert
            Assert.NotNull(result);
        }

        //Caso: Obtener todas las tareas con estado Pendiente
        [Fact]
        public async Task GetAllTaskAsyc_PendingTasksFound_ReturnTasks()
        {
            //Arrage
            var tasksTest = new List<Domain.Model.TaskModel>
            {
                new Domain.Model.TaskModel {Id = 1, Description = "Test Task #1", DueTime = DateTime.Now.AddDays(5), 
                    Status = "Pending", AditionalData = "N/A" },
                new Domain.Model.TaskModel {Id = 1002, Description = "Test Task #2", DueTime = DateTime.Now.AddDays(5), 
                    Status = "Pending", AditionalData = "N/A" }
            };

            _iTaskRepoMock.Setup(t => t.GetAllAsync(f => f.Status == "Pending"))
                .ReturnsAsync(new Result(true, "Succes", tasksTest));

            //Act
            var result = await _taskService.GetPendingTasks();

            //Assert
            Assert.True(result.IsSuccess);
        }

        //Caso: Crear una tarea con una fecha anterior al dia de creacion
        [Fact]
        public async Task CreateTaskAsync_WithInvalidDueTime_ReturnIsSuccessFalse()
        {
            //Arrage
            var taskTest = new Domain.Model.TaskModel { 
                Id = 1, Description = "Task Test #1", 
                DueTime = DateTime.Now.AddDays(-1), Status = "Pending",
                AditionalData = "CreateTaskAsync_WithInvalidDueTime_ReturnIsSuccessFalse"
            };

            //Act
            var result = await _taskService.CreateTaskAsync(taskTest);

            //Assert
            Assert.False(result.IsSuccess);
        }


    }
}