using System;

namespace FF1Bot
{
    public interface IMemoryReader
    {
        void Close();
        int Read(IntPtr ptrBaseAdress, int iBytesToRead = 2);
        IntPtr GetAdress(IntPtr ptrBaseAdress, uint uiOffset);
    }
}