namespace picnic_be.Models
{
    public class PlanFood : PlanItem
    {
        public ICollection<PreparerFood>? FoodPreparers { get; set; }
    }
}
