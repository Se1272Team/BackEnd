namespace BackEndBookShop.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangepublishDateDefault : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Books", "publishDate", c => c.DateTime(nullable: false, defaultValueSql: "1/1/2018"));
        }
        
        public override void Down()
        {
        }
    }
}
