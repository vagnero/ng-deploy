using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;
using TreinamentoEvoSystems.Data;
using TreinamentoEvoSystems.Repositorios;
using TreinamentoEvoSystems.Repositorios.Interfaces;

namespace TreinamentoEvoSystems
{
    public class Program
    {

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCors(options =>
            {
                options.AddPolicy("AllowAll",
                    builder =>
                    {
                        builder.AllowAnyOrigin()
                               .AllowAnyHeader()
                               .AllowAnyMethod();
                    });
            });

            // Outras configurações...
        }

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