using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Lingomon_asp.Data;

public partial class TheDbContext : DbContext
{
    public TheDbContext()
    {
    }

    public TheDbContext(DbContextOptions<DbContext> options) : base(options)
    {
    }

    public virtual DbSet<Answer> Answers { get; set; }

    public virtual DbSet<Class> Classes { get; set; }

    public virtual DbSet<Enduser> Endusers { get; set; }

    public virtual DbSet<Language> Languages { get; set; }

    public virtual DbSet<Lingodex> Lingodices { get; set; }

    public virtual DbSet<Lingomon> Lingomons { get; set; }

    public virtual DbSet<Npc> Npcs { get; set; }

    public virtual DbSet<Question> Questions { get; set; }

    public virtual DbSet<Type> Types { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseNpgsql("Server=lingomon.postgres.database.azure.com;Port=5432;Database=postgres;User Id=postgres;Password=LingoMon2023Summer;");

    // config all entities
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .HasPostgresExtension("pg_catalog", "azure")
            .HasPostgresExtension("pg_catalog", "pgaadauth")
            .HasPostgresExtension("pg_cron");

        // config Answer entity
        modelBuilder.Entity<Answer>(entity =>
        {
            entity.HasKey(e => e.Questionid).HasName("answers_pkey");

            // config table
            entity.ToTable("answers");

            //config properties
            entity.Property(e => e.Questionid)
                .ValueGeneratedNever()
                .HasColumnName("questionid");
            entity.Property(e => e.Correct)
                .HasMaxLength(100)
                .HasColumnName("correct");
            entity.Property(e => e.Wrong1)
                .HasMaxLength(100)
                .HasColumnName("wrong1");
            entity.Property(e => e.Wrong2)
                .HasMaxLength(100)
                .HasColumnName("wrong2");
            entity.Property(e => e.Wrong3)
                .HasMaxLength(100)
                .HasColumnName("wrong3");

            // config foreign keys
            entity.HasOne(d => d.Question).WithOne(p => p.Answer)
                .HasForeignKey<Answer>(d => d.Questionid)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("answers_questionid_fkey");
        });

        // config Class entity
        modelBuilder.Entity<Class>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("class_pkey");

            // config table
            entity.ToTable("class");

            //config properties
            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Level).HasColumnName("level");
        });

        // config EndUser entity
        modelBuilder.Entity<Enduser>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("enduser_pkey");

            // config table
            entity.ToTable("enduser");

            //config properties
            entity.Property(e => e.Id)
                .HasMaxLength(30)
                .HasColumnName("id");
            entity.Property(e => e.Classid).HasColumnName("classid");
            entity.Property(e => e.Conjugation).HasColumnName("conjugation");
            entity.Property(e => e.Diction).HasColumnName("diction");
            entity.Property(e => e.Grammar).HasColumnName("grammar");
            entity.Property(e => e.Lingomon).HasColumnName("lingomon");
            entity.Property(e => e.Password)
                .HasMaxLength(30)
                .HasColumnName("password");
            entity.Property(e => e.Spelling).HasColumnName("spelling");

            // config foreign keys
            entity.HasOne(d => d.Class).WithMany(p => p.Endusers)
                .HasForeignKey(d => d.Classid)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("enduser_classid_fkey");

            entity.HasOne(d => d.LingomonNavigation).WithMany(p => p.Endusers)
                .HasForeignKey(d => d.Lingomon)
                .HasConstraintName("enduser_lingomon_fkey");
        });

        // config Language entity
        modelBuilder.Entity<Language>(entity =>
        {
            entity.HasKey(e => e.Name).HasName("language_pkey");

            // config table
            entity.ToTable("language");

            //config properties
            entity.Property(e => e.Name)
                .HasMaxLength(30)
                .HasColumnName("name");
        });

        // config Lingodex entity
        modelBuilder.Entity<Lingodex>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("lingodex_pkey");

            // config table
            entity.ToTable("lingodex");

            entity.HasIndex(e => e.Name, "lingodex_name_key").IsUnique();

            //config properties
            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Hp).HasColumnName("hp");
            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .HasColumnName("name");
            entity.Property(e => e.Typeid).HasColumnName("typeid");

            // config foreign keys
            entity.HasOne(d => d.Type).WithMany(p => p.Lingodices)
                .HasForeignKey(d => d.Typeid)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("lingodex_typeid_fkey");

            // config foreign keys
            entity.HasMany(d => d.Questions).WithMany(p => p.Dices)
                .UsingEntity<Dictionary<string, object>>(
                    "Dextoquestion",
                    r => r.HasOne<Question>().WithMany()
                        .HasForeignKey("Questionid")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("dextoquestion_questionid_fkey"),
                    l => l.HasOne<Lingodex>().WithMany()
                        .HasForeignKey("Dexid")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("dextoquestion_dexid_fkey"),
                    j =>
                    {
                        j.HasKey("Dexid", "Questionid").HasName("dextoquestion_pkey");
                        j.ToTable("dextoquestion");
                        j.IndexerProperty<int>("Dexid").HasColumnName("dexid");
                        j.IndexerProperty<int>("Questionid").HasColumnName("questionid");
                    });
        });

        // config Lingomon entity
        modelBuilder.Entity<Lingomon>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("lingomon_pkey");

            // config table
            entity.ToTable("lingomon");

            //config properties
            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Dexid).HasColumnName("dexid");

            // config foreign keys
            entity.HasOne(d => d.Dex).WithMany(p => p.Lingomons)
                .HasForeignKey(d => d.Dexid)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("lingomon_dexid_fkey");
        });

        // config NPC entity
        modelBuilder.Entity<Npc>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("npc_pkey");

            // config table
            entity.ToTable("npc");

            //config properties
            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Dialogue)
                .HasMaxLength(500)
                .HasColumnName("dialogue");
            entity.Property(e => e.Lingomon).HasColumnName("lingomon");
            entity.Property(e => e.Name)
                .HasMaxLength(30)
                .HasColumnName("name");

            // config foreign keys
            entity.HasOne(d => d.LingomonNavigation).WithMany(p => p.Npcs)
                .HasForeignKey(d => d.Lingomon)
                .HasConstraintName("npc_lingomon_fkey");
        });

        // config Question entity
        modelBuilder.Entity<Question>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("question_pkey");

            // config table
            entity.ToTable("question");

            entity.HasIndex(e => e.Prompt, "question_prompt_key").IsUnique();

            //config properties
            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Language)
                .HasMaxLength(30)
                .HasColumnName("language");
            entity.Property(e => e.Prompt)
                .HasMaxLength(150)
                .HasColumnName("prompt");
            entity.Property(e => e.Strength).HasColumnName("strength");
            entity.Property(e => e.Typeid).HasColumnName("typeid");

            // config foreign keys
            entity.HasOne(d => d.LanguageNavigation).WithMany(p => p.Questions)
                .HasForeignKey(d => d.Language)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("question_language_fkey");

            // config foreign keys
            entity.HasOne(d => d.Type).WithMany(p => p.Questions)
                .HasForeignKey(d => d.Typeid)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("question_typeid_fkey");
        });

        // config Type entity
        modelBuilder.Entity<Type>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("type_pkey");

            // config table
            entity.ToTable("type");

            entity.HasIndex(e => e.Name, "type_name_key").IsUnique();

            //config properties
            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Name)
                .HasMaxLength(30)
                .HasColumnName("name");
        });
        modelBuilder.HasSequence("jobid_seq", "cron");
        modelBuilder.HasSequence("runid_seq", "cron");

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
