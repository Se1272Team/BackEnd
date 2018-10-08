namespace BackEndBookShop.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class changeCreatedDate : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Books", "CreatedUtc", c => c.DateTime(nullable: false, defaultValueSql: "GETUTCDATE()"));
            DropColumn("dbo.Books", "CreatedDate");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Books", "CreatedDate", c => c.DateTime());
            DropColumn("dbo.Books", "CreatedUtc");
        }
    }
}
