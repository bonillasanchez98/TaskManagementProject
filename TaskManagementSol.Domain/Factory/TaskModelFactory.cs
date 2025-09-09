using TaskManagementSol.Domain.Model;

namespace TaskManagementSol.Domain.Factory
{
    public static class TaskModelFactory
    {
        public static TaskModel CreateHighPrioriyTask(string description)
        {
            return new TaskModel
            {
                Description = description,
                DueTime = DateTime.Now.AddDays(1),
                Status = "Pending",
                AditionalData = "High Priority"
            };
        }
    }
}
