using Microsoft.EntityFrameworkCore;
using TaskManagementSol.Application.Interface.Repos;
using TaskManagementSol.Application.Interface.Task;
using TaskManagementSol.Application.Service;
using TaskManagementSol.Persistence.Context;
using TaskManagementSol.Persistence.Repositories.TaskRepo;

namespace TaskManagementSol.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddDbContext<TaskManagementContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("TaskManagerConn"))
            );

            builder.Services.AddScoped<ITaskRepo, TaskRepo>();

            builder.Services.AddScoped<ITaskService, TaskService>();

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            using (var scope = app.Services.CreateScope())
            {
                var context = scope.ServiceProvider.GetRequiredService<TaskManagementContext>();
                context.Database.Migrate();
            }

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
