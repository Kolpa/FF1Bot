namespace FF1Bot
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            MemoryReaderFactory memoryReaderFactory = new MemoryReaderFactory();
            Bot bot = new Bot(memoryReaderFactory);
            while (!bot.ShutdownRequested) bot.Run();
        }
    }
}