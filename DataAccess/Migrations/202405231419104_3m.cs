﻿namespace DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _3m : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Articles", "Summary", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Articles", "Summary");
        }
    }
}
