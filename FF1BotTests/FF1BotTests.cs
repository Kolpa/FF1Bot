using FF1Bot;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FF1BotTests
{
    [TestClass]
    public class FF1BotTests
    {
        [TestMethod]
        public void TestMethod1()
        {
            Bot bot = new Bot();
            Assert.IsNotNull(bot);
            Assert.IsFalse(bot.ShutdownRequested);
        }
    }
}