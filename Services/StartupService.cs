using Discord;
using Discord.Commands;
using Discord.WebSocket;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Reflection;
using System.Threading.Tasks;

namespace DiscordBot
{
    public class StartupService
    {
        private readonly DiscordSocketClient client;
        private readonly CommandService commands;
        private readonly AppSettings settings;

        public StartupService(IServiceProvider services)
        {
            this.client = services.GetRequiredService<DiscordSocketClient>();
            this.commands = services.GetRequiredService<CommandService>();
            this.settings = services.GetRequiredService<AppSettings>();
        }

        public async Task StartAsync()
        {
            var token = settings.Token;
            if (string.IsNullOrWhiteSpace(token)) throw new Exception("Please enter your bot's token into the `appsettings.json` file found in the applications root directory.");

            await client.LoginAsync(TokenType.Bot, token);
            await client.StartAsync();

            await commands.AddModulesAsync(Assembly.GetEntryAssembly());
        }
    }
}