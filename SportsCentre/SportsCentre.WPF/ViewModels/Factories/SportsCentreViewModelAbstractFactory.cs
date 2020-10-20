using SportsCentre.WPF.State.Navigators;
using System;

namespace SportsCentre.WPF.ViewModels.Factories
{
    public class SportsCentreViewModelAbstractFactory : ISportsCentreViewModelAbstractFactory
    {
        private ISportsCentreViewModelFactory<CentresViewModel> _centresViewModelFactory;
        private ISportsCentreViewModelFactory<CourtsViewModel> _courtsViewModelFactory;

        public SportsCentreViewModelAbstractFactory(
                ISportsCentreViewModelFactory<CentresViewModel> centresViewModelFactory,
                ISportsCentreViewModelFactory<CourtsViewModel> courtsViewModelFactory
        )
        {
            _centresViewModelFactory = centresViewModelFactory;
            _courtsViewModelFactory = courtsViewModelFactory;
        }

        public ViewModelBase CreateViewModel(ViewType viewType)
        {
            switch (viewType)
            {
                case ViewType.Centres:
                    return _centresViewModelFactory.CreateViewModel();
                case ViewType.Courts:
                    return _courtsViewModelFactory.CreateViewModel();
                default:
                    throw new ArgumentException("The view type does not have a ViewModel", "viewType ");
            }
        }
    }
}
