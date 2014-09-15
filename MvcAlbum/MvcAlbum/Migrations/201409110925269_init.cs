namespace MvcAlbum.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class init : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Albums",
                c => new
                    {
                        AlbumID = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Thumbnail = c.String(),
                    })
                .PrimaryKey(t => t.AlbumID);
            
            CreateTable(
                "dbo.Pictures",
                c => new
                    {
                        PictureID = c.Int(nullable: false, identity: true),
                        Title = c.String(),
                        Path = c.String(),
                        AlbumID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.PictureID)
                .ForeignKey("dbo.Albums", t => t.AlbumID, cascadeDelete: true)
                .Index(t => t.AlbumID);
            
        }
        
        public override void Down()
        {
            DropIndex("dbo.Pictures", new[] { "AlbumID" });
            DropForeignKey("dbo.Pictures", "AlbumID", "dbo.Albums");
            DropTable("dbo.Pictures");
            DropTable("dbo.Albums");
        }
    }
}
