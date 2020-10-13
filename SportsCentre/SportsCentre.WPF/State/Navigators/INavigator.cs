﻿using SportsCentre.WPF.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

namespace SportsCentre.WPF.State.Navigators
{
    public enum ViewType
    {
        Home,
        Centres,
        Courts,
        Matches
    }

    public interface INavigator
    {
        ViewModelBase CurrentViewModel { get; set; }
        ICommand UpdateCurrentViewModelCommand { get; }
    }
}
