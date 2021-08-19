using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WinhexWebServer.Models
{
    public class UserAction
    {
        [Key]
        public int Id { get; set; }
        public DateTime ActionDateTime { get; set; }
        public string AppTitle { get; set; }
        public string TextLog { get; set; }
        [ForeignKey("LogId")]
        public int LogId { get; set; }
        public UserLog UserLog { get; set; }
    }
}