using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorApp1.Models
{
    public class Character
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [Length(2, 50)]
        public string Name { get; set; } = "Frodo";

        [Range(0, 1000)]
        public int HitPoints { get; set; } = 100;

        [Range(0, 1000)]
        public int Strength { get; set; } = 10;

        [Range(0, 1000)]
        public int Defense { get; set; } = 10;

        [Range(0, 1000)]
        public int Intelligence { get; set; } = 10;

        public RPGClass Class { get; set; } = RPGClass.Knight;
    }
}