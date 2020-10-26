using SportsCentre.Domain.Models;

namespace SportsCentre.WPF.Models
{
    public class PlayerInfo
    {
        public int Id { get; set; }
        public string Info { get; set; }

        public PlayerInfo() { }
        public PlayerInfo(Player player) 
        {
            Id = player.Id;
            Info = $"Id: {player.Id}, Name: {player.Firstname} {player.Lastname}, Club: {player.Club.Name}";
        }
    }
}
