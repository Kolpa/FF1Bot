using FF1Bot;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace FF1BotTests
{
    [TestClass]
    public class BotTests
    {
        [TestMethod]
        public void Bot_Constructor_ShouldPass()
        {
            Bot bot = new Bot();
            Assert.IsNotNull(bot);
            Assert.IsFalse(bot.ShutdownRequested);
        }
    }
}