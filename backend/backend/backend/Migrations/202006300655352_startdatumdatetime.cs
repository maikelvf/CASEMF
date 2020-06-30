namespace backend.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class startdatumdatetime : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Cursusinstanties", "Startdatum", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Cursusinstanties", "Startdatum", c => c.String());
        }
    }
}
