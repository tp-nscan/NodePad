using System.Collections.Generic;
using System.Linq;
using System.Windows.Media;
using NodePad.Common;
using TT;

namespace NodePad.ViewModel.Common
{
    public class Grid2D2IVm : BindableBase
    {
        public Grid2D2IVm(Sz2<int> strides, ColorLeg2I<float> colorLeg, string title = "")
        {
            Strides = strides;
            ColorLeg2I = colorLeg;
            WbImageVm = new WbImageVm();
            Title = title;
            var sp = A2dUt.GetColumn(colorLeg.spanC, colorLeg.spanC.GetLength(1) - 1);
            LegendVm = new LegendVm(
                minVal: "<" + colorLeg.rangeV.MinY,
                midVal: ColorSets2I.GetLegMidVal(colorLeg).ToString(),
                maxVal: ">" + colorLeg.rangeV.MaxY,
                minCol: colorLeg.exteriorC,
                midColors: sp,
                maxColor: colorLeg.exteriorC
                );
        }

        private WbImageVm _wbImageVm;
        public WbImageVm WbImageVm
        {
            get { return _wbImageVm; }
            set
            {
                SetProperty(ref _wbImageVm, value);
            }
        }

        public Sz2<int> Strides { get; }

        public ColorLeg2I<float> ColorLeg2I { get; }

        public List<P2V<int, P2<float>>> Values { get; private set; }

        private LegendVm _legendVm;
        public LegendVm LegendVm
        {
            get { return _legendVm; }
            set
            {
                SetProperty(ref _legendVm, value);
            }
        }

        public void UpdateData(IEnumerable<P2V<int, P2<float>>> values)
        {
            if (values == null) return;

            Values = values.ToList();
            WbImageVm.ImageData = Id.MakeImageData(
                    imageSize: new Sz2<double>(-1.0, -1.0),
                    plotPoints: new List<P2V<float, Color>>(),
                    filledRects: Values.Select(MakeRectangle).ToList(),
                    openRects: new List<RV<float, Color>>(),
                    plotLines: new List<LS2V<float, Color>>()
                );
        }

        public RV<float, Color> MakeRectangle(P2V<int, P2<float>> v)
        {
            return new RV<float, Color>(
                    minX: v.X,
                    minY: v.Y,
                    maxX: v.X + 1.0f,
                    maxY: v.Y + 1.0f,
                    v: ColorSets2I.GetLeg2IColorF32(ColorLeg2I, v.V)
                );
        }

        private string _title;
        public string Title
        {
            get { return _title; }
            set { SetProperty(ref _title, value); }
        }
    }
}
