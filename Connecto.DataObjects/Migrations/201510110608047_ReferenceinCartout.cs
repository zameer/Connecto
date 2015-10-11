namespace Connecto.DataObjects.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ReferenceinCartout : DbMigration
    {
        public override void Up()
        {
            AddColumn("Product.Order", "ReferenceCode", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("Product.Order", "ReferenceCode");
        }
    }
}
