using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Security.Cryptography;
using NodePad.Common;
using TT;

namespace NodePad.ViewModel.Common.ParamVm
{
    public class ParamGroupVm : BindableBase, IParamVm, IParamGroupVm
    {
        private ObservableCollection<IParamVm> _children 
            = new ObservableCollection<IParamVm>();
        
        private List<IParamGroupVm> _paramGroupVms = new List<IParamGroupVm>();
        private List<IParamChildVm> _paramChildVms = new List<IParamChildVm>();
        private ParamGroup _updatedParamGroup;

        public ParamGroupVm(ParamGroup paramGroup, string key)
        {
            ParamGroup = paramGroup;

            Key = (string.IsNullOrEmpty(key))
                ? paramGroup.key
                : key + "." + paramGroup.key;

            foreach (var pram in Params.GetParams(paramGroup))
            {
                var res = Params.ForCSharp(pram);
                var vm = (pram.IsPF32)
                    ? (IParamChildVm)(new F32ParamVm(Key, (Param.PF32)res.Item1))
                    : new IntParamVm(Key, (Param.PInt)res.Item1);

                _children.Add(vm);
                _paramChildVms.Add(vm);
            }

            foreach (var pg in Params.GetParamGroups(paramGroup))
            {
                var gvm = new ParamGroupVm(pg, Key);
                _children.Add(gvm);
            }
        }

        public ParamGroup ParamGroup { get; }

        public ObservableCollection<IParamVm> Children
        {
            get { return _children; }
            set { _children = value; }
        }

        public bool IsDirty
        {
            get { return Children.Any(c=>c.IsDirty); }
        }

        public string Key { get; }

        public ParamVmType ParamVmType => ParamVmType.Group;

        public ParamGroup UpdatedParamGroup
        {
            get { return _updatedParamGroup; }
        }
    }
}
