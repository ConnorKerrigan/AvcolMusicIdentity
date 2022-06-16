using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace AvcolMusicIdentity.Models
{
    public class Teacher
    {
        [Required]
        [Display(Name = "Teacher ID")]
        [StringLength(3, ErrorMessage = "Please enter the three-letter code for this student's homeroom teacher.")]
        [RegularExpression(@"^[A-Z]{3}$", ErrorMessage = "Please enter a three letter code in all caps.")]
        public string TeacherID { get; set; }
        [Required]
        [StringLength(30, ErrorMessage = "The name entered is too long.")]
        [RegularExpression(@"^[A-Za-z\']*$", ErrorMessage = "Only letters can be used")]
        public string Surname { get; set; }
        [Required]
        [StringLength(30, ErrorMessage = "The name entered is too long.")]
        [RegularExpression(@"^[A-Za-z\']*$", ErrorMessage = "Only letters can be used")]
        [Display(Name = "First Name")]
        public string Firstname { get; set; }

        public ICollection<Class> Classes { get; set; }
        public ICollection<Group> Groups { get; set; }
    }
}
