using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace WebDatabaseFirst.Models
{
    public partial class BibliotecaContext : DbContext
    {
        public BibliotecaContext()
        {
        }

        public BibliotecaContext(DbContextOptions<BibliotecaContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Cliente> Clientes { get; set; }
        public virtual DbSet<Libro> Libros { get; set; }
        public virtual DbSet<Prestamo> Prestamos { get; set; }
        public virtual DbSet<PrestamoLibro> PrestamoLibros { get; set; }

        

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "Modern_Spanish_CI_AS");

            modelBuilder.Entity<Cliente>(entity =>
            {
                entity.ToTable("Cliente");

                entity.Property(e => e.CedulaCliente)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.NombreCliente)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Libro>(entity =>
            {
                entity.ToTable("Libro");

                entity.Property(e => e.AutorLibro)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.NombreLibro)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Prestamo>(entity =>
            {
                entity.ToTable("Prestamo");

                entity.Property(e => e.PrestamoId)
                    .ValueGeneratedOnAdd()
                    .HasColumnName("PrestamoID");

                entity.Property(e => e.Fecha).HasColumnType("date");

                entity.HasOne(d => d.PrestamoNavigation)
                    .WithOne(p => p.Prestamo)
                    .HasForeignKey<Prestamo>(d => d.PrestamoId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Prestamo_Cliente");

                entity.HasOne(d => d.Prestamo1)
                    .WithOne(p => p.Prestamo)
                    .HasForeignKey<Prestamo>(d => d.PrestamoId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Prestamo_PrestamoLibro");
            });

            modelBuilder.Entity<PrestamoLibro>(entity =>
            {
                entity.ToTable("Prestamo_Libro");

                entity.Property(e => e.PrestamoLibroId).ValueGeneratedOnAdd();

                entity.HasOne(d => d.PrestamoLibroNavigation)
                    .WithOne(p => p.PrestamoLibro)
                    .HasForeignKey<PrestamoLibro>(d => d.PrestamoLibroId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Prestamo_Libro_Libro");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
