using Lib.AspNetCore.ServerSentEvents;
using Tic_Tac_Toe_Web_API.Managers;
using Tic_Tac_Toe_Web_API.Managers.Interfaces;
using Tic_Tac_Toe_Web_API.Models.Mappers;
using Tic_Tac_Toe_Web_API.Respository;
using Tic_Tac_Toe_Web_API.Respository.Interfaces;

namespace Tic_Tac_Toe_Web_API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddCors(options =>
            {
                options.AddDefaultPolicy(
                    policy =>
                    {
                        policy.WithOrigins("http://localhost:4200")
                            .AllowAnyMethod()
                            .AllowAnyHeader();
                    });
            });

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            
            builder.Services.AddSingleton<AppDbContext>();
            builder.Services.AddSingleton<IPlayersRepository, PlayersRepository>();
            builder.Services.AddSingleton<IResultsRepository, ResultsRepository>();

            builder.Services.AddScoped<IGameManager, GameManager>();
            builder.Services.AddScoped<IPlayerManager, PlayerManager>();
            builder.Services.AddScoped<AllGamesMapper>();
            builder.Services.AddScoped<TicTacToeGameMapper>();
            builder.Services.AddScoped<PlayerMapper>();
            builder.Services.AddScoped<RotaGameMapper>();
            

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseCors();
            app.UseAuthorization();


            app.MapControllers();
            app.Run();
        }
    }
}