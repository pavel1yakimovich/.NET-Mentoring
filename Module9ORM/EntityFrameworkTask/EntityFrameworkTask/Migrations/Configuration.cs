namespace EntityFrameworkTask.Migrations
{
    using EntityFrameworkTask.ORM;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Data.Entity.Validation;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<EntityFrameworkTask.ORM.Northwind>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(EntityFrameworkTask.ORM.Northwind context)
        {
            try
            {
                //  This method will be called after migrating to the latest version.

                //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
                //  to avoid creating duplicate seed data.
                context.Categories.AddOrUpdate(c => c.CategoryID,
                    new Category
                    {
                        CategoryName = "FirstCategory",
                        Description = "Description of 1st category"
                    },
                    new Category
                    {
                        CategoryName = "SecondCategory",
                        Description = "Description of 2nd category"
                    },
                    new Category
                    {
                        CategoryName = "ThirdCategory",
                        Description = "Description of 3rd category"
                    });

                var region1 = new Region
                {
                    RegionID = 0,
                    RegionDescription = "First region"
                };

                var region2 = new Region
                {
                    RegionID = 1,
                    RegionDescription = "Second region"
                };

                context.Regions.AddOrUpdate(r => r.RegionID,
                    region1,
                    region2,
                    new Region
                    {
                        RegionID = 2,
                        RegionDescription = "Third region"
                    });
                context.SaveChanges();
                
                var territory1 = new Territory
                {
                    TerritoryID = "1",
                    TerritoryDescription = "FirstTerritory",
                    RegionID = region1.RegionID
                };

                var territory2 = new Territory
                {
                    TerritoryID = "2",
                    TerritoryDescription = "SecondTerritory",
                    RegionID = region2.RegionID
                };

                context.Territories.AddOrUpdate(t => t.TerritoryID, territory1, territory2);
                
                context.SaveChanges();
            }
            catch (DbEntityValidationException e)
            {
                foreach (var eve in e.EntityValidationErrors)
                {
                    Console.WriteLine("Entity of type \"{0}\" in state \"{1}\" has the following validation errors:",
                        eve.Entry.Entity.GetType().Name, eve.Entry.State);
                    foreach (var ve in eve.ValidationErrors)
                    {
                        Console.WriteLine("- Property: \"{0}\", Error: \"{1}\"",
                            ve.PropertyName, ve.ErrorMessage);
                    }
                }
                throw;
            }
        }
    }
}
