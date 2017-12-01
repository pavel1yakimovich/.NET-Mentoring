using LinqToDB.Mapping;

namespace Task1.Models
{
    [Table(Name = "Territories")]
    public class Territory
    {
        [PrimaryKey, Identity]
        public int TerritoryID { get; set; }
        
        [Column(Name = "TerritoryDescription"), NotNull]
        public string Description { get; set; }

        [Column(Name = "RegionID")]
        public int RegionID { get; set; }
        [Association(ThisKey = "RegionID", OtherKey = "RegionID"), NotNull]
        public Region Region { get; set; }
    }
}
