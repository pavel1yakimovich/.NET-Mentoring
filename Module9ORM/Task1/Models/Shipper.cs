using LinqToDB.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task1.Models
{
    [Table("Shippers")]
    public class Shipper
    {
        [PrimaryKey, Identity]
        public int ShipperID { get; set; }

        [Column]
        public string CompanyName { get; set; }

        [Association(ThisKey = "ShipperID", OtherKey = "ShipVia", CanBeNull = false)]
        public IEnumerable<Order> Orders { get; set; }
    }
}
