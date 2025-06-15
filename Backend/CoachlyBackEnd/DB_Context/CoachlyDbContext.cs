using CoachlyBackEnd.Models.Enums;
using Microsoft.EntityFrameworkCore;
using Npgsql;

namespace CoachlyBackEnd.Models;

public partial class CoachlyDbContext : DbContext
{
    public CoachlyDbContext()
    {
    }

    public CoachlyDbContext(DbContextOptions<CoachlyDbContext> options)
        : base(options)
    {
        
    }

    public virtual DbSet<Location> Locations { get; set; }

    public virtual DbSet<Payment> Payments { get; set; }

    public virtual DbSet<Review> Reviews { get; set; }

    public virtual DbSet<Session> Sessions { get; set; }

    public virtual DbSet<SessionParticipant> SessionParticipants { get; set; }

    public virtual DbSet<Specialization> Specializations { get; set; }

    public virtual DbSet<Subscribtion> Subscribtions { get; set; }

    public virtual DbSet<Trainer> Trainers { get; set; }

    public virtual DbSet<TrainerAvailability> TrainerAvailabilities { get; set; }

    public virtual DbSet<TrainerDocument> TrainerDocuments { get; set; }

    public virtual DbSet<TrainerSpecialization> TrainerSpecializations { get; set; }

    public virtual DbSet<User> Users { get; set; }

    public virtual DbSet<UserSubscribtion> UserSubscribtions { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        var dataSourceBuilder =
            new NpgsqlDataSourceBuilder(
                "Host=localhost;Port=5432;Database=CoachlyDB;Username=twinter;Password=koksaer123");
        // optionsBuilder.UseNpgsql("Host=localhost;Port=5432;Database=CoachlyDB;Username=twinter;Password=koksaer123");
        
        NpgsqlConnection.GlobalTypeMapper.EnableDynamicJson();
        
        dataSourceBuilder.EnableUnmappedTypes();
        var dataSource = dataSourceBuilder.Build();

        // optionsBuilder.UseNpgsql(dataSource, o =>
        // {
        //     o.MapEnum<DocumentType>("document_type");
        //     o.MapEnum<PaymentMethod>("payment_method");
        //     o.MapEnum<PaymentStatus>("payment_status");
        //     o.MapEnum<SessionParticipantsStatus>("session_participants_status");
        //     o.MapEnum<SessionStatus>("session_status");
        //     o.MapEnum<SessionType>("session_type");
        //     o.MapEnum<UserRole>("user_role");
        //     o.MapEnum<CurrencyType>("currency_type");
        // });
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .HasPostgresEnum("document_type", new[] { "certificate", "diploma" })
            .HasPostgresEnum("payment_method", new[] { "card", "cash" })
            .HasPostgresEnum("payment_status", new[] { "pending", "succeeded", "failed", "canceled", "refunded", "expired", "in_review" })
            .HasPostgresEnum("session_participants_status", new[] { "accepted", "pending", "rejected" })
            .HasPostgresEnum("session_status", new[] { "scheduled", "completed", "canceled" })
            .HasPostgresEnum("session_type", new[] { "individual", "group" })
            .HasPostgresEnum("user_role", new[] { "Client", "Trainer", "Admin" })
            .HasPostgresEnum("currency_type", new[] { "USD", "EUR", "UAH" });

        modelBuilder.Entity<Location>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("location_pkey");

            entity.ToTable("locations");

            entity.Property(e => e.Id)
                .HasDefaultValueSql("nextval('location_id_seq'::regclass)")
                .HasColumnName("id");
            entity.Property(e => e.BuildingNumber)
                .HasMaxLength(20)
                .HasColumnName("building_number");
            entity.Property(e => e.City)
                .HasMaxLength(100)
                .HasColumnName("city");
            entity.Property(e => e.Country)
                .HasMaxLength(100)
                .HasColumnName("country");
            entity.Property(e => e.GymName)
                .HasMaxLength(100)
                .HasColumnName("gym_name");
            entity.Property(e => e.Street)
                .HasMaxLength(100)
                .HasColumnName("street");
        });

        modelBuilder.Entity<Payment>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("payments_pkey");

            entity.ToTable("payments");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Amount)
                .HasPrecision(10, 2)
                .HasColumnName("amount");
            entity.Property(e => e.PaymentDate)
                .HasDefaultValueSql("now()")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("payment_date");
            entity.Property(e => e.Method).HasColumnType("payment_method").HasColumnName("payment_method");
            entity.Property(e => e.Status).HasColumnType("payment_status").HasColumnName("status");
            entity.Property(e => e.Currency).HasColumnType("currency_type").HasColumnName("currency");
            entity.Property(e => e.StripePaymentId).HasMaxLength(100).HasColumnName("stripe_payment_id");
        });

        modelBuilder.Entity<Review>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("review_pkey");

            entity.ToTable("review");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("now()")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("created_at");
            entity.Property(e => e.Rating)
                .HasPrecision(2, 1)
                .HasColumnName("rating");
            entity.Property(e => e.Text).HasColumnName("text");
            entity.Property(e => e.TrainerId).HasColumnName("trainer_id");
            entity.Property(e => e.UserId).HasColumnName("user_id");

            entity.HasOne(d => d.Trainer).WithMany(p => p.Reviews)
                .HasForeignKey(d => d.TrainerId)
                .HasConstraintName("review_trainer_id_fkey");

            entity.HasOne(d => d.User).WithMany(p => p.Reviews)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("review_user_id_fkey");
        });

        modelBuilder.Entity<Session>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("sessions_pkey");

            entity.ToTable("sessions");

            entity.HasIndex(e => e.PaymentId, "unique_session_payment_id").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.DurationMinutes)
                .HasDefaultValue(60)
                .HasColumnName("duration_minutes");
            entity.Property(e => e.MaxParticipants)
                .HasDefaultValue(1)
                .HasColumnName("max_participants");
            entity.Property(e => e.PaymentId).HasColumnName("payment_id");
            entity.Property(e => e.Price)
                .HasPrecision(10, 2)
                .HasColumnName("price");
            entity.Property(e => e.ScheduledAt)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("scheduled_at");
            entity.Property(e => e.TrainerId).HasColumnName("trainer_id");

            entity.HasOne(d => d.Payment).WithOne(p => p.Session)
                .HasForeignKey<Session>(d => d.PaymentId)
                .HasConstraintName("sessions_payment_id_fkey");

            entity.HasOne(d => d.Trainer).WithMany(p => p.Sessions)
                .HasForeignKey(d => d.TrainerId)
                .HasConstraintName("sessions_trainer_id_fkey");
            
            entity.Property(e => e.Status).HasColumnType("session_status").HasColumnName("status");
            entity.Property(e => e.Type).HasColumnType("session_type").HasColumnName("session_type");
        });

        modelBuilder.Entity<SessionParticipant>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("session_participants_pkey");

            entity.ToTable("session_participants");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.JoinedAt)
                .HasDefaultValueSql("now()")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("joined_at");
            entity.Property(e => e.SessionId).HasColumnName("session_id");
            entity.Property(e => e.UserIs).HasColumnName("user_is");

            entity.HasOne(d => d.Session).WithMany(p => p.SessionParticipants)
                .HasForeignKey(d => d.SessionId)
                .HasConstraintName("session_participants_session_id_fkey");

            entity.HasOne(d => d.UserIsNavigation).WithMany(p => p.SessionParticipants)
                .HasForeignKey(d => d.UserIs)
                .HasConstraintName("session_participants_user_is_fkey");
            
            entity.Property(e => e.Status).HasColumnType("session_participants_status").HasColumnName("status");
        });

        modelBuilder.Entity<Specialization>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("specializations_pkey");

            entity.ToTable("specializations");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Name)
                .HasMaxLength(30)
                .HasColumnName("name");
        });

        modelBuilder.Entity<Subscribtion>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("subscriptions_pkey");

            entity.ToTable("subscribtions");

            entity.HasIndex(e => e.PaymentId, "unique_payment_id").IsUnique();

            entity.Property(e => e.Id)
                .HasDefaultValueSql("nextval('subscriptions_id_seq'::regclass)")
                .HasColumnName("id");
            entity.Property(e => e.Conditions).HasColumnName("conditions");
            entity.Property(e => e.PaymentId).HasColumnName("payment_id");
            entity.Property(e => e.Price)
                .HasPrecision(10, 2)
                .HasColumnName("price");
            entity.Property(e => e.TrainerId).HasColumnName("trainer_id");
            entity.Property(e => e.ValidityPeriod)
                .HasDefaultValueSql("'30 days'::interval")
                .HasColumnType("interval")
                .HasColumnName("validity_period");

            entity.HasOne(d => d.Payment).WithOne(p => p.Subscribtion)
                .HasForeignKey<Subscribtion>(d => d.PaymentId)
                .HasConstraintName("subscriptions_payment_id_fkey");

            entity.HasOne(d => d.Trainer).WithMany(p => p.Subscribtions)
                .HasForeignKey(d => d.TrainerId)
                .HasConstraintName("subscriptions_trainer_id_fkey");
        });

        modelBuilder.Entity<Trainer>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("trainers_pkey");

            entity.ToTable("trainers");

            entity.HasIndex(e => e.UserId, "unique_user_id").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.AvgRating)
                .HasPrecision(2, 1)
                .HasColumnName("avg_rating");
            entity.Property(e => e.Bio).HasColumnName("bio");
            entity.Property(e => e.LocationId).HasColumnName("location_id");
            entity.Property(e => e.UserId).HasColumnName("user_id");

            entity.HasOne(d => d.Location).WithMany(p => p.Trainers)
                .HasForeignKey(d => d.LocationId)
                .HasConstraintName("trainers_location_id_fkey");

            entity.HasOne(d => d.User).WithOne(p => p.Trainer)
                .HasForeignKey<Trainer>(d => d.UserId)
                .HasConstraintName("trainers_user_id_fkey");
        });

        modelBuilder.Entity<TrainerAvailability>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("trainer_availability_pkey");

            entity.ToTable("trainer_availability");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.DayOfWeek).HasColumnName("day_of_week");
            entity.Property(e => e.EndTime)
                .HasColumnType("time without time zone")
                .HasColumnName("end_time");
            entity.Property(e => e.StartTime)
                .HasColumnType("time without time zone")
                .HasColumnName("start_time");
            entity.Property(e => e.TrainerId).HasColumnName("trainer_id");

            entity.HasOne(d => d.Trainer).WithMany(p => p.TrainerAvailabilities)
                .HasForeignKey(d => d.TrainerId)
                .HasConstraintName("trainer_availability_trainer_id_fkey");
        });

        modelBuilder.Entity<TrainerDocument>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("trainer_documents_pkey");

            entity.ToTable("trainer_documents");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.ExpirationDate)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("expiration_date");
            entity.Property(e => e.FileName).HasColumnName("file_name");
            entity.Property(e => e.IssuedDate)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("issued_date");
            entity.Property(e => e.Title)
                .HasMaxLength(100)
                .HasColumnName("title");
            entity.Property(e => e.TrainerId).HasColumnName("trainer_id");
            entity.Property(e => e.UploadedAt)
                .HasDefaultValueSql("now()")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("uploaded_at");

            entity.HasOne(d => d.Trainer).WithMany(p => p.TrainerDocuments)
                .HasForeignKey(d => d.TrainerId)
                .HasConstraintName("trainer_documents_trainer_id_fkey");
        });

        modelBuilder.Entity<TrainerSpecialization>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("trainer_specializations_pkey");

            entity.ToTable("trainer_specializations");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.SpecalizationId).HasColumnName("specalization_id");
            entity.Property(e => e.TrainerId).HasColumnName("trainer_id");

            entity.HasOne(d => d.Specalization).WithMany(p => p.TrainerSpecializations)
                .HasForeignKey(d => d.SpecalizationId)
                .HasConstraintName("trainer_specializations_specalization_id_fkey");

            entity.HasOne(d => d.Trainer).WithMany(p => p.TrainerSpecializations)
                .HasForeignKey(d => d.TrainerId)
                .HasConstraintName("trainer_specializations_trainer_id_fkey");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("users_pkey");

            entity.ToTable("users");

            entity.HasIndex(e => e.Email, "users_email_key").IsUnique();

            entity.HasIndex(e => e.Phone, "users_phone_key").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("now()")
                .HasColumnName("created_at");
            entity.Property(e => e.Email)
                .HasMaxLength(255)
                .HasColumnName("email");
            entity.Property(e => e.FirstName)
                .HasMaxLength(50)
                .HasColumnName("first_name");
            entity.Property(e => e.LastName)
                .HasMaxLength(50)
                .HasColumnName("last_name");
            entity.Property(e => e.PasswordHash).HasColumnName("password_hash");
            entity.Property(e => e.Phone)
                .HasMaxLength(20)
                .HasColumnName("phone");
            entity.Property(e=>e.Role).HasColumnType("user_role").HasColumnName("role");
        });

        modelBuilder.Entity<UserSubscribtion>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("user_subscribtions_pkey");

            entity.ToTable("user_subscribtions");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.SubscribtionId).HasColumnName("subscribtion_id");
            entity.Property(e => e.UserId).HasColumnName("user_id");

            entity.HasOne(d => d.Subscribtion).WithMany(p => p.UserSubscribtions)
                .HasForeignKey(d => d.SubscribtionId)
                .HasConstraintName("user_subscribtions_subscribtion_id_fkey");

            entity.HasOne(d => d.User).WithMany(p => p.UserSubscribtions)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("user_subscribtions_user_id_fkey");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
