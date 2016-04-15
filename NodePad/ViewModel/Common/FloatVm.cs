using System;
using System.ComponentModel;
using System.Globalization;
using System.Reactive.Subjects;
using System.Windows.Input;
using NodePad.Common;

namespace NodePad.ViewModel.Common
{
    public class FloatVm : BindableBase, IDataErrorInfo
    {
        private readonly Subject<float> _valueChanged = new Subject<float>();
        public IObservable<float> OnValueChanged => _valueChanged;

        public FloatVm(float val, float minVal, float maxVal, string caption)
        {
            FloatVal = val;
            StrVal = FloatVal.ToString(CultureInfo.InvariantCulture);
            MaxVal = maxVal;
            Caption = caption;
            MinVal = minVal;
        }

        public float FloatVal { get; private set; }

        private string _strVal;
        public string StrVal
        {
            get { return _strVal; }
            set { SetProperty(ref _strVal, value); }
        }

        public float MinVal { get; private set; }

        public float MaxVal { get; private set; }

        public string Caption { get; private set; }

        #region UpdateCommand

        RelayCommand _updateCommand;

        public ICommand UpdateCommand => _updateCommand ?? (_updateCommand = new RelayCommand(
            DoUpdate,
            CanUpdate
            ));

        private void DoUpdate()
        {
            FloatVal = float.Parse(_strVal);
            _valueChanged.OnNext(FloatVal);
        }

        bool CanUpdate()
        {
            return Error == String.Empty;
        }

        #endregion // UpdateCommand

        public string this[string columnName]
        {
            get
            {
                switch (columnName)
                {
                    case "StrVal":
                        return FloatZ(StrVal, MinVal, MaxVal);
                }
                return String.Empty;
            }
        }

        public string Error => this["StrVal"];

        static string FloatZ(string val, float minVal, float maxVal)
        {
            float outVal;
            if (float.TryParse(val, out outVal))
            {
                if (outVal <= minVal) return $"must be > {minVal}";
                if (outVal > maxVal) return $"must be < {maxVal}";
                return String.Empty;
            }
            return "must be an integer";
        }
    }
}
