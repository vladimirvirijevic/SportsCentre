using SportsCentre.WPF.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

namespace SportsCentre.WPF.State.Navigators
{
    public enum ViewType
    {
        Centres,
        Courts,
        Matches,
        Trainings,
        Clubs,
        Coaches,
        Players,
        Permits
    }

    public interface INavigator
    {
        ViewModelBase CurrentViewModel { get; set; }
        ICommand UpdateCurrentViewModelCommand { get; }
    }
}
