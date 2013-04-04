using System;
using System.Diagnostics;
using System.Threading;
using WindowsInput;

namespace FF1Bot
{
    public abstract class GameStage
    {
        protected IMemoryReaderFactory _memoryReaderFactory;
        protected Bot _bot;

        protected GameStage(Bot bot, IMemoryReaderFactory memoryReaderFactory)
        {
            _bot = bot;
            _memoryReaderFactory = memoryReaderFactory;
            _dtLastMessagePrint = DateTime.Now;
        }

        public abstract void Run();

        protected bool DoesEmulatorHaveFocus()
        {
            IMemoryReader memoryReader = _memoryReaderFactory.Create();
            return memoryReader.GetFocusProcess().Id == _memoryReaderFactory.GetProcessID();
        }

        public void PrintFocusAlert() { PrintConsoleLine("Der Emulator hat keinen Fokus!"); }

        private string _sLastConsoleMessage = "";
        private DateTime _dtLastMessagePrint;

        protected void PrintConsoleLine(string sMessage)
        {
            if (_sLastConsoleMessage == sMessage && (DateTime.Now - _dtLastMessagePrint).TotalSeconds < 5) return;
            _sLastConsoleMessage = sMessage;
            _dtLastMessagePrint = DateTime.Now;
            Console.Out.WriteLine(sMessage);
        }
    }

    public class GameStageEmulatorNotLaunched : GameStage
    {
        private const string EMULATOR_EXECUTABLE_PATH = @"emulator\fceux.exe";
        private const string EMULATOR_EXECUTABLE_PATH_DEBUG = @"..\..\..\emulator\fceux.exe";

        public GameStageEmulatorNotLaunched(Bot bot, IMemoryReaderFactory memoryReaderFactory) : base(bot, memoryReaderFactory) { }

        public override void Run()
        {
            PrintConsoleLine("Starte Emulator.");
            StartEmulator();
            _bot.SetGameStage(new GameStageEmulatorLaunchedRomNotLoaded(_bot, _memoryReaderFactory));
        }

        private void StartEmulator()
        {
            Process p = Process.Start(Debugger.IsAttached ? EMULATOR_EXECUTABLE_PATH_DEBUG : EMULATOR_EXECUTABLE_PATH);
            if (p == null) throw new Exception("Emulator konnte nicht gestartet werden!");
            _memoryReaderFactory.SetProcessID(p.Id);
        }
    }

    public class GameStageEmulatorLaunchedRomNotLoaded : GameStage
    {
        public GameStageEmulatorLaunchedRomNotLoaded(Bot bot, IMemoryReaderFactory memoryReaderFactory) : base(bot, memoryReaderFactory) { }

        public override void Run()
        {
            if (!DoesEmulatorHaveFocus())
            {
                PrintFocusAlert();
                return;
            }
            Thread.Sleep(200);
            InputSimulator.SimulateKeyDown(VirtualKeyCode.LCONTROL);
            InputSimulator.SimulateKeyDown(VirtualKeyCode.VK_O);
            Thread.Sleep(200);
            InputSimulator.SimulateKeyUp(VirtualKeyCode.VK_O);
            InputSimulator.SimulateKeyUp(VirtualKeyCode.LCONTROL);
            Thread.Sleep(200);
            InputSimulator.SimulateTextEntry("Final Fantasy (U) [!].nes");
            Thread.Sleep(200);
            InputSimulator.SimulateKeyPress(VirtualKeyCode.RETURN);
            
            _bot.SetGameStage(new GameStagePrelude(_bot,_memoryReaderFactory));
        }
    }

    public class GameStagePrelude : GameStage
    {
        public GameStagePrelude(Bot bot, IMemoryReaderFactory memoryReaderFactory) : base(bot, memoryReaderFactory) { }

        public override void Run() { }
    }
}