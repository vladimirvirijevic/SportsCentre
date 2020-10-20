using System;
using System.Collections.Generic;
using System.Text;

namespace SportsCentre.WPF.ViewModels.Factories
{
    public class CentreViewModelFactory : ISportsCentreViewModelFactory<CentresViewModel>
    {
        public CentresViewModel CreateViewModel()
        {
            return new CentresViewModel();
        }
    }
}
