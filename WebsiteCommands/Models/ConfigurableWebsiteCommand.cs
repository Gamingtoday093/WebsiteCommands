using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace WebsiteCommands.Models
{
    public class ConfigurableWebsiteCommand
    {
        public string Name { get; set; }
        public string Message { get; set; }
        public string URL { get; set; }
    }
}
