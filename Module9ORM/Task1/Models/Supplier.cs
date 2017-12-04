using LinqToDB.Mapping;

namespace Task1.Models
{
    [Table(Name = "Suppliers")]
    public class Supplier
    {
        [PrimaryKey, Identity]
        public int SupplierID { get; set; }

        [Column(Name = "CompanyName")]
        public string Name { get; set; }
    }
}
