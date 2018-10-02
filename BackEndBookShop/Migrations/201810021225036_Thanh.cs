namespace BackEndBookShop.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Thanh : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.BookAuthors",
                c => new
                    {
                        Book_ID = c.Int(nullable: false),
                        Author_ID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Book_ID, t.Author_ID })
                .ForeignKey("dbo.Books", t => t.Book_ID, cascadeDelete: true)
                .ForeignKey("dbo.Authors", t => t.Author_ID, cascadeDelete: true)
                .Index(t => t.Book_ID)
                .Index(t => t.Author_ID);
            
            CreateIndex("dbo.Books", "CategoryID");
            CreateIndex("dbo.Books", "PulisherID");
            AddForeignKey("dbo.Books", "CategoryID", "dbo.Categories", "ID", cascadeDelete: true);
            AddForeignKey("dbo.Books", "PulisherID", "dbo.Publishers", "ID", cascadeDelete: true);
            DropColumn("dbo.Books", "AuthorID");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Books", "AuthorID", c => c.Int(nullable: false));
            DropForeignKey("dbo.Books", "PulisherID", "dbo.Publishers");
            DropForeignKey("dbo.Books", "CategoryID", "dbo.Categories");
            DropForeignKey("dbo.BookAuthors", "Author_ID", "dbo.Authors");
            DropForeignKey("dbo.BookAuthors", "Book_ID", "dbo.Books");
            DropIndex("dbo.BookAuthors", new[] { "Author_ID" });
            DropIndex("dbo.BookAuthors", new[] { "Book_ID" });
            DropIndex("dbo.Books", new[] { "PulisherID" });
            DropIndex("dbo.Books", new[] { "CategoryID" });
            DropTable("dbo.BookAuthors");
        }
    }
}
