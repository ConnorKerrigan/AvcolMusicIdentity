using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace AvcolMusicIdentity.Models
{
    public class Group
    {
        public int GroupID { get; set; }
        [Required]
        [Display(Name = "Teacher")]
        public string TeacherID { get; set; }
        [Required]
        [StringLength(25)]
        [RegularExpression(@"^[A-Za-z]*$", ErrorMessage = "Only letters can be used")]
        public string Instrument { get; set; }
        [Required]
        [StringLength(3, ErrorMessage = "Please enter the three-letter room name (or two-letter in three letter format eg. A.2).")]
        [RegularExpression(@"^[A-Z\d\.]{3}$", ErrorMessage = "Please enter a three letter code in all caps (or two-letter in three letter format eg. A.2).")]
        public string Room { get; set; }
        [Required]
        [StringLength(8, ErrorMessage = "Enter valid day")]
        [RegularExpression(@"^[A-Za-z]*$", ErrorMessage = "Only letters can be used")]
        public string Day { get; set; }

        public Teacher Teacher { get; set; }
        public ICollection<MusicTimetable> MusicTimetables { get; set; }
        public ICollection<Lesson> Lessons { get; set; }
    }
}
