using System;

namespace Common.Model
{
    public class CourseModel
    {
        public int CourseId { get; set; }
        public string CourseName { get; set; }
        public bool? IsActive { get; set; }
        public DateTime? CreatedOn { get; set; }
    }
}
