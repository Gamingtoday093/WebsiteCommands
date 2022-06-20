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
using Rocket.Unturned.Player;
using Rocket.Unturned;
using System.Threading;
using System.IO;
using Dir = System.IO.Directory;

namespace WebsiteCommands
{
    public class WebsiteCommands : RocketPlugin<WebsiteCommandsConfiguration>
    {
        public static WebsiteCommands Instance { get; private set; }
        public static WebsiteCommandsConfiguration Config { get; private set; }
        public static Color DefaultMessageColour { get; private set; }
        private HashSet<ulong> ExistingPlayers { get; set; }
        protected override void Load()
        {
            Instance = this;
            Config = Configuration.Instance;
            DefaultMessageColour = UnturnedChat.GetColorFromName(Config.DefaultTextColour, Color.green);

            if (Config.OnJoin.Enabled)
            {
                ExistingPlayers = GetExistingPlayers();
                U.Events.OnPlayerConnected += OnPlayerJoin;
            }

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
            if (Config.OnJoin.Enabled)
                U.Events.OnPlayerConnected -= OnPlayerJoin;

            Logger.Log($"{Name} has been Unloaded");
        }

        private HashSet<ulong> GetExistingPlayers()
        {
            // Players Directory
            string PlayersDirectory = Path.Combine(Path.GetDirectoryName(System.Environment.CurrentDirectory), "Players");

            if (!Dir.Exists(PlayersDirectory)) return null;
            HashSet<ulong> ExistingPlayers = new HashSet<ulong>();
            foreach (string player in Dir.GetDirectories(PlayersDirectory))
                ExistingPlayers.Add(ulong.Parse(player.Substring(0, 17)));
            return ExistingPlayers;
        }

        private void OnPlayerJoin(UnturnedPlayer player)
        {
            if (!Config.OnJoin.Enabled) return;
            if (Config.OnJoin.OnlyFirstJoin && (ExistingPlayers is null || ExistingPlayers.Contains(player.CSteamID.m_SteamID))) return;

            if (Config.OnJoin.OnlyFirstJoin) ExistingPlayers.Add(player.CSteamID.m_SteamID);

            player.Player.sendBrowserRequest(Config.OnJoin.Message, Config.OnJoin.URL);
            if (Config.ShouldLogToChat) UnturnedChat.Say(player, Instance.Translate("WCSuccess"), DefaultMessageColour);
        }

        public override TranslationList DefaultTranslations => new TranslationList()
        {
            { "WCSuccess", "Browser Request Sent!" }
        };
    }
}
