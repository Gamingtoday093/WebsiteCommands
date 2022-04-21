using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Logger = Rocket.Core.Logging.Logger;
using Rocket.Core.Plugins;
using Rocket.Core;
using WebsiteCommands.Models;
using Rocket.API.Collections;
using UnityEngine;
using Rocket.Unturned.Chat;
using SDG.Unturned;

namespace WebsiteCommands
{
    public class WebsiteCommands : RocketPlugin<WebsiteCommandsConfiguration>
    {
        public static WebsiteCommands Instance { get; private set; }
        public static WebsiteCommandsConfiguration Config { get; private set; }
        public static Color DefaultMessageColour { get; private set; }
        protected override void Load()
        {
            Instance = this;
            Config = Configuration.Instance;
            DefaultMessageColour = UnturnedChat.GetColorFromName(Config.DefaultTextColour, Color.green);

            foreach (ConfigurableWebsiteCommand command in Config.Commands)
            {
                if (R.Commands.Commands.Any(c => c.Name.ToLower() == command.Name.ToLower()))
                {
                    Logger.LogError($"[{Name}] Can't Register Duplicate Command {command.Name}");
                    continue;
                }
                R.Commands.Register(new WebsiteCommand(command));
            }

            Logger.Log($"{Name} {Assembly.GetName().Version} by Gamingtoday093 has been Loaded");
        }

        protected override void Unload()
        {
            Logger.Log($"{Name} has been Unloaded");
        }

        public override TranslationList DefaultTranslations => new TranslationList()
        {
            { "WCSuccess", "Browser Request Sent!" }
        };
    }
}
