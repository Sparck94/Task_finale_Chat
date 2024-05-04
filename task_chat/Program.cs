
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using MongoDB.Driver;
using System.Text;
using task_chat.Models;
using task_chat.Repos;
using task_chat.Services;

namespace task_chat
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.


            builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = "YourIssuer",
                    ValidAudience = "YourAudience",
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("your_super_secret_key_your_super_secret_key"))
                };
            });
            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddDbContext<TaskChatContext>(options => options.UseSqlServer(
                builder.Configuration.GetConnectionString("DefaultConnection")
                ));


            builder.Services.AddSingleton<IMongoClient>(new MongoClient("mongodb://root:toor@localhost:27017/"));
            builder.Services.AddScoped<IMongoDatabase>(provider =>
            {
                
                var client = provider.GetService<IMongoClient>();
                return client.GetDatabase("ChatDB");
            });

            builder.Services.AddScoped<UtenteRepo>();
            builder.Services.AddScoped<UtenteService>();
            builder.Services.AddScoped<StanzaRepo>();
            builder.Services.AddScoped<StanzaService>();
            builder.Services.AddScoped<MessaggioRepo>();
            builder.Services.AddScoped<MessaggioService>();





            var app = builder.Build();

            app.UseCors(builder =>
                builder
                .WithOrigins("*")
                .AllowAnyMethod()
                .AllowAnyHeader());

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthentication();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
