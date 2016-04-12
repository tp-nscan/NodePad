﻿using NodePad.Common;
using NodePad.ViewModel.Common;
using NodePad.ViewModel.Design.Common;

namespace NodePad.ViewModel.Pages.Sandbox
{
    public class Hist2dPageVm : BindableBase
    {
        public Hist2dPageVm()
        {
            Hist2DVm = new Hist2DvmD();
        }

        private Hist2DVm _hist2DVm;
        public Hist2DVm Hist2DVm
        {
            get { return _hist2DVm; }
            set
            {
                SetProperty(ref _hist2DVm, value);
            }
        }
    }
}
