using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace EmployeePayrollMVC.Models
{
    public class EmployeeDataModel
    {
        [Key]
        public int EmployeeId { get; set; }
        [Required]
        [DisplayName("First Name")]
        [RegularExpression("^[A-Z]{1}[a-z]{2,}$", ErrorMessage = "First Name Start with capital letter and has minimum three character")]
        public string FirstName { get; set; }
        [DisplayName("Last Name")]
        public string LastName { get; set; }
        public int Age { get; set; }
        public string Department { get; set; }
        public int Salary { get; set; }
        [DisplayName("Start Date")]
        [DisplayFormat(DataFormatString ="{0:dd-MM-yyyy}")]
        public DateTime StartedDate { get; set; }
    }
}
