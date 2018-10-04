using System;
using System.Reflection;
using System.Threading.Tasks;
using Discord;
using Discord.Commands;
using Discord.WebSocket;
using Microsoft.Extensions.DependencyInjection;

namespace DiscordBot
{
    class Program
    {
        static async Task Main(string[] args)
        {
            await RunBotAsync();
        }

        static async Task RunBotAsync()
        {
            var services = new ServiceCollection()
                .AddSingleton(new DiscordSocketClient())
                .AddSingleton(new CommandService())
                .AddSingleton(AppSettings.Current)
                .AddSingleton<StartupService>()
                .AddSingleton<CommandHandlingService>();

            var provider = services.BuildServiceProvider();
            provider.GetRequiredService<CommandHandlingService>();

            await provider.GetRequiredService<StartupService>().StartAsync();

            await Task.Delay(-1);
        }
    }
}
