using Rocket.API;
using Rocket.Unturned.Chat;
using Rocket.Unturned.Player;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebsiteCommands.Models
{
    public class WebsiteCommand : IRocketCommand
    {
        public AllowedCaller AllowedCaller => AllowedCaller.Player;

        public string Name => WebsiteCommandConfig.Name;

        public string Help => "";

        public string Syntax => "";

        public List<string> Aliases => new List<string>();

        public List<string> Permissions => new List<string>() { "WebsiteCommands.Execute" };
        
        public ConfigurableWebsiteCommand WebsiteCommandConfig { get; set; }

        public WebsiteCommand(ConfigurableWebsiteCommand websiteCommand)
        {
            WebsiteCommandConfig = websiteCommand;
        }

        public void Execute(IRocketPlayer caller, string[] command)
        {
            UnturnedPlayer player = (UnturnedPlayer)caller;
            player.Player.sendBrowserRequest(WebsiteCommandConfig.Message, WebsiteCommandConfig.URL);
            if (WebsiteCommands.Config.ShouldLogToChat) UnturnedChat.Say(caller, WebsiteCommands.Instance.Translate("WCSuccess"), WebsiteCommands.DefaultMessageColour);
        }
    }
}
