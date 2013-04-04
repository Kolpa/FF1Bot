using System;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Timers;

namespace FF1Bot
{
    public class Bot
    {
        private const string EmulatorProcessName = "fceux";

        #region User32.dll Importe

        [DllImport("user32.dll")]
        private static extern IntPtr GetForegroundWindow();

        [DllImport("user32.dll", SetLastError = true)]
        private static extern uint GetWindowThreadProcessId(IntPtr hWnd, out int lpdwProcessId);

        [DllImport("user32.dll")]
        private static extern bool GetWindowRect(IntPtr hWnd, out Rectangle rect);

        [DllImport("kernel32.dll")]
        public static extern IntPtr OpenProcess(int dwDesiredAccess, bool bInheritHandle, int dwProcessId);

        [DllImport("kernel32.dll")]
        private static extern bool ReadProcessMemory(IntPtr hProcess, IntPtr lpBaseAddress, byte[] lpBuffer, UIntPtr nSize, ref uint lpNumberOfBytesWritten);

        [DllImport("kernel32.dll")]
        private static extern bool CloseHandle(IntPtr hObject);

        #endregion

        #region Private Properties

        private static readonly Timer Timer = new Timer(100);
        private static Rectangle _bounds;
        private static IntPtr _activeWindowHandle;

        #endregion

        #region Public Properties

        public bool ShutdownRequested { get; private set; }

        #endregion

        #region Constructors

        public Bot()
        {
            ShutdownRequested = false;
        }

        #endregion

        #region Public Methods

        public void Run()
        {
        }

        #endregion
    }
}