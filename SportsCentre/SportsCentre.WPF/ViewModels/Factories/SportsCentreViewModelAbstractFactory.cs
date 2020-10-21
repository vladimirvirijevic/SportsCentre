using SportsCentre.WPF.State.Navigators;
using System;

namespace SportsCentre.WPF.ViewModels.Factories
{
    public class SportsCentreViewModelAbstractFactory : ISportsCentreViewModelAbstractFactory
    {
        private ISportsCentreViewModelFactory<CentresViewModel> _centresViewModelFactory;
        private ISportsCentreViewModelFactory<CourtsViewModel> _courtsViewModelFactory;
        private ISportsCentreViewModelFactory<MatchesViewModel> _matchesViewModelFactory;
        private ISportsCentreViewModelFactory<TrainingsViewModel> _trainingsViewModelFactory;

        public SportsCentreViewModelAbstractFactory(
                ISportsCentreViewModelFactory<CentresViewModel> centresViewModelFactory,
                ISportsCentreViewModelFactory<CourtsViewModel> courtsViewModelFactory,
                ISportsCentreViewModelFactory<MatchesViewModel> matchesViewModelFactory,
                ISportsCentreViewModelFactory<TrainingsViewModel> trainingsViewModelFactory
        )
        {
            _centresViewModelFactory = centresViewModelFactory;
            _courtsViewModelFactory = courtsViewModelFactory;
            _matchesViewModelFactory = matchesViewModelFactory;
            _trainingsViewModelFactory = trainingsViewModelFactory;
        }

        public ViewModelBase CreateViewModel(ViewType viewType)
        {
            switch (viewType)
            {
                case ViewType.Centres:
                    return _centresViewModelFactory.CreateViewModel();
                case ViewType.Courts:
                    return _courtsViewModelFactory.CreateViewModel();
                case ViewType.Matches:
                    return _matchesViewModelFactory.CreateViewModel();
                case ViewType.Trainings:
                    return _trainingsViewModelFactory.CreateViewModel();
                default:
                    throw new ArgumentException("The view type does not have a ViewModel", "viewType ");
            }
        }
    }
}
