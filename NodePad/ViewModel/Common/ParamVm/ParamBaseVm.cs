using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Subjects;
using NodePad.Common;
using TT;

namespace NodePad.ViewModel.Common.ParamVm
{
    public interface IParamBaseVm
    {
        Param Param { get; }
        Param MakeUpdated();
        bool IsDirty { get; }
        string Key { get; }
    }

    public static class ParamVm
    {
        public static IDictionary<string, Param> GetParamictionary(this IEnumerable<IParamBaseVm> paramsE)
        {
            return paramsE.Select(pvm => pvm.IsDirty ?
                 new Tuple<string, Param>(pvm.Key, pvm.MakeUpdated() )
               : new Tuple<string, Param>(pvm.Key, pvm.Param )
                         ).ToDictionary(keySelector: kp => kp.Item1, 
                                        elementSelector: kp => kp.Item2);
        }

    }

    public abstract class ParamBaseVm : BindableBase, IParamBaseVm, IReportsChanges<IParamBaseVm>
    {
        protected ParamBaseVm(string key, Param param)
        {
            Key = key;
            Param = param;
        }

        protected readonly Subject<IParamBaseVm> ParamBaseVmChanged = new Subject<IParamBaseVm>();
        public IObservable<IParamBaseVm> ReportAChange => ParamBaseVmChanged;

        public Param Param { get; }

        public abstract Param MakeUpdated();

        public bool IsDirty { get; protected set; }

        public string Key { get; }
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

        public override Param MakeUpdated()
        {
            return Params.UpdateParam(Param, Value);
        }

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

        public override Param MakeUpdated()
        {
            return Params.UpdateParam(Param, Value);
        }

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
