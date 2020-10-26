using SportsCentre.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace SportsCentre.WPF.Models
{
    public class TrainingInfo
    {
        public int Id { get; set; }
        public string Info { get; set; }

        public TrainingInfo() { }
        public TrainingInfo(Training training)
        {
            Id = training.Id;
            Info = $"Id: {training.Id}, Court: {training.Court.Name}, Description: {training.Description}";
        }
    }
}
