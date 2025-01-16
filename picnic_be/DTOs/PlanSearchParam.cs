namespace picnic_be.Dtos
{
    public class PlanSearchParam
    {
        public string? Name { get; set; } = null;
        public int? PlaceId { get; set; } = null;
        public DateTime? StartTime { get; set; } = null;
        public DateTime? EndTime { get; set; } = null;
    }
}
