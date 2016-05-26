using System.Collections.Generic;
using System.Collections.ObjectModel;
using TT;

namespace NodePad.ViewModel.Common.ParamVm
{
    public enum ParamVmType
    {
        Group,
        Node
    }

    public interface IParamVm
    {
        bool IsDirty { get; }
        string Key { get; }
        string Path { get; }
        ParamVmType ParamVmType { get; }
        ObservableCollection<IParamVm> Children { get; } 
    }

    public interface IParamGroupVm : IParamVm
    {
        ParamGroup UpdatedParamGroup { get; }
    }

    public interface IParamChildVm : IParamVm
    {
        Param UpdatedParam { get; }
    }

}