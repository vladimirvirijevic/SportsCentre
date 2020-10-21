using SportsCentre.Domain.Interfaces;

namespace SportsCentre.WPF.ViewModels.Factories
{
    public class CourtsViewModelFactory : ISportsCentreViewModelFactory<CourtsViewModel>
    {
        private readonly ICourtService _courtService;
        private readonly ICentreService _centreService;

        public CourtsViewModelFactory(ICourtService courtService, ICentreService centreService)
        {
            _courtService = courtService;
            _centreService = centreService;
        }

        public CourtsViewModel CreateViewModel()
        {
            return new CourtsViewModel(_courtService, _centreService);
        }
    }
}
