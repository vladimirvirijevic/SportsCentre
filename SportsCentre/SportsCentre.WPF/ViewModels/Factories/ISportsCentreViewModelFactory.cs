using System;
using System.Collections.Generic;
using System.Text;

namespace SportsCentre.WPF.ViewModels.Factories
{
    public interface ISportsCentreViewModelFactory<T> where T : ViewModelBase
    {
        T CreateViewModel();
    }
}
