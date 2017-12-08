namespace EntityFrameworkTask.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Version_3 : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.Region", newName: "Regions");
            AddColumn("dbo.Customers", "FoundationDate", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Customers", "FoundationDate");
            RenameTable(name: "dbo.Regions", newName: "Region");
        }
    }
}
