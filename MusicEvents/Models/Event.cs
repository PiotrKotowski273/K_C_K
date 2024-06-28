namespace MusicEvents.Models
{
    public class Event
    {
        public int Id { get; set; }
        public string? Title { get; set; }
        public string? Location { get; set; }
        public string? Genre { get; set; }
        public string? Artist { get; set; }
        public DateTime Date { get; set; }
        public decimal TicketPrice { get; set; }
        public int AvailableSeats { get; set; }
    }
}
