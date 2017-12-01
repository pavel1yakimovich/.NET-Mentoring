using LinqToDB.Mapping;

namespace Task1.Models
{
    [Table("EmployeeTerritories")]
    public class EmployeeTerritories
    {
        [Column("EmployeeID"), PrimaryKey(1), Identity]
        public int EmployeeID { get; set; }

        [Column("TerritoryID"), PrimaryKey(2), Identity]
        public int TerritoryID { get; set; }

        [Association(ThisKey = "EmployeeID", OtherKey = "EmployeeID")]
        public Employee Employee { get; set; }

        [Association(ThisKey = "TerritoryID", OtherKey = "TerritoryID")]
        public Territory Territories { get; set; }
    }
}
