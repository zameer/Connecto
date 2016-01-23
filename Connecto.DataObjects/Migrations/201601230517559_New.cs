namespace Connecto.DataObjects.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class New : DbMigration
    {
        public override void Up()
        {
            
            AddColumn("Connecto.CompanyLocation", "PrinterName", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("Connecto.CompanyLocation", "PrinterName");
            DropColumn("Connecto.CompanyLocation", "Contact");
        }
    }
}
