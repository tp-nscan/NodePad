using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using NodePad.Common;
using TT;

namespace NodePad.ViewModel.Common.ParamVm
{
    public class ParamGroupVm : BindableBase, IParamGroupVm
    {
        private ObservableCollection<IParamVm> _children 
            = new ObservableCollection<IParamVm>();
        
        private readonly List<IParamGroupVm> _paramGroupVms = new List<IParamGroupVm>();
        private readonly List<IParamChildVm> _paramChildVms = new List<IParamChildVm>();

        public ParamGroupVm(ParamGroup paramGroup, string parentKey)
        {
            ParamGroup = paramGroup;
            Path = Key.MakePath(parentKey);

            foreach (var pram in Params.GetChildParams(paramGroup))
            {
                var res = Params.ForCSharp(pram);
                var vm = (pram.IsPF32)
                    ? (IParamChildVm)(new F32ParamVm(Path, (Param.PF32)res.Item1))
                    : new IntParamVm(Path, (Param.PInt)res.Item1);

                _children.Add(vm);
                _paramChildVms.Add(vm);
            }

            foreach (var pg in Params.GetChildParamGroups(paramGroup))
            {
                var gvm = new ParamGroupVm(pg, Path);
                _children.Add(gvm);
                _paramGroupVms.Add(gvm);
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

        public string Key => ParamGroup.key;

        public string Path { get; }

        public ParamVmType ParamVmType => ParamVmType.Group;

        public ParamGroup UpdatedParamGroup
        {
            get
            {
                return Params.MakeParamGroup(
                    key: Key,
                    descr: String.Empty,
                    prgs: _paramGroupVms.Select(gvm=>gvm.UpdatedParamGroup),
                    prs:  _paramChildVms.Select(cvm=>cvm.UpdatedParam)
                );

            }
        }
    }
}
