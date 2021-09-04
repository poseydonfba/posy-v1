namespace POSY.Infra.CrossCutting.Identity.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Claims",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Name = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Funcao",
                c => new
                    {
                        FuncaoId = c.Guid(nullable: false),
                        Name = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.FuncaoId)
                .Index(t => t.Name, unique: true, name: "RoleNameIndex");
            
            CreateTable(
                "dbo.UsuarioFuncao",
                c => new
                    {
                        UsuarioId = c.Guid(nullable: false),
                        FuncaoId = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => new { t.UsuarioId, t.FuncaoId })
                .ForeignKey("dbo.Funcao", t => t.FuncaoId, cascadeDelete: true)
                .ForeignKey("dbo.Usuario", t => t.UsuarioId, cascadeDelete: true)
                .Index(t => t.UsuarioId)
                .Index(t => t.FuncaoId);
            
            CreateTable(
                "dbo.Usuario",
                c => new
                    {
                        UsuarioId = c.Guid(nullable: false),
                        Dir = c.DateTime(nullable: false),
                        Der = c.DateTime(),
                        Email = c.String(maxLength: 256),
                        EmailConfirmed = c.Boolean(nullable: false),
                        PasswordHash = c.String(),
                        SecurityStamp = c.String(),
                        PhoneNumber = c.String(),
                        PhoneNumberConfirmed = c.Boolean(nullable: false),
                        TwoFactorEnabled = c.Boolean(nullable: false),
                        LockoutEndDateUtc = c.DateTime(),
                        LockoutEnabled = c.Boolean(nullable: false),
                        AccessFailedCount = c.Int(nullable: false),
                        UserName = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.UsuarioId)
                .Index(t => t.UserName, unique: true, name: "UserNameIndex");
            
            CreateTable(
                "dbo.UsuarioClaim",
                c => new
                    {
                        UsuarioClaimId = c.Int(nullable: false, identity: true),
                        UsuarioId = c.Guid(nullable: false),
                        ClaimType = c.String(),
                        ClaimValue = c.String(),
                    })
                .PrimaryKey(t => t.UsuarioClaimId)
                .ForeignKey("dbo.Usuario", t => t.UsuarioId, cascadeDelete: true)
                .Index(t => t.UsuarioId);
            
            CreateTable(
                "dbo.UsuarioLogin",
                c => new
                    {
                        LoginProvider = c.String(nullable: false, maxLength: 128),
                        ProviderKey = c.String(nullable: false, maxLength: 128),
                        UsuarioId = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => new { t.LoginProvider, t.ProviderKey, t.UsuarioId })
                .ForeignKey("dbo.Usuario", t => t.UsuarioId, cascadeDelete: true)
                .Index(t => t.UsuarioId);
            
            CreateTable(
                "dbo.UsuarioCliente",
                c => new
                    {
                        UsuarioClienteId = c.Int(nullable: false, identity: true),
                        ClientKey = c.String(),
                        UsuarioId = c.Guid(),
                    })
                .PrimaryKey(t => t.UsuarioClienteId)
                .ForeignKey("dbo.Usuario", t => t.UsuarioId)
                .Index(t => t.UsuarioId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.UsuarioCliente", "UsuarioId", "dbo.Usuario");
            DropForeignKey("dbo.UsuarioFuncao", "UsuarioId", "dbo.Usuario");
            DropForeignKey("dbo.UsuarioLogin", "UsuarioId", "dbo.Usuario");
            DropForeignKey("dbo.UsuarioClaim", "UsuarioId", "dbo.Usuario");
            DropForeignKey("dbo.UsuarioFuncao", "FuncaoId", "dbo.Funcao");
            DropIndex("dbo.UsuarioCliente", new[] { "UsuarioId" });
            DropIndex("dbo.UsuarioLogin", new[] { "UsuarioId" });
            DropIndex("dbo.UsuarioClaim", new[] { "UsuarioId" });
            DropIndex("dbo.Usuario", "UserNameIndex");
            DropIndex("dbo.UsuarioFuncao", new[] { "FuncaoId" });
            DropIndex("dbo.UsuarioFuncao", new[] { "UsuarioId" });
            DropIndex("dbo.Funcao", "RoleNameIndex");
            DropTable("dbo.UsuarioCliente");
            DropTable("dbo.UsuarioLogin");
            DropTable("dbo.UsuarioClaim");
            DropTable("dbo.Usuario");
            DropTable("dbo.UsuarioFuncao");
            DropTable("dbo.Funcao");
            DropTable("dbo.Claims");
        }
    }
}
