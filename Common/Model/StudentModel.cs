using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace Common.Model
{
    public class StudentModel
    {     
        [HiddenInput(DisplayValue = false)]
        public int StudentId { get; set; }

        [Required(ErrorMessage = "Field is required")]
        [Display(Name="Name: ")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Field is required")]
        [DataType(DataType.Date)]
        [Display(Name = "Date of birth: ")]       
        public string DateOfBirth { get; set; }
        
        //[HiddenInput(DisplayValue = false)]
        //public DateTime? CreatedDate { get; set; }

        //[HiddenInput(DisplayValue = false)]
        //public bool? IsActive { get; set; }

        [Required(ErrorMessage = "Field is required")]
        [Display(Name = "Gender: ")]
        public Gender StudentGender { get; set; }

        public string GenderType { get; set; }

        [HiddenInput(DisplayValue = false)]
        public int Age { get; set; }
        
        public List<SelectListItem> Courses { get; set; }
  
        public int[] CouseIds { get; set; }
        public string CourseId { get; set; }

    }
}
