namespace POSY.Infra.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Amizade",
                c => new
                    {
                        SolicitadoPorId = c.Guid(nullable: false),
                        SolicitadoParaId = c.Guid(nullable: false),
                        DataSolicitacao = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        DataResposta = c.DateTime(precision: 7, storeType: "datetime2"),
                        StatusSolicitacao = c.Int(nullable: false),
                        Uer = c.Guid(),
                        Der = c.DateTime(precision: 7, storeType: "datetime2"),
                    })
                .PrimaryKey(t => new { t.SolicitadoPorId, t.SolicitadoParaId, t.DataSolicitacao })
                .ForeignKey("dbo.Usuario", t => t.SolicitadoParaId)
                .ForeignKey("dbo.Usuario", t => t.SolicitadoPorId)
                .Index(t => t.SolicitadoPorId)
                .Index(t => t.SolicitadoParaId);
            
            //CreateTable(
            //    "dbo.Usuario",
            //    c => new
            //        {
            //            UsuarioId = c.Guid(nullable: false),
            //            Email = c.String(nullable: false, maxLength: 256, unicode: false),
            //            EmailConfirmed = c.Boolean(nullable: false),
            //            PasswordHash = c.String(maxLength: 100, unicode: false),
            //            SecurityStamp = c.String(maxLength: 100, unicode: false),
            //            PhoneNumber = c.String(maxLength: 100, unicode: false),
            //            PhoneNumberConfirmed = c.Boolean(nullable: false),
            //            TwoFactorEnabled = c.Boolean(nullable: false),
            //            LockoutEndDateUtc = c.DateTime(precision: 7, storeType: "datetime2"),
            //            LockoutEnabled = c.Boolean(nullable: false),
            //            AccessFailedCount = c.Int(nullable: false),
            //            UserName = c.String(nullable: false, maxLength: 256, unicode: false),
            //            Dir = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
            //            Der = c.DateTime(precision: 7, storeType: "datetime2"),
            //        })
            //    .PrimaryKey(t => t.UsuarioId);
            
            CreateTable(
                "dbo.Perfil",
                c => new
                    {
                        UsuarioId = c.Guid(nullable: false),
                        Nome = c.String(nullable: false, maxLength: 15, unicode: false),
                        Sobrenome = c.String(nullable: false, maxLength: 15, unicode: false),
                        Alias = c.String(nullable: false, maxLength: 36, unicode: false),
                        PaisId = c.String(nullable: false, maxLength: 100, unicode: false),
                        DataNascimento = c.DateTime(nullable: false, storeType: "date"),
                        Sexo = c.Int(nullable: false),
                        EstadoCivil = c.Int(nullable: false),
                        FrasePerfil = c.Binary(),
                        DescricaoPerfil = c.Binary(),
                        Dar = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                    })
                .PrimaryKey(t => t.UsuarioId)
                .ForeignKey("dbo.Usuario", t => t.UsuarioId)
                .Index(t => t.UsuarioId)
                .Index(t => t.Alias, unique: true);
            
            CreateTable(
                "dbo.Privacidade",
                c => new
                    {
                        UsuarioId = c.Guid(nullable: false),
                        VerRecado = c.Boolean(nullable: false),
                        EscreverRecado = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.UsuarioId)
                .ForeignKey("dbo.Usuario", t => t.UsuarioId)
                .Index(t => t.UsuarioId);
            
            CreateTable(
                "dbo.Categoria",
                c => new
                    {
                        CategoriaId = c.Int(nullable: false, identity: true),
                        Nome = c.String(nullable: false, maxLength: 100, unicode: false),
                    })
                .PrimaryKey(t => t.CategoriaId);
            
            CreateTable(
                "dbo.Comunidade",
                c => new
                    {
                        ComunidadeId = c.Guid(nullable: false),
                        UsuarioId = c.Guid(nullable: false),
                        Alias = c.String(nullable: false, maxLength: 36, unicode: false),
                        Nome = c.String(nullable: false, maxLength: 100, unicode: false),
                        TipoComunidade = c.Int(nullable: false),
                        CategoriaId = c.Int(nullable: false),
                        DescricaoPerfil = c.Binary(),
                        Dir = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        Uar = c.Guid(nullable: false),
                        Dar = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        Uer = c.Guid(),
                        Der = c.DateTime(precision: 7, storeType: "datetime2"),
                    })
                .PrimaryKey(t => t.ComunidadeId)
                .ForeignKey("dbo.Categoria", t => t.CategoriaId)
                .ForeignKey("dbo.Usuario", t => t.UsuarioId)
                .Index(t => t.UsuarioId)
                .Index(t => t.Alias, unique: true, name: "IX_ALIAS_CMM")
                .Index(t => t.CategoriaId);
            
            CreateTable(
                "dbo.Connection",
                c => new
                    {
                        ConnectionId = c.String(nullable: false, maxLength: 100, unicode: false),
                        UsuarioId = c.Guid(nullable: false),
                        UserAgent = c.String(nullable: false, maxLength: 250, unicode: false),
                        Connected = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.ConnectionId);
            
            CreateTable(
                "dbo.Depoimento",
                c => new
                    {
                        DepoimentoId = c.Guid(nullable: false),
                        EnviadoPorId = c.Guid(nullable: false),
                        EnviadoParaId = c.Guid(nullable: false),
                        DescricaoDepoimento = c.Binary(nullable: false),
                        DataDepoimento = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        StatusDepoimento = c.Int(nullable: false),
                        DataResposta = c.DateTime(precision: 7, storeType: "datetime2"),
                        Uer = c.Guid(),
                        Der = c.DateTime(precision: 7, storeType: "datetime2"),
                    })
                .PrimaryKey(t => t.DepoimentoId)
                .ForeignKey("dbo.Usuario", t => t.EnviadoParaId)
                .ForeignKey("dbo.Usuario", t => t.EnviadoPorId)
                .Index(t => t.EnviadoPorId)
                .Index(t => t.EnviadoParaId);
            
            CreateTable(
                "dbo.Membro",
                c => new
                    {
                        ComunidadeId = c.Guid(nullable: false),
                        UsuarioMembroId = c.Guid(nullable: false),
                        DataSolicitacao = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        DataResposta = c.DateTime(precision: 7, storeType: "datetime2"),
                        UsuarioRespostaId = c.Guid(),
                        StatusSolicitacao = c.Int(nullable: false),
                        Uer = c.Guid(),
                        Der = c.DateTime(precision: 7, storeType: "datetime2"),
                    })
                .PrimaryKey(t => new { t.ComunidadeId, t.UsuarioMembroId, t.DataSolicitacao })
                .ForeignKey("dbo.Comunidade", t => t.ComunidadeId)
                .ForeignKey("dbo.Usuario", t => t.UsuarioMembroId)
                .ForeignKey("dbo.Usuario", t => t.UsuarioRespostaId)
                .Index(t => t.ComunidadeId)
                .Index(t => t.UsuarioMembroId)
                .Index(t => t.UsuarioRespostaId);
            
            CreateTable(
                "dbo.Moderador",
                c => new
                    {
                        ComunidadeId = c.Guid(nullable: false),
                        UsuarioModeradorId = c.Guid(nullable: false),
                        DataOperacao = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        UsuarioOperacaoId = c.Guid(nullable: false),
                        Uer = c.Guid(),
                        Der = c.DateTime(precision: 7, storeType: "datetime2"),
                    })
                .PrimaryKey(t => new { t.ComunidadeId, t.UsuarioModeradorId, t.DataOperacao })
                .ForeignKey("dbo.Comunidade", t => t.ComunidadeId)
                .ForeignKey("dbo.Usuario", t => t.UsuarioModeradorId)
                .ForeignKey("dbo.Usuario", t => t.UsuarioOperacaoId)
                .Index(t => t.ComunidadeId)
                .Index(t => t.UsuarioModeradorId)
                .Index(t => t.UsuarioOperacaoId);
            
            CreateTable(
                "dbo.PostOculto",
                c => new
                    {
                        UsuarioId = c.Guid(nullable: false),
                        PostPerfilId = c.Guid(nullable: false),
                        Data = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        StatusPost = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.UsuarioId, t.PostPerfilId, t.Data })
                .ForeignKey("dbo.PostPerfil", t => t.PostPerfilId)
                .ForeignKey("dbo.Usuario", t => t.UsuarioId)
                .Index(t => t.UsuarioId)
                .Index(t => t.PostPerfilId);
            
            CreateTable(
                "dbo.PostPerfil",
                c => new
                    {
                        PostPerfilId = c.Guid(nullable: false),
                        UsuarioId = c.Guid(nullable: false),
                        DescricaoPost = c.Binary(nullable: false),
                        DataPost = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        Der = c.DateTime(precision: 7, storeType: "datetime2"),
                    })
                .PrimaryKey(t => t.PostPerfilId)
                .ForeignKey("dbo.Usuario", t => t.UsuarioId)
                .Index(t => t.UsuarioId);
            
            CreateTable(
                "dbo.PostPerfilBloqueado",
                c => new
                    {
                        UsuarioId = c.Guid(nullable: false),
                        UsuarioIdBloqueado = c.Guid(nullable: false),
                        DataBloqueio = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        Uer = c.Guid(),
                        Der = c.DateTime(precision: 7, storeType: "datetime2"),
                    })
                .PrimaryKey(t => new { t.UsuarioId, t.UsuarioIdBloqueado })
                .ForeignKey("dbo.Usuario", t => t.UsuarioId)
                .Index(t => t.UsuarioId);
            
            CreateTable(
                "dbo.PostPerfilComentario",
                c => new
                    {
                        PostPerfilComentarioId = c.Guid(nullable: false),
                        PostPerfilId = c.Guid(nullable: false),
                        UsuarioId = c.Guid(nullable: false),
                        Comentario = c.Binary(),
                        Data = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        Uer = c.Guid(),
                        Der = c.DateTime(precision: 7, storeType: "datetime2"),
                    })
                .PrimaryKey(t => t.PostPerfilComentarioId)
                .ForeignKey("dbo.PostPerfil", t => t.PostPerfilId)
                .ForeignKey("dbo.Usuario", t => t.UsuarioId)
                .Index(t => t.PostPerfilId)
                .Index(t => t.UsuarioId);
            
            CreateTable(
                "dbo.RecadoComentario",
                c => new
                    {
                        RecadoComentarioId = c.Guid(nullable: false),
                        RecadoId = c.Guid(nullable: false),
                        UsuarioId = c.Guid(nullable: false),
                        DescricaoComentario = c.Binary(nullable: false),
                        DataComentario = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        Uer = c.Guid(),
                        Der = c.DateTime(precision: 7, storeType: "datetime2"),
                    })
                .PrimaryKey(t => t.RecadoComentarioId)
                .ForeignKey("dbo.Recado", t => t.RecadoId)
                .ForeignKey("dbo.Usuario", t => t.UsuarioId)
                .Index(t => t.RecadoId)
                .Index(t => t.UsuarioId);
            
            CreateTable(
                "dbo.Recado",
                c => new
                    {
                        RecadoId = c.Guid(nullable: false),
                        EnviadoPorId = c.Guid(nullable: false),
                        EnviadoParaId = c.Guid(nullable: false),
                        DescricaoRecado = c.Binary(nullable: false),
                        DataRecado = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        StatusRecado = c.Int(nullable: false),
                        DataLeitura = c.DateTime(precision: 7, storeType: "datetime2"),
                        Uer = c.Guid(),
                        Der = c.DateTime(precision: 7, storeType: "datetime2"),
                    })
                .PrimaryKey(t => t.RecadoId)
                .ForeignKey("dbo.Usuario", t => t.EnviadoParaId)
                .ForeignKey("dbo.Usuario", t => t.EnviadoPorId)
                .Index(t => t.EnviadoPorId)
                .Index(t => t.EnviadoParaId);
            
            CreateTable(
                "dbo.Topico",
                c => new
                    {
                        TopicoId = c.Guid(nullable: false),
                        ComunidadeId = c.Guid(nullable: false),
                        UsuarioId = c.Guid(nullable: false),
                        DataTopico = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        Titulo = c.String(nullable: false, maxLength: 100, unicode: false),
                        Descricao = c.Binary(nullable: false),
                        TipoTopico = c.Int(nullable: false),
                        Uer = c.Guid(),
                        Der = c.DateTime(precision: 7, storeType: "datetime2"),
                        Uerp = c.Guid(),
                        Derp = c.DateTime(precision: 7, storeType: "datetime2"),
                    })
                .PrimaryKey(t => t.TopicoId)
                .ForeignKey("dbo.Comunidade", t => t.ComunidadeId)
                .ForeignKey("dbo.Usuario", t => t.UsuarioId)
                .Index(t => t.ComunidadeId)
                .Index(t => t.UsuarioId);
            
            CreateTable(
                "dbo.TopicoPost",
                c => new
                    {
                        TopicoPostId = c.Guid(nullable: false),
                        TopicoId = c.Guid(nullable: false),
                        UsuarioId = c.Guid(nullable: false),
                        DataPost = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        Descricao = c.Binary(nullable: false),
                        Uer = c.Guid(),
                        Der = c.DateTime(precision: 7, storeType: "datetime2"),
                        Uerp = c.Guid(),
                        Derp = c.DateTime(precision: 7, storeType: "datetime2"),
                    })
                .PrimaryKey(t => t.TopicoPostId)
                .ForeignKey("dbo.Topico", t => t.TopicoId)
                .ForeignKey("dbo.Usuario", t => t.UsuarioId)
                .Index(t => t.TopicoId)
                .Index(t => t.UsuarioId);
            
            CreateTable(
                "dbo.VideoComentario",
                c => new
                    {
                        VideoComentarioId = c.Guid(nullable: false),
                        VideoId = c.Guid(nullable: false),
                        UsuarioId = c.Guid(nullable: false),
                        DescricaoComentario = c.Binary(nullable: false),
                        DataComentario = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        Uer = c.Guid(),
                        Der = c.DateTime(precision: 7, storeType: "datetime2"),
                    })
                .PrimaryKey(t => t.VideoComentarioId)
                .ForeignKey("dbo.Usuario", t => t.UsuarioId)
                .ForeignKey("dbo.Video", t => t.VideoId)
                .Index(t => t.VideoId)
                .Index(t => t.UsuarioId);
            
            CreateTable(
                "dbo.Video",
                c => new
                    {
                        VideoId = c.Guid(nullable: false),
                        UsuarioId = c.Guid(nullable: false),
                        Url = c.String(nullable: false, maxLength: 100, unicode: false),
                        NomeVideo = c.Binary(nullable: false),
                        DataVideo = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        Der = c.DateTime(precision: 7, storeType: "datetime2"),
                    })
                .PrimaryKey(t => t.VideoId)
                .ForeignKey("dbo.Usuario", t => t.UsuarioId)
                .Index(t => t.UsuarioId);
            
            CreateTable(
                "dbo.VisitantePerfil",
                c => new
                    {
                        VisitanteId = c.Guid(nullable: false),
                        VisitadoId = c.Guid(nullable: false),
                        DataVisita = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                    })
                .PrimaryKey(t => new { t.VisitanteId, t.VisitadoId, t.DataVisita })
                .ForeignKey("dbo.Usuario", t => t.VisitadoId)
                .ForeignKey("dbo.Usuario", t => t.VisitanteId)
                .Index(t => t.VisitanteId)
                .Index(t => t.VisitadoId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.VisitantePerfil", "VisitanteId", "dbo.Usuario");
            DropForeignKey("dbo.VisitantePerfil", "VisitadoId", "dbo.Usuario");
            DropForeignKey("dbo.VideoComentario", "VideoId", "dbo.Video");
            DropForeignKey("dbo.Video", "UsuarioId", "dbo.Usuario");
            DropForeignKey("dbo.VideoComentario", "UsuarioId", "dbo.Usuario");
            DropForeignKey("dbo.TopicoPost", "UsuarioId", "dbo.Usuario");
            DropForeignKey("dbo.TopicoPost", "TopicoId", "dbo.Topico");
            DropForeignKey("dbo.Topico", "UsuarioId", "dbo.Usuario");
            DropForeignKey("dbo.Topico", "ComunidadeId", "dbo.Comunidade");
            DropForeignKey("dbo.RecadoComentario", "UsuarioId", "dbo.Usuario");
            DropForeignKey("dbo.RecadoComentario", "RecadoId", "dbo.Recado");
            DropForeignKey("dbo.Recado", "EnviadoPorId", "dbo.Usuario");
            DropForeignKey("dbo.Recado", "EnviadoParaId", "dbo.Usuario");
            DropForeignKey("dbo.PostPerfilComentario", "UsuarioId", "dbo.Usuario");
            DropForeignKey("dbo.PostPerfilComentario", "PostPerfilId", "dbo.PostPerfil");
            DropForeignKey("dbo.PostPerfilBloqueado", "UsuarioId", "dbo.Usuario");
            DropForeignKey("dbo.PostOculto", "UsuarioId", "dbo.Usuario");
            DropForeignKey("dbo.PostOculto", "PostPerfilId", "dbo.PostPerfil");
            DropForeignKey("dbo.PostPerfil", "UsuarioId", "dbo.Usuario");
            DropForeignKey("dbo.Moderador", "UsuarioOperacaoId", "dbo.Usuario");
            DropForeignKey("dbo.Moderador", "UsuarioModeradorId", "dbo.Usuario");
            DropForeignKey("dbo.Moderador", "ComunidadeId", "dbo.Comunidade");
            DropForeignKey("dbo.Membro", "UsuarioRespostaId", "dbo.Usuario");
            DropForeignKey("dbo.Membro", "UsuarioMembroId", "dbo.Usuario");
            DropForeignKey("dbo.Membro", "ComunidadeId", "dbo.Comunidade");
            DropForeignKey("dbo.Depoimento", "EnviadoPorId", "dbo.Usuario");
            DropForeignKey("dbo.Depoimento", "EnviadoParaId", "dbo.Usuario");
            DropForeignKey("dbo.Comunidade", "UsuarioId", "dbo.Usuario");
            DropForeignKey("dbo.Comunidade", "CategoriaId", "dbo.Categoria");
            DropForeignKey("dbo.Amizade", "SolicitadoPorId", "dbo.Usuario");
            DropForeignKey("dbo.Amizade", "SolicitadoParaId", "dbo.Usuario");
            DropForeignKey("dbo.Privacidade", "UsuarioId", "dbo.Usuario");
            DropForeignKey("dbo.Perfil", "UsuarioId", "dbo.Usuario");
            DropIndex("dbo.VisitantePerfil", new[] { "VisitadoId" });
            DropIndex("dbo.VisitantePerfil", new[] { "VisitanteId" });
            DropIndex("dbo.Video", new[] { "UsuarioId" });
            DropIndex("dbo.VideoComentario", new[] { "UsuarioId" });
            DropIndex("dbo.VideoComentario", new[] { "VideoId" });
            DropIndex("dbo.TopicoPost", new[] { "UsuarioId" });
            DropIndex("dbo.TopicoPost", new[] { "TopicoId" });
            DropIndex("dbo.Topico", new[] { "UsuarioId" });
            DropIndex("dbo.Topico", new[] { "ComunidadeId" });
            DropIndex("dbo.Recado", new[] { "EnviadoParaId" });
            DropIndex("dbo.Recado", new[] { "EnviadoPorId" });
            DropIndex("dbo.RecadoComentario", new[] { "UsuarioId" });
            DropIndex("dbo.RecadoComentario", new[] { "RecadoId" });
            DropIndex("dbo.PostPerfilComentario", new[] { "UsuarioId" });
            DropIndex("dbo.PostPerfilComentario", new[] { "PostPerfilId" });
            DropIndex("dbo.PostPerfilBloqueado", new[] { "UsuarioId" });
            DropIndex("dbo.PostPerfil", new[] { "UsuarioId" });
            DropIndex("dbo.PostOculto", new[] { "PostPerfilId" });
            DropIndex("dbo.PostOculto", new[] { "UsuarioId" });
            DropIndex("dbo.Moderador", new[] { "UsuarioOperacaoId" });
            DropIndex("dbo.Moderador", new[] { "UsuarioModeradorId" });
            DropIndex("dbo.Moderador", new[] { "ComunidadeId" });
            DropIndex("dbo.Membro", new[] { "UsuarioRespostaId" });
            DropIndex("dbo.Membro", new[] { "UsuarioMembroId" });
            DropIndex("dbo.Membro", new[] { "ComunidadeId" });
            DropIndex("dbo.Depoimento", new[] { "EnviadoParaId" });
            DropIndex("dbo.Depoimento", new[] { "EnviadoPorId" });
            DropIndex("dbo.Comunidade", new[] { "CategoriaId" });
            DropIndex("dbo.Comunidade", "IX_ALIAS_CMM");
            DropIndex("dbo.Comunidade", new[] { "UsuarioId" });
            DropIndex("dbo.Privacidade", new[] { "UsuarioId" });
            DropIndex("dbo.Perfil", new[] { "Alias" });
            DropIndex("dbo.Perfil", new[] { "UsuarioId" });
            DropIndex("dbo.Amizade", new[] { "SolicitadoParaId" });
            DropIndex("dbo.Amizade", new[] { "SolicitadoPorId" });
            DropTable("dbo.VisitantePerfil");
            DropTable("dbo.Video");
            DropTable("dbo.VideoComentario");
            DropTable("dbo.TopicoPost");
            DropTable("dbo.Topico");
            DropTable("dbo.Recado");
            DropTable("dbo.RecadoComentario");
            DropTable("dbo.PostPerfilComentario");
            DropTable("dbo.PostPerfilBloqueado");
            DropTable("dbo.PostPerfil");
            DropTable("dbo.PostOculto");
            DropTable("dbo.Moderador");
            DropTable("dbo.Membro");
            DropTable("dbo.Depoimento");
            DropTable("dbo.Connection");
            DropTable("dbo.Comunidade");
            DropTable("dbo.Categoria");
            DropTable("dbo.Privacidade");
            DropTable("dbo.Perfil");
            DropTable("dbo.Usuario");
            DropTable("dbo.Amizade");
        }
    }
}
