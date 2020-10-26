using SportsCentre.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace SportsCentre.WPF.Models
{
    public class PermitInfo
    {
        public int Id { get; set; }
        public string Info { get; set; }

        public PermitInfo() { }
        public PermitInfo(Permit permit)
        {
            Id = permit.Id;
            Info = $"Id: {Id}";
        }
    }
}
