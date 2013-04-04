using System;
using System.Drawing;
using System.Runtime.InteropServices;

namespace FF1Bot
{
    public class Memory
    {
        #region User32.dll Importe

        [DllImport("kernel32.dll")]
        public static extern IntPtr OpenProcess(int dwDesiredAccess, bool bInheritHandle, int dwProcessId);
        
        [DllImport("kernel32.dll")]
        private static extern bool ReadProcessMemory(IntPtr hProcess, IntPtr lpBaseAddress, byte[] lpBuffer, UIntPtr nSize, ref uint lpNumberOfBytesWritten);

        [DllImport("kernel32.dll")]
        private static extern bool CloseHandle(IntPtr hObject);

        #endregion

        private static IntPtr _handle;

        public Memory(int id)
        {
            _handle = OpenProcess(0x1F0FFF, true, id);
        }

        public static void Close()
        {
            CloseHandle(_handle);
        }

        public static int Read(IntPtr adress, int iBytesToRead = 2)
        {
            var bytes = new byte[24];
            uint rw = 0;
            ReadProcessMemory(_handle, adress, bytes, (UIntPtr) iBytesToRead, ref rw);
            var result = BitConverter.ToInt32(bytes, 0);
            return result;
        }

        private static IntPtr GetAdress(IntPtr pointer, uint offset)
        {
            var bytes = new byte[24];
            uint rw = 0;
            ReadProcessMemory(_handle, pointer, bytes, (UIntPtr) sizeof (int), ref rw);
            var pt = BitConverter.ToUInt32(bytes, 0);
            var var = (IntPtr) (pt + offset);
            return var;
        }
    }
}
