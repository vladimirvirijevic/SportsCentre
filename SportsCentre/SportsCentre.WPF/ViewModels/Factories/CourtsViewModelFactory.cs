using System;
using System.Collections.Generic;
using System.Text;

namespace SportsCentre.WPF.ViewModels.Factories
{
    public class CourtsViewModelFactory : ISportsCentreViewModelFactory<CourtsViewModel>
    {
        public CourtsViewModel CreateViewModel()
        {
            return new CourtsViewModel();
        }
    }
}
