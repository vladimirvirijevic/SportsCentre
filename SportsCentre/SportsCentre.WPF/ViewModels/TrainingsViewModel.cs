using Microsoft.EntityFrameworkCore;
using SportsCentre.Data;
using SportsCentre.Domain.Interfaces;
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
    public class TrainingsViewModel : ViewModelBase
    {
        #region Private Properties
        private string messageText;
        private string messageForeground;
        private DateTime? date;
        private string description;
        private int duration;
        private ObservableCollection<Training> trainings = new ObservableCollection<Training>();
        private ObservableCollection<Player> players = new ObservableCollection<Player>();

        private CheckableObservableCollection<string> items = new CheckableObservableCollection<string>();
        private CheckableObservableCollection<PlayerInfo> itemsPlayersInfo = new CheckableObservableCollection<PlayerInfo>();
        #endregion

        #region Public Getters and Setters
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

        public TrainingsViewModel()
        {
            Date = null;
            Duration = 0;
            MessageText = "";
            Description = "";

            AddCommand = new RelayCommand(new Action<object>(Add));
            DeleteCommand = new RelayCommand(new Action<object>(Delete));

            ItemsPlayersInfo = new CheckableObservableCollection<PlayerInfo>();

            GetPlayers();
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

            var time = Date.Value.TimeOfDay.ToString().Split(':');
            string selectedTime = time[0] + ":" + time[1];

            try
            {
                using (var context = new SportsCentreDbContext())
                {
                    var entity = new Training
                    {
                        Date = Date.Value.Date.ToShortDateString(),
                        Time = selectedTime,
                        Duration = Duration,
                        Description = Description
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

            List<Training> itemList = new List<Training>();
            using (var context = new SportsCentreDbContext())
            {
                itemList = context.Trainings.ToList();
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

        private void Delete(object obj)
        {
            MessageText = "";
            ListView gridView = (ListView)obj;

            if (gridView.SelectedItems.Count == 0)
            {
                return;
            }

            int id = ((Training)gridView.SelectedItems[0]).Id;

            try
            {
                using (var context = new SportsCentreDbContext())
                {
                    var training = context.Trainings.Find(id);
                    context.Trainings.Remove(training);
                    context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                MessageForeground = "Red";
                MessageText = "Can`t delete training";
                return;
            }

            GetTrainings();

            MessageForeground = "Green";
            MessageText = "Training deleted successfully!";
        }
    }
}
