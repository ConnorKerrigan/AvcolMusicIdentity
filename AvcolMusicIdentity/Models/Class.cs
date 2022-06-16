using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace AvcolMusicIdentity.Models
{
    public class Class
    {
        public int ClassID { get; set; }
        [Required]
        [Display(Name = "Student")]
        public int StudentID { get; set; }
        [Required]
        [Display(Name = "Teacher")]
        public string TeacherID { get; set; }
        [Required]
        [DataType(DataType.Date)]
        public DateTime Date { get; set; }
        [Required]
        [DataType(DataType.Time)]
        [Display(Name = "Start Time")]
        public DateTime StartTime { get; set; }
        [Required]
        [DataType(DataType.Time)]
        [Display(Name = "End Time")]
        public DateTime EndTime { get; set; }

        public Student Student { get; set; }
        public Teacher Teacher { get; set; }
    }
}
