namespace BackEndBookShop.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class changeCategoryNameType : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Categories", "Name", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Categories", "Name", c => c.Int(nullable: false));
        }
    }
}
