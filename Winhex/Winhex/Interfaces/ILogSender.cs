namespace Winhex.Interfaces
{
    public interface ILogSender
    {
        bool SendToServer(object destinationPoint, object data);
    }
}