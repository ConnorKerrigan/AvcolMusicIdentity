using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace AvcolMusicIdentity.Models
{
    public class Student
    {
        public int StudentID { get; set; }
        [Required]
        [StringLength(30, ErrorMessage ="The name entered is too long.")]
        [RegularExpression(@"^[A-Za-z\']*$", ErrorMessage = "Only letters can be used")]
        public string Surname { get; set; }
        [Required]
        [Display(Name = "First Name")]
        [StringLength(30, ErrorMessage = "The name entered is too long.")]
        [RegularExpression(@"^[A-Za-z\']*$", ErrorMessage = "Only letters can be used")]
        public string FirstName { get; set; }
        [Required]
        [Range(9,13,ErrorMessage ="Please enter a valid year level for Avondale College (Ranges from 9 - 13).")]
        public int Year { get; set; }
        [Required]
        [Display(Name = "Home Room")]
        [StringLength(3, ErrorMessage ="Please enter the three-letter code for this student's homeroom teacher.")]
        [RegularExpression(@"^[A-Z]{3}$", ErrorMessage ="Please enter a three letter code in all caps.")]
        public string HomeRoom { get; set; }

        public ICollection<MusicTimetable> MusicTimetables { get; set; }
        public ICollection<Class> Classes { get; set; }
    }
}