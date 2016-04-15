using System;
using System.Windows.Input;
using NodePad.Common;
using NodePad.ViewModel.Common;
using TT;

namespace NodePad.ViewModel.Pages.CPU
{
    public class RingPageVm : BindableBase
    {
        private static int GridStride = 128;
        public static Sz2<int> Bounds = new Sz2<int>(GridStride, GridStride);

        public RingPageVm()
        {
            Grid2DVm = new Grid2DVm<float>(Bounds, ColorSets.RedBlueSFLeg, "CPU test");
            StepSizeVm = new FloatVm(0.1f, 0.0f, 0.4f, "Step Size");
            StepSizeVm.OnValueChanged.Subscribe(ResetStepSize);
        }

        void ResetStepSize(float stepSize)
        {

        }

        private Grid2DVm<float> _graphVm;
        public Grid2DVm<float> Grid2DVm
        {
            get { return _graphVm; }
            set
            {
                SetProperty(ref _graphVm, value);
            }
        }


        #region StartCommand

        RelayCommand _startCommand;

        public ICommand StartCommand => _startCommand ?? (_startCommand = new RelayCommand(
            DoStart,
            CanStart
            ));

        private void DoStart()
        {

        }

        bool CanStart()
        {
            return true;
        }

        #endregion // StartCommand


        #region StopCommand

        RelayCommand _stopCommand;

        public ICommand StopCommand => _stopCommand ?? (_stopCommand = new RelayCommand(
            DoStop,
            CanStop
            ));

        private void DoStop()
        {

        }

        bool CanStop()
        {
            return true;
        }

        #endregion // StopCommand


        public FloatVm StepSizeVm { get; }

    }
}
