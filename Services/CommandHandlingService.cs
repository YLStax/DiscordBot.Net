using System;
using System.Threading.Tasks;
using Discord.Commands;
using Discord.WebSocket;
using Microsoft.Extensions.DependencyInjection;

namespace DiscordBot
{
    public class CommandHandlingService
    {
        private readonly IServiceProvider services;
        private readonly DiscordSocketClient client;
        private readonly CommandService commands;
        private readonly AppSettings settings;

        public CommandHandlingService(IServiceProvider services, DiscordSocketClient client, CommandService commands, AppSettings settings)
        {
            this.services = services;
            this.client = client;
            this.commands = commands;
            this.settings = settings;

            client.MessageReceived += OnMessageReceivedAsync;
        }

        private async Task OnMessageReceivedAsync(SocketMessage rawMessage)
        {
            if (!(rawMessage is SocketUserMessage message)) return;
            if (message.Author.Id == client.CurrentUser.Id) return;

            int argPos = 0;
            if (!(message.HasCharPrefix(settings.Prefix, ref argPos) || message.HasMentionPrefix(client.CurrentUser, ref argPos))) return;

            var context = new CommandContext(client, message);
            var result = await commands.ExecuteAsync(context, argPos, services);

            if (!result.IsSuccess) await context.Channel.SendMessageAsync(result.ErrorReason);
        }
    }
}