using System;
using System.IO.Ports;
using System.Collections.Generic;
using SmartPlug.Common;

namespace SmartPlug.Services
{
    public class SerialPortService: ISerialPortService
    {
        private readonly SerialPort _serialPort = new()
        {
            Parity = Parity.None,
            DataBits = 8,
            BaudRate = 9600,
            StopBits = StopBits.One
        };

        private readonly WeakEvent<EventArgs<string>> _dataReceived = new();
        
        IWeakEvent<EventArgs<string>> ISerialPortService.DataReceived => _dataReceived;

        public SerialPortService()
        {
            _serialPort.DataReceived += OnDataReceived;
        }

        public void Toggle(int plugNumber, bool state)
        {
        }

        public void SetPortName(string portName)
        {
        }

        public IList<string> GetAvailablePortNames()
        {
            return new List<string>{"todo", "todo2"};
            //return SerialPort.GetPortNames();
        }


        private void OnDataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            var inputstring = _serialPort.ReadExisting();
            _dataReceived.Raise(this, new EventArgs<string>(inputstring));
        }
    }
}