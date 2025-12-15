namespace EFarming.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddingContactEntities : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.SustainabilityContacts",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Name = c.String(maxLength: 50),
                        Date = c.DateTime(nullable: false),
                        Comment = c.String(maxLength: 500),
                        CreatedAt = c.DateTime(),
                        UpdatedAt = c.DateTime(),
                        DeletedAt = c.DateTime(),
                        Location_Id = c.Int(),
                        Type_Id = c.Int(),
                        User_Id = c.Guid(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.SustainabilityContactLocations", t => t.Location_Id)
                .ForeignKey("dbo.SustainabilityContactTypes", t => t.Type_Id)
                .ForeignKey("dbo.Users", t => t.User_Id)
                .Index(t => t.Location_Id)
                .Index(t => t.Type_Id)
                .Index(t => t.User_Id);
            
            CreateTable(
                "dbo.SustainabilityContactLocations",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(maxLength: 50),
                        Description = c.String(maxLength: 500),
                        CreatedAt = c.DateTime(),
                        UpdatedAt = c.DateTime(),
                        DeletedAt = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.SustainabilityContactTopics",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(maxLength: 50),
                        Description = c.String(maxLength: 500),
                        CreatedAt = c.DateTime(),
                        UpdatedAt = c.DateTime(),
                        DeletedAt = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.SustainabilityContactTypes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(maxLength: 50),
                        Description = c.String(maxLength: 500),
                        CreatedAt = c.DateTime(),
                        UpdatedAt = c.DateTime(),
                        DeletedAt = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.SustainabilityContactFarm",
                c => new
                    {
                        ContactId = c.Guid(nullable: false),
                        FarmId = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => new { t.ContactId, t.FarmId })
                .ForeignKey("dbo.SustainabilityContacts", t => t.ContactId, cascadeDelete: true)
                .ForeignKey("dbo.farms", t => t.FarmId, cascadeDelete: true)
                .Index(t => t.ContactId)
                .Index(t => t.FarmId);
            
            CreateTable(
                "dbo.SustainabilityContactTopic",
                c => new
                    {
                        ContactId = c.Guid(nullable: false),
                        TopicId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.ContactId, t.TopicId })
                .ForeignKey("dbo.SustainabilityContacts", t => t.ContactId, cascadeDelete: true)
                .ForeignKey("dbo.SustainabilityContactTopics", t => t.TopicId, cascadeDelete: true)
                .Index(t => t.ContactId)
                .Index(t => t.TopicId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.SustainabilityContacts", "User_Id", "dbo.Users");
            DropForeignKey("dbo.SustainabilityContacts", "Type_Id", "dbo.SustainabilityContactTypes");
            DropForeignKey("dbo.SustainabilityContactTopic", "TopicId", "dbo.SustainabilityContactTopics");
            DropForeignKey("dbo.SustainabilityContactTopic", "ContactId", "dbo.SustainabilityContacts");
            DropForeignKey("dbo.SustainabilityContacts", "Location_Id", "dbo.SustainabilityContactLocations");
            DropForeignKey("dbo.SustainabilityContactFarm", "FarmId", "dbo.farms");
            DropForeignKey("dbo.SustainabilityContactFarm", "ContactId", "dbo.SustainabilityContacts");
            DropIndex("dbo.SustainabilityContactTopic", new[] { "TopicId" });
            DropIndex("dbo.SustainabilityContactTopic", new[] { "ContactId" });
            DropIndex("dbo.SustainabilityContactFarm", new[] { "FarmId" });
            DropIndex("dbo.SustainabilityContactFarm", new[] { "ContactId" });
            DropIndex("dbo.SustainabilityContacts", new[] { "User_Id" });
            DropIndex("dbo.SustainabilityContacts", new[] { "Type_Id" });
            DropIndex("dbo.SustainabilityContacts", new[] { "Location_Id" });
            DropTable("dbo.SustainabilityContactTopic");
            DropTable("dbo.SustainabilityContactFarm");
            DropTable("dbo.SustainabilityContactTypes");
            DropTable("dbo.SustainabilityContactTopics");
            DropTable("dbo.SustainabilityContactLocations");
            DropTable("dbo.SustainabilityContacts");
        }
    }
}
