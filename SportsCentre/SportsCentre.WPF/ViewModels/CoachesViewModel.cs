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
    public class CoachesViewModel : ViewModelBase
    {
        #region Private Properties
        private string messageText;
        private string messageForeground;
        private DateTime? birthdate;
        private string firstname;
        private string lastname;
        private int selectedClubId;
        private string selectedType;
        private int duration;
        private ObservableCollection<Coach> coaches = new ObservableCollection<Coach>();
        private ObservableCollection<Club> clubs = new ObservableCollection<Club>();
        private ObservableCollection<string> types = new ObservableCollection<string>();
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

        public ObservableCollection<Coach> Coaches
        {
            get { return coaches; }
            set
            {
                coaches = value;
                OnPropertyChanged("Coaches");
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

        public CoachesViewModel()
        {
            Birthdate = null;
            MessageText = "";
            SelectedType = "";
            Firstname = "";
            Lastname = "";

            AddCommand = new RelayCommand(new Action<object>(Add));
            DeleteCommand = new RelayCommand(new Action<object>(Delete));

            types.Add("Support");
            types.Add("Regular");

            GetClubs();
            GetCoaches();
        }

        private void Add(object obj)
        {
            if (Birthdate == null || SelectedType == "" || Firstname == "" || Lastname == "")
            {
                MessageForeground = "Red";
                MessageText = "All fields are required!";
                return;
            }

            try
            {
                using (var context = new SportsCentreDbContext())
                {
                    var club = context.Clubs.Find(SelectedClubId);

                    var entity = new Coach
                    {
                        Birthdate = Birthdate.Value.Date.ToShortDateString(),
                        Firstname = Firstname,
                        Lastname = Lastname,
                        Type = SelectedType,
                        Club = club
                    };

                    context.Coaches.Add(entity);
                    context.SaveChanges();
                }

                Birthdate = null;
                MessageText = "";
                SelectedType = "";
                Firstname = "";
                Lastname = "";

                MessageForeground = "Green";
                MessageText = "Coach Successfully Added!";
                GetCoaches();
            }
            catch (Exception ex)
            {
                MessageForeground = "Red";
                MessageText = "There was an error!";
            }
        }
        private void GetCoaches()
        {
            coaches.Clear();

            List<Coach> itemList = new List<Coach>();
            using (var context = new SportsCentreDbContext())
            {
                itemList = context.Coaches.Include(x => x.Club).ToList();
            }

            itemList.ForEach(x => coaches.Add(x));
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

            int id = ((Coach)gridView.SelectedItems[0]).Id;

            try
            {
                using (var context = new SportsCentreDbContext())
                {
                    var coach = context.Coaches.Find(id);
                    context.Coaches.Remove(coach);
                    context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                MessageForeground = "Red";
                MessageText = "Can`t delete coach";
                return;
            }

            GetCoaches();

            MessageForeground = "Green";
            MessageText = "Coach deleted successfully!";
        }
    }
}
