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
    public class PermitsViewModel : ViewModelBase
    {
        #region Private Properties
        private string messageText;
        private int selectedTrainingId;
        private string messageForeground;

        private ObservableCollection<Permit> permits = new ObservableCollection<Permit>();
        private ObservableCollection<Training> trainings = new ObservableCollection<Training>();
        #endregion

        #region Public Getters and Setters
        public int SelectedTrainingId
        {
            get { return selectedTrainingId; }
            set
            {
                selectedTrainingId = value;
                OnPropertyChanged("SelectedTrainingId");
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

        public ObservableCollection<Permit> Permits
        {
            get { return permits; }
            set
            {
                permits = value;
                OnPropertyChanged("Permits");
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

        public PermitsViewModel()
        {
            SelectedTrainingId = -1;

            AddCommand = new RelayCommand(new Action<object>(Add));
            DeleteCommand = new RelayCommand(new Action<object>(Delete));

            GetPermits();
            GetTrainings();
        }

        private void Add(object obj)
        {
            if (SelectedTrainingId == -1)
            {
                MessageForeground = "Red";
                MessageText = "All fields are required!";
                return;
            }

            try
            {
                using (var context = new SportsCentreDbContext())
                {
                    Training training = context.Trainings.Find(SelectedTrainingId);

                    var entity = new Permit
                    {
                        Training = training
                    };

                    context.Permits.Add(entity);
                    context.SaveChanges();
                }

                MessageForeground = "Green";
                MessageText = "Permit Successfully Added!";
                GetPermits();
            }
            catch (Exception ex)
            {
                MessageForeground = "Red";
                MessageText = "There was an error!";
            }
        }

        private void GetPermits()
        {
            permits.Clear();

            List<Permit> itemList = new List<Permit>();
            using (var context = new SportsCentreDbContext())
            {
                itemList = context.Permits.Include(x => x.Training).ToList();
            }

            itemList.ForEach(x => permits.Add(x));
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

        private void Delete(object obj)
        {
            MessageText = "";
            ListView gridView = (ListView)obj;

            if (gridView.SelectedItems.Count == 0)
            {
                return;
            }

            int id = ((Permit)gridView.SelectedItems[0]).Id;

            try
            {
                using (var context = new SportsCentreDbContext())
                {
                    var permit = context.Permits.Find(id);
                    context.Permits.Remove(permit);
                    context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                MessageForeground = "Red";
                MessageText = "Can`t delete permit";
                return;
            }

            GetPermits();

            MessageForeground = "Green";
            MessageText = "Permit deleted successfully!";
        }
    }
}
