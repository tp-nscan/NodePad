using NodePad.ViewModel.Common;
using TT;

namespace NodePad.ViewModel.Design.Common
{
    public class SliderVmD : SliderVm
    {
        public SliderVmD() : base(new I<float>(1.0f, 50.0f), 2, "0", "DisplayFrequency")
        {
            Title = "Display Frequency";
            Value = 10;
        }
    }

}
