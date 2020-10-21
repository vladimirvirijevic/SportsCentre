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
    public class PlayersViewModel : ViewModelBase
    {
        #region Private Properties
        private string messageText;
        private string messageForeground;
        private DateTime? birthdate;
        private string firstname;
        private string lastname;
        private string position;
        private int selectedClubId;

        private ObservableCollection<Club> clubs = new ObservableCollection<Club>();
        private ObservableCollection<Player> players = new ObservableCollection<Player>();
        #endregion

        #region Public Getters and Setters
        public int SelectedClubId
        {
            get { return selectedClubId; }
            set
            {
                selectedClubId = value;
                OnPropertyChanged("SelectedClubId");
            }
        }
        public string Firstname
        {
            get { return firstname; }
            set
            {
                firstname = value;
                OnPropertyChanged("Firstname");
            }
        }
        public string Lastname
        {
            get { return lastname; }
            set
            {
                lastname = value;
                OnPropertyChanged("Lastname");
            }
        }
        public string Position
        {
            get { return position; }
            set
            {
                position = value;
                OnPropertyChanged("Position");
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
        public DateTime? Birthdate
        {
            get { return birthdate; }
            set
            {
                birthdate = value;
                OnPropertyChanged("Birthdate");
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
        public ObservableCollection<Player> Players
        {
            get { return players; }
            set
            {
                players = value;
                OnPropertyChanged("Players");
            }
        }
        #endregion

        #region Commands
        public ICommand AddCommand { get; set; }
        public ICommand DeleteCommand { get; set; }
        #endregion

        public PlayersViewModel()
        {
            Birthdate = null;
            Firstname = "";
            Lastname = "";
            Position = "";
            SelectedClubId = -1;

            AddCommand = new RelayCommand(new Action<object>(Add));
            DeleteCommand = new RelayCommand(new Action<object>(Delete));

            GetClubs();
            GetPlayers();
        }

        private void Add(object obj)
        {
            if (Birthdate == null || Firstname == "" || Lastname == "" || Position == "" || SelectedClubId == -1)
            {
                MessageForeground = "Red";
                MessageText = "All fields are required!";
                return;
            }

            try
            {
                using (var context = new SportsCentreDbContext())
                {
                    Club club = context.Clubs.Find(SelectedClubId);

                    var entity = new Player
                    {
                        Birthdate = Birthdate.Value.Date.ToShortDateString(),
                        Firstname = Firstname,
                        Lastname = Lastname,
                        Position = Position,
                        Club = club
                    };

                    context.Players.Add(entity);
                    context.SaveChanges();
                }

                Birthdate = null;
                Firstname = "";
                Lastname = "";
                Position = "";

                MessageForeground = "Green";
                MessageText = "Player Successfully Added!";
                GetPlayers();
            }
            catch (Exception ex)
            {
                MessageForeground = "Red";
                MessageText = "There was an error!";
            }
        }

        private void GetPlayers()
        {
            players.Clear();

            List<Player> itemList = new List<Player>();
            using (var context = new SportsCentreDbContext())
            {
                itemList = context.Players.Include(x => x.Club).ToList();
            }

            itemList.ForEach(x => players.Add(x));
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

            int id = ((Player)gridView.SelectedItems[0]).Id;

            try
            {
                using (var context = new SportsCentreDbContext())
                {
                    var player = context.Players.Find(id);
                    context.Players.Remove(player);
                    context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                MessageForeground = "Red";
                MessageText = "Can`t delete player";
                return;
            }

            GetPlayers();

            MessageForeground = "Green";
            MessageText = "Player deleted successfully!";
        }
    }
}
