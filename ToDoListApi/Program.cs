
using Microsoft.OpenApi.Models;
using System.Reflection;
using ToDoListApi.Repositories;

namespace ToDoListApi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();

            var info = new OpenApiInfo()
            {
                Title = "ToDoList API Documentation",
                Version = "v1",
                Description = "Simple API Project for a To-Do List",
                Contact = new OpenApiContact()
                {
                    Name = "Ayca Yildirim",
                    Email = "eaycayildirim@gmail.com",
                }

            };
            builder.Services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", info);
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                c.IncludeXmlComments(xmlPath);
            });

            builder.Services.AddSingleton<IToDoListRepository, ToDoListRepository>();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI(c =>
                {
                    c.RoutePrefix = "swagger";
                    c.SwaggerEndpoint(url: "/swagger/v1/swagger.json", name: "ToDoList API");
                });
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
