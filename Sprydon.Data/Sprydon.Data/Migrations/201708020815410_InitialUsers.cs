namespace Sprydon.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialUsers : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ApplicationUser",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Forename = c.String(),
                        Surname = c.String(),
                        EditedBy = c.String(nullable: false),
                        CreatedBy = c.String(nullable: false),
                        Created = c.DateTime(nullable: false),
                        LastEdited = c.DateTime(nullable: false),
                        TimeStamp = c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "rowversion"),
                    })
                .PrimaryKey(t => t.Id);

            Sql("INSERT INTO APPLICATIONUSER(FORENAME, SURNAME, EDITEDBY,CREATEDBY,CREATED,LASTEDITED) VALUES ('David','Bowman','system','system',getdate(),getdate())");
            Sql("INSERT INTO APPLICATIONUSER(FORENAME, SURNAME, EDITEDBY,CREATEDBY,CREATED,LASTEDITED) VALUES ('Frank','Poole','system','system',getdate(),getdate())");
            Sql("INSERT INTO APPLICATIONUSER(FORENAME, SURNAME, EDITEDBY,CREATEDBY,CREATED,LASTEDITED) VALUES ('Bill','Michaels','system','system',getdate(),getdate())");
            Sql("INSERT INTO APPLICATIONUSER(FORENAME, SURNAME, EDITEDBY,CREATEDBY,CREATED,LASTEDITED) VALUES ('Tanya','Kirbuk','system','system',getdate(),getdate())");

        }

        public override void Down()
        {
            DropTable("dbo.ApplicationUser");
        }
    }
}
