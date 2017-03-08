namespace BookSvc.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CreacionInicial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Autor",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Nombre = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Libro",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Titulo = c.String(nullable: false),
                        Anio = c.Int(nullable: false),
                        Precio = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Genero = c.String(),
                        AutorId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Autor", t => t.AutorId, cascadeDelete: true)
                .Index(t => t.AutorId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Libro", "AutorId", "dbo.Autor");
            DropIndex("dbo.Libro", new[] { "AutorId" });
            DropTable("dbo.Libro");
            DropTable("dbo.Autor");
        }
    }
}
