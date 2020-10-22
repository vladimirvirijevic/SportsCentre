using Microsoft.EntityFrameworkCore;
using SportsCentre.Data;
using SportsCentre.Domain.Models;
using SportsCentre.WPF.Commands;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Windows.Input;

namespace SportsCentre.WPF.ViewModels
{
    public class SellTicketViewModel : ViewModelBase
    {
        #region Private Properties
        private string messageText;
        private int selectedTicketId;
        private int selectedGuestId;
        private string messageForeground;

        private ObservableCollection<Ticket> tickets = new ObservableCollection<Ticket>();
        private ObservableCollection<Ticket> ticketsToSell = new ObservableCollection<Ticket>();
        private ObservableCollection<Guest> guests = new ObservableCollection<Guest>();
        #endregion

        #region Public Getters and Setters
        public int SelectedTicketId
        {
            get { return selectedTicketId; }
            set
            {
                selectedTicketId = value;
                OnPropertyChanged("SelectedTicketId");
            }
        }

        public int SelectedGuestId
        {
            get { return selectedGuestId; }
            set
            {
                selectedGuestId = value;
                OnPropertyChanged("SelectedGuestId");
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


        public ObservableCollection<Ticket> Tickets
        {
            get { return tickets; }
            set
            {
                tickets = value;
                OnPropertyChanged("Tickets");
            }
        }

        public ObservableCollection<Ticket> TicketsToSell
        {
            get { return ticketsToSell; }
            set
            {
                ticketsToSell = value;
                OnPropertyChanged("TicketsToSell");
            }
        }
        #endregion

        #region Commands
        public ICommand AddCommand { get; set; }
        //public ICommand DeleteCommand { get; set; }
        #endregion

        public SellTicketViewModel()
        {
            SelectedGuestId = -1;
            SelectedTicketId = -1;

            AddCommand = new RelayCommand(new Action<object>(Add));
            //DeleteCommand = new RelayCommand(new Action<object>(Delete));

            GetGuests();
            GetTickets();
            GetSellTickets();
        }

        private void Add(object obj)
        {
            if (SelectedGuestId == -1 || SelectedTicketId == -1)
            {
                MessageForeground = "Red";
                MessageText = "All fields are required!";
                return;
            }

            try
            {
                using (var context = new SportsCentreDbContext())
                {
                    Ticket ticket = context.Tickets.Find(SelectedTicketId);
                    Guest guest = context.Guests.Find(SelectedGuestId);

                    ticket.Guest = guest;

                    context.SaveChanges();

                    SelectedGuestId = -1;
                    SelectedTicketId = -1;
                }

                MessageForeground = "Green";
                MessageText = "Ticket Sold Successfully!";

                GetSellTickets();
                GetTickets();
                GetGuests();
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
                itemList = context.Tickets.Include(x => x.Guest).Include(x => x.Match).ToList();
            }

            itemList.ForEach(x => tickets.Add(x));
        }

        private void GetSellTickets()
        {
            ticketsToSell.Clear();

            List<Ticket> itemList = new List<Ticket>();
            using (var context = new SportsCentreDbContext())
            {
                itemList = context.Tickets.Include(x => x.Guest).Where(x => x.Guest == null).ToList();
            }

            itemList.ForEach(x => ticketsToSell.Add(x));
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
    }
}
