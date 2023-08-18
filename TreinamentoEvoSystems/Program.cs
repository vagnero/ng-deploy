using Microsoft.AspNetCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using System.Text.Json.Serialization;
using TreinamentoEvoSystems.Data;
using TreinamentoEvoSystems.Repositorios;
using TreinamentoEvoSystems.Repositorios.Interfaces;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;


namespace TreinamentoEvoSystems
{
    public class Program
    {

      


        public static IWebHostBuilder CreateHostBuilder(string[] args) =>
    WebHost.CreateDefaultBuilder(args)
        .ConfigureServices((hostContext, services) =>
        {
            // ...
            // Configurar o Swagger
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "Nome da Sua API",
                    Version = "v1",
                    Description = "Descrição da sua API",
                    // Pode adicionar mais informações aqui
                });
            });
            // ...
        })
        .Configure(app =>
        {
            // ...
            // Habilitar o Swagger UI
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Nome da Sua API V1");
            });
            // ...
        });



        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
                        

            builder.Services.AddControllers().AddJsonOptions(options =>
            {
                options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
            });
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.ConfigureSwaggerGen(setup =>
            {
                setup.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
                {
                    Title = "Weather Forecasts",
                    Version = "v1"
                });
            });


            builder.Services.AddCors(p => p.AddPolicy("corsapp", builder =>
            {
                builder.WithOrigins("*").AllowAnyMethod().AllowAnyHeader();
            }));

            // enable single domain
            // multiple domain
            // any domain

            //Configuração para usar o SQL Server como banco de dados padrão. O nome foi criado em "ConnectionString dentro de appsettings.json".
            builder.Services.AddEntityFrameworkSqlServer()
                .AddDbContext<SistemaDBContext>(
                options => options.UseSqlServer(builder.Configuration.GetConnectionString("DataBase"))
                );

            //Este código permite que todas vez que for chamada a interface a classe a ser instanciada será o DepartamentoRepositorio. 
            builder.Services.AddScoped<IDepartamentoRepositorios, DepartamentoRepositorio>();
            builder.Services.AddScoped<IFuncionarioRepositorios, FuncionarioRepositorio>();

            var app = builder.Build();
            app.UseSwagger();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }
            



            app.UseHttpsRedirection();
            app.UseRouting();
            app.UseCors("corsapp");
            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }


}