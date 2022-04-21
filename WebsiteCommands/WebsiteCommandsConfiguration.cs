using Rocket.API;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebsiteCommands.Models;
using System.Xml.Serialization;

namespace WebsiteCommands
{
    public class WebsiteCommandsConfiguration : IRocketPluginConfiguration
    {
        public string DefaultTextColour { get; set; }
        public bool ShouldLogToChat { get; set; }
        [XmlArrayItem("Command")]
        public ConfigurableWebsiteCommand[] Commands { get; set; }
        public void LoadDefaults()
        {
            DefaultTextColour = "green";
            ShouldLogToChat = true;
            Commands = new ConfigurableWebsiteCommand[]
            {
                new ConfigurableWebsiteCommand()
                {
                    Name = "Discord",
                    Message = "Join my Discord Server!",
                    URL = "https://discord.gg/WrrJf3f2Zt"
                },
                new ConfigurableWebsiteCommand()
                {
                    Name = "Rules",
                    Message = "Yeah your Rules Could go here too!",
                    URL = "https://steamcommunity.com/sharedfiles/filedetails/?id=2545218634"
                }
            };
        }
    }
}
