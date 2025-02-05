namespace picnic_be.Models
{
    public class PlanTool : PlanItem
    {
        public ICollection<PreparerTool>? ToolPreparers { get; set; }
    }
}
