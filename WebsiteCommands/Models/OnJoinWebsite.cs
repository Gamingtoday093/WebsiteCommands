using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace WebsiteCommands.Models
{
    public class OnJoinWebsite
    {
        [XmlAttribute]
        public bool Enabled { get; set; }
        [XmlAttribute]
        public bool OnlyFirstJoin { get; set; }
        public string Message { get; set; }
        public string URL { get; set; }
    }
}
