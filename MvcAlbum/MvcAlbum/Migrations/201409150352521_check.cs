namespace MvcAlbum.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class check : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Albums", "Name", c => c.String(nullable: false, maxLength: 50));
            AlterColumn("dbo.Albums", "Thumbnail", c => c.String(nullable: false));
            AlterColumn("dbo.Pictures", "Title", c => c.String(nullable: false, maxLength: 50));
            AlterColumn("dbo.Pictures", "Path", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Pictures", "Path", c => c.String());
            AlterColumn("dbo.Pictures", "Title", c => c.String());
            AlterColumn("dbo.Albums", "Thumbnail", c => c.String());
            AlterColumn("dbo.Albums", "Name", c => c.String());
        }
    }
}
