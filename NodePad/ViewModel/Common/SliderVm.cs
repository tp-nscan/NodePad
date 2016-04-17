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
            set
            {
                _title = value;
                OnPropertyChanged("Title");
            }
        }

        public string LegendMaximum => Interval.Max.ToString(NumberFormat);

        public string LegendMinimum => Interval.Min.ToString(NumberFormat);

        public string LegendValue => Value.ToString(NumberFormat);

        private double _value;
        public double Value
        {
            get { return _value; }
            set
            {
                _value = value;
                _sliderVmChanged.OnNext(this);
                IsDirty = true;
                OnPropertyChanged("Value");
                OnPropertyChanged("LegendValue");
            }
        }

        public double TickFrequency { get; }

        public bool IsDirty { get; set; }

        private readonly Subject<SliderVm> _sliderVmChanged
            = new Subject<SliderVm>();
        public IObservable<SliderVm> OnSliderVmChanged => _sliderVmChanged;
    }
}
