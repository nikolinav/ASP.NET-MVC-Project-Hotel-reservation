namespace Hotel.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedSmestaj : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Rezervacijas",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ImeIPrezime = c.String(nullable: false, maxLength: 50),
                        DatumOd = c.DateTime(nullable: false),
                        DatumDo = c.DateTime(nullable: false),
                        Otkazana = c.Boolean(nullable: false),
                        SobaId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Sobas", t => t.SobaId, cascadeDelete: true)
                .Index(t => t.SobaId);
            
            CreateTable(
                "dbo.Sobas",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        BrojSobe = c.Int(nullable: false),
                        BrojKreveta = c.Int(nullable: false),
                        Cena = c.Decimal(nullable: false, precision: 18, scale: 2),
                        SmestajId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Smestajs", t => t.SmestajId, cascadeDelete: true)
                .Index(t => t.SmestajId);
            
            CreateTable(
                "dbo.Smestajs",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Naziv = c.String(nullable: false),
                        Opis = c.String(),
                        Adresa = c.String(),
                        Ocena = c.Double(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Sobas", "SmestajId", "dbo.Smestajs");
            DropForeignKey("dbo.Rezervacijas", "SobaId", "dbo.Sobas");
            DropIndex("dbo.Sobas", new[] { "SmestajId" });
            DropIndex("dbo.Rezervacijas", new[] { "SobaId" });
            DropTable("dbo.Smestajs");
            DropTable("dbo.Sobas");
            DropTable("dbo.Rezervacijas");
        }
    }
}
