using LinqToDB.Mapping;

namespace Task1.Models
{
    [Table(Name = "Categories")]
    public class Category
    {
        [PrimaryKey, Identity]
        public int CategoryID { get; set; }
        [Column(Name = "CategoryName")]
        public string Name { get; set; }
    }
}
