namespace mvctk.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("course")]
    public partial class course
    {
        [Key]
        [Required]
        [StringLength(50)]
        public string ID { get; set; }

        [Required]
        [StringLength(50)]
        public string Name { get; set; }

        [Required]
        public int? Points { get; set; }

        [Required]
        [StringLength(50)]
        public string ExamA { get; set; }

        [Required]
        [StringLength(50)]
        public string ExamB { get; set; }

        [Required]
        [StringLength(50)]
        public string LecturerID { get; set; }

        [Required]
        [StringLength(50)]
        public string Time { get; set; }

        [Required]
        [StringLength(50)]
        public string Day { get; set; }

        [Required]
        [StringLength(50)]
        public string ClassRoom { get; set; }
    }
}
