namespace Shop.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddQuantityColumnToProductTable : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Product", "Quantity", c => c.Int(nullable: false));
            Sql("Update dbo.Product Set Quantity = 0 ");
        }
        
        public override void Down()
        {
            DropColumn("dbo.Product", "Quantity");
        }
    }
}
