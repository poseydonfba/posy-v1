﻿using POSY.Domain.Entities;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Infrastructure.Annotations;
using System.Data.Entity.ModelConfiguration;

namespace POSY.Infra.Data.EntityConfig
{
    public class PerfilConfig : EntityTypeConfiguration<Perfil>
    {
        public PerfilConfig()
        {
            ToTable("Perfil");

            HasKey(x => x.UsuarioId);

            Property(x => x.Nome).HasMaxLength(Perfil.NomeMaxLength).IsRequired();
            Property(x => x.Sobrenome).HasMaxLength(Perfil.SobrenomeMaxLength).IsRequired();

            Property(x => x.Alias)
                .HasMaxLength(Perfil.AliasMaxLength)
                .HasColumnAnnotation(
                    IndexAnnotation.AnnotationName,
                    new IndexAnnotation(new IndexAttribute("IX_Alias", 1) { IsUnique = true }))
                .IsRequired();

            Property(x => x.PaisId).IsRequired();
            Property(x => x.DataNascimento).HasColumnType("date").IsRequired();
            Property(x => x.Sexo).IsRequired();
            Property(x => x.EstadoCivil).IsRequired();

            Ignore(x => x.Foto);
            Ignore(x => x.PerfilHtml);
            Ignore(x => x.FraseHtml);
        }
    }
}
