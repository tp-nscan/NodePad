using System;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Threading;
using NodePad.Common;
using NodePad.Model;
using NodePad.ViewModel.Common;
using TT;

namespace NodePad.ViewModel.Pages.CPU
{
    public class RingPageVm : BindableBase
    {
        public RingPageVm()
        {
            Grid2DVm = new Grid2DVm<float>(Bounds, ColorSets.QuadColorUFLeg, "CPU test");
            StepSizeVm = new FloatVm(0.1f, 0.0f, 0.4f, "Step Size");
            DisplayFrequencySliderVm = new SliderVm(new I<float>(1.0f, 50.0f), 2, "0")
                                            { Title = "Display Frequency", Value = 20 };
            
            StarGrid = StarProcs.RandStarGrid(Bounds, 1293);
            UpdateUi();
        }

        private Grid2DVm<float> _graphVm;

        public Grid2DVm<float> Grid2DVm
        {
            get { return _graphVm; }
            set { SetProperty(ref _graphVm, value); }
        }

        #region local vars

        private static readonly int GridStride = 128;
        private static readonly Sz2<int> Bounds = new Sz2<int>(GridStride, GridStride);
        private StarGrid StarGrid { get; set; }
        private CancellationTokenSource _cancellationTokenSource = new CancellationTokenSource();
        private bool _isRunning;
        private readonly Stopwatch _stopwatch = new Stopwatch();

        #endregion

        #region public vars

        public string ElapsedTime => $"{_stopwatch.Elapsed.Hours.ToString("00")}:" +
                                     $"{_stopwatch.Elapsed.Minutes.ToString("00")}:" +
                                     $"{_stopwatch.Elapsed.Seconds.ToString("00")}:" +
                                     $"{_stopwatch.Elapsed.Milliseconds.ToString("000")}";

        public int Generation => StarGrid.Generation;

        public SliderVm DisplayFrequencySliderVm { get; }

        #endregion

        #region StartCommand

        private RelayCommand _startCommand;

        public ICommand StartCommand => _startCommand ?? (_startCommand = new RelayCommand(
            DoStart,
            CanStart
            ));

        private async void DoStart()
        {
            _cancellationTokenSource = new CancellationTokenSource();
            _isRunning = true;
            CommandManager.InvalidateRequerySuggested();

            await Task.Run(() =>
            {
                _stopwatch.Start();

                for (var i = 0; _isRunning; i++)
                {
                    StarGrid.GetDeltas();
                    StarGrid.Update(StepSizeVm.FloatVal);
                    if (_cancellationTokenSource.IsCancellationRequested)
                    {
                        _isRunning = false;
                        _stopwatch.Stop();
                        CommandManager.InvalidateRequerySuggested();
                    }

                    if (i%(int) DisplayFrequencySliderVm.Value == 0)
                    {
                        Application.Current.Dispatcher.Invoke
                            (
                                UpdateUi,
                                DispatcherPriority.Background
                            );
                    }
                }

            },
                cancellationToken: _cancellationTokenSource.Token
                );

        }

        private bool CanStart()
        {
            return !_isRunning;
        }

        #endregion // StartCommand


        #region StopCommand

        private RelayCommand _stopCommand;

        public ICommand StopCommand => _stopCommand ?? (_stopCommand = new RelayCommand(
            DoStop,
            CanStop
            ));

        private void DoStop()
        {
            _cancellationTokenSource.Cancel();
        }

        private bool CanStop()
        {
            return this._isRunning;
        }

        #endregion // StopCommand


        public FloatVm StepSizeVm { get; }

        private void UpdateUi()
        {
            Grid2DVm.UpdateData(StarGrid.ToP2Vs());
            OnPropertyChanged("Generation");
            OnPropertyChanged("ElapsedTime");
        }
    }
}
