using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Avalonia.Threading;
using OxyPlot;
using OxyPlot.Axes;
using OxyPlot.Series;
using SmartPlug.Services;
using ReactiveUI;
using SmartPlug.Common;
using Smartplug.Models;

namespace Smartplug.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {
        private readonly ISerialPortService _serialPortService = new FakeSerialPortService();
        private readonly IParseArduinoDataService _parseArduinoDataService = new ParseArduinoDataService();
        private string? _portName;
        private readonly ObservableCollection<PlugViewModel> _plugViewModels = new();
        private const int _plugsCount = 5;

        public ObservableCollection<PlugViewModel> PlugViewModels => _plugViewModels;
        
        public string? PortName
        {
            get => _portName;
            set => this.RaiseAndSetIfChanged(ref _portName, value);
        }

        public IList<string> PortNames => _serialPortService.GetAvailablePortNames();

        public MainWindowViewModel()
        {
            for (var i = 0; i < _plugsCount; i++)
            {
                _plugViewModels.Add(new PlugViewModel(new Plug(i, _serialPortService, _parseArduinoDataService)));
            }
        }
    }
}
