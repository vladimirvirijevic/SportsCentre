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
    public class CourtsViewModel : ViewModelBase
    {
        #region Private Properties
        private readonly ICourtService _courtService;
        private readonly ICentreService _centreService;
        private string messageText;
        private string messageForeground;
        private string name;
        private string description;
        private int capacity;
        private int selectedCenterId;
        private Centre selectedCenter;
        private ObservableCollection<Court> courts = new ObservableCollection<Court>();
        private ObservableCollection<Centre> centres = new ObservableCollection<Centre>();
        #endregion

        #region Public Getters and Setters
        public Centre SelectedCenter
        {
            get { return selectedCenter; }
            set
            {
                selectedCenter = value;
                OnPropertyChanged("SelectedCenter");
            }
        }
        public int SelectedCenterId
        {
            get { return selectedCenterId; }
            set
            {
                selectedCenterId = value;
                OnPropertyChanged("SelectedCenterId");
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
        public string Name
        {
            get { return name; }
            set
            {
                name = value;
                OnPropertyChanged("Name");
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
        public int Capacity
        {
            get { return capacity; }
            set
            {
                capacity = value;
                OnPropertyChanged("Capacity");
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

        public ObservableCollection<Centre> Centres
        {
            get { return centres; }
            set
            {
                centres = value;
                OnPropertyChanged("Centres");
            }
        }
        #endregion

        #region Commands
        public ICommand AddCommand { get; set; }
        public ICommand DeleteCommand { get; set; }
        #endregion

        public CourtsViewModel(ICourtService courtService, ICentreService centreService)
        {
            _courtService = courtService;
            _centreService = centreService;

            Name = "";
            Description = "";
            Capacity = 0;
            MessageText = "";
            SelectedCenterId = -1;

            AddCommand = new RelayCommand(new Action<object>(Add));
            DeleteCommand = new RelayCommand(new Action<object>(Delete));

            GetCourts();
            GetCentres();
        }

        private void Add(object obj)
        {
            if (Name == "" || Description == "" || Capacity == 0 || SelectedCenterId == -1)
            {
                MessageForeground = "Red";
                MessageText = "All fields are required!";
                return;
            }

            var entity = new Court
            {
                Name = Name,
                Description = Description,
                Capacity = Capacity,
                Centre = _centreService.Get(SelectedCenterId)
            };

            try
            {
                _courtService.Add(entity);
                Name = "";
                Description = "";
                Capacity = 0;

                MessageForeground = "Green";
                MessageText = "Court Successfully Added!";
                GetCourts();
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
            var itemList = _courtService.GetAll();

            itemList.ForEach(x => courts.Add(x));
        }

        private void GetCentres()
        {
            centres.Clear();
            var itemList = _centreService.GetAll();

            itemList.ForEach(x => centres.Add(x));
        }


        private void Delete(object obj)
        {
            MessageText = "";
            ListView gridView = (ListView)obj;

            if (gridView.SelectedItems.Count == 0)
            {
                return;
            }

            int id = ((Court)gridView.SelectedItems[0]).Id;

            bool result = _courtService.Delete(id);

            if (!result)
            {
                MessageForeground = "Red";
                MessageText = "Can`t delete court with activity";
                return;
            }

            GetCourts();

            MessageForeground = "Green";
            MessageText = "Court deleted successfully!";
        }
    }
}
