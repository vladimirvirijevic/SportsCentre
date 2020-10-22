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

        private readonly INavigator _navigator;

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
                    case ViewType.Centres:
                        _navigator.CurrentViewModel = new CentresViewModel();
                        break;
                    case ViewType.Courts:
                        _navigator.CurrentViewModel = new CourtsViewModel();
                        break;
                    case ViewType.Matches:
                        _navigator.CurrentViewModel = new MatchesViewModel();
                        break;
                    case ViewType.Trainings:
                        _navigator.CurrentViewModel = new TrainingsViewModel();
                        break;
                    case ViewType.Clubs:
                        _navigator.CurrentViewModel = new ClubsViewModel();
                        break;
                    case ViewType.Players:
                        _navigator.CurrentViewModel = new PlayersViewModel();
                        break;
                    case ViewType.Coaches:
                        _navigator.CurrentViewModel = new CoachesViewModel();
                        break;
                    case ViewType.Permits:
                        _navigator.CurrentViewModel = new PermitsViewModel();
                        break;
                    case ViewType.Tickets:
                        _navigator.CurrentViewModel = new TicketsViewModel();
                        break;
                    case ViewType.Members:
                        _navigator.CurrentViewModel = new MembersViewModel();
                        break;
                    case ViewType.Guests:
                        _navigator.CurrentViewModel = new GuestsViewModel();
                        break;
                    default:
                        break;
                }
            }
        }
    }
}
