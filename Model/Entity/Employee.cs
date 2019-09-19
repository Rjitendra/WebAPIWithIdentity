using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Model.Entity
{
    [Table("Client", Schema = "dbo")]
    public class Employee
    {
        public Employee()
        {
        }
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string MobileNo { get; set; }
        public DateTimeOffset DOB { get; set; }
        public bool Gender { get; set; }
        public string Address { get; set; }
    }
}
