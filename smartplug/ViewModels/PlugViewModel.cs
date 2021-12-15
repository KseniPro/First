using System;
using Avalonia.Threading;
using OxyPlot;
using OxyPlot.Axes;
using OxyPlot.Series;
using ReactiveUI;
using SmartPlug.Common;
using Smartplug.Models;

namespace Smartplug.ViewModels
{
    public class PlugViewModel : ViewModelBase
    {
        private readonly Plug _plug;
        private PlotModel _plugPlotModel;

        public bool Toggle
        {
            get => _plug.State;
            set
            {
                _plug.State = value;
                this.RaisePropertyChanged(nameof(Toggle));
            }
        }

        public int PlugId
        {
            get => _plug.Id;
        }

        public PlotModel PlugPlotModel
        {
            get => _plugPlotModel;
            set => this.RaiseAndSetIfChanged(ref _plugPlotModel, value);
        }

        public PlugViewModel(Plug plug)
        {
            _plug = plug ?? throw new ArgumentNullException(nameof(plug));
            _plug.CurrentAcChanged.Subscribe(PlugAcChanged);
            var plug1PlotModel = new PlotModel
            {
                Background = OxyColors.Black,
                PlotAreaBorderColor = OxyColors.White
            };

            plug1PlotModel.Axes.Add(new LinearAxis
            {
                Position = AxisPosition.Left,
                Minimum = 0,
                Maximum = 100,
                AxislineColor = OxyColors.White,
                TextColor = OxyColors.White,
                TitleColor = OxyColors.White,
                TicklineColor = OxyColors.White,
                ExtraGridlineColor = OxyColors.White,
                MajorGridlineColor = OxyColors.White,
                MinorGridlineColor = OxyColors.White,
                MinorTicklineColor = OxyColors.White
            });
            plug1PlotModel.Series.Add(new LineSeries
            {
                LineStyle = LineStyle.Solid
            });
            PlugPlotModel = plug1PlotModel;
        }

        private void PlugAcChanged(object sender, EventArgs<double> args)
        {
            var lineSeries = (LineSeries)PlugPlotModel.Series[0];
            var pointsCount = lineSeries.Points.Count;
            lineSeries.Points.Add(new DataPoint(pointsCount, args.Value));
            this.RaisePropertyChanged(nameof(PlugPlotModel));
            Dispatcher.UIThread.InvokeAsync(() => PlugPlotModel.InvalidatePlot(true),
                DispatcherPriority.Background);
        }
    }
}