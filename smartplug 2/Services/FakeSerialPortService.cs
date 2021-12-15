using System;
using System.Collections.Generic;
using System.Timers;
using SmartPlug.Common;

namespace SmartPlug.Services
{
    public class FakeSerialPortService: ISerialPortService
    {
        private const int MaxAc = 100;
        private readonly WeakEvent<EventArgs<string>> _dataReceived = new();
        private readonly Timer _timer = new(1000);
        private readonly Random _random = new();

        public IWeakEvent<EventArgs<string>> DataReceived => _dataReceived;

        public FakeSerialPortService()
        {
            _timer.Elapsed += TimerOnElapsed;
            _timer.Enabled = true;
            _timer.Start();
        }

        private void TimerOnElapsed(object sender, ElapsedEventArgs e)
        {
            var arduinoString = string.Empty;
            for (var i = 0; i < 5; i++)
            {
                var ac = _random.Next(MaxAc);
                arduinoString += $"t{i}={ac}\r\nw={ac}\r\n";
            }
            _dataReceived.Raise(this, new EventArgs<string>(arduinoString));            
        }

        public void Toggle(int plugNumber, bool state)
        {
            //throw new NotImplementedException();
        }

        public void SetPortName(string portName)
        {
        }

        public IList<string> GetAvailablePortNames()
        {
            return new List<string>
            {
                "COM1",
                "COM2"
            };
        }

    }
}