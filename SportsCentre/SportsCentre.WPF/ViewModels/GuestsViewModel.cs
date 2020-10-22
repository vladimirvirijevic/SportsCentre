using SportsCentre.Data;
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
    public class GuestsViewModel : ViewModelBase
    {
        #region Private Properties
        private string messageText;
        private string messageForeground;
        private string firstname;
        private string lastname;

        private ObservableCollection<Guest> guests = new ObservableCollection<Guest>();
        #endregion

        #region Public Getters and Setters
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

        public ObservableCollection<Guest> Guests
        {
            get { return guests; }
            set
            {
                guests = value;
                OnPropertyChanged("Guests");
            }
        }
        #endregion

        #region Commands
        public ICommand AddCommand { get; set; }
        public ICommand DeleteCommand { get; set; }
        #endregion

        public GuestsViewModel()
        {
            Firstname = "";
            Lastname = "";

            AddCommand = new RelayCommand(new Action<object>(Add));
            DeleteCommand = new RelayCommand(new Action<object>(Delete));

            GetGuests();
        }

        private void Add(object obj)
        {
            if (Firstname == "" || Lastname == "")
            {
                MessageForeground = "Red";
                MessageText = "All fields are required!";
                return;
            }

            try
            {
                using (var context = new SportsCentreDbContext())
                {
                    var entity = new Guest
                    {
                        Firstname = Firstname,
                        Lastname = Lastname,
                    };

                    context.Guests.Add(entity);
                    context.SaveChanges();
                }

                Firstname = "";
                Lastname = "";

                MessageForeground = "Green";
                MessageText = "Guest Successfully Added!";
                GetGuests();
            }
            catch (Exception ex)
            {
                MessageForeground = "Red";
                MessageText = "There was an error!";
            }
        }

        private void GetGuests()
        {
            guests.Clear();

            List<Guest> itemList = new List<Guest>();
            using (var context = new SportsCentreDbContext())
            {
                itemList = context.Guests.ToList();
            }

            itemList.ForEach(x => guests.Add(x));
        }

        private void Delete(object obj)
        {
            MessageText = "";
            ListView gridView = (ListView)obj;

            if (gridView.SelectedItems.Count == 0)
            {
                return;
            }

            int id = ((Guest)gridView.SelectedItems[0]).Id;

            try
            {
                using (var context = new SportsCentreDbContext())
                {
                    var guest = context.Guests.Find(id);
                    context.Guests.Remove(guest);
                    context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                MessageForeground = "Red";
                MessageText = "Can`t delete guest";
                return;
            }

            GetGuests();

            MessageForeground = "Green";
            MessageText = "Guest deleted successfully!";
        }
    }
}
