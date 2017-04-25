using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Swsu.Lignis.Logger.Server.Psdb.Entities;

namespace RadioInformationCenter.Model
{
   public class RicEvent
    {
        public DateTime DateTime { get; set; }
        public Software SoftwareItem { get; set; }
        public string Message { get; set; }
    }
}
