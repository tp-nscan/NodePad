﻿using System;
using System.Collections.ObjectModel;
using System.Reactive.Subjects;
using NodePad.Common;
using TT;

namespace NodePad.ViewModel.Common.ParamVm
{
    public static class ParamVm
    {
        public static string MakePath(this string childKey, string parentKey)
        {
            return (string.IsNullOrEmpty(parentKey))
                ? childKey
                : parentKey + "." + childKey;
        }
    }


    public abstract class ParamBaseVm : BindableBase, IParamChildVm
    {
        protected ParamBaseVm(string parentKey, Param param)
        {
            Param = param;
            Path = Key.MakePath(parentKey);
        }

        public string Key => Params.GetKey(Param);

        protected readonly Subject<IParamVm> ParamBaseVmChanged = new Subject<IParamVm>();

        public IObservable<IParamVm> ReportAChange => ParamBaseVmChanged;

        public Param Param { get; }

        public bool IsDirty { get; protected set; }

        public string Path { get; }

        public abstract Param UpdatedParam { get; }

        public ParamVmType ParamVmType => ParamVmType.Node;
        public abstract ObservableCollection<IParamVm> Children { get; }
    }


    public class IntParamVm : ParamBaseVm
    {
        public IntParamVm(string path, Param.PInt param) : base(path, param)
        {
            _value = param.Item.value;
            Interval = param.Item.bounds;
            TickFrequency = VmUtil.TicFrequency(Interval.Min, Interval.Max);
            Title = param.Item.descr;
            NumberFormat = VmUtil.NumberFormat(Interval.Min, Interval.Max);
        }

        public override Param UpdatedParam => 
            (IsDirty) ? Params.UpdateParam(Param, Value) : Param;

        public override ObservableCollection<IParamVm> Children { get; } 
            = new ObservableCollection<IParamVm>();

        public string LegendMaximum => Interval.Max.ToString(NumberFormat);

        public string LegendMinimum => Interval.Min.ToString(NumberFormat);

        public string LegendValue => Value.ToString(NumberFormat);

        private int _value;
        public int  Value
        {
            get { return _value; }
            set
            {
                if (!SetProperty(ref _value, value)) return;
                ParamBaseVmChanged.OnNext(this);
                OnPropertyChanged("LegendValue");
                IsDirty = true;
            }
        }

        public I<int> Interval { get; }

        public double TickFrequency { get; }

        public string NumberFormat { get; }

        public string Title { get; }
    }


    public class F32ParamVm : ParamBaseVm
    {
        public F32ParamVm(string path, Param.PF32 param) : base(path, param)
        {
            _value = param.Item.value;
            Interval = param.Item.bounds;
            TickFrequency = VmUtil.TicFrequency(Interval.Min, Interval.Max);
            Title = param.Item.descr;
            NumberFormat = VmUtil.NumberFormat(Interval.Min, Interval.Max);
        }

        public override Param UpdatedParam =>
            (IsDirty) ? Params.UpdateParam(Param, Value) : Param;


        public override ObservableCollection<IParamVm> Children { get; }
            = new ObservableCollection<IParamVm>();

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
                ParamBaseVmChanged.OnNext(this);
                OnPropertyChanged("LegendValue");
                IsDirty = true;
            }
        }

        public I<float> Interval { get; }

        public double TickFrequency { get; }

        public string NumberFormat { get; }

        public string Title { get; }

    }



}
