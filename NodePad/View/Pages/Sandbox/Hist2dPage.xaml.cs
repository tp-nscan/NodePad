﻿using FirstFloor.ModernUI.Windows;
using FirstFloor.ModernUI.Windows.Navigation;
using NodePad.ViewModel.Pages.Sandbox;

namespace NodePad.View.Pages.Sandbox
{
    /// <summary>
    /// Interaction logic for Hist2dPage.xaml
    /// </summary>
    public partial class Hist2dPage : IContent
    {
        public Hist2dPage()
        {
            InitializeComponent();
            DataContext = new Hist2dPageVm(10);
        }

        public void OnFragmentNavigation(FragmentNavigationEventArgs e)
        {

        }

        public void OnNavigatedFrom(NavigationEventArgs e)
        {

        }

        public void OnNavigatedTo(NavigationEventArgs e)
        {

        }

        public void OnNavigatingFrom(NavigatingCancelEventArgs e)
        {
            //if (MessageBoxResult.No == ModernDialog.ShowMessage("Navigate away?", "navigate", System.Windows.MessageBoxButton.YesNo))
            //{
            //    e.Cancel = true;
            //}
        }
    }
}
