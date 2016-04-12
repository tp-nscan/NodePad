﻿using NodePad.Common;
using NodePad.ViewModel.Common;
using TT;

namespace NodePad.ViewModel.Pages.Sandbox
{
    public class ControlPageVm : BindableBase
    {
        public ControlPageVm()
        {
            Sz2IntVm = new Sz2IntVm(new Sz2<int>(5, 6), 800);
        }

        private Sz2IntVm _sz2IntVm;
        public Sz2IntVm Sz2IntVm
        {
            get { return _sz2IntVm; }
            set
            {
                SetProperty(ref _sz2IntVm, value);
            }
        }

    }
}
