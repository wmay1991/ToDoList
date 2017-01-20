namespace ToDoList.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RemoveDateCreated : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.ToDoList", "date_created");
        }
        
        public override void Down()
        {
            AddColumn("dbo.ToDoList", "date_created", c => c.DateTime(nullable: false));
        }
    }
}
