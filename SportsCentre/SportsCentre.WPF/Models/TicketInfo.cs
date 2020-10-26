using SportsCentre.Domain.Models;

namespace SportsCentre.WPF.Models
{
    public class TicketInfo
    {
        public int Id { get; set; }
        public string Info { get; set; }

        public TicketInfo() { }
        public TicketInfo(Ticket ticket)
        {
            Id = ticket.Id;
        }
    }
}
