namespace Taşınmaz_Projesi.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class tasinmaz : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Tasinmazs", "IlID", c => c.Int(nullable: false));
            AddColumn("dbo.Tasinmazs", "IlceID", c => c.Int(nullable: false));
            AddColumn("dbo.Tasinmazs", "MahalleID", c => c.Int(nullable: false));
            CreateIndex("dbo.Tasinmazs", "IlID");
            CreateIndex("dbo.Tasinmazs", "IlceID");
            CreateIndex("dbo.Tasinmazs", "MahalleID");
            AddForeignKey("dbo.Tasinmazs", "IlID", "dbo.Ils", "IlID", cascadeDelete: true);
            AddForeignKey("dbo.Tasinmazs", "IlceID", "dbo.Ilces", "IlceID", cascadeDelete: true);
            AddForeignKey("dbo.Tasinmazs", "MahalleID", "dbo.Mahalles", "MahalleID", cascadeDelete: true);
            DropColumn("dbo.Tasinmazs", "Il");
            DropColumn("dbo.Tasinmazs", "Ilce");
            DropColumn("dbo.Tasinmazs", "Mahalle");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Tasinmazs", "Mahalle", c => c.String(nullable: false));
            AddColumn("dbo.Tasinmazs", "Ilce", c => c.String(nullable: false));
            AddColumn("dbo.Tasinmazs", "Il", c => c.String(nullable: false));
            DropForeignKey("dbo.Tasinmazs", "MahalleID", "dbo.Mahalles");
            DropForeignKey("dbo.Tasinmazs", "IlceID", "dbo.Ilces");
            DropForeignKey("dbo.Tasinmazs", "IlID", "dbo.Ils");
            DropIndex("dbo.Tasinmazs", new[] { "MahalleID" });
            DropIndex("dbo.Tasinmazs", new[] { "IlceID" });
            DropIndex("dbo.Tasinmazs", new[] { "IlID" });
            DropColumn("dbo.Tasinmazs", "MahalleID");
            DropColumn("dbo.Tasinmazs", "IlceID");
            DropColumn("dbo.Tasinmazs", "IlID");
        }
    }
}
