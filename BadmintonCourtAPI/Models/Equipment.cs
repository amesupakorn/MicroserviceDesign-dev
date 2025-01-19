namespace BadmintonCourtAPI.Models
{
    public class Equipment
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public bool IsBorrowed { get; set; }
        public string? BorrowedBy { get; set; } 
        public DateTime? BorrowedDate { get; set; } 
    }

}
