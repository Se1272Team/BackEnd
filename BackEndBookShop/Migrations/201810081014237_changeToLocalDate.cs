namespace BackEndBookShop.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class changeToLocalDate : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Books", "CreatedDate", c => c.DateTime(nullable: false, defaultValueSql: "GETDATE()"));
            DropColumn("dbo.Books", "CreatedUtc");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Books", "CreatedUtc", c => c.DateTime(nullable: false));
            DropColumn("dbo.Books", "CreatedDate");
        }
    }
}
