using SportsCentre.WPF.State.Navigators;
using System;
using System.Collections.Generic;
using System.Text;

namespace SportsCentre.WPF.ViewModels
{
    public class SalesViewModel : ViewModelBase
    {
        public INavigator Navigator { get; set; }

        public SalesViewModel()
        {
            Navigator = new Navigator();

            Navigator.UpdateCurrentViewModelCommand.Execute(ViewType.Members);
        }
    }
}
