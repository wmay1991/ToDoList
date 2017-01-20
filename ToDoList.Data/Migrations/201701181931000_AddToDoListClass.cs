namespace ToDoList.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddToDoListClass : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ToDoList",
                c => new
                    {
                        id = c.Guid(nullable: false),
                        Description = c.String(maxLength: 300),
                        isCompleted = c.Boolean(nullable: false, defaultValue: false),
                })
                .PrimaryKey(t => t.id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.ToDoList");
        }
    }
}
