using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Timers;

namespace FF1Bot
{
    class Program
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

        #region Dll Wrapper

        private static IntPtr Open(int id)
        {
            return OpenProcess(0x1F0FFF, true, id);
        }

        private static void Close(IntPtr handle)
        {
            CloseHandle(handle);
        }

        private static int Read(IntPtr process, IntPtr adress, int iBytesToRead = 2)
        {
            byte[] bytes = new byte[24];
            uint rw = 0;
            ReadProcessMemory(process, adress, bytes, (UIntPtr)iBytesToRead, ref rw);
            int result = BitConverter.ToInt32(bytes, 0);
            return result;
        }

        private static IntPtr GetAdress(IntPtr process, IntPtr pointer, uint offset)
        {
            byte[] bytes = new byte[24];
            uint rw = 0;
            ReadProcessMemory(process, pointer, bytes, (UIntPtr)sizeof(int), ref rw);
            uint pt = BitConverter.ToUInt32(bytes, 0);
            IntPtr var = (IntPtr)(pt + offset);
            return var;
        }

        #endregion

        #region Private Properties

        private static readonly Timer Timer = new Timer(100);
        private static Rectangle _bounds;
        private static IntPtr _activeWindowHandle;

        #endregion


        static void Main(string[] args)
        {
        }
    }
}
