using System.Threading.Tasks;
using Discord;
using Discord.Commands;
using Discord.WebSocket;

namespace DiscordBot.Modules
{
    [Name("Example")]
    public class ExampleModule : ModuleBase
    {
        [Command("echo")]
        [Summary("Echo your message.")]
        public async Task Echo([Remainder]string text)
            => await ReplyAsync(text);
    }
}