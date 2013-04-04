using FF1Bot;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FF1BotTests
{
    [TestClass]
    public class BotTests
    {
        private IMemoryReaderFactory _memoryReaderFactory;
        
        [TestInitialize]
        public void BeforeTests()
        {
            _memoryReaderFactory = new MemoryReaderFactory();
        }
        
        [TestMethod]
        public void Bot_Constructor_ShouldPass()
        {
            Bot bot = new Bot(_memoryReaderFactory);
            Assert.IsNotNull(bot);
            Assert.IsFalse(bot.ShutdownRequested);
        }
    }
}