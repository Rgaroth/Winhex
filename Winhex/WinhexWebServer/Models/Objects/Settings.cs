using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Winhex.WebServer.Models.Objects
{
    public class Settings
    {
        [Key]
        public int Id { get; set; }
        public string ParameterName { get; set; }
        public string ParameterValue { get; set; }

        public Settings()
        {
            ParameterName = ParameterValue = "";
        }
    }
}
