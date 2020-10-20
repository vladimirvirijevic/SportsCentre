using SportsCentre.WPF.State.Navigators;

namespace SportsCentre.WPF.ViewModels.Factories
{
    public interface ISportsCentreViewModelAbstractFactory
    {
        ViewModelBase CreateViewModel(ViewType viewType);
    }
}
