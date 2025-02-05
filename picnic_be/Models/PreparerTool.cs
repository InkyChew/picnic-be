namespace picnic_be.Models
{
    public class PreparerTool
    {
        public int UserId { get; set; }
        public User? User { get; set; }
        public int PlanToolId { get; set; }
        public PlanTool? PlanTool { get; set; }
    }
}
