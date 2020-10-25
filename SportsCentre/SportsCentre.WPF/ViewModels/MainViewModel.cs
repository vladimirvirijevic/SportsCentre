using SportsCentre.Domain.Interfaces;
using SportsCentre.WPF.State.Navigators;
using System;
using System.Collections.Generic;
using System.Text;

namespace SportsCentre.WPF.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        public INavigator Navigator { get; set; }

        /*
        public MainViewModel(INavigator navigator)
        {
            Navigator = navigator;

            Navigator.UpdateCurrentViewModelCommand.Execute(ViewType.Login);

            var adminWindow = new AdminWindow();
            adminWindow.Show();
        }
        */
        public MainViewModel()
        {
            Navigator = new Navigator();

            Navigator.UpdateCurrentViewModelCommand.Execute(ViewType.Login);

            //var adminWindow = new AdminWindow();
            //adminWindow.Show();
        }
    }
}
