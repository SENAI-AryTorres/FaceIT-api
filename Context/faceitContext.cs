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
        public virtual DbSet<BancoDados> BancoDados { get; set; }
        public virtual DbSet<Candidato> Candidato { get; set; }
        public virtual DbSet<Endereco> Endereco { get; set; }
        public virtual DbSet<Framework> Framework { get; set; }
        public virtual DbSet<Ide> Ide { get; set; }
        public virtual DbSet<Idioma> Idioma { get; set; }
        public virtual DbSet<Imagem> Imagem { get; set; }
        public virtual DbSet<LinguagemProgramacao> LinguagemProgramacao { get; set; }
        public virtual DbSet<Pessoa> Pessoa { get; set; }
        public virtual DbSet<PessoaFisica> PessoaFisica { get; set; }
        public virtual DbSet<PessoaJuridica> PessoaJuridica { get; set; }
        public virtual DbSet<Plataforma> Plataforma { get; set; }
        public virtual DbSet<Proposta> Proposta { get; set; }
        public virtual DbSet<Skill> Skill { get; set; }
        public virtual DbSet<SkillDb> SkillDb { get; set; }
        public virtual DbSet<SkillFw> SkillFw { get; set; }
        public virtual DbSet<SkillIde> SkillIde { get; set; }
        public virtual DbSet<SkillIdioma> SkillIdioma { get; set; }
        public virtual DbSet<SkillLp> SkillLp { get; set; }
        public virtual DbSet<SkillPlataforma> SkillPlataforma { get; set; }
        public virtual DbSet<SkillVersionamento> SkillVersionamento { get; set; }
        public virtual DbSet<Versionamento> Versionamento { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer("Server=face-it.mssql.somee.com ;Database=face-it;User id=DonSantos_SQLLogin_1;pwd=zuradk54yr;");
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

            modelBuilder.Entity<BancoDados>(entity =>
            {
                entity.Property(e => e.Id)
                    .HasColumnName("ID")
                    .ValueGeneratedNever();

                entity.Property(e => e.Db)
                    .HasColumnName("db")
                    .HasMaxLength(150)
                    .IsUnicode(false);
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

            modelBuilder.Entity<Framework>(entity =>
            {
                entity.Property(e => e.Id)
                    .HasColumnName("ID")
                    .ValueGeneratedNever();

                entity.Property(e => e.Fw)
                    .HasMaxLength(150)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Ide>(entity =>
            {
                entity.ToTable("IDE");

                entity.Property(e => e.Id)
                    .HasColumnName("ID")
                    .ValueGeneratedNever();

                entity.Property(e => e.Nome)
                    .HasMaxLength(150)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Idioma>(entity =>
            {
                entity.Property(e => e.Id)
                    .HasColumnName("ID")
                    .ValueGeneratedNever();

                entity.Property(e => e.Nome)
                    .HasMaxLength(150)
                    .IsUnicode(false);
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

            modelBuilder.Entity<LinguagemProgramacao>(entity =>
            {
                entity.Property(e => e.Id)
                    .HasColumnName("ID")
                    .ValueGeneratedNever();

                entity.Property(e => e.Linguagem)
                    .HasMaxLength(150)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Pessoa>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Email)
                    .HasMaxLength(150)
                    .IsUnicode(false);

                entity.Property(e => e.GoogleId).HasColumnName("GoogleID");

                entity.Property(e => e.Idskill).HasColumnName("IDSkill");

                entity.Property(e => e.Senha)
                    .HasMaxLength(150)
                    .IsUnicode(false);

                entity.Property(e => e.Tipo)
                    .HasMaxLength(2)
                    .IsUnicode(false);

                entity.HasOne(d => d.IdskillNavigation)
                    .WithMany(p => p.Pessoa)
                    .HasForeignKey(d => d.Idskill)
                    .HasConstraintName("FK_Pessoa_Skill");
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

            modelBuilder.Entity<Plataforma>(entity =>
            {
                entity.Property(e => e.Id)
                    .HasColumnName("ID")
                    .ValueGeneratedNever();

                entity.Property(e => e.Plataforma1)
                    .HasColumnName("Plataforma")
                    .HasMaxLength(150)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Proposta>(entity =>
            {
                entity.Property(e => e.Id)
                    .HasColumnName("ID")
                    .ValueGeneratedNever();

                entity.Property(e => e.Cidade)
                    .HasMaxLength(150)
                    .IsUnicode(false);

                entity.Property(e => e.Descricao)
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.Idskill).HasColumnName("IDSkill");

                entity.Property(e => e.Latitude)
                    .HasMaxLength(150)
                    .IsUnicode(false);

                entity.Property(e => e.Longitude)
                    .HasMaxLength(150)
                    .IsUnicode(false);

                entity.Property(e => e.TipoContrato)
                    .HasMaxLength(2)
                    .IsUnicode(false);

                entity.HasOne(d => d.IdskillNavigation)
                    .WithMany(p => p.Proposta)
                    .HasForeignKey(d => d.Idskill)
                    .HasConstraintName("FK_Proposta_Skill");
            });

            modelBuilder.Entity<Skill>(entity =>
            {
                entity.Property(e => e.Id)
                    .HasColumnName("ID")
                    .ValueGeneratedNever();
            });

            modelBuilder.Entity<SkillDb>(entity =>
            {
                entity.HasKey(e => new { e.Idskill, e.Iddb });

                entity.ToTable("Skill_DB");

                entity.Property(e => e.Idskill).HasColumnName("IDSkill");

                entity.Property(e => e.Iddb).HasColumnName("IDDB");

                entity.HasOne(d => d.IddbNavigation)
                    .WithMany(p => p.SkillDb)
                    .HasForeignKey(d => d.Iddb)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Skill_DB_BancoDados");

                entity.HasOne(d => d.IdskillNavigation)
                    .WithMany(p => p.SkillDb)
                    .HasForeignKey(d => d.Idskill)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Skill_DB_Skill");
            });

            modelBuilder.Entity<SkillFw>(entity =>
            {
                entity.HasKey(e => new { e.Idskill, e.Idfw });

                entity.ToTable("Skill_FW");

                entity.Property(e => e.Idskill).HasColumnName("IDSkill");

                entity.Property(e => e.Idfw).HasColumnName("IDFW");

                entity.HasOne(d => d.IdfwNavigation)
                    .WithMany(p => p.SkillFw)
                    .HasForeignKey(d => d.Idfw)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Skill_FW_Framework");

                entity.HasOne(d => d.IdskillNavigation)
                    .WithMany(p => p.SkillFw)
                    .HasForeignKey(d => d.Idskill)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Skill_FW_Skill");
            });

            modelBuilder.Entity<SkillIde>(entity =>
            {
                entity.HasKey(e => new { e.Idskill, e.Idide });

                entity.ToTable("Skill_IDE");

                entity.Property(e => e.Idskill).HasColumnName("IDSkill");

                entity.Property(e => e.Idide).HasColumnName("IDIDE");

                entity.HasOne(d => d.IdideNavigation)
                    .WithMany(p => p.SkillIde)
                    .HasForeignKey(d => d.Idide)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Skill_IDE_IDE");

                entity.HasOne(d => d.IdskillNavigation)
                    .WithMany(p => p.SkillIde)
                    .HasForeignKey(d => d.Idskill)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Skill_IDE_Skill");
            });

            modelBuilder.Entity<SkillIdioma>(entity =>
            {
                entity.HasKey(e => new { e.Idskill, e.Ididioma });

                entity.ToTable("Skill_Idioma");

                entity.Property(e => e.Idskill).HasColumnName("IDSkill");

                entity.Property(e => e.Ididioma).HasColumnName("IDIdioma");

                entity.HasOne(d => d.IdidiomaNavigation)
                    .WithMany(p => p.SkillIdioma)
                    .HasForeignKey(d => d.Ididioma)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Skill_Idioma_Idioma");

                entity.HasOne(d => d.IdskillNavigation)
                    .WithMany(p => p.SkillIdioma)
                    .HasForeignKey(d => d.Idskill)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Skill_Idioma_Skill");
            });

            modelBuilder.Entity<SkillLp>(entity =>
            {
                entity.HasKey(e => new { e.Idskill, e.Idlp });

                entity.ToTable("Skill_Lp");

                entity.Property(e => e.Idskill).HasColumnName("IDSkill");

                entity.Property(e => e.Idlp).HasColumnName("IDLp");

                entity.HasOne(d => d.IdlpNavigation)
                    .WithMany(p => p.SkillLp)
                    .HasForeignKey(d => d.Idlp)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Skill_Lp_LinguagemProgramacao");

                entity.HasOne(d => d.IdskillNavigation)
                    .WithMany(p => p.SkillLp)
                    .HasForeignKey(d => d.Idskill)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Skill_Lp_Skill");
            });

            modelBuilder.Entity<SkillPlataforma>(entity =>
            {
                entity.HasKey(e => new { e.Idskill, e.Idplataforma });

                entity.ToTable("Skill_Plataforma");

                entity.Property(e => e.Idskill).HasColumnName("IDSkill");

                entity.Property(e => e.Idplataforma).HasColumnName("IDPlataforma");

                entity.HasOne(d => d.IdplataformaNavigation)
                    .WithMany(p => p.SkillPlataforma)
                    .HasForeignKey(d => d.Idplataforma)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Skill_Plataforma_Plataforma");

                entity.HasOne(d => d.IdskillNavigation)
                    .WithMany(p => p.SkillPlataforma)
                    .HasForeignKey(d => d.Idskill)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Skill_Plataforma_Skill");
            });

            modelBuilder.Entity<SkillVersionamento>(entity =>
            {
                entity.HasKey(e => new { e.Idskill, e.Idversionamento });

                entity.ToTable("Skill_Versionamento");

                entity.Property(e => e.Idskill).HasColumnName("IDSkill");

                entity.Property(e => e.Idversionamento).HasColumnName("IDVersionamento");

                entity.HasOne(d => d.IdskillNavigation)
                    .WithMany(p => p.SkillVersionamento)
                    .HasForeignKey(d => d.Idskill)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Skill_Versionamento_Skill");

                entity.HasOne(d => d.IdversionamentoNavigation)
                    .WithMany(p => p.SkillVersionamento)
                    .HasForeignKey(d => d.Idversionamento)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Skill_Versionamento_Versionamento");
            });

            modelBuilder.Entity<Versionamento>(entity =>
            {
                entity.Property(e => e.Id)
                    .HasColumnName("ID")
                    .ValueGeneratedNever();

                entity.Property(e => e.Ferramenta)
                    .HasMaxLength(150)
                    .IsUnicode(false);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
