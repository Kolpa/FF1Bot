namespace FF1Bot
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            Bot bot = new Bot();
            while (!bot.ShutdownRequested)
            {
                bot.Run();
            }
        }
    }
}