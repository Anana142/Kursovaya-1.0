using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Kursovaya_1._0;

public partial class SportclubContext : DbContext
{
    public SportclubContext()
    {
    }

    public SportclubContext(DbContextOptions<SportclubContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Attendance> Attendances { get; set; }

    public virtual DbSet<Branch> Branches { get; set; }

    public virtual DbSet<Client> Clients { get; set; }

    public virtual DbSet<Discount> Discounts { get; set; }

    public virtual DbSet<Graph> Graphs { get; set; }

    public virtual DbSet<Period> Periods { get; set; }

    public virtual DbSet<Post> Posts { get; set; }

    public virtual DbSet<Sale> Sales { get; set; }

    public virtual DbSet<Service> Services { get; set; }

    public virtual DbSet<Serviceworkersgraph> Serviceworkersgraphs { get; set; }

    public virtual DbSet<Subscription> Subscriptions { get; set; }

    public virtual DbSet<Worker> Workers { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseMySql("server=localhost;user=root;password=Craz200gise;database=sportclub", Microsoft.EntityFrameworkCore.ServerVersion.Parse("8.0.32-mysql"));

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .UseCollation("utf8mb4_0900_ai_ci")
            .HasCharSet("utf8mb4");

        modelBuilder.Entity<Attendance>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity
                .ToTable("attendance")
                .UseCollation("utf8mb4_unicode_ci");

            entity.HasIndex(e => e.IdSubscription, "FK_attendance_IdSubscription");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.Date).HasColumnType("datetime");

            entity.HasOne(d => d.IdSubscriptionNavigation).WithMany(p => p.Attendances)
                .HasForeignKey(d => d.IdSubscription)
                .HasConstraintName("FK_attendance_IdSubscription");
        });

        modelBuilder.Entity<Branch>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity
                .ToTable("branch")
                .UseCollation("utf8mb4_unicode_ci");

            entity.Property(e => e.Adress).HasMaxLength(255);
            entity.Property(e => e.Title).HasMaxLength(255);
        });

        modelBuilder.Entity<Client>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity
                .ToTable("client")
                .UseCollation("utf8mb4_unicode_ci");

            entity.Property(e => e.Gender).HasMaxLength(255);
            entity.Property(e => e.Name).HasMaxLength(50);
            entity.Property(e => e.Patronymic).HasMaxLength(255);
            entity.Property(e => e.PhoneNumber).HasMaxLength(255);
            entity.Property(e => e.SurName).HasMaxLength(255);
        });

        modelBuilder.Entity<Discount>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity
                .ToTable("discount")
                .UseCollation("utf8mb4_unicode_ci");

            entity.Property(e => e.Title).HasMaxLength(255);
        });

        modelBuilder.Entity<Graph>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity
                .ToTable("graph")
                .UseCollation("utf8mb4_unicode_ci");

            entity.Property(e => e.DayOfWeek).HasMaxLength(255);
            entity.Property(e => e.EndTime).HasColumnType("time");
            entity.Property(e => e.StartTime).HasColumnType("time");
        });

        modelBuilder.Entity<Period>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity
                .ToTable("period")
                .UseCollation("utf8mb4_unicode_ci");
        });

        modelBuilder.Entity<Post>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity
                .ToTable("posts")
                .UseCollation("utf8mb4_unicode_ci");

            entity.Property(e => e.Post1)
                .HasMaxLength(255)
                .HasColumnName("Post");
        });

        modelBuilder.Entity<Sale>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity
                .ToTable("sale")
                .UseCollation("utf8mb4_unicode_ci");

            entity.HasIndex(e => e.IdSubscription, "FK_sale_IdSubscription");

            entity.HasIndex(e => e.IdWorker, "FK_sale_IdWorker2");

            entity.Property(e => e.Date).HasColumnType("datetime");
            entity.Property(e => e.Sum).HasPrecision(10, 2);

            entity.HasOne(d => d.IdSubscriptionNavigation).WithMany(p => p.Sales)
                .HasForeignKey(d => d.IdSubscription)
                .HasConstraintName("FK_sale_IdSubscription");

            entity.HasOne(d => d.IdWorkerNavigation).WithMany(p => p.Sales)
                .HasForeignKey(d => d.IdWorker)
                .HasConstraintName("FK_sale_IdWorker2");
        });

        modelBuilder.Entity<Service>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity
                .ToTable("service")
                .UseCollation("utf8mb4_unicode_ci");

            entity.HasIndex(e => e.IdBranch, "FK_service_IdBranch");

            entity.Property(e => e.Description).HasMaxLength(255);
            entity.Property(e => e.PricePerHour).HasPrecision(10, 2);
            entity.Property(e => e.Title).HasMaxLength(255);

            entity.HasOne(d => d.IdBranchNavigation).WithMany(p => p.Services)
                .HasForeignKey(d => d.IdBranch)
                .HasConstraintName("FK_service_IdBranch");
        });

        modelBuilder.Entity<Serviceworkersgraph>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity
                .ToTable("serviceworkersgraph")
                .UseCollation("utf8mb4_unicode_ci");

            entity.HasIndex(e => e.IdGraph, "FK_ServiceWorkersGraph_IdGraph");

            entity.HasIndex(e => e.IdService, "FK_serviceworkersgraph_IdService2");

            entity.HasIndex(e => e.IdWorker, "FK_serviceworkersgraph_IdWorker2");

            entity.HasOne(d => d.IdGraphNavigation).WithMany(p => p.Serviceworkersgraphs)
                .HasForeignKey(d => d.IdGraph)
                .HasConstraintName("FK_ServiceWorkersGraph_IdGraph");

            entity.HasOne(d => d.IdServiceNavigation).WithMany(p => p.Serviceworkersgraphs)
                .HasForeignKey(d => d.IdService)
                .HasConstraintName("FK_serviceworkersgraph_IdService2");

            entity.HasOne(d => d.IdWorkerNavigation).WithMany(p => p.Serviceworkersgraphs)
                .HasForeignKey(d => d.IdWorker)
                .HasConstraintName("FK_serviceworkersgraph_IdWorker2");
        });

        modelBuilder.Entity<Subscription>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity
                .ToTable("subscription")
                .UseCollation("utf8mb4_unicode_ci");

            entity.HasIndex(e => e.IdPeriod, "FK_Subscription_IdPeriod");

            entity.HasIndex(e => e.IdClient, "FK_subscription_IdClient");

            entity.HasIndex(e => e.IdDiscount, "FK_subscription_IdDiscount");

            entity.Property(e => e.Status).HasMaxLength(255);

            entity.HasOne(d => d.IdClientNavigation).WithMany(p => p.Subscriptions)
                .HasForeignKey(d => d.IdClient)
                .HasConstraintName("FK_subscription_IdClient");

            entity.HasOne(d => d.IdDiscountNavigation).WithMany(p => p.Subscriptions)
                .HasForeignKey(d => d.IdDiscount)
                .HasConstraintName("FK_subscription_IdDiscount");

            entity.HasOne(d => d.IdPeriodNavigation).WithMany(p => p.Subscriptions)
                .HasForeignKey(d => d.IdPeriod)
                .HasConstraintName("FK_Subscription_IdPeriod");

            entity.HasMany(d => d.IdServices).WithMany(p => p.IdSubscrirtions)
                .UsingEntity<Dictionary<string, object>>(
                    "Subscriptionservice",
                    r => r.HasOne<Serviceworkersgraph>().WithMany()
                        .HasForeignKey("IdService")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK_subscriptionservice_IdService2"),
                    l => l.HasOne<Subscription>().WithMany()
                        .HasForeignKey("IdSubscrirtion")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK_subscriptionservice_IdSubscrirtion"),
                    j =>
                    {
                        j.HasKey("IdSubscrirtion", "IdService")
                            .HasName("PRIMARY")
                            .HasAnnotation("MySql:IndexPrefixLength", new[] { 0, 0 });
                        j.ToTable("subscriptionservice");
                        j.HasIndex(new[] { "IdService" }, "FK_subscriptionservice_IdService2");
                    });
        });

        modelBuilder.Entity<Worker>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity
                .ToTable("workers")
                .UseCollation("utf8mb4_unicode_ci");

            entity.HasIndex(e => e.IdPost, "FK_workers_IdPost2");

            entity.Property(e => e.Email).HasMaxLength(50);
            entity.Property(e => e.Gender).HasMaxLength(255);
            entity.Property(e => e.Login).HasMaxLength(255);
            entity.Property(e => e.Name).HasMaxLength(50);
            entity.Property(e => e.PassportDetails).HasMaxLength(255);
            entity.Property(e => e.Password).HasMaxLength(255);
            entity.Property(e => e.Patronymic).HasMaxLength(255);
            entity.Property(e => e.PhoneNumber).HasMaxLength(255);
            entity.Property(e => e.PlaceOfRegistration).HasMaxLength(255);
            entity.Property(e => e.Surname).HasMaxLength(255);

            entity.HasOne(d => d.IdPostNavigation).WithMany(p => p.Workers)
                .HasForeignKey(d => d.IdPost)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_workers_IdPost2");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
