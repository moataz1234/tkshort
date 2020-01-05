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
        [StringLength(50)]
        public string ID { get; set; }

        [StringLength(50)]
        public string Name { get; set; }

        public int? Points { get; set; }

        [StringLength(50)]
        public string ExamA { get; set; }

        [StringLength(50)]
        public string ExamB { get; set; }

        [StringLength(50)]
        public string LecturerID { get; set; }

        [StringLength(50)]
        public string Time { get; set; }

        [StringLength(50)]
        public string ClassRoom { get; set; }
    }
}
