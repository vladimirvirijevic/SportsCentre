using SportsCentre.WPF.State.Navigators;
using System;
using System.Collections.Generic;
using System.Text;

namespace SportsCentre.WPF.ViewModels
{
    public class AdminViewModel : ViewModelBase
    {
        public INavigator Navigator { get; set; }

        public AdminViewModel()
        {
            Navigator = new Navigator();

            Navigator.UpdateCurrentViewModelCommand.Execute(ViewType.Users);
        }
    }
}
