namespace BadmintonCourtAPI.Models
{
    public class Member
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public int Points { get; set; }
    }
}

