﻿using GestorContatos.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GestorContatos.Infrastructure.Repository.Configurations;
public class ContatosConfiguration : IEntityTypeConfiguration<Contato>
{
    public void Configure(EntityTypeBuilder<Contato> builder)
    {
        builder.ToTable("Contatos");
        builder.HasKey(p => p.Id);
        builder.Property(p => p.Id).HasColumnType("INT").UseIdentityColumn();
        builder.Property(p => p.Nome).HasColumnName("Nome").HasColumnType("VARCHAR(100)").IsRequired();
        builder.Property(p => p.Telefone).HasColumnName("Telefone").HasColumnType("VARCHAR(20)").IsRequired();
        builder.Property(p => p.Email).HasColumnName("Email").HasColumnType("VARCHAR(100)").IsRequired();

        builder.HasOne(p => p.Regiao)
               .WithMany()
               .HasForeignKey(p => p.RegiaoId)
               .OnDelete(DeleteBehavior.Restrict); // Impede de excluir uma Regiao se houver Contatos que estejam usando essa Regiao.
    }
}
