﻿using System;
using System.ComponentModel;
using NodePad.Common;

namespace NodePad.ViewModel.Common
{
    public class IntVm : BindableBase, IDataErrorInfo
    {
        public IntVm(int val, int maxVal, string caption, int minVal)
        {
            IntVal = val;
            StrVal = IntVal.ToString();
            MaxVal = maxVal;
            Caption = caption;
            MinVal = minVal;
        }

        public int IntVal { get; private set; }

        private string _strVal;
        public string StrVal
        {
            get { return _strVal; }
            set { SetProperty(ref _strVal, value); }
        }

        public int MinVal { get; private set; }

        public int MaxVal { get; private set; }

        public string Caption { get; private set; }

        public string this[string columnName]
        {
            get
            {
                switch (columnName)
                {
                    case "StrVal":
                        return IntZ(StrVal, MinVal, MaxVal);
                }
                return String.Empty;
            }
        }

        public string Error => this["StrVal"];

        static string IntZ(string val, int minVal, int maxVal)
        {
            int outVal;
            if (int.TryParse(val, out outVal))
            {
                if (outVal <= minVal) return $"must be > {minVal}";
                if (outVal > maxVal) return $"must be < {maxVal}";
                return String.Empty;
            }
            return "must be an integer";
        }
    }
}