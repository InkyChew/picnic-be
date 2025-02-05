namespace picnic_be.Models
{
    public class PreparerFood
    {
        public int UserId { get; set; }
        public User? User { get; set; }
        public int PlanFoodId { get; set; }
        public PlanFood? PlanFood { get; set; }
    }
}
