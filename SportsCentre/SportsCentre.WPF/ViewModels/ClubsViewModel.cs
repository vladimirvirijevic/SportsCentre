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
    public class ClubsViewModel : ViewModelBase
    {
        #region Private Properties
        private string messageText;
        private string messageForeground;
        private DateTime? founded;
        private string name;
        private string country;
        private string city;
        private string sport;
        private ObservableCollection<Club> clubs = new ObservableCollection<Club>();
        #endregion

        #region Public Getters and Setters
        public string Name
        {
            get { return name; }
            set
            {
                name = value;
                OnPropertyChanged("Name");
            }
        }
        public string Country
        {
            get { return country; }
            set
            {
                country = value;
                OnPropertyChanged("Country");
            }
        }
        public string City
        {
            get { return city; }
            set
            {
                city = value;
                OnPropertyChanged("City");
            }
        }
        public string Sport
        {
            get { return sport; }
            set
            {
                sport = value;
                OnPropertyChanged("Sport");
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
        public DateTime? Founded
        {
            get { return founded; }
            set
            {
                founded = value;
                OnPropertyChanged("Founded");
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
        #endregion

        #region Commands
        public ICommand AddCommand { get; set; }
        public ICommand DeleteCommand { get; set; }
        #endregion

        public ClubsViewModel()
        {
            Founded = null;
            Name = "";
            Country = "";
            City = "";
            Sport = "";

            AddCommand = new RelayCommand(new Action<object>(Add));
            DeleteCommand = new RelayCommand(new Action<object>(Delete));

            GetClubs();
        }

        private void Add(object obj)
        {
            if (Founded == null || Name == "" || Country == "" || City == "" || Sport == "")
            {
                MessageForeground = "Red";
                MessageText = "All fields are required!";
                return;
            }

            try
            {
                using (var context = new SportsCentreDbContext())
                {
                    var entity = new Club
                    {
                        Founded = Founded.Value.Date.ToShortDateString(),
                        Name = Name,
                        Country = Country,
                        City = City,
                        Sport = Sport
                    };

                    context.Clubs.Add(entity);
                    context.SaveChanges();
                }

                Founded = null;
                Name = "";
                Country = "";
                City = "";
                Sport = "";

                MessageForeground = "Green";
                MessageText = "Club Successfully Added!";
                GetClubs();
            }
            catch (Exception ex)
            {
                MessageForeground = "Red";
                MessageText = "There was an error!";
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

            int id = ((Club)gridView.SelectedItems[0]).Id;

            try
            {
                using (var context = new SportsCentreDbContext())
                {
                    var club = context.Clubs.Find(id);
                    context.Clubs.Remove(club);
                    context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                MessageForeground = "Red";
                MessageText = "Can`t delete club";
                return;
            }

            GetClubs();

            MessageForeground = "Green";
            MessageText = "Club deleted successfully!";
        }
    }
}
