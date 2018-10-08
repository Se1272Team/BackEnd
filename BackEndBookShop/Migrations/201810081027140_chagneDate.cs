namespace BackEndBookShop.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class chagneDate : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Books", "publishDate", c => c.DateTime(nullable: false, defaultValueSql: "2018-01-01"));
        }
        
        public override void Down()
        {
        }
    }
}
