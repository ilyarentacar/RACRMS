using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace RACRMS.Entity
{
    public partial class ILYA_RACRMSContext : DbContext
    {
        public ILYA_RACRMSContext()
        {
        }

        public ILYA_RACRMSContext(DbContextOptions<ILYA_RACRMSContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Car> Car { get; set; }
        public virtual DbSet<CarBrand> CarBrand { get; set; }
        public virtual DbSet<CarChassisType> CarChassisType { get; set; }
        public virtual DbSet<CarClass> CarClass { get; set; }
        public virtual DbSet<CarFuelType> CarFuelType { get; set; }
        public virtual DbSet<CarGearType> CarGearType { get; set; }
        public virtual DbSet<CarModel> CarModel { get; set; }
        public virtual DbSet<CarPreference> CarPreference { get; set; }
        public virtual DbSet<CarRentalPrice> CarRentalPrice { get; set; }
        public virtual DbSet<CarRentalRequirement> CarRentalRequirement { get; set; }
        public virtual DbSet<CarType> CarType { get; set; }
        public virtual DbSet<Customer> Customer { get; set; }
        public virtual DbSet<Preference> Preference { get; set; }
        public virtual DbSet<Requirement> Requirement { get; set; }
        public virtual DbSet<Reservation> Reservation { get; set; }
        public virtual DbSet<User> User { get; set; }
        public virtual DbSet<UserRole> UserRole { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#if DEBUG
                optionsBuilder.UseSqlServer("Server=.;Database=ILYA_RACRMS;User Id=sa;Password=07081983Konya");
#else
                optionsBuilder.UseSqlServer("Server=localhost;Database=ILYA_RACRMS;User Id=ilya_sa;Password=PCZ6hqP!GAg%4cBP");
#endif
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Car>(entity =>
            {
                entity.Property(e => e.CreateDate).HasColumnType("datetime");

                entity.Property(e => e.PlateNumber)
                    .IsRequired()
                    .HasMaxLength(10);

                entity.Property(e => e.UpdateDate).HasColumnType("datetime");

                entity.HasOne(d => d.CarBrand)
                    .WithMany(p => p.Car)
                    .HasForeignKey(d => d.CarBrandId)
                    .OnDelete(DeleteBehavior.ClientSetNull);

                entity.HasOne(d => d.CarChassisType)
                    .WithMany(p => p.Car)
                    .HasForeignKey(d => d.CarChassisTypeId)
                    .OnDelete(DeleteBehavior.ClientSetNull);

                entity.HasOne(d => d.CarClass)
                    .WithMany(p => p.Car)
                    .HasForeignKey(d => d.CarClassId)
                    .OnDelete(DeleteBehavior.ClientSetNull);

                entity.HasOne(d => d.CarFuelType)
                    .WithMany(p => p.Car)
                    .HasForeignKey(d => d.CarFuelTypeId)
                    .OnDelete(DeleteBehavior.ClientSetNull);

                entity.HasOne(d => d.CarGearType)
                    .WithMany(p => p.Car)
                    .HasForeignKey(d => d.CarGearTypeId)
                    .OnDelete(DeleteBehavior.ClientSetNull);

                entity.HasOne(d => d.CarModel)
                    .WithMany(p => p.Car)
                    .HasForeignKey(d => d.CarModelId)
                    .OnDelete(DeleteBehavior.ClientSetNull);

                entity.HasOne(d => d.CarType)
                    .WithMany(p => p.Car)
                    .HasForeignKey(d => d.CarTypeId)
                    .OnDelete(DeleteBehavior.ClientSetNull);
            });

            modelBuilder.Entity<CarBrand>(entity =>
            {
                entity.Property(e => e.CreateDate).HasColumnType("datetime");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(15);

                entity.Property(e => e.UpdateDate).HasColumnType("datetime");
            });

            modelBuilder.Entity<CarChassisType>(entity =>
            {
                entity.Property(e => e.CreateDate).HasColumnType("datetime");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(15);

                entity.Property(e => e.UpdateDate).HasColumnType("datetime");
            });

            modelBuilder.Entity<CarClass>(entity =>
            {
                entity.Property(e => e.CreateDate).HasColumnType("datetime");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(15);

                entity.Property(e => e.UpdateDate).HasColumnType("datetime");
            });

            modelBuilder.Entity<CarFuelType>(entity =>
            {
                entity.Property(e => e.CreateDate).HasColumnType("datetime");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(15);

                entity.Property(e => e.UpdateDate).HasColumnType("datetime");
            });

            modelBuilder.Entity<CarGearType>(entity =>
            {
                entity.Property(e => e.CreateDate).HasColumnType("datetime");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(15);

                entity.Property(e => e.UpdateDate).HasColumnType("datetime");
            });

            modelBuilder.Entity<CarModel>(entity =>
            {
                entity.Property(e => e.CreateDate).HasColumnType("datetime");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(15);

                entity.Property(e => e.UpdateDate).HasColumnType("datetime");

                entity.HasOne(d => d.CarBrand)
                    .WithMany(p => p.CarModel)
                    .HasForeignKey(d => d.CarBrandId)
                    .OnDelete(DeleteBehavior.ClientSetNull);

                entity.HasOne(d => d.CarChassisType)
                    .WithMany(p => p.CarModel)
                    .HasForeignKey(d => d.CarChassisTypeId)
                    .OnDelete(DeleteBehavior.ClientSetNull);

                entity.HasOne(d => d.CarClass)
                    .WithMany(p => p.CarModel)
                    .HasForeignKey(d => d.CarClassId)
                    .OnDelete(DeleteBehavior.ClientSetNull);

                entity.HasOne(d => d.CarFuelType)
                    .WithMany(p => p.CarModel)
                    .HasForeignKey(d => d.CarFuelTypeId)
                    .OnDelete(DeleteBehavior.ClientSetNull);

                entity.HasOne(d => d.CarGearType)
                    .WithMany(p => p.CarModel)
                    .HasForeignKey(d => d.CarGearTypeId)
                    .OnDelete(DeleteBehavior.ClientSetNull);

                entity.HasOne(d => d.CarType)
                    .WithMany(p => p.CarModel)
                    .HasForeignKey(d => d.CarTypeId)
                    .OnDelete(DeleteBehavior.ClientSetNull);
            });

            modelBuilder.Entity<CarPreference>(entity =>
            {
                entity.HasKey(e => new { e.Id, e.CarId, e.PreferenceId });

                entity.Property(e => e.Id).ValueGeneratedOnAdd();

                entity.HasOne(d => d.Car)
                    .WithMany(p => p.CarPreference)
                    .HasForeignKey(d => d.CarId)
                    .OnDelete(DeleteBehavior.ClientSetNull);

                entity.HasOne(d => d.Preference)
                    .WithMany(p => p.CarPreference)
                    .HasForeignKey(d => d.PreferenceId)
                    .OnDelete(DeleteBehavior.ClientSetNull);
            });

            modelBuilder.Entity<CarRentalPrice>(entity =>
            {
                entity.Property(e => e.CreateDate).HasColumnType("datetime");

                entity.Property(e => e.EndDate).HasColumnType("date");

                entity.Property(e => e.RentPrice).HasColumnType("money");

                entity.Property(e => e.StartDate).HasColumnType("date");

                entity.Property(e => e.UpdateDate).HasColumnType("datetime");

                entity.HasOne(d => d.Car)
                    .WithMany(p => p.CarRentalPrice)
                    .HasForeignKey(d => d.CarId)
                    .OnDelete(DeleteBehavior.ClientSetNull);
            });

            modelBuilder.Entity<CarRentalRequirement>(entity =>
            {
                entity.HasKey(e => new { e.Id, e.CarId, e.RequirementId });

                entity.Property(e => e.Id).ValueGeneratedOnAdd();

                entity.HasOne(d => d.Car)
                    .WithMany(p => p.CarRentalRequirement)
                    .HasForeignKey(d => d.CarId)
                    .OnDelete(DeleteBehavior.ClientSetNull);

                entity.HasOne(d => d.Requirement)
                    .WithMany(p => p.CarRentalRequirement)
                    .HasForeignKey(d => d.RequirementId)
                    .OnDelete(DeleteBehavior.ClientSetNull);
            });

            modelBuilder.Entity<CarType>(entity =>
            {
                entity.Property(e => e.CreateDate).HasColumnType("datetime");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(15);

                entity.Property(e => e.UpdateDate).HasColumnType("datetime");
            });

            modelBuilder.Entity<Customer>(entity =>
            {
                entity.Property(e => e.CellNumber).HasMaxLength(20);

                entity.Property(e => e.CreateDate).HasColumnType("datetime");

                entity.Property(e => e.EmailAddress)
                    .IsRequired()
                    .HasMaxLength(35);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(25);

                entity.Property(e => e.Surname)
                    .IsRequired()
                    .HasMaxLength(40);

                entity.Property(e => e.UpdateDate).HasColumnType("datetime");
            });

            modelBuilder.Entity<Preference>(entity =>
            {
                entity.Property(e => e.CreateDate).HasColumnType("datetime");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(15);

                entity.Property(e => e.UpdateDate).HasColumnType("datetime");
            });

            modelBuilder.Entity<Requirement>(entity =>
            {
                entity.Property(e => e.CreateDate).HasColumnType("datetime");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(15);

                entity.Property(e => e.UpdateDate).HasColumnType("datetime");
            });

            modelBuilder.Entity<Reservation>(entity =>
            {
                entity.Property(e => e.ApprovalDate).HasColumnType("datetime");

                entity.Property(e => e.CreateDate).HasColumnType("datetime");

                entity.Property(e => e.EmailSentDate).HasColumnType("datetime");

                entity.Property(e => e.EmailTemplate).HasMaxLength(1500);

                entity.Property(e => e.EndDate).HasColumnType("datetime");

                entity.Property(e => e.RejectDate).HasColumnType("datetime");

                entity.Property(e => e.StartDate).HasColumnType("datetime");

                entity.Property(e => e.TotalPrice).HasColumnType("money");

                entity.Property(e => e.UpdateDate).HasColumnType("datetime");

                entity.HasOne(d => d.ApprovingUser)
                    .WithMany(p => p.ReservationApprovingUser)
                    .HasForeignKey(d => d.ApprovingUserId);

                entity.HasOne(d => d.Car)
                    .WithMany(p => p.Reservation)
                    .HasForeignKey(d => d.CarId)
                    .OnDelete(DeleteBehavior.ClientSetNull);

                entity.HasOne(d => d.Customer)
                    .WithMany(p => p.Reservation)
                    .HasForeignKey(d => d.CustomerId)
                    .OnDelete(DeleteBehavior.ClientSetNull);

                entity.HasOne(d => d.RejectingUser)
                    .WithMany(p => p.ReservationRejectingUser)
                    .HasForeignKey(d => d.RejectingUserId);
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.Property(e => e.CreateDate).HasColumnType("datetime");

                entity.Property(e => e.EmailAddress)
                    .IsRequired()
                    .HasMaxLength(35);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(25);

                entity.Property(e => e.Password)
                    .IsRequired()
                    .HasMaxLength(25);

                entity.Property(e => e.Surname)
                    .IsRequired()
                    .HasMaxLength(40);

                entity.Property(e => e.UpdateDate).HasColumnType("datetime");

                entity.Property(e => e.Username)
                    .IsRequired()
                    .HasMaxLength(25);

                entity.HasOne(d => d.UserRole)
                    .WithMany(p => p.User)
                    .HasForeignKey(d => d.UserRoleId)
                    .OnDelete(DeleteBehavior.ClientSetNull);
            });

            modelBuilder.Entity<UserRole>(entity =>
            {
                entity.Property(e => e.CreateDate).HasColumnType("datetime");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(25);

                entity.Property(e => e.UpdateDate).HasColumnType("datetime");
            });
        }
    }
}
