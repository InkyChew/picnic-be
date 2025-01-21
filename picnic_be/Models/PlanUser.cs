namespace picnic_be.Models
{
    public class PlanUser
    {
        public int PlanId { get; set; }
        public int UserId { get; set; }
        public bool IsHost { get; set; } = false;
        public InvitationStatus Status { get; set; } = InvitationStatus.Pending;
        public DateTimeOffset CreatedAt { get; set; }
        public DateTimeOffset UpdatedAt { get; set; }

        // Navigation Properties
        public Plan? Plan { get; set; }
        public User? User { get; set; }
    }
}
