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
        [Column(TypeName = "date")]
        public DateTime ExamA { get; set; }

        [Required]
        [Column(TypeName = "date")]
        public DateTime ExamB { get; set; }

        [Required]
        [StringLength(50)]
        public string LecturerID { get; set; }

        [Required]
        [StringLength(50)]
        [RegularExpression(@"^(?:[01][0-9]|2[0-3]):[0][0]$",ErrorMessage ="you must enter a time like hh:00")]
        public string startlec { get; set; }

        [Required]
        [StringLength(50)]
        public string endlec { get; set; }

        [Required]
        [StringLength(50)]
        [RegularExpression(@"^sunday|monday|tuesday|wednesday|thursday|friday$",ErrorMessage ="Day=sunday-friday")]
        public string Day { get; set; }

        [Required]
        [StringLength(50)]
        public string ClassRoom { get; set; }
    }
}
