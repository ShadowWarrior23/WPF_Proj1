using Microsoft.EntityFrameworkCore;
using WPF_Proj1;

namespace WebApplication1
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services
            builder.Services.AddControllers();

            builder.Services.AddCors(options =>
            {
                options.AddPolicy("ReactDev", policy =>
                {
                    policy
                        .WithOrigins("http://127.0.0.1:5173", "http://localhost:5173")
                        .AllowAnyHeader()
                        .AllowAnyMethod();
                });
            });

            var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

            var dbPath = Path.GetFullPath(Path.Combine(builder.Environment.ContentRootPath, "..", "WPF_Proj1", "PixaFork.db"));
            Console.WriteLine("DB PATH: " + dbPath);

            builder.Services.AddDbContext<AppDbContext>(options =>
                options.UseSqlite($"Data Source={dbPath}"));

            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            app.UseCors("ReactDev");

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
