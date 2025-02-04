using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace picnic_be.Models
{
    public class Plan
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Column(TypeName = "nvarchar(30)")]
        public string Name { get; set; } = null!;
        [Column(TypeName = "nvarchar(500)")]
        public string Description { get; set; } = string.Empty;
        public int PlaceId { get; set; }
        [Column(TypeName = "datetimeoffset(0)")]
        public DateTimeOffset StartTime { get; set; }
        [Column(TypeName = "datetimeoffset(0)")]
        public DateTimeOffset EndTime { get; set; }
        public DateTimeOffset CreatedAt { get; set; }
        public DateTimeOffset UpdatedAt { get; set; }

        // Navigation Properties
        public ICollection<PlanFood>? Foods { get; set; }
        public ICollection<PlanTool>? Tools { get; set; }
        public ICollection<PlanUser>? Users { get; set; }
    }
}
