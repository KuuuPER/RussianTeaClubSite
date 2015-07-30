namespace RussianTeaClub.Domain.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Articles",
                c => new
                    {
                        ArticleId = c.Guid(nullable: false),
                        Name = c.String(),
                        Description = c.String(),
                        Content = c.String(),
                    })
                .PrimaryKey(t => t.ArticleId);
            
            CreateTable(
                "dbo.Tags",
                c => new
                    {
                        TagId = c.Guid(nullable: false),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.TagId);
            
            CreateTable(
                "dbo.TagArticles",
                c => new
                    {
                        Tag_TagId = c.Guid(nullable: false),
                        Article_ArticleId = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => new { t.Tag_TagId, t.Article_ArticleId })
                .ForeignKey("dbo.Tags", t => t.Tag_TagId, cascadeDelete: true)
                .ForeignKey("dbo.Articles", t => t.Article_ArticleId, cascadeDelete: true)
                .Index(t => t.Tag_TagId)
                .Index(t => t.Article_ArticleId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.TagArticles", "Article_ArticleId", "dbo.Articles");
            DropForeignKey("dbo.TagArticles", "Tag_TagId", "dbo.Tags");
            DropIndex("dbo.TagArticles", new[] { "Article_ArticleId" });
            DropIndex("dbo.TagArticles", new[] { "Tag_TagId" });
            DropTable("dbo.TagArticles");
            DropTable("dbo.Tags");
            DropTable("dbo.Articles");
        }
    }
}
