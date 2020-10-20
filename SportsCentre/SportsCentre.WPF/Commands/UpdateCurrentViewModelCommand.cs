﻿using SportsCentre.WPF.State.Navigators;
using SportsCentre.WPF.ViewModels;
using SportsCentre.WPF.ViewModels.Factories;
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
        private readonly ISportsCentreViewModelAbstractFactory _viewModelFactory;

        public UpdateCurrentViewModelCommand(INavigator navigator, ISportsCentreViewModelAbstractFactory viewModelFactory)
        {
            _navigator = navigator;
            _viewModelFactory = viewModelFactory;
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

                _navigator.CurrentViewModel = _viewModelFactory.CreateViewModel(viewType);
            }
        }
    }
}
