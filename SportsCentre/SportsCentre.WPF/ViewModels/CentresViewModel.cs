using SportsCentre.Domain.Interfaces;
using SportsCentre.Domain.Models;
using SportsCentre.WPF.Commands;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace SportsCentre.WPF.ViewModels
{
    public class CentresViewModel : ViewModelBase
    {
        #region Private Properties
        private readonly ICentreService _centreService;
        private string messageText;
        private string name;
        private string country;
        private string city;
        private string address;

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
        #endregion

        #region Commands
        public ICommand AddCenterCommand { get; set; }
        #endregion



        public CentresViewModel(ICentreService centreService)
        {
            _centreService = centreService;

            AddCenterCommand = new RelayCommand(new Action<object>(AddCentre));
            MessageText = "";

            Name = "";
            Country = "";
            City = "";
            Address = "";
        }

        private void AddCentre(object obj)
        {
            if (Country == "" || City == "" || Address == "" || Name == "")
            {
                MessageText = "All fields are required!";

                if ((TextBlock)obj != null)
                {
                    ((TextBlock)obj).Foreground = Brushes.Red;
                    return;
                }
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
                _centreService.Add(center);
                Country = "";
                City = "";
                Address = "";
                Name = "";

                if ((TextBlock)obj != null)
                {
                    ((TextBlock)obj).Foreground = Brushes.Green;
                }
                MessageText = "Sports Center Successfully Added!";
            }
            catch (Exception)
            {
                if ((TextBlock)obj != null)
                {
                    MessageText = "There was an error!";
                    ((TextBlock)obj).Foreground = Brushes.Red;
                }
            }
        }
    }
}
