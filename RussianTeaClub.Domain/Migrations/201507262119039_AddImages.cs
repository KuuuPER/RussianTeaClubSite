namespace RussianTeaClub.Domain.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddImages : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ContentImages",
                c => new
                    {
                        ContentImageId = c.Guid(nullable: false),
                        ArticleId = c.Guid(),
                        ImageData = c.Binary(),
                        ImageMimeType = c.String(),
                    })
                .PrimaryKey(t => t.ContentImageId)
                .ForeignKey("dbo.Articles", t => t.ArticleId)
                .Index(t => t.ArticleId);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ContentImages", "ArticleId", "dbo.Articles");
            DropIndex("dbo.ContentImages", new[] { "ArticleId" });
            DropTable("dbo.ContentImages");
        }
    }
}
