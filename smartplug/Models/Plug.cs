using System;
using SmartPlug.Common;
using SmartPlug.Services;

namespace Smartplug.Models
{
    public class Plug: IPlug
    {
        private readonly IParseArduinoDataService _parseArduinoDataService;
        private readonly WeakEvent<EventArgs<double>> _currentAcChanged = new();
        private double _currentAc;

        public int Id { get; set; }
        public bool State { get; set; }

        public double CurrentAc
        {
            get => _currentAc;
            private set
            {
                _currentAc = value;
                _currentAcChanged.Raise(this, new EventArgs<double>(value));
            }
        }
        public IWeakEvent<EventArgs<double>> CurrentAcChanged => _currentAcChanged;

        public Plug(
            int id,
            ISerialPortService serialPortService,
            IParseArduinoDataService parseArduinoDataService)
        {
            Id = id;
            ISerialPortService serialPortService1 = serialPortService ?? throw new ArgumentNullException(nameof(serialPortService));
            _parseArduinoDataService = parseArduinoDataService ?? throw new ArgumentNullException(nameof(parseArduinoDataService));
            serialPortService1.DataReceived.Subscribe(OnDataReceived);
        }

        private void OnDataReceived(object sender, EventArgs<string> args)
        {
            var currentAc = _parseArduinoDataService.GetToogleAc(Id, args.Value);
            if (currentAc.HasValue)
                CurrentAc = currentAc.Value;
        }

    }
}