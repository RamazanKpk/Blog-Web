namespace DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class m2 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Articles", "UserId", c => c.Int());
            CreateIndex("dbo.Articles", "UserId");
            AddForeignKey("dbo.Articles", "UserId", "dbo.Users", "UserId");
        }

        public override void Down()
        {
            DropForeignKey("dbo.Articles", "UserId", "dbo.Users");
            DropIndex("dbo.Articles", new[] { "UserId" });
            DropColumn("dbo.Articles", "UserId");
        }
    }
}
