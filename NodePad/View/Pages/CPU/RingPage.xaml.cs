﻿using NodePad.ViewModel.Pages.CPU;

namespace NodePad.View.Pages.CPU
{
    public partial class RingPage
    {
        public RingPage()
        {
            InitializeComponent();
            DataContext = new RingPageVm();
        }
    }
}
