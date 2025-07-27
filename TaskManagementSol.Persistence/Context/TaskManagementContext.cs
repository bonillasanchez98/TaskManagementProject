using Microsoft.EntityFrameworkCore;
using System.Data;
using TaskManagementSol.Domain.Model;

namespace TaskManagementSol.Persistence.Context
{
    public class TaskManagementContext : DbContext
    {
        public TaskManagementContext(DbContextOptions<TaskManagementContext> options)
            : base (options)
        {
            
        }

        public DbSet<TaskModel> Task_tb { get; set; }
    }
}
