namespace backend.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class foreignkeysadded : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Cursusinstanties",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Startdatum = c.String(),
                        Cursus_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Cursus", t => t.Cursus_Id)
                .Index(t => t.Cursus_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Cursusinstanties", "Cursus_Id", "dbo.Cursus");
            DropIndex("dbo.Cursusinstanties", new[] { "Cursus_Id" });
            DropTable("dbo.Cursusinstanties");
        }
    }
}
