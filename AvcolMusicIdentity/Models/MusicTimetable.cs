using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace AvcolMusicIdentity.Models
{
    public class MusicTimetable
    {
        public int MusicTimetableID { get; set; }
        [Required]
        [Display(Name = "Student")]
        public int StudentID { get; set; }
        [Required]
        [Display(Name = "Group")]
        public int GroupID { get; set; }

        public Group Group { get; set; }
        public Student Student { get; set; }
    }
}
