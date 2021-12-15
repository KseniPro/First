using System.Collections.Generic;
using SmartPlug.Common;

namespace SmartPlug.Services
{
    public interface ISerialPortService
    {
        void Toggle(int plugNumber, bool state);
        void SetPortName(string portName);
        IList<string> GetAvailablePortNames();
        IWeakEvent<EventArgs<string>> DataReceived { get; }
    }
}