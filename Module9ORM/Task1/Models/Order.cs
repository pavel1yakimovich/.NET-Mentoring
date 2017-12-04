using LinqToDB.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task1.Models
{
    [Table(Name = "Orders")]
    public class Order
    {
        [PrimaryKey, Identity]
        public int OrderId { get; set; }

        [Column("ShipVia")]
        public int ShipperID { get; set; }
        [Association(ThisKey = "ShipVia", OtherKey = "ShipperID")]
        public Shipper Shipper { get; set; }

        [Column("EmployeeID")]
        public int EmployeeID { get; set; }
        [Association(ThisKey = "EmployeeID", OtherKey = "EmployeeID")]
        public Employee Employee { get; set; }
    }
}
