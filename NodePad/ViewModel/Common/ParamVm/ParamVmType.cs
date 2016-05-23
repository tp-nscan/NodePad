using System.Collections.Generic;
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
        ParamVmType ParamVmType { get; }
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