using LinqToDB.Mapping;

namespace Task1.Models
{
    [Table("Order Details")]
    public class OrderDetails
    {
        [Column("OrderID"), PrimaryKey(1), Identity]
        public int OrderID { get; set; }

        [Column("ProductID"), PrimaryKey(2), Identity]
        public int ProductID { get; set; }

        [Association(ThisKey = "OrderID", OtherKey = "OrderID")]
        public Order Order { get; set; }

        [Association(ThisKey = "ProductID", OtherKey = "ProductID")]
        public Product Product { get; set; }
    }
}
