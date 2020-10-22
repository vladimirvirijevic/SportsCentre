using Microsoft.EntityFrameworkCore;
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
    public class TicketsViewModel : ViewModelBase
    {
        #region Private Properties
        private string messageText;
        private int selectedMatchId;
        private int seatNumber;
        private double price;
        private string messageForeground;

        private ObservableCollection<Ticket> tickets = new ObservableCollection<Ticket>();
        private ObservableCollection<Match> matches = new ObservableCollection<Match>();
        #endregion

        #region Public Getters and Setters
        public int SelectedMatchId
        {
            get { return selectedMatchId; }
            set
            {
                selectedMatchId = value;
                OnPropertyChanged("SelectedMatchId");
            }
        }

        public int SeatNumber
        {
            get { return seatNumber; }
            set
            {
                seatNumber = value;
                OnPropertyChanged("SeatNumber");
            }
        }

        public double Price
        {
            get { return price; }
            set
            {
                price = value;
                OnPropertyChanged("Price");
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

        public ObservableCollection<Match> Matches
        {
            get { return matches; }
            set
            {
                matches = value;
                OnPropertyChanged("Matches");
            }
        }
        public ObservableCollection<Ticket> Tickets
        {
            get { return tickets; }
            set
            {
                tickets = value;
                OnPropertyChanged("Tickets");
            }
        }
        #endregion

        #region Commands
        public ICommand AddCommand { get; set; }
        public ICommand DeleteCommand { get; set; }
        #endregion

        public TicketsViewModel()
        {
            SelectedMatchId = -1;
            Price = 0;
            SeatNumber = 0;

            AddCommand = new RelayCommand(new Action<object>(Add));
            DeleteCommand = new RelayCommand(new Action<object>(Delete));

            GetMatches();
            GetTickets();
        }

        private void Add(object obj)
        {
            if (SelectedMatchId == -1 || Price == 0 || SeatNumber == 0)
            {
                MessageForeground = "Red";
                MessageText = "All fields are required!";
                return;
            }

            try
            {
                using (var context = new SportsCentreDbContext())
                {
                    Match match = context.Matches.Find(SelectedMatchId);

                    var entity = new Ticket
                    {
                        SeatNumber = SeatNumber,
                        Price = Price,
                        Match = match
                    };

                    context.Tickets.Add(entity);
                    context.SaveChanges();

                    SelectedMatchId = -1;
                    Price = 0;
                    SeatNumber = 0;
                }

                MessageForeground = "Green";
                MessageText = "Ticket Successfully Added!";
                GetTickets();
            }
            catch (Exception ex)
            {
                MessageForeground = "Red";
                MessageText = "There was an error!";
            }
        }

        private void GetTickets()
        {
            tickets.Clear();

            List<Ticket> itemList = new List<Ticket>();
            using (var context = new SportsCentreDbContext())
            {
                itemList = context.Tickets.Include(x => x.Match).ToList();
            }

            itemList.ForEach(x => tickets.Add(x));
        }

        private void GetMatches()
        {
            matches.Clear();

            List<Match> itemList = new List<Match>();
            using (var context = new SportsCentreDbContext())
            {
                itemList = context.Matches.ToList();
            }

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

            int id = ((Ticket)gridView.SelectedItems[0]).Id;

            try
            {
                using (var context = new SportsCentreDbContext())
                {
                    var ticket = context.Tickets.Find(id);
                    context.Tickets.Remove(ticket);
                    context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                MessageForeground = "Red";
                MessageText = "Can`t delete ticket";
                return;
            }

            GetTickets();

            MessageForeground = "Green";
            MessageText = "Ticket deleted successfully!";
        }
    }
}
