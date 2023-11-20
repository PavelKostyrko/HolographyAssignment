using AutoMapper;
using FootballManager.Data;
using FootballManager.DtoModels;
using FootballManager.Models;
using FootballManager.ViewModels.Services;
using FootballManager.ViewModels.Services.Interfaces;
using FootballManager.Views.Windows;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;

namespace FootballManager
{
    public class Program
    {
        [STAThread]
        public static void Main()
        {
            var _mapperConfiguration = new MapperConfiguration(cfg => {
                cfg.CreateMap<Player, PlayerDto>().ReverseMap();
                cfg.CreateMap<Team, TeamDto>().ReverseMap();
                cfg.CreateMap<Biography, BiographyDto>().ReverseMap();
            });

            var host = Host.CreateDefaultBuilder()
                .ConfigureServices(services =>
                {
                    services.AddSingleton<App>();
                    services.AddSingleton<MainWindow>();
                    services.AddTransient<ContextFM>();
                    services.AddTransient(mapperConfiguration => _mapperConfiguration.CreateMapper());
                    services.AddTransient<IPlayerService, PlayerService>();
                    services.AddTransient<ITeamService, TeamService>();
                    services.AddTransient<ITransferService, TransferService>();
                })
            .Build();

            var app = host.Services.GetService<App>();

            app?.Run();
        }
    }
}
