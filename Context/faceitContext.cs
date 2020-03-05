using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using faceitapi.Models;

namespace faceitapi.Context
{
    public partial class faceitContext : DbContext
    {
        public faceitContext()
        {
        }

        public faceitContext(DbContextOptions<faceitContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Anexo> Anexo { get; set; }
        public virtual DbSet<Candidato> Candidato { get; set; }
        public virtual DbSet<Endereco> Endereco { get; set; }
        public virtual DbSet<Imagem> Imagem { get; set; }
        public virtual DbSet<Pessoa> Pessoa { get; set; }
        public virtual DbSet<PessoaFisica> PessoaFisica { get; set; }
        public virtual DbSet<PessoaJuridica> PessoaJuridica { get; set; }
        public virtual DbSet<PessoaSkill> PessoaSkill { get; set; }
        public virtual DbSet<Proposta> Proposta { get; set; }
        public virtual DbSet<PropostaSkill> PropostaSkill { get; set; }
        public virtual DbSet<Skill> Skill { get; set; }
        public virtual DbSet<TipoSkill> TipoSkill { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer("Server=face-it.mssql.somee.com;Database=face-it;user=DonSantos_SQLLogin_1;password=zuradk54yr;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Anexo>(entity =>
            {
                entity.HasKey(e => e.Idpessoa);

                entity.Property(e => e.Idpessoa)
                    .HasColumnName("IDPessoa")
                    .ValueGeneratedNever();

                entity.Property(e => e.Bytes)
                    .IsRequired()
                    .HasMaxLength(500)
                    .IsFixedLength();

                entity.Property(e => e.Nome)
                    .HasMaxLength(150)
                    .IsUnicode(false);

                entity.HasOne(d => d.IdpessoaNavigation)
                    .WithOne(p => p.Anexo)
                    .HasForeignKey<Anexo>(d => d.Idpessoa)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Anexo_Pessoa");
            });

            modelBuilder.Entity<Candidato>(entity =>
            {
                entity.HasKey(e => new { e.Idproposta, e.Idpessoa });

                entity.Property(e => e.Idproposta).HasColumnName("IDProposta");

                entity.Property(e => e.Idpessoa).HasColumnName("IDPessoa");

                entity.HasOne(d => d.IdpessoaNavigation)
                    .WithMany(p => p.Candidato)
                    .HasForeignKey(d => d.Idpessoa)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Candidato_Pessoa");

                entity.HasOne(d => d.IdpropostaNavigation)
                    .WithMany(p => p.Candidato)
                    .HasForeignKey(d => d.Idproposta)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Candidato_Proposta");
            });

            modelBuilder.Entity<Endereco>(entity =>
            {
                entity.HasKey(e => e.Idpessoa);

                entity.Property(e => e.Idpessoa)
                    .HasColumnName("IDPessoa")
                    .ValueGeneratedNever();

                entity.Property(e => e.Bairro)
                    .HasMaxLength(150)
                    .IsUnicode(false);

                entity.Property(e => e.Cep)
                    .HasColumnName("CEP")
                    .HasMaxLength(9)
                    .IsUnicode(false);

                entity.Property(e => e.Complemento)
                    .HasMaxLength(150)
                    .IsUnicode(false);

                entity.Property(e => e.Logradouro)
                    .HasMaxLength(150)
                    .IsUnicode(false);

                entity.Property(e => e.Municipio)
                    .HasMaxLength(150)
                    .IsUnicode(false);

                entity.Property(e => e.Numero)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Pais)
                    .HasMaxLength(150)
                    .IsUnicode(false);

                entity.Property(e => e.Uf)
                    .HasColumnName("UF")
                    .HasMaxLength(2)
                    .IsUnicode(false);

                entity.HasOne(d => d.IdpessoaNavigation)
                    .WithOne(p => p.Endereco)
                    .HasForeignKey<Endereco>(d => d.Idpessoa)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Endereco_Pessoa");
            });

            modelBuilder.Entity<Imagem>(entity =>
            {
                entity.HasKey(e => e.Idpessoa);

                entity.Property(e => e.Idpessoa)
                    .HasColumnName("IDPessoa")
                    .ValueGeneratedNever();

                entity.Property(e => e.Bytes)
                    .IsRequired()
                    .HasMaxLength(500)
                    .IsFixedLength();

                entity.Property(e => e.Nome)
                    .HasMaxLength(150)
                    .IsUnicode(false);

                entity.HasOne(d => d.IdpessoaNavigation)
                    .WithOne(p => p.Imagem)
                    .HasForeignKey<Imagem>(d => d.Idpessoa)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Imagem_Pessoa");
            });

            modelBuilder.Entity<Pessoa>(entity =>
            {
                entity.HasKey(e => e.Idpessoa);

                entity.Property(e => e.Idpessoa).HasColumnName("IDPessoa");

                entity.Property(e => e.Email)
                    .HasMaxLength(150)
                    .IsUnicode(false);

                entity.Property(e => e.GoogleId).HasColumnName("GoogleID");

                entity.Property(e => e.Senha)
                    .HasMaxLength(150)
                    .IsUnicode(false);

                entity.Property(e => e.Tipo)
                    .HasMaxLength(2)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<PessoaFisica>(entity =>
            {
                entity.HasKey(e => e.Idpessoa);

                entity.Property(e => e.Idpessoa)
                    .HasColumnName("IDPessoa")
                    .ValueGeneratedNever();

                entity.Property(e => e.Cpf)
                    .IsRequired()
                    .HasColumnName("CPF")
                    .HasMaxLength(14)
                    .IsUnicode(false);

                entity.Property(e => e.Nome)
                    .HasMaxLength(150)
                    .IsUnicode(false);

                entity.Property(e => e.Rg)
                    .HasColumnName("RG")
                    .HasMaxLength(45)
                    .IsUnicode(false);

                entity.HasOne(d => d.IdpessoaNavigation)
                    .WithOne(p => p.PessoaFisica)
                    .HasForeignKey<PessoaFisica>(d => d.Idpessoa)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_PessoaFisica_Pessoa");
            });

            modelBuilder.Entity<PessoaJuridica>(entity =>
            {
                entity.HasKey(e => e.Idpessoa);

                entity.Property(e => e.Idpessoa)
                    .HasColumnName("IDPessoa")
                    .ValueGeneratedNever();

                entity.Property(e => e.Cnpj)
                    .IsRequired()
                    .HasColumnName("CNPJ")
                    .HasMaxLength(18)
                    .IsUnicode(false);

                entity.Property(e => e.Ie)
                    .HasColumnName("IE")
                    .HasMaxLength(45)
                    .IsUnicode(false);

                entity.Property(e => e.NomeFantasia)
                    .HasMaxLength(150)
                    .IsUnicode(false);

                entity.Property(e => e.RazaoSocial)
                    .HasMaxLength(150)
                    .IsUnicode(false);

                entity.HasOne(d => d.IdpessoaNavigation)
                    .WithOne(p => p.PessoaJuridica)
                    .HasForeignKey<PessoaJuridica>(d => d.Idpessoa)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_PessoaJuridica_Pessoa");
            });

            modelBuilder.Entity<PessoaSkill>(entity =>
            {
                entity.HasKey(e => new { e.Idpessoa, e.Idskill, e.IdtipoSkill });

                entity.Property(e => e.Idpessoa).HasColumnName("IDPessoa");

                entity.Property(e => e.Idskill).HasColumnName("IDSkill");

                entity.Property(e => e.IdtipoSkill).HasColumnName("IDTipoSkill");

                entity.HasOne(d => d.IdpessoaNavigation)
                    .WithMany(p => p.PessoaSkill)
                    .HasForeignKey(d => d.Idpessoa)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_PessoaSkill_Pessoa");

                entity.HasOne(d => d.Id)
                    .WithMany(p => p.PessoaSkill)
                    .HasForeignKey(d => new { d.Idskill, d.IdtipoSkill })
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_PessoaSkill_Skill");
            });

            modelBuilder.Entity<Proposta>(entity =>
            {
                entity.HasKey(e => e.Idproposta);

                entity.Property(e => e.Idproposta)
                    .HasColumnName("IDProposta")
                    .ValueGeneratedNever();

                entity.Property(e => e.Cidade)
                    .HasMaxLength(150)
                    .IsUnicode(false);

                entity.Property(e => e.Descricao)
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.Latitude)
                    .HasMaxLength(150)
                    .IsUnicode(false);

                entity.Property(e => e.Longitude)
                    .HasMaxLength(150)
                    .IsUnicode(false);

                entity.Property(e => e.TipoContrato)
                    .HasMaxLength(2)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<PropostaSkill>(entity =>
            {
                entity.HasKey(e => new { e.Idproposta, e.Idskill, e.IdtipoSkill });

                entity.Property(e => e.Idproposta).HasColumnName("IDProposta");

                entity.Property(e => e.Idskill).HasColumnName("IDSkill");

                entity.Property(e => e.IdtipoSkill).HasColumnName("IDTipoSkill");

                entity.HasOne(d => d.IdpropostaNavigation)
                    .WithMany(p => p.PropostaSkill)
                    .HasForeignKey(d => d.Idproposta)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_PropostaSkill_Proposta");

                entity.HasOne(d => d.Id)
                    .WithMany(p => p.PropostaSkill)
                    .HasForeignKey(d => new { d.Idskill, d.IdtipoSkill })
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_PropostaSkill_Skill");
            });

            modelBuilder.Entity<Skill>(entity =>
            {
                entity.HasKey(e => new { e.Idskill, e.IdtipoSkill });

                entity.Property(e => e.Idskill).HasColumnName("IDSkill");

                entity.Property(e => e.IdtipoSkill).HasColumnName("IDTipoSkill");

                entity.Property(e => e.Descricao)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.HasOne(d => d.IdtipoSkillNavigation)
                    .WithMany(p => p.Skill)
                    .HasForeignKey(d => d.IdtipoSkill)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Skill_TipoSkill");
            });

            modelBuilder.Entity<TipoSkill>(entity =>
            {
                entity.HasKey(e => e.IdtipoSkill);

                entity.Property(e => e.IdtipoSkill)
                    .HasColumnName("IDTipoSkill")
                    .ValueGeneratedNever();

                entity.Property(e => e.Descricao)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
