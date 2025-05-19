using Microsoft.EntityFrameworkCore;

using PatientManagementSystem.Data.Entities;


namespace PatientManagementSystem.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<ApplicationUser> ApplicationUsers { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Patient> Patients { get; set; }
        public DbSet<Appointment> Appointments { get; set; }
        public DbSet<MedicalRecord> MedicalRecords { get; set; }
        public DbSet<Vital> Vitals { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Roles
            modelBuilder.Entity<Role>(entity =>
            {
                entity.HasKey(r => r.Id);
                entity.HasIndex(r => r.RoleId).IsUnique();
                entity.Property(r => r.RoleId).IsRequired().HasMaxLength(50);
                entity.HasIndex(r => r.Name).IsUnique();
                entity.Property(r => r.Name).IsRequired().HasMaxLength(100);
                entity.Property(r => r.Description).HasMaxLength(255);
            });

            // ApplicationUsers
            modelBuilder.Entity<ApplicationUser>(entity =>
            {
                entity.HasKey(u => u.Id);
                entity.HasIndex(u => u.Username).IsUnique();
                entity.Property(u => u.Username).IsRequired().HasMaxLength(100);
                entity.HasIndex(u => u.Email).IsUnique();
                entity.Property(u => u.Email).IsRequired().HasMaxLength(256);
                entity.Property(u => u.PasswordHash).IsRequired();
                entity.Property(u => u.CreatedAt).HasDefaultValueSql("GETDATE()");
                entity.HasOne(u => u.Role)
                      .WithMany(r => r.ApplicationUsers)
                      .HasForeignKey(u => u.RoleId)
                      .OnDelete(DeleteBehavior.NoAction)
                      .IsRequired();
            });

            // Employees
            modelBuilder.Entity<Employee>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.FullName).IsRequired().HasMaxLength(100);
                entity.HasIndex(e => e.Email).IsUnique();
                entity.Property(e => e.Email).IsRequired().HasMaxLength(256);
                entity.Property(e => e.Gender).IsRequired().HasMaxLength(10);
                entity.Property(e => e.ContactNumber).IsRequired().HasMaxLength(20);
                entity.Property(e => e.Address).IsRequired().HasMaxLength(300);
                entity.Property(e => e.IsActive).IsRequired();
                entity.Property(e => e.CreatedAt).HasDefaultValueSql("GETDATE()");
            });

            // Patients
            modelBuilder.Entity<Patient>(entity =>
            {
                entity.HasKey(p => p.Id);
                entity.Property(p => p.FirstName).IsRequired().HasMaxLength(100);
                entity.Property(p => p.LastName).IsRequired().HasMaxLength(100);
                entity.Property(p => p.DateOfBirth).IsRequired();
                entity.Property(p => p.ContactNumber).IsRequired().HasMaxLength(20);
                entity.Property(p => p.Address).HasMaxLength(300);
                entity.Property(p => p.CreatedAt).HasDefaultValueSql("GETDATE()");
            });

            // Appointments
            modelBuilder.Entity<Appointment>(entity =>
            {
                entity.HasKey(a => a.Id);
                entity.Property(a => a.AppointmentDateTime).IsRequired();
                entity.Property(a => a.Reason).HasMaxLength(300);
                entity.Property(a => a.CreatedAt).HasDefaultValueSql("GETDATE()");
                entity.HasOne(a => a.Patient)
                      .WithMany(p => p.Appointments)
                      .HasForeignKey(a => a.PatientId)
                      .OnDelete(DeleteBehavior.NoAction)
                      .IsRequired();
            });

            // MedicalRecords
            modelBuilder.Entity<MedicalRecord>(entity =>
            {
                entity.HasKey(mr => mr.Id);
                entity.Property(mr => mr.FilePath).HasMaxLength(300);
                entity.Property(mr => mr.UploadedAt).HasDefaultValueSql("GETDATE()");
                entity.HasOne(mr => mr.Patient)
                      .WithMany(p => p.MedicalRecords)
                      .HasForeignKey(mr => mr.PatientId)
                      .OnDelete(DeleteBehavior.NoAction)
                      .IsRequired();
            });

            // Vitals
            modelBuilder.Entity<Vital>(entity =>
            {
                entity.HasKey(v => v.Id);
                entity.Property(v => v.RecordedAt).HasDefaultValueSql("GETDATE()");
                entity.Property(v => v.BloodPressure).HasMaxLength(20);
                entity.Property(v => v.Temperature).HasColumnType("decimal(4,1)");
                entity.HasOne(v => v.Patient)
                      .WithMany(p => p.Vitals)
                      .HasForeignKey(v => v.PatientId)
                      .OnDelete(DeleteBehavior.NoAction)
                      .IsRequired();
            });

            //// Enum conversions
            modelBuilder.Entity<Patient>()
                .Property(p => p.Gender)
                //.HasConversion<string>()
                .IsRequired();

            modelBuilder.Entity<Appointment>()
                .Property(a => a.Status)
                .HasDefaultValue("Scheduled");
             //   .HasConversion<string>()
             //   .HasDefaultValue(AppointmentStatus.Scheduled);

            modelBuilder.Entity<MedicalRecord>()
                .Property(mr => mr.RecordType);
           //     .HasConversion<string>();
        }

    }
}
