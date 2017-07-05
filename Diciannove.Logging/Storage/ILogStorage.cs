namespace Diciannove.Logging.Storage
{
    public interface ILogStorage
    {
        void Save(LogMessage message);
    }
}