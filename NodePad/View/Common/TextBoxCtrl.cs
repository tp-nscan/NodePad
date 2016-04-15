using System.Windows;
using System.Windows.Controls;

namespace NodePad.View.Common
{
    public class TextBoxCtrl : Control
    {
        public Orientation Orientation
        {
            get { return (Orientation)GetValue(OrientationProperty); }
            set { SetValue(OrientationProperty, value); }
        }

        public static readonly DependencyProperty OrientationProperty =
            DependencyProperty.Register("Orientation", typeof(Orientation), typeof(TextBoxCtrl), null);
    }
}
