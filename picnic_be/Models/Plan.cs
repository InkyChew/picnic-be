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
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

        // Navigation Properties
        public ICollection<PlanFood>? Foods { get; set; }
        public ICollection<PlanTool>? Tools { get; set; }
        public ICollection<PlanUser>? Users { get; set; }
    }
}
