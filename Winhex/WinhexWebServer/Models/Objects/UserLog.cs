using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WinhexWebServer.Models
{
    public class UserLog
    {
        [Key]
        public int Id { get; set; }
        public string CompName { get; set; }
        public string CustomNote { get; set; }
        public virtual List<UserAction> Logs { get; set; }

        public UserLog()
        {
            Logs = new List<UserAction>();
            CustomNote = "";
        }
    }
}
