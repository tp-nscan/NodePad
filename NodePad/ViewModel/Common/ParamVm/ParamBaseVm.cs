using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reactive.Subjects;
using NodePad.Common;
using TT;

namespace NodePad.ViewModel.Common.ParamVm
{
    public static class ParamVm
    {
        public static string MakeKey(this string childKey, string parentKey)
        {
            return (string.IsNullOrEmpty(parentKey))
                ? childKey
                : parentKey + "." + childKey;
        }


        //public static IDictionary<string, Param> GetParamictionary(this IEnumerable<IParamVm> paramsE)
        //{

        //}

    //public static IDictionary<string, Param> GetParamictionary(this IEnumerable<IParamVm> paramsE)
        //{
        //    return paramsE.Select(pvm => pvm.IsDirty ?
        //         new Tuple<string, Param>(pvm.Key, pvm.MakeUpdated() )
        //       : new Tuple<string, Param>(pvm.Key, pvm.Param )
        //                 ).ToDictionary(keySelector: kp => kp.Item1, 
        //                                elementSelector: kp => kp.Item2);
        //}

    }

    public abstract class ParamBaseVm : BindableBase, IParamChildVm
    {
        protected ParamBaseVm(string key, Param param)
        {
            Key = key;
            Param = param;
        }

        protected readonly Subject<IParamVm> ParamBaseVmChanged = new Subject<IParamVm>();

        public IObservable<IParamVm> ReportAChange => ParamBaseVmChanged;

        public Param Param { get; }

        public bool IsDirty { get; protected set; }

        public string Key { get; }

        public abstract Param UpdatedParam { get; }

        public ParamVmType ParamVmType => ParamVmType.Node;

    }


    public class IntParamVm : ParamBaseVm
    {
        public IntParamVm(string key, Param.PInt param) : base(key, param)
        {
            _value = param.Item.value;
            Interval = param.Item.bounds;
            TickFrequency = VmUtil.TicFrequency(Interval.Min, Interval.Max);
            Title = param.Item.descr;
            NumberFormat = VmUtil.NumberFormat(Interval.Min, Interval.Max);
        }

        public override Param UpdatedParam => 
            (IsDirty) ? Params.UpdateParam(Param, Value) : Param;

        private int _value;
        public int  Value
        {
            get { return _value; }
            set
            {
                if (!SetProperty(ref _value, value)) return;
                ParamBaseVmChanged.OnNext(this);
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
        public F32ParamVm(string key, Param.PF32 param) : base(key, param)
        {
            _value = param.Item.value;
            Interval = param.Item.bounds;
            TickFrequency = VmUtil.TicFrequency(Interval.Min, Interval.Max);
            Title = param.Item.descr;
            NumberFormat = VmUtil.NumberFormat(Interval.Min, Interval.Max);
        }

        public override Param UpdatedParam =>
            (IsDirty) ? Params.UpdateParam(Param, Value) : Param;

        private float _value;
        public float Value
        {
            get { return _value; }
            set
            {
                if (!SetProperty(ref _value, value)) return;
                ParamBaseVmChanged.OnNext(this);
                IsDirty = true;
            }
        }

        public I<float> Interval { get; }

        public double TickFrequency { get; }

        public string NumberFormat { get; }

        public string Title { get; }

    }



}
