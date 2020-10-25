using Microsoft.EntityFrameworkCore;
using SportsCentre.Data;
using SportsCentre.Domain.Interfaces;
using SportsCentre.Domain.Models;
using SportsCentre.WPF.Commands;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Windows.Controls;
using System.Windows.Input;

namespace SportsCentre.WPF.ViewModels
{
    public class MatchesViewModel : ViewModelBase
    {
        #region Private Properties
        private string messageText;
        private string messageForeground;
        private DateTime? date;
        private string selectedType;
        private int duration;
        private int selectedClubId1;
        private int selectedClubId2;
        private int selectedCourtId;
        private ObservableCollection<Match> matches = new ObservableCollection<Match>();
        private ObservableCollection<MatchInfo> matchesInfo = new ObservableCollection<MatchInfo>();
        private ObservableCollection<Court> courts = new ObservableCollection<Court>();
        private ObservableCollection<Club> clubs = new ObservableCollection<Club>();
        private ObservableCollection<string> types = new ObservableCollection<string>();
        #endregion

        #region Public Getters and Setters
        public int SelectedCourtId
        {
            get { return selectedCourtId; }
            set
            {
                selectedCourtId = value;
                OnPropertyChanged("SelectedCourtId");
            }
        }

        public ObservableCollection<Court> Courts
        {
            get { return courts; }
            set
            {
                courts = value;
                OnPropertyChanged("Courts");
            }
        }
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

        public int SelectedClubId1
        {
            get { return selectedClubId1; }
            set
            {
                selectedClubId1 = value;
                OnPropertyChanged("SelectedClubId1");
            }
        }

        public int SelectedClubId2
        {
            get { return selectedClubId2; }
            set
            {
                selectedClubId2 = value;
                OnPropertyChanged("SelectedClubId2");
            }
        }

        public ObservableCollection<MatchInfo> MatchesInfo
        {
            get { return matchesInfo; }
            set
            {
                matchesInfo = value;
                OnPropertyChanged("MatchesInfo");
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

        public ObservableCollection<Club> Clubs
        {
            get { return clubs; }
            set
            {
                clubs = value;
                OnPropertyChanged("Clubs");
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

        public MatchesViewModel()
        {
            Date = null;
            Duration = 0;
            MessageText = "";
            SelectedType = "";
            SelectedClubId1 = -1;
            SelectedClubId2 = -1;
            SelectedCourtId = -1;

            AddCommand = new RelayCommand(new Action<object>(Add));
            DeleteCommand = new RelayCommand(new Action<object>(Delete));

            types.Add("Friendly");
            types.Add("Regular");

            GetClubs();
            GetMatches();
            GetCourts();
        }

        private void Add(object obj)
        {
            if (Date == null || SelectedType == "" || Duration == 0 || SelectedClubId1 == -1 || SelectedClubId2 == -1 || SelectedCourtId == -1)
            {
                MessageForeground = "Red";
                MessageText = "All fields are required!";
                return;
            }

            var time = Date.Value.TimeOfDay.ToString().Split(':');
            string selectedTime = time[0] + ":" + time[1];

            try
            {
                using (var context = new SportsCentreDbContext())
                {
                    var court = context.Courts.Find(SelectedCourtId);

                    var entity = new Match
                    {
                        Date = Date.Value.Date.ToShortDateString(),
                        Time = selectedTime,
                        Duration = Duration,
                        Type = SelectedType,
                        FirstClubId = SelectedClubId1,
                        SecondClubId = SelectedClubId2,
                        Court = court
                    };

                    context.Matches.Add(entity);
                    context.SaveChanges();
                }

                Date = null;
                Duration = 0;
                SelectedClubId1 = -1;
                SelectedClubId2 = -1;

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
        private void GetCourts()
        {
            courts.Clear();

            List<Court> itemList = new List<Court>();
            using (var context = new SportsCentreDbContext())
            {
                itemList = context.Courts.ToList();
            }

            itemList.ForEach(x => courts.Add(x));
        }

        private void GetMatches()
        {
            matches.Clear();
            matchesInfo.Clear();

            List<Match> itemList = new List<Match>();
            List<MatchInfo> matchList = new List<MatchInfo>();

            using (var context = new SportsCentreDbContext())
            {
                itemList = context.Matches.Include(x => x.Court).ToList();

                foreach (Match m in itemList)
                {
                    matches.Add(m);

                    var firstClub = context.Clubs.Find(m.FirstClubId);
                    var secondClub = context.Clubs.Find(m.SecondClubId);

                    if (firstClub != null && secondClub != null)
                    {
                        var matchInfo = new MatchInfo(m);
                        matchInfo.FirstClub = firstClub;
                        matchInfo.SecondClub = secondClub;

                        matchesInfo.Add(matchInfo);
                    }
                }
            }
        }

        private void GetClubs()
        {
            clubs.Clear();

            List<Club> itemList = new List<Club>();
            using (var context = new SportsCentreDbContext())
            {
                itemList = context.Clubs.ToList();
            }

            itemList.ForEach(x => clubs.Add(x));
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

            try
            {
                using (var context = new SportsCentreDbContext())
                {
                    var match = context.Matches.Find(id);
                    context.Matches.Remove(match);
                    context.SaveChanges();
                }
            }
            catch (Exception ex)
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
