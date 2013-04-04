using System;
using System.Drawing;
using System.Timers;

namespace FF1Bot
{
    public class Bot
    {
        private const string EmulatorProcessName = "fceux";

        #region Private Properties

        private static readonly Timer Timer = new Timer(100);
        private static Rectangle _bounds;
        private static IntPtr _activeWindowHandle;

        #endregion

        #region Public Properties

        public bool ShutdownRequested { get; private set; }

        #endregion

        #region Constructors

        public Bot() { ShutdownRequested = false; }

        #endregion

        #region Public Methods

        public void Run() { }

        #endregion
    }
}