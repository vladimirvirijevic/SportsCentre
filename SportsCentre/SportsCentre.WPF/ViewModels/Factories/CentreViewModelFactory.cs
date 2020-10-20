using SportsCentre.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace SportsCentre.WPF.ViewModels.Factories
{
    public class CentreViewModelFactory : ISportsCentreViewModelFactory<CentresViewModel>
    {
        private readonly ICentreService _centreService;

        public CentreViewModelFactory(ICentreService centreService)
        {
            _centreService = centreService;
        }

        public CentresViewModel CreateViewModel()
        {
            return new CentresViewModel(_centreService);
        }
    }
}
