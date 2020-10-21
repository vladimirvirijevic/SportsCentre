using SportsCentre.Data;
using SportsCentre.Domain.Interfaces;
using SportsCentre.Domain.Models;
using SportsCentre.WPF.Commands;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace SportsCentre.WPF.ViewModels
{
    public class CentresViewModel : ViewModelBase
    {
        #region Private Properties
        private string messageText;
        private string messageForeground;
        private string name;
        private string country;
        private string city;
        private string address;
        private ObservableCollection<Centre> centres = new ObservableCollection<Centre>();

        #endregion

        #region Public Getters and Setters
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
        public string Address
        {
            get { return address; }
            set
            {
                address = value;
                OnPropertyChanged("Address");
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
        public ICommand AddCenterCommand { get; set; }
        public ICommand DeleteCentreCommand { get; set; }
        #endregion



        public CentresViewModel()
        {
            AddCenterCommand = new RelayCommand(new Action<object>(AddCentre));
            DeleteCentreCommand = new RelayCommand(new Action<object>(DeleteCentre));
            MessageText = "";

            Name = "";
            Country = "";
            City = "";
            Address = "";
            MessageForeground = "Green";
            GetCentres();
        }

        private void AddCentre(object obj)
        {
            if (Country == "" || City == "" || Address == "" || Name == "")
            {
                MessageForeground = "Red";
                MessageText = "All fields are required!";
                return;
            }

            var center = new Centre
            {
                Country = Country,
                City = City,
                Address = Address,
                Name = Name
            };

            try
            {
                using (var context = new SportsCentreDbContext())
                {
                    context.Centres.Add(center);
                    context.SaveChanges();
                }

                Country = "";
                City = "";
                Address = "";
                Name = "";

                MessageForeground = "Green";
                MessageText = "Sports Center Successfully Added!";
                GetCentres();
            }
            catch (Exception ex)
            {
                MessageForeground = "Red";
                MessageText = "There was an error!";
            }
        }

        private void DeleteCentre(object obj)
        {
            MessageText = "";
            ListView gridView = (ListView)obj;

            if (gridView.SelectedItems.Count == 0)
            {
                return;
            }

            int id = ((Centre)gridView.SelectedItems[0]).Id;

            try
            {
                using (var context = new SportsCentreDbContext())
                {
                    var centre = context.Centres.Find(id);
                    context.Centres.Remove(centre);
                    context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                MessageForeground = "Red";
                MessageText = "Can`t delete centre with courts";
                return;
            }

            GetCentres();

            MessageForeground = "Green";
            MessageText = "Centre deleted successfully!";
        }

        private void GetCentres()
        {
            centres.Clear();
            List<Centre> centersList = new List<Centre>();
            using (var context = new SportsCentreDbContext())
            {
                centersList = context.Centres.ToList();
            }

            centersList.ForEach(c => centres.Add(c));
        }
    }
}
