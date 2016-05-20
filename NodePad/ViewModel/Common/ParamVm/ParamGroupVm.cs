using System.Collections.ObjectModel;
using NodePad.Common;
using TT;

namespace NodePad.ViewModel.Common.ParamVm
{
    public class ParamGroupVm : BindableBase
    {
        private ObservableCollection<ParamGroupVm> _paramGroupVms 
            = new ObservableCollection<ParamGroupVm>( );

        private ObservableCollection<IParamBaseVm> _paramVms
            = new ObservableCollection<IParamBaseVm>();


        public ParamGroupVm(ParamGroup paramGroup)
        {
            foreach (var pg in Params.GetParamGroups(paramGroup))
            {
                ParamGroupVms.Add(new ParamGroupVm(pg));
            }

            foreach (var pram in Params.GetParams(paramGroup))
            {
               // ParamGroupVms.Add(new ParamGroupVm(pg));
            }
        }

        public ObservableCollection<ParamGroupVm> ParamGroupVms
        {
            get { return _paramGroupVms; }
            set { _paramGroupVms = value; }
        }

        public ObservableCollection<IParamBaseVm> ParamVms
        {
            get { return _paramVms; }
            set { _paramVms = value; }
        }
    }
}
