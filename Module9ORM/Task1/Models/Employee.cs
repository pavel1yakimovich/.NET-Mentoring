using LinqToDB.Mapping;
using System.Collections.Generic;

namespace Task1.Models
{
    [Table(Name = "Employees")]
    public class Employee
    {
        [PrimaryKey, Identity]
        public int EmployeeID { get; set; }

        [Column(Name = "FirstName")]
        public string FirstName { get; set; }
        [Column(Name = "LastName")]
        public string LastName { get; set; }
        
        [Association(ThisKey = "EmployeeID", OtherKey = "EmployeeID")]
        public IEnumerable<EmployeeTerritories> EmployeeTerritories { get; set; }
    }
}
