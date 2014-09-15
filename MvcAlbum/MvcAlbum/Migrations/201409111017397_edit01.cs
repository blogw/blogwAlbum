namespace MvcAlbum.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class edit01 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Pictures", "AlbumID", "dbo.Albums");
            DropIndex("dbo.Pictures", new[] { "AlbumID" });
        }
        
        public override void Down()
        {
            CreateIndex("dbo.Pictures", "AlbumID");
            AddForeignKey("dbo.Pictures", "AlbumID", "dbo.Albums", "AlbumID", cascadeDelete: true);
        }
    }
}
