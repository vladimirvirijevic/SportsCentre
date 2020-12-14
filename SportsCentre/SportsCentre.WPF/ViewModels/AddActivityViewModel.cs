using Microsoft.EntityFrameworkCore;
using SportsCentre.Data;
using SportsCentre.Domain.Models;
using SportsCentre.WPF.Commands;
using SportsCentre.WPF.Controls.CheckboxList;
using SportsCentre.WPF.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Windows.Controls;
using System.Windows.Input;

namespace SportsCentre.WPF.ViewModels
{
    public class AddActivityViewModel : ViewModelBase
    {
        #region Private Properties
        private string messageText;
        private string messageForeground;
        private DateTime? date;
        private string selectedType;
        private string selectedTimeMatch;
        private string duration;
        private int selectedClubId1;
        private int selectedClubId2;
        private int selectedCourtId;
        private ObservableCollection<Match> matches = new ObservableCollection<Match>();
        private ObservableCollection<MatchInfo> matchesInfo = new ObservableCollection<MatchInfo>();
        private ObservableCollection<Court> courts = new ObservableCollection<Court>();
        private ObservableCollection<Club> clubs = new ObservableCollection<Club>();
        private ObservableCollection<string> types = new ObservableCollection<string>();

        private ObservableCollection<string> startTimeMatch = new ObservableCollection<string>();
        private ObservableCollection<string> durations = new ObservableCollection<string>();

        // Trainings
        private string messageTextTraining;
        private DateTime? dateTraining;
        private string description;
        private string durationTraining;
        private int selectedCourtIdTraining;
        private string selectedTimeTraining;
        private ObservableCollection<Training> trainings = new ObservableCollection<Training>();
        private ObservableCollection<Player> players = new ObservableCollection<Player>();

