using SportsCentre.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace SportsCentre.WPF.Models
{
    public class MemberInfo
    {
        public int Id { get; set; }
        public string Info { get; set; }

        public MemberInfo() { }
        public MemberInfo(Member member)
        {
            Id = member.Id;
            Info = $"Id: {Id}, {member.Firstname} {member.Lastname}";
        }
    }
}
