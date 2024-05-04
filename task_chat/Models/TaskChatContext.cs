using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace task_chat.Models;

public partial class TaskChatContext : DbContext
{
    public TaskChatContext()
    {
    }

    public TaskChatContext(DbContextOptions<TaskChatContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Utente> Utentes { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Data Source=DESKTOP-UQ617D9\\SQLEXPRESS01;Initial Catalog=task_chat;Encrypt=False;Trusted_Connection=true;TrustServerCertificate=false");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Utente>(entity =>
        {
            entity.HasKey(e => e.UtenteId).HasName("PK__Utente__CA5C22532821D798");

            entity.ToTable("Utente");

            entity.HasIndex(e => new { e.CodiceUtente, e.Username }, "UQ__Utente__CEAFE985FCF2A038").IsUnique();

            entity.Property(e => e.UtenteId).HasColumnName("utenteID");
            entity.Property(e => e.CodiceUtente)
                .HasMaxLength(250)
                .IsUnicode(false)
                .HasColumnName("codice_utente");
            entity.Property(e => e.IsDeleted).HasColumnName("isDeleted");
            entity.Property(e => e.PasswordUtente)
                .HasMaxLength(250)
                .IsUnicode(false)
                .HasColumnName("password_utente");
            entity.Property(e => e.Username)
                .HasMaxLength(250)
                .IsUnicode(false)
                .HasColumnName("username");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
