using System;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Threading;
using NodePad.Common;
using NodePad.Model.P;
using NodePad.ViewModel.Common;
using TT;

namespace NodePad.ViewModel.Pages.CPU
{
    public class Ring6PageVm : BindableBase
    {
        public Ring6PageVm()
        {
            Randy = new ThreadSafeRandom();

            DisplayFrequencySliderVm = new SliderVm(new I<float>(1.0f, 50.0f), 1, "0", "DisplayFrequency")
            { Title = "Display Frequency", Value = 20 };

            StepSizeVm = new SliderVm(new I<float>(0.0f, 0.3f), 0.02, "0.00", "StepSize")
            { Title = "Step Size", Value = 0.1f };

            DisplayFrequencySliderVm = new SliderVm(new I<float>(1.0f, 100.0f), 1, "0", "DisplayFrequency") { Title = "Display Frequency", Value = 50 };

            NoiseLevelVm = new SliderVm(new I<float>(0.0f, 0.3f), 0.002, "0.000", "NoiseLevel")
            { Title = "Noise", Value = 0.02f };

            Grid2DVm = new Grid2DVm<float>(Bounds, ColorSets.QuadColorUFLeg, "Ring 1");

            NodeArray = NodeProcs.RandNodeArray(Bounds, Randy);

            UpdateUi();
        }


        #region local vars

        private static readonly int GridStride = 128;
        private static readonly Sz2<int> Bounds = new Sz2<int>(GridStride, GridStride);
        private NodeArray NodeArray { get; set; }
        private CancellationTokenSource _cancellationTokenSource = new CancellationTokenSource();
        private bool _isRunning;
        private readonly Stopwatch _stopwatch = new Stopwatch();
        private Random Randy { get; set; }

        #endregion


        #region public vars

        public string ElapsedTime => $"{_stopwatch.Elapsed.Hours.ToString("00")}:" +
                                     $"{_stopwatch.Elapsed.Minutes.ToString("00")}:" +
                                     $"{_stopwatch.Elapsed.Seconds.ToString("00")}:" +
                                     $"{_stopwatch.Elapsed.Milliseconds.ToString("000")}";


        public int Generation => NodeArray.Generation;

        private Grid2DVm<float> _grid2DVm;
        public Grid2DVm<float> Grid2DVm
        {
            get { return _grid2DVm; }
            set { SetProperty(ref _grid2DVm, value); }
        }

        public SliderVm NoiseLevelVm { get; }

        public SliderVm StepSizeVm { get; }

        public SliderVm DisplayFrequencySliderVm { get; }

        #endregion


        #region StartCommand

        private RelayCommand _startCommand;

        public ICommand StartCommand => _startCommand ?? (_startCommand = new RelayCommand(
            param => DoStart(),
            param => CanStart()
            ));

        private async void DoStart()
        {
            _cancellationTokenSource = new CancellationTokenSource();
            _isRunning = true;
            CommandManager.InvalidateRequerySuggested();

            await Task.Run(async () =>
            {
                _stopwatch.Start();

                for (var i = 0; _isRunning; i++)
                {
                    NodeArray = await NodeArray.UpdateFred(StepSizeVm.Value, NoiseLevelVm.Value, Randy);

                    if (_cancellationTokenSource.IsCancellationRequested)
                    {
                        _isRunning = false;
                        _stopwatch.Stop();
                        CommandManager.InvalidateRequerySuggested();
                    }

                    if (i % (int)DisplayFrequencySliderVm.Value == 0)
                    {
                        Application.Current.Dispatcher.Invoke
                            (
                                UpdateUi,
                                DispatcherPriority.Background
                            );
                    }
                }

            }, _cancellationTokenSource.Token );

        }

        private bool CanStart()
        {
            return !_isRunning;
        }

        #endregion // StartCommand


        #region StopCommand

        private RelayCommand _stopCommand;

        public ICommand StopCommand => _stopCommand ?? (_stopCommand = new RelayCommand(
            param => DoStop(),
            param => CanStop()
            ));

        private void DoStop()
        {
            _cancellationTokenSource.Cancel();
        }

        private bool CanStop()
        {
            return _isRunning;
        }

        #endregion // StopCommand


        private void UpdateUi()
        {
            Grid2DVm.UpdateData(NodeArray.DataToP2Vs(Bounds));
            OnPropertyChanged("Generation");
            OnPropertyChanged("ElapsedTime");
        }
    }
}
