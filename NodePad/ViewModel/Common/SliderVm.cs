using System;
using System.Reactive.Subjects;
using NodePad.Common;
using TT;

namespace NodePad.ViewModel.Common
{
    public class SliderVm : BindableBase
    {
        public SliderVm(I<float> interval,
                        double tickFrequency,
                        string numberFormat)
        {
            TickFrequency = tickFrequency;
            NumberFormat = numberFormat;
            Interval = interval;
            Value = Interval.Mid();
        }

        private string NumberFormat { get; }

        public I<float> Interval { get; }

        private string _title;
        public string Title
        {
            get { return _title; }
            set { SetProperty(ref _title, value); }
        }

        public string LegendMaximum => Interval.Max.ToString(NumberFormat);

        public string LegendMinimum => Interval.Min.ToString(NumberFormat);

        public string LegendValue => Value.ToString(NumberFormat);

        private float _value;
        public float Value
        {
            get { return _value; }
            set
            {
                if (!SetProperty(ref _value, value)) return;
                _sliderVmChanged.OnNext(this);
                OnPropertyChanged("LegendValue");
                IsDirty = true;
            }
        }

        public double TickFrequency { get; }

        public bool IsDirty { get; set; }

        private readonly Subject<SliderVm> _sliderVmChanged
            = new Subject<SliderVm>();
        public IObservable<SliderVm> OnSliderVmChanged => _sliderVmChanged;
    }
}