        private CheckableObservableCollection<string> items = new CheckableObservableCollection<string>();
        private CheckableObservableCollection<PlayerInfo> itemsPlayersInfo = new CheckableObservableCollection<PlayerInfo>();
        private ObservableCollection<string> startTimeTraining = new ObservableCollection<string>();


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
        public string SelectedTimeMatch
        {
            get { return selectedTimeMatch; }
            set
            {
                selectedTimeMatch = value;
                OnPropertyChanged("SelectedTimeMatch");
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
        public string Duration
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

        public ObservableCollection<string> Durations
        {
            get { return durations; }
            set
            {
                durations = value;
                OnPropertyChanged("Durations");
            }
        }

        public ObservableCollection<string> TimesMatches
        {
            get { return startTimeMatch; }
            set
            {
                startTimeMatch = value;
                OnPropertyChanged("TimesMatches");
            }
        }

        // Trainings
        public string SelectedTimeTraining
        {
            get { return selectedTimeTraining; }
            set
            {
                selectedTimeTraining = value;
                OnPropertyChanged("SelectedTimeTraining");
            }
        }
        public CheckableObservableCollection<string> Items
        {
            get { return items; }
            set
            {
                items = value;
                OnPropertyChanged("Items");
            }
        }

        public CheckableObservableCollection<PlayerInfo> ItemsPlayersInfo
        {
            get { return itemsPlayersInfo; }
            set
            {
                itemsPlayersInfo = value;
                OnPropertyChanged("ItemsPlayersInfo");
            }
        }
        public string Description
        {
            get { return description; }
            set
            {
                description = value;
                OnPropertyChanged("Description");
            }
        }
        public string MessageTextTraining
        {
            get { return messageTextTraining; }
            set
            {
                messageTextTraining = value;
                OnPropertyChanged("MessageTextTraining");
            }
        }
        
        public DateTime? DateTraining
        {
            get { return dateTraining; }
            set
            {
                dateTraining = value;
                OnPropertyChanged("DateTraining");
            }
        }
        public string DurationTraining
        {
            get { return durationTraining; }
            set
            {
                durationTraining = value;
                OnPropertyChanged("DurationTraining");
            }
        }

        public int SelectedCourtIdTraining
        {
            get { return selectedCourtIdTraining; }
            set
            {
                selectedCourtIdTraining = value;
                OnPropertyChanged("SelectedCourtIdTraining");
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
        public ICommand AddMatchCommand { get; set; }
        public ICommand AddTrainingCommand { get; set; }
        #endregion

        public AddActivityViewModel()
        {
            Date = null;
            Duration = "";
            MessageText = "";
            SelectedType = "";
            SelectedClubId1 = -1;
            SelectedClubId2 = -1;
            SelectedCourtId = -1;

            SelectedTimeMatch = "";
            SelectedTimeTraining = "";

            DateTraining = null;
            DurationTraining = "";
            MessageTextTraining = "";
            Description = "";
            SelectedCourtIdTraining = -1;

            AddMatchCommand = new RelayCommand(new Action<object>(AddMatch));
            AddTrainingCommand = new RelayCommand(new Action<object>(AddTraining));

            types.Add("Friendly");
            types.Add("Regular");

            SetTimes();
            GetClubs();
            GetMatches();
            GetCourts();
            GetPlayers();
            GetTrainings();
        }
        private void AddTraining(object obj)
        {
            if (DateTraining == null || Description == "" || DurationTraining == "" || SelectedCourtIdTraining == -1)
            {
                MessageForeground = "Red";
                MessageText = "All fields are required!";
                return;
            }

            // Selected Playes
            var list = (ListBox)obj;

            var items = list.Items.SourceCollection;

            List<int> playerIds = new List<int>();

            foreach (var item in items)
            {
                CheckWrapper<PlayerInfo> checkItem = (CheckWrapper<PlayerInfo>)item;

                if (checkItem.IsChecked)
                {
                    playerIds.Add(checkItem.Value.Id);
                }
            }

            try
            {
                using (var context = new SportsCentreDbContext())
                {
                    var court = context.Courts.Find(SelectedCourtIdTraining);

                    var entity = new Training
                    {
                        Date = DateTraining.Value.Date.ToShortDateString(),
                        Time = SelectedTimeMatch,
                        Duration =int.Parse(DurationTraining),
                        Description = Description,
                        Court = court
                    };

                    context.Trainings.Add(entity);
                    context.SaveChanges();

                    foreach (int playerId in playerIds)
                    {
                        TrainingPlayer trainingPlayer = new TrainingPlayer
                        {
                            TrainingId = entity.Id,
                            PlayerId = playerId
                        };

                        context.TrainingPlayers.Add(trainingPlayer);
                        context.SaveChanges();
                    }
                }

                DateTraining = null;
                DurationTraining = "";
                Description = "";

                MessageForeground = "Green";
                MessageTextTraining = "Training Successfully Added!";
                GetTrainings();
            }
            catch (Exception ex)
            {
                MessageForeground = "Red";
                MessageTextTraining = "There was an error!";
            }
        }
        private void GetTrainings()
        {
            trainings.Clear();

            List<Training> itemList = new List<Training>();
            using (var context = new SportsCentreDbContext())
            {
                itemList = context.Trainings.Include(x => x.Court).ToList();
            }

            itemList.ForEach(x => trainings.Add(x));
        }

        private void GetPlayers()
        {
            players.Clear();
            itemsPlayersInfo.Clear();

            List<Player> itemList = new List<Player>();
            using (var context = new SportsCentreDbContext())
            {
                itemList = context.Players.Include(x => x.Club).ToList();
            }

            itemList.ForEach(x => {
                players.Add(x);

                if (x.Club != null)
                {
                    itemsPlayersInfo.Add(new PlayerInfo(x));
                }
            });
        }

        private void AddMatch(object obj)
        {
            if (Date == null || SelectedType == "" || Duration == "" || SelectedClubId1 == -1 || SelectedClubId2 == -1 || SelectedCourtId == -1 || SelectedTimeMatch == "")
            {
                MessageForeground = "Red";
                MessageText = "All fields are required!";
                return;
            }

            try
            {
                using (var context = new SportsCentreDbContext())
                {
                    var court = context.Courts.Find(SelectedCourtId);

                    var entity = new Match
                    {
                        Date = Date.Value.Date.ToShortDateString(),
                        Time = SelectedTimeMatch,
                        Duration = int.Parse(Duration),
                        Type = SelectedType,
                        FirstClubId = SelectedClubId1,
                        SecondClubId = SelectedClubId2,
                        Court = court
                    };

                    context.Matches.Add(entity);
                    context.SaveChanges();
                }

                Date = null;
                Duration = "";
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

        private void SetTimes()
        {
            startTimeMatch.Add("08:00");
            startTimeMatch.Add("08:30");
            startTimeMatch.Add("09:00");
            startTimeMatch.Add("09:30");
            startTimeMatch.Add("10:00");
            startTimeMatch.Add("10:30");
            startTimeMatch.Add("11:00");
            startTimeMatch.Add("11:30");
            startTimeMatch.Add("12:00");
            startTimeMatch.Add("12:30");
            startTimeMatch.Add("13:00");
            startTimeMatch.Add("13:30");
            startTimeMatch.Add("14:00");
            startTimeMatch.Add("14:30");
            startTimeMatch.Add("15:00");
            startTimeMatch.Add("15:30");
            startTimeMatch.Add("16:00");
            startTimeMatch.Add("16:30");
            startTimeMatch.Add("17:00");
            startTimeMatch.Add("17:30");
            startTimeMatch.Add("18:00");
            startTimeMatch.Add("18:30");
            startTimeMatch.Add("19:00");
            startTimeMatch.Add("19:30");
            startTimeMatch.Add("20:00");
            startTimeMatch.Add("20:30");
            startTimeMatch.Add("21:00");
            startTimeMatch.Add("21:30");

            startTimeTraining.Add("08:00");
            startTimeTraining.Add("08:30");
            startTimeTraining.Add("09:00");
            startTimeTraining.Add("09:30");
            startTimeTraining.Add("10:00");
            startTimeTraining.Add("10:30");
            startTimeTraining.Add("11:00");
            startTimeTraining.Add("11:30");
            startTimeTraining.Add("12:00");
            startTimeTraining.Add("12:30");
            startTimeTraining.Add("13:00");
            startTimeTraining.Add("13:30");
            startTimeTraining.Add("14:00");
            startTimeTraining.Add("14:30");
            startTimeTraining.Add("15:00");
            startTimeTraining.Add("15:30");
            startTimeTraining.Add("16:00");
            startTimeTraining.Add("16:30");
            startTimeTraining.Add("17:00");
            startTimeTraining.Add("17:30");
            startTimeTraining.Add("18:00");
            startTimeTraining.Add("18:30");
            startTimeTraining.Add("19:00");
            startTimeTraining.Add("19:30");
            startTimeTraining.Add("20:00");
            startTimeTraining.Add("20:30");
            startTimeTraining.Add("21:00");
            startTimeTraining.Add("21:30");

            durations.Add("30");
            durations.Add("60");
            durations.Add("90");
            durations.Add("120");
            durations.Add("150");
            durations.Add("180");
            durations.Add("210");
        }
    }
}
