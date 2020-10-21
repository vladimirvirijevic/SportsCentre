using SportsCentre.Domain.Interfaces;
using SportsCentre.Domain.Models;
using SportsCentre.WPF.Commands;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Windows.Controls;
using System.Windows.Input;

namespace SportsCentre.WPF.ViewModels
{
    public class TrainingsViewModel : ViewModelBase
    {
        #region Private Properties
        private readonly ITrainingService _trainingService;
        private string messageText;
        private string messageForeground;
        private DateTime? date;
        private string description;
        private int duration;
        private ObservableCollection<Training> trainings = new ObservableCollection<Training>();
        #endregion

        #region Public Getters and Setters
        public string Description
        {
            get { return description; }
            set
            {
                description = value;
                OnPropertyChanged("Description");
            }
        }
        public string MessageText
        {
            get { return messageText; }
            set
            {
                messageText = value;
                OnPropertyChanged("MessageText");
            }
        }
        public string MessageForeground
        {
            get { return messageForeground; }
            set
            {
                messageForeground = value;
                OnPropertyChanged("MessageForeground");
            }
        }
        public DateTime? Date
        {
            get { return date; }
            set
            {
                date = value;
                OnPropertyChanged("Date");
            }
        }
        public int Duration
        {
            get { return duration; }
            set
            {
                duration = value;
                OnPropertyChanged("Duration");
            }
        }

        public ObservableCollection<Training> Trainings
        {
            get { return trainings; }
            set
            {
                trainings = value;
                OnPropertyChanged("Trainings");
            }
        }
        #endregion

        #region Commands
        public ICommand AddCommand { get; set; }
        public ICommand DeleteCommand { get; set; }
        #endregion

        public TrainingsViewModel(ITrainingService trainingService)
        {
            _trainingService = trainingService;

            Date = null;
            Duration = 0;
            MessageText = "";
            Description = "";

            AddCommand = new RelayCommand(new Action<object>(Add));
            DeleteCommand = new RelayCommand(new Action<object>(Delete));

            GetTrainings();
        }

        private void Add(object obj)
        {
            if (Date == null || Description == "" || Duration == 0)
            {
                MessageForeground = "Red";
                MessageText = "All fields are required!";
                return;
            }

            var time = Date.Value.TimeOfDay.ToString().Split(':');
            string selectedTime = time[0] + ":" + time[1];

            var entity = new Training
            {
                Date = Date.Value.Date.ToShortDateString(),
                Time = selectedTime,
                Duration = Duration,
                Description = Description
            };

            try
            {
                _trainingService.Add(entity);
                Date = null;
                Duration = 0;
                Description = "";

                MessageForeground = "Green";
                MessageText = "Training Successfully Added!";
                GetTrainings();
            }
            catch (Exception ex)
            {
                MessageForeground = "Red";
                MessageText = "There was an error!";
            }
        }
        private void GetTrainings()
        {
            trainings.Clear();
            var itemList = _trainingService.GetAll();

            itemList.ForEach(x => trainings.Add(x));
        }

        private void Delete(object obj)
        {
            MessageText = "";
            ListView gridView = (ListView)obj;

            if (gridView.SelectedItems.Count == 0)
            {
                return;
            }

            int id = ((Training)gridView.SelectedItems[0]).Id;

            bool result = _trainingService.Delete(id);

            if (!result)
            {
                MessageForeground = "Red";
                MessageText = "Can`t delete training";
                return;
            }

            GetTrainings();

            MessageForeground = "Green";
            MessageText = "Match deleted successfully!";
        }
    }
}
