namespace EntityFrameworkTask.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Version_2 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.CreditCards",
                c => new
                    {
                        CreditCardId = c.Int(nullable: false, identity: true),
                        CardHolder = c.String(),
                        ExpirationDate = c.DateTime(nullable: false),
                        EmployeeId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.CreditCardId)
                .ForeignKey("dbo.Employees", t => t.EmployeeId, cascadeDelete: true)
                .Index(t => t.EmployeeId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.CreditCards", "EmployeeId", "dbo.Employees");
            DropIndex("dbo.CreditCards", new[] { "EmployeeId" });
            DropTable("dbo.CreditCards");
        }
    }
}
