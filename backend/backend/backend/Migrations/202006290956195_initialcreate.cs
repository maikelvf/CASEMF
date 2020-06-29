namespace backend.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class initialcreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Cursus",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Duur = c.Int(nullable: false),
                        Titel = c.String(),
                        Code = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Cursus");
        }
    }
}
