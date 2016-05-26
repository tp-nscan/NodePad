using System;
using System.Windows;
using System.Windows.Controls;
using NodePad.ViewModel.Common.ParamVm;

namespace NodePad.View.Common.Param
{
    public class ParamTreeTemplateSelector :  DataTemplateSelector
    {
        public DataTemplate GroupTemplate { get; set; }

        public DataTemplate ParamTemplate { get; set; }

        public DataTemplate DefaultTemplate { get; set; }

        public override DataTemplate SelectTemplate(object item, DependencyObject container)
        {
            var legendVm = item as IParamVm;

            if (legendVm != null)
            {
                switch (legendVm.ParamVmType)
                {
                    case ParamVmType.Group:
                        return GroupTemplate;
                    case ParamVmType.Node:
                        return ParamTemplate;
                    default:
                        throw new Exception($"MainContentType {legendVm.ParamVmType} not handled in SelectTemplate");
                }
            }

            return DefaultTemplate;
        }
    }
}
