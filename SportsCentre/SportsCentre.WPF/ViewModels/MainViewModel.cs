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
        private readonly ICentreService _centreService;

        public MainViewModel(INavigator navigator, ICentreService centreService)
        {
            Navigator = navigator;
            _centreService = centreService;
        }
    }
}
