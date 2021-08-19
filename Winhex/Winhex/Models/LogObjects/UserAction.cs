using System;

namespace Winhex.Models
{
    [Serializable]
    public class UserAction
    {
        public int Id { get; set; }
        public DateTime ActionDateTime { get; set; }
        public string AppTitle { get; set; }
        public string TextLog { get; set; }

        public UserAction()
        {
            AppTitle = TextLog = "";
        }
    }
}