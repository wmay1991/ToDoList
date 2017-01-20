namespace ToDoList.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DateCompleted : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ToDoList", "date_completed", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.ToDoList", "date_completed");
        }
    }
}
