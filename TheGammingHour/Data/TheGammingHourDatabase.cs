using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Microsoft.CodeAnalysis.VisualBasic.Syntax;
using Microsoft.EntityFrameworkCore;
using NuGet.Packaging.Signing;
using UTAD.LAB._2022.TheGammingHour.Models;

namespace UTAD.LAB._2022.TheGammingHour.Data;

public partial class TheGammingHourDatabase : DbContext
{

    public List<string> Admnistrador { get; set; }

    public TheGammingHourDatabase(DbContextOptions<TheGammingHourDatabase> options)
        : base(options)
    {
        Admnistrador = new List<string>();
        Admnistrador.Add("franciscodiogo14@gmail.com");
    }

    public long VerifMod(Utilizador utilizador)
    {
        long id = 0;
        foreach (Grupo g in Grupos)
        {
            if (g.Nome == "Cliente")
            {
                id = g.Id;
            }
        }
        foreach (string email in Admnistrador)
        {
            if (utilizador.Email == email)
            {
                foreach (Grupo g in Grupos)
                {
                    if (g.Nome == "Admnistrador")
                    {
                        id = g.Id;
                        return id;
                    }
                }
            }
        
        }
        return id;
    }

    public virtual DbSet<CategoriaFavoritum> CategoriaFavorita { get; set; }

    public virtual DbSet<CategoriaJogo> CategoriaJogos { get; set; }

    public virtual DbSet<Categoria> Categoria { get; set; }

    public virtual DbSet<CompraJogo> CompraJogos { get; set; }

    public virtual DbSet<Grupo> Grupos { get; set; }

    public virtual DbSet<GrupoMenu> GrupoMenus { get; set; }

    public virtual DbSet<Jogo> Jogos { get; set; }

    public virtual DbSet<Menu> Menus { get; set; }

    public virtual DbSet<Pagamento> Pagamentos { get; set; }

    public virtual DbSet<Plataforma> Plataformas { get; set; }

    public virtual DbSet<PlataformaJogo> PlataformaJogos { get; set; }

    public virtual DbSet<Produtora> Produtoras { get; set; }

    public virtual DbSet<ProdutoraJogo> ProdutoraJogos { get; set; }

    public virtual DbSet<Utilizador> Utilizadors { get; set; }

    public virtual DbSet<UtilizadorGrupo> UtilizadorGrupos { get; set; }

    /*protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseSqlServer("name=TheGammingHourDatabase");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<CategoriaFavoritum>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Categori__3214EC272751D308");

            entity.HasOne(d => d.Categoria).WithMany(p => p.CategoriaFavorita)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Categoria__Categ__46E78A0C");

            entity.HasOne(d => d.Utilizador).WithMany(p => p.CategoriaFavorita)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Categoria__Utili__45F365D3");
        });

        modelBuilder.Entity<CategoriaJogo>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Categori__3214EC2749882C36");

            entity.HasOne(d => d.Categoria).WithMany(p => p.CategoriaJogos)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Categoria__Categ__3D5E1FD2");

            entity.HasOne(d => d.Jogo).WithMany(p => p.CategoriaJogos)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Categoria__JogoI__3E52440B");
        });

        modelBuilder.Entity<Categorium>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Categori__3214EC27D4E0BB54");

            entity.Property(e => e.Nome).IsFixedLength();
        });

        modelBuilder.Entity<CompraJogo>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__CompraJo__3214EC27D707F4FF");

            entity.HasOne(d => d.Jogo).WithMany(p => p.CompraJogos)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__CompraJog__JogoI__4222D4EF");

            entity.HasOne(d => d.Pagamento).WithMany(p => p.CompraJogos)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__CompraJog__Pagam__4316F928");

            entity.HasOne(d => d.Utilizador).WithMany(p => p.CompraJogos)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__CompraJog__Utili__412EB0B6");
        });

        modelBuilder.Entity<Grupo>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Grupo__3214EC27422DB51D");

            entity.Property(e => e.Nome).IsFixedLength();
        });

        modelBuilder.Entity<GrupoMenu>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Grupo_Me__3214EC27962D9892");

            entity.HasOne(d => d.Grupo).WithMany(p => p.GrupoMenus)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Grupo_Men__Grupo__49C3F6B7");

            entity.HasOne(d => d.Menu).WithMany(p => p.GrupoMenus)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Grupo_Men__MenuI__4AB81AF0");
        });

        modelBuilder.Entity<Jogo>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Jogo__3214EC27D296D4AF");

            entity.Property(e => e.Nome).IsFixedLength();
        });

        modelBuilder.Entity<Menu>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Menu__3214EC2791A38DAD");

            entity.Property(e => e.Caption).IsFixedLength();
            entity.Property(e => e.Tooltip).IsFixedLength();
        });

        modelBuilder.Entity<Pagamento>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Pagament__3214EC27B544C204");

            entity.Property(e => e.CodigoPostal).IsFixedLength();
            entity.Property(e => e.Morada).IsFixedLength();
            entity.Property(e => e.Nif).IsFixedLength();

            entity.HasOne(d => d.Utilizador).WithMany(p => p.Pagamentos)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Pagamento__Utili__32E0915F");
        });

        modelBuilder.Entity<Plataforma>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Platafor__3214EC273027F89B");

            entity.Property(e => e.Nome).IsFixedLength();
        });

        modelBuilder.Entity<PlataformaJogo>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Platafor__3214EC271A085479");

            entity.HasOne(d => d.Jogo).WithMany(p => p.PlataformaJogos)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Plataform__JogoI__3A81B327");

            entity.HasOne(d => d.Plataforma).WithMany(p => p.PlataformaJogos)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Plataform__Plata__398D8EEE");
        });

        modelBuilder.Entity<Produtora>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Produtor__3214EC27BCB9BAD1");

            entity.Property(e => e.Nome).IsFixedLength();
        });

        modelBuilder.Entity<ProdutoraJogo>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Produtor__3214EC2735DD9CEB");

            entity.HasOne(d => d.Jogo).WithMany(p => p.ProdutoraJogos)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Produtora__JogoI__36B12243");

            entity.HasOne(d => d.Produtora).WithMany(p => p.ProdutoraJogos)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Produtora__Produ__35BCFE0A");
        });

        modelBuilder.Entity<Utilizador>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Utilizad__3214EC27F04804B6");

            entity.Property(e => e.Email).IsFixedLength();
            entity.Property(e => e.Nome).IsFixedLength();
            entity.Property(e => e.Password).IsFixedLength();
            entity.Property(e => e.Username).IsFixedLength();

            entity.HasOne(d => d.Grupo).WithMany(p => p.Utilizadors)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Utilizado__Grupo__286302EC");
        });

        modelBuilder.Entity<UtilizadorGrupo>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Utilizad__3214EC27FE433541");

            entity.HasOne(d => d.Grupo).WithMany(p => p.UtilizadorGrupos)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Utilizado__Grupo__4E88ABD4");

            entity.HasOne(d => d.Utilizador).WithMany(p => p.UtilizadorGrupos)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Utilizado__Utili__4D94879B");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
*/}
