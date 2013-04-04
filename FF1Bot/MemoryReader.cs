using System;
using System.Diagnostics;
using System.Runtime.InteropServices;

namespace FF1Bot
{
    public interface IMemoryReader
    {
        void Close();
        int Read(IntPtr ptrBaseAdress, int iBytesToRead = 2);
        IntPtr GetAdress(IntPtr ptrBaseAdress, uint uiOffset);
        Process GetFocusProcess();
    }

    public class MemoryReader : IMemoryReader
    {
        #region User32.dll Importe

        [DllImport("kernel32.dll")]
        public static extern IntPtr OpenProcess(int dwDesiredAccess, bool bInheritHandle, int dwProcessId);

        [DllImport("kernel32.dll")]
        private static extern bool ReadProcessMemory(IntPtr hProcess, IntPtr lpBaseAddress, byte[] lpBuffer, UIntPtr nSize, ref uint lpNumberOfBytesWritten);

        [DllImport("kernel32.dll")]
        private static extern bool CloseHandle(IntPtr hObject);

        [DllImport("user32.dll", SetLastError = true)]
        static extern uint GetWindowThreadProcessId(IntPtr hWnd, out int lpdwProcessId);

        [DllImport("user32.dll")]
        private static extern IntPtr GetForegroundWindow();

        #endregion

        #region Private Properties

        private readonly IntPtr _handle;

        #endregion

        #region Constructors

        public MemoryReader(int iProcessID) { _handle = OpenProcess(0x1F0FFF, true, iProcessID); }

        #endregion

        #region Destructor

        ~MemoryReader() { Close(); }

        #endregion

        #region Public Methods

        public void Close() { CloseHandle(_handle); }

        public int Read(IntPtr ptrBaseAdress, int iBytesToRead = 2)
        {
            byte[] baBuffer = new byte[24];
            uint uiBytesWritten = 0;
            ReadProcessMemory(_handle, ptrBaseAdress, baBuffer, (UIntPtr) iBytesToRead, ref uiBytesWritten);
            int iResult = BitConverter.ToInt32(baBuffer, 0);
            return iResult;
        }

        public IntPtr GetAdress(IntPtr ptrBaseAdress, uint uiOffset)
        {
            byte[] baBuffer = new byte[24];
            uint uiBytesWritten = 0;
            ReadProcessMemory(_handle, ptrBaseAdress, baBuffer, (UIntPtr) sizeof (int), ref uiBytesWritten);
            uint uiFoundAdress = BitConverter.ToUInt32(baBuffer, 0);
            IntPtr ptrTargetAdress = (IntPtr) (uiFoundAdress + uiOffset);
            return ptrTargetAdress;
        }

        public Process GetFocusProcess()
        {
            int iFocusWindowProcessID;
            GetWindowThreadProcessId(GetForegroundWindow(), out iFocusWindowProcessID);
            Process focusProcess = Process.GetProcessById(iFocusWindowProcessID);
            return focusProcess;
        }

        #endregion
    }
}