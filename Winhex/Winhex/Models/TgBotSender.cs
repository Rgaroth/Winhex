using Winhex.Interfaces;

namespace Winhex.Models
{
    public class TgBotSender : ILogSender
    {
        public bool SendToServer(object destinationPoint, object data)
        {
            return true;
        }
    }
}