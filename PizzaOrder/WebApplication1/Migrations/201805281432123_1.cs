namespace WebApplication1.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _1 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.DbAdditions",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.DbNameDrinks",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        NameDrink = c.Int(nullable: false),
                        DbAddition_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.DbAdditions", t => t.DbAddition_Id)
                .Index(t => t.DbAddition_Id);
            
            CreateTable(
                "dbo.DbNumberDrinks",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        numberDrink = c.Int(nullable: false),
                        DbAddition_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.DbAdditions", t => t.DbAddition_Id)
                .Index(t => t.DbAddition_Id);
            
            CreateTable(
                "dbo.DbNumberSauces",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        numberSauce = c.Int(nullable: false),
                        DbAddition_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.DbAdditions", t => t.DbAddition_Id)
                .Index(t => t.DbAddition_Id);
            
            CreateTable(
                "dbo.DbNameSauces",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        NameSauce = c.Int(nullable: false),
                        DbAddition_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.DbAdditions", t => t.DbAddition_Id)
                .Index(t => t.DbAddition_Id);
            
            CreateTable(
                "dbo.DbNamePizzas",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        NamePizza = c.Int(nullable: false),
                        DbPizzaRequirement_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.DbPizzaRequirements", t => t.DbPizzaRequirement_Id)
                .Index(t => t.DbPizzaRequirement_Id);
            
            CreateTable(
                "dbo.DbNumberPizzas",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        numberPizza = c.Int(nullable: false),
                        DbPizzaRequirement_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.DbPizzaRequirements", t => t.DbPizzaRequirement_Id)
                .Index(t => t.DbPizzaRequirement_Id);
            
            CreateTable(
                "dbo.DbPizzaRequirements",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Additions_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.DbAdditions", t => t.Additions_Id)
                .Index(t => t.Additions_Id);
            
            CreateTable(
                "dbo.DbOrders",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        dateTime = c.DateTime(nullable: false),
                        FullNameCustomer = c.String(),
                        Address = c.String(),
                        Price = c.Double(nullable: false),
                        Currency = c.Int(nullable: false),
                        Pizza_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.DbPizzaRequirements", t => t.Pizza_Id)
                .Index(t => t.Pizza_Id);
            
            CreateTable(
                "dbo.AspNetRoles",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name, unique: true, name: "RoleNameIndex");
            
            CreateTable(
                "dbo.AspNetUserRoles",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        RoleId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.UserId, t.RoleId })
                .ForeignKey("dbo.AspNetRoles", t => t.RoleId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.RoleId);
            
            CreateTable(
                "dbo.AspNetUsers",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Email = c.String(maxLength: 256),
                        EmailConfirmed = c.Boolean(nullable: false),
                        PasswordHash = c.String(),
                        SecurityStamp = c.String(),
                        PhoneNumber = c.String(),
                        PhoneNumberConfirmed = c.Boolean(nullable: false),
                        TwoFactorEnabled = c.Boolean(nullable: false),
                        LockoutEndDateUtc = c.DateTime(),
                        LockoutEnabled = c.Boolean(nullable: false),
                        AccessFailedCount = c.Int(nullable: false),
                        UserName = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.UserName, unique: true, name: "UserNameIndex");
            
            CreateTable(
                "dbo.AspNetUserClaims",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(nullable: false, maxLength: 128),
                        ClaimType = c.String(),
                        ClaimValue = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.AspNetUserLogins",
                c => new
                    {
                        LoginProvider = c.String(nullable: false, maxLength: 128),
                        ProviderKey = c.String(nullable: false, maxLength: 128),
                        UserId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.LoginProvider, t.ProviderKey, t.UserId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AspNetUserRoles", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserLogins", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserClaims", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserRoles", "RoleId", "dbo.AspNetRoles");
            DropForeignKey("dbo.DbOrders", "Pizza_Id", "dbo.DbPizzaRequirements");
            DropForeignKey("dbo.DbNumberPizzas", "DbPizzaRequirement_Id", "dbo.DbPizzaRequirements");
            DropForeignKey("dbo.DbNamePizzas", "DbPizzaRequirement_Id", "dbo.DbPizzaRequirements");
            DropForeignKey("dbo.DbPizzaRequirements", "Additions_Id", "dbo.DbAdditions");
            DropForeignKey("dbo.DbNameSauces", "DbAddition_Id", "dbo.DbAdditions");
            DropForeignKey("dbo.DbNumberSauces", "DbAddition_Id", "dbo.DbAdditions");
            DropForeignKey("dbo.DbNumberDrinks", "DbAddition_Id", "dbo.DbAdditions");
            DropForeignKey("dbo.DbNameDrinks", "DbAddition_Id", "dbo.DbAdditions");
            DropIndex("dbo.AspNetUserLogins", new[] { "UserId" });
            DropIndex("dbo.AspNetUserClaims", new[] { "UserId" });
            DropIndex("dbo.AspNetUsers", "UserNameIndex");
            DropIndex("dbo.AspNetUserRoles", new[] { "RoleId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "UserId" });
            DropIndex("dbo.AspNetRoles", "RoleNameIndex");
            DropIndex("dbo.DbOrders", new[] { "Pizza_Id" });
            DropIndex("dbo.DbPizzaRequirements", new[] { "Additions_Id" });
            DropIndex("dbo.DbNumberPizzas", new[] { "DbPizzaRequirement_Id" });
            DropIndex("dbo.DbNamePizzas", new[] { "DbPizzaRequirement_Id" });
            DropIndex("dbo.DbNameSauces", new[] { "DbAddition_Id" });
            DropIndex("dbo.DbNumberSauces", new[] { "DbAddition_Id" });
            DropIndex("dbo.DbNumberDrinks", new[] { "DbAddition_Id" });
            DropIndex("dbo.DbNameDrinks", new[] { "DbAddition_Id" });
            DropTable("dbo.AspNetUserLogins");
            DropTable("dbo.AspNetUserClaims");
            DropTable("dbo.AspNetUsers");
            DropTable("dbo.AspNetUserRoles");
            DropTable("dbo.AspNetRoles");
            DropTable("dbo.DbOrders");
            DropTable("dbo.DbPizzaRequirements");
            DropTable("dbo.DbNumberPizzas");
            DropTable("dbo.DbNamePizzas");
            DropTable("dbo.DbNameSauces");
            DropTable("dbo.DbNumberSauces");
            DropTable("dbo.DbNumberDrinks");
            DropTable("dbo.DbNameDrinks");
            DropTable("dbo.DbAdditions");
        }
    }
}
