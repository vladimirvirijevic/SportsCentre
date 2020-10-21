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
    public class MatchesViewModel : ViewModelBase
    {
        #region Private Properties
        private readonly IMatchService _matchService;
        private string messageText;
        private string messageForeground;
        private DateTime? date;
        private string selectedType;
        private int duration;
        private ObservableCollection<Match> matches = new ObservableCollection<Match>();
        private ObservableCollection<string> types = new ObservableCollection<string>();
        #endregion

        #region Public Getters and Setters
        public string SelectedType
        {
            get { return selectedType; }
            set
            {
                selectedType = value;
                OnPropertyChanged("SelectedType");
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

        public ObservableCollection<Match> Matches
        {
            get { return matches; }
            set
            {
                matches = value;
                OnPropertyChanged("Matches");
            }
        }

        public ObservableCollection<string> Types
        {
            get { return types; }
            set
            {
                types = value;
                OnPropertyChanged("Types");
            }
        }
        #endregion

        #region Commands
        public ICommand AddCommand { get; set; }
        public ICommand DeleteCommand { get; set; }
        #endregion

        public MatchesViewModel(IMatchService matchService)
        {
            _matchService = matchService;

            Date = null;
            Duration = 0;
            MessageText = "";
            SelectedType = "";

            AddCommand = new RelayCommand(new Action<object>(Add));
            DeleteCommand = new RelayCommand(new Action<object>(Delete));

            types.Add("Friendly");
            types.Add("Regular");

            GetMatches();
        }

        private void Add(object obj)
        {
            if (Date == null || SelectedType == "" || Duration == 0)
            {
                MessageForeground = "Red";
                MessageText = "All fields are required!";
                return;
            }

            var time = Date.Value.TimeOfDay.ToString().Split(':');
            string selectedTime = time[0] + ":" + time[1];

            var entity = new Match
            {
                Date = Date.Value.Date.ToShortDateString(),
                Time = selectedTime,
                Duration = Duration,
                Type = SelectedType
            };

            try
            {
                _matchService.Add(entity);
                Date = null;
                Duration = 0;

                MessageForeground = "Green";
                MessageText = "Match Successfully Added!";
                GetMatches();
            }
            catch (Exception ex)
            {
                MessageForeground = "Red";
                MessageText = "There was an error!";
            }
        }
        private void GetMatches()
        {
            matches.Clear();
            var itemList = _matchService.GetAll();

            itemList.ForEach(x => matches.Add(x));
        }

        private void GetCentres()
        {
            matches.Clear();
            var itemList = _matchService.GetAll();

            itemList.ForEach(x => matches.Add(x));
        }


        private void Delete(object obj)
        {
            MessageText = "";
            ListView gridView = (ListView)obj;

            if (gridView.SelectedItems.Count == 0)
            {
                return;
            }

            int id = ((Match)gridView.SelectedItems[0]).Id;

            bool result = _matchService.Delete(id);

            if (!result)
            {
                MessageForeground = "Red";
                MessageText = "Can`t delete match";
                return;
            }

            GetMatches();

            MessageForeground = "Green";
            MessageText = "Match deleted successfully!";
        }
    }
}
