namespace DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class L3 : DbMigration
    {
        public override void Up()
        {
           
        }

        public override void Down()
        {
            DropColumn("dbo.Articles", "Summary");
        }
    }
}
