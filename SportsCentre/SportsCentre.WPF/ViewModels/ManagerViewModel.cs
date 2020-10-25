using SportsCentre.WPF.State.Navigators;
using System;
using System.Collections.Generic;
using System.Text;

namespace SportsCentre.WPF.ViewModels
{
    public class ManagerViewModel : ViewModelBase
    {
        public INavigator Navigator { get; set; }

        public ManagerViewModel()
        {
            Navigator = new Navigator();

            Navigator.UpdateCurrentViewModelCommand.Execute(ViewType.Centres);
        }
    }
}
