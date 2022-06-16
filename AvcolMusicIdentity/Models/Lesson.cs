using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace AvcolMusicIdentity.Models
{
    public class Lesson
    {
        public int LessonID { get; set; }
        [Required]
        [Display(Name = "Group")]
        public int GroupID { get; set; }
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

        public Group Group { get; set; }
    }
}
