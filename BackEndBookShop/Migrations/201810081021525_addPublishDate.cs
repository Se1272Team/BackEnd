namespace BackEndBookShop.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addPublishDate : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Books", "publishDate", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Books", "publishDate");
        }
    }
}
