namespace FF1Bot
{
    public interface IMemoryReaderFactory
    {
    }

    public class MemoryReaderFactory : IMemoryReaderFactory
    {
        public static IMemoryReader Create(int iProcessID) { return new MemoryReader(iProcessID); }
    }
}