namespace ToDoList.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DateCompletedRemove : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.ToDoList", "date_completed");
        }
        
        public override void Down()
        {
            AddColumn("dbo.ToDoList", "date_completed", c => c.DateTime(nullable: false));
        }
    }
}
