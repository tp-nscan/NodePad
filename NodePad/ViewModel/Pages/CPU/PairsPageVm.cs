using System.Diagnostics;
using System.Linq;
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
    public class PairsPageVm : BindableBase
    {
        public PairsPageVm()
        {
            GridValsVm = new Grid2DVm<float>(Bounds, ColorSets.QuadColorUFLeg, "Ring 2 Values");

            GridDeltasVm = new Grid2DVm<float>(Bounds, ColorSets.BlueUFLeg, "Ring 2 Deltas");


            DisplayFrequencySliderVm = new SliderVm(new I<float>(1.0f, 50.0f), 1, "0")
            { Title = "Display Frequency", Value = 2 };


            NoiseLevelVm = new SliderVm(new I<float>(0.0f, 0.3f), 0.002, "0.000")
            { Title = "Noise", Value = 0.03f };


            StepSizeVm = new SliderVm(new I<float>(0.0f, 0.3f), 0.02, "0.00")
            { Title = "Step Size", Value = 0.1f };


            NoiseFieldDecayVm = new SliderVm(new I<float>(0.0f, 0.9f), 0.02, "0.00")
            { Title = "Noise field decay", Value = 0.3f };


            DeltaNoiseCplVm = new SliderVm(new I<float>(0.0f, 0.9f), 0.02, "0.00")
            { Title = "Delta noise cpl", Value = 0.2f };


            NoiseFieldCplVm = new SliderVm(new I<float>(0.0f, 0.4f), 0.002, "0.000")
            { Title = "Noise field cpl", Value = 0.03f };


            Star2Grid = Star2Procs.RandStarGrid(Bounds, 1293);
            UpdateUi();
        }


        private float _noiseField;
        public float NoiseField
        {
            get { return _noiseField; }
            set { SetProperty(ref _noiseField, value); }
        }


        private float _absDelta;
        public float AbsDelta
        {
            get { return _absDelta; }
            set { SetProperty(ref _absDelta, value); }
        }


        private Grid2DVm<float> _gridValsVm;
        public Grid2DVm<float> GridValsVm
        {
            get { return _gridValsVm; }
            set { SetProperty(ref _gridValsVm, value); }
        }


        private Grid2DVm<float> _gridDeltasVm;
        public Grid2DVm<float> GridDeltasVm
        {
            get { return _gridDeltasVm; }
            set { SetProperty(ref _gridDeltasVm, value); }
        }

        #region local vars

        private static readonly int GridStride = 128;
        private static readonly Sz2<int> Bounds = new Sz2<int>(GridStride, GridStride);
        private Star2Grid Star2Grid { get; }
        private CancellationTokenSource _cancellationTokenSource = new CancellationTokenSource();
        private bool _isRunning;
        private readonly Stopwatch _stopwatch = new Stopwatch();

        #endregion

        #region public vars

        public string ElapsedTime => $"{_stopwatch.Elapsed.Hours.ToString("00")}:" +
                                     $"{_stopwatch.Elapsed.Minutes.ToString("00")}:" +
                                     $"{_stopwatch.Elapsed.Seconds.ToString("00")}:" +
                                     $"{_stopwatch.Elapsed.Milliseconds.ToString("000")}";

        public int Generation => Star2Grid.Generation;

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

            await Task.Run(() =>
            {
                _stopwatch.Start();

                for (var i = 0; _isRunning; i++)
                {
                    Star2Grid.GetDeltas();
                    Star2Grid.Update(
                            step: StepSizeVm.Value,
                            noise: NoiseLevelVm.Value,
                            nfDecay: NoiseFieldDecayVm.Value,
                            absDeltaToNoise: DeltaNoiseCplVm.Value,
                            nfCpl: NoiseFieldCplVm.Value);

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

            }, _cancellationTokenSource.Token
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

        public SliderVm DisplayFrequencySliderVm { get; }

        public SliderVm NoiseLevelVm { get; }

        public SliderVm StepSizeVm { get; }

        public SliderVm NoiseFieldDecayVm { get; }

        public SliderVm DeltaNoiseCplVm { get; }

        public SliderVm NoiseFieldCplVm { get; }

        private void UpdateUi()
        {
            GridValsVm.UpdateData(Star2Grid.CurValuesAsP2Vs());
            GridDeltasVm.UpdateData(Star2Grid.CurDeltasAsP2Vs());
            AbsDelta = A2dUt.flattenRowMajor(Star2Grid.Stars).Sum(st => st.AbsDelta);
            NoiseField = A2dUt.flattenRowMajor(Star2Grid.Stars).Sum(st => st.NoiseField);
            OnPropertyChanged("Generation");
            OnPropertyChanged("ElapsedTime");
        }
    }
}
