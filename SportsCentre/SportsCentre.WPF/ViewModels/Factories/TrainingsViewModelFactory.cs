using SportsCentre.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace SportsCentre.WPF.ViewModels.Factories
{
    public class TrainingsViewModelFactory : ISportsCentreViewModelFactory<TrainingsViewModel>
    {
        private readonly ITrainingService _trainingService;

        public TrainingsViewModelFactory(ITrainingService trainingService)
        {
            _trainingService = trainingService;
        }

        public TrainingsViewModel CreateViewModel()
        {
            return new TrainingsViewModel(_trainingService);
        }
    }
}
