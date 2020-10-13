using SportsCentre.WPF.State.Navigators;
using SportsCentre.WPF.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

namespace SportsCentre.WPF.Commands
{
    public class UpdateCurrentViewModelCommand : ICommand 
    {
        public event EventHandler CanExecuteChanged;

        private INavigator _navigator;

        public UpdateCurrentViewModelCommand(INavigator navigator)
        {
            _navigator = navigator;
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            if (parameter is ViewType)
            {
                ViewType viewType = (ViewType)parameter;

                switch (viewType)
                {
                    case ViewType.Home:
                        _navigator.CurrentViewModel = new HomeViewModel();
                        break;
                    case ViewType.Centres:
                        _navigator.CurrentViewModel = new CentresViewModel();
                        break;
                    case ViewType.Courts:
                        _navigator.CurrentViewModel = new CourtsViewModel();
                        break;
                    case ViewType.Matches:
                        _navigator.CurrentViewModel = new MatchesViewModel();
                        break;
                    default:
                        break;
                }
            }
        }
    }
}
