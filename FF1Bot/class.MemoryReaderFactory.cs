using System;

namespace FF1Bot
{
    public interface IMemoryReaderFactory
    {
        IMemoryReader Create();
        void SetProcessID(int iProcessID);
        int GetProcessID();
    }

    public class MemoryReaderFactory : IMemoryReaderFactory
    {
        private int _iProcessID;


        public IMemoryReader Create()
        {
            if (_iProcessID == 0) throw new Exception("Emulator-Prozess noch nicht gestartet oder eingebunden!");
            return new MemoryReader(_iProcessID);
        }

        public void SetProcessID(int iProcessID) { _iProcessID = iProcessID; }

        public int GetProcessID() { return _iProcessID; }
    }
}