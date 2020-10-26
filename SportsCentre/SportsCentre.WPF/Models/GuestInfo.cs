using SportsCentre.Domain.Models;

namespace SportsCentre.WPF.Models
{
    public class GuestInfo
    {
        public int Id { get; set; }
        public string Info { get; set; }

        public GuestInfo() { }
        public GuestInfo(Guest guest)
        {
            Id = guest.Id;
            Info = $"Id: {guest.Id}, {guest.Firstname} {guest.Lastname}";
        }
    }
}
