﻿using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace picnic_be.Models
{
    public class PlanItem
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public int PlanId { get; set; }
        [Column(TypeName = "nvarchar(30)")]
        public string Name { get; set; } = null!;
        [Column(TypeName = "nvarchar(500)")]
        public string Note { get; set; } = string.Empty;
        public bool Prepared { get; set; } = false;
        [Column(TypeName = "money")]
        public decimal Price { get; set; } = decimal.Zero;
        public int Count { get; set; } = 1;
        public DateTimeOffset CreatedAt { get; set; }
        public DateTimeOffset UpdatedAt { get; set; }

        // Navigation Properties
        public ICollection<PlanUser> Preparers { get; set; } = [];
        public Plan? Plan { get; set; }
    }
}
