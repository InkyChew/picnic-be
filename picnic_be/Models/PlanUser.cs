namespace picnic_be.Models
{
    public class PlanUser
    {
        public int PlanId { get; set; }
        public int UserId { get; set; }
        public InvitationStatus Status { get; set; } = InvitationStatus.Pending;
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

        // Navigation Properties
        public Plan? Plan { get; set; }
        public User? User { get; set; }
    }
}
