namespace Shop.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RenameIdentityTables : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.AspNetUsers", newName: "ApplicationUsers");
            RenameTable(name: "dbo.AspNetUserClaims", newName: "ApplicationUserClaims");
            RenameTable(name: "dbo.AspNetUserLogins", newName: "ApplicationUserLogins");
            RenameTable(name: "dbo.AspNetUserRoles", newName: "ApplicationUserRoles");
            RenameTable(name: "dbo.AspNetRoles", newName: "ApplicationRoles");
        }
        
        public override void Down()
        {
            RenameTable(name: "dbo.ApplicationRoles", newName: "AspNetRoles");
            RenameTable(name: "dbo.ApplicationUserRoles", newName: "AspNetUserRoles");
            RenameTable(name: "dbo.ApplicationUserLogins", newName: "AspNetUserLogins");
            RenameTable(name: "dbo.ApplicationUserClaims", newName: "AspNetUserClaims");
            RenameTable(name: "dbo.ApplicationUsers", newName: "AspNetUsers");
        }
    }
}
