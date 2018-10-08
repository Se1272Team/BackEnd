namespace BackEndBookShop.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class adf : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Books", "publishDate", c => c.DateTime(nullable: false, defaultValue: new DateTime(2018,1,1)));
        }
        
        public override void Down()
        {
        }
    }
}
