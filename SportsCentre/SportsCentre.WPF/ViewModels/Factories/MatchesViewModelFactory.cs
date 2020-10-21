using SportsCentre.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace SportsCentre.WPF.ViewModels.Factories
{
    public class MatchesViewModelFactory : ISportsCentreViewModelFactory<MatchesViewModel>
    {
        private readonly IMatchService _matchService;

        public MatchesViewModelFactory(IMatchService matchService)
        {
            _matchService = matchService;
        }

        public MatchesViewModel CreateViewModel()
        {
            return new MatchesViewModel(_matchService);
        }
    }
}
