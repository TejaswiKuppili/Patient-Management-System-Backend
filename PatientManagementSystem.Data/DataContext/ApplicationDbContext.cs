using Microsoft.EntityFrameworkCore;
using PatientManagementSystem.Data.Entities;


namespace PatientManagementSystem.Data.DataContext
{
    /// <summary>
    /// Represents the database context for the Patient Management System,
    /// managing all entities and their relationships.
    /// </summary>
    public class ApplicationDbContext : DbContext
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ApplicationDbContext"/> class.
        /// </summary>
        /// <param name="options">The options for this context.</param>
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        /// <summary>
        /// Gets or sets the DbSet for ApplicationUser entities.
        /// </summary>
        public DbSet<ApplicationUser> ApplicationUsers { get; set; }

        /// <summary>
        /// Gets or sets the DbSet for Role entities.
        /// </summary>
        public DbSet<Role> Roles { get; set; }

        /// <summary>
        /// Gets or sets the DbSet for Employee entities.
        /// </summary>
        public DbSet<Employee> Employees { get; set; }

        /// <summary>
        /// Gets or sets the DbSet for Patient entities.
        /// </summary>
        public DbSet<Patient> Patients { get; set; }

        /// <summary>
        /// Gets or sets the DbSet for Appointment entities.
        /// </summary>
        public DbSet<Appointment> Appointments { get; set; }

        /// <summary>
        /// Gets or sets the DbSet for MedicalRecord entities.
        /// </summary>
        public DbSet<MedicalRecord> MedicalRecords { get; set; }

        /// <summary>
        /// Gets or sets the DbSet for Vital entities.
        /// </summary>
        public DbSet<Vital> Vitals { get; set; }

        /// <summary>
        /// Gets or sets the DbSet for RefreshToken entities.
        /// </summary>
        public DbSet<RefreshToken> RefreshTokens { get; set; }

        /// <summary>
        /// Gets or sets the DbSet for Specialty entities of doctors.
        /// </summary>
        public DbSet<Specialty> Specialties { get; set; }
        
        /// <summary>
        /// Gets or sets the DbSet for Profile entities.
        /// </summary>
        public DbSet<Profile> Profiles { get; set; }

        /// <summary>
        /// Provides a filtered query for users who are doctors.
        /// </summary>
        public IQueryable<ApplicationUser> Doctors =>
            ApplicationUsers.Where(u => u.RoleId == 2);


        /// <summary>
        /// Configures the model and defines the schema for the database.
        /// </summary>
        /// <param name="modelBuilder">The builder used to construct the model for this context.</param>
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
                //entity.Property(u => u.RoleId).IsRequired();
                entity.Property(u => u.CreatedAt).HasDefaultValueSql("GETDATE()");
                entity.HasOne(u => u.Role)
                      .WithMany(r => r.ApplicationUsers)
                      .HasForeignKey(u => u.RoleId)
                      .OnDelete(DeleteBehavior.NoAction)
                      .IsRequired();
                entity.HasOne(u => u.Specialty)
                            .WithMany()
                            .HasForeignKey(u => u.SpecialtyId)
                            .OnDelete(DeleteBehavior.SetNull); 

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
                entity.Property(p => p.ReasonForVisit).IsRequired();


                entity.HasOne(p => p.CreatedByEmployee)
                    .WithMany(u => u.PatientsCreated)
                  .HasForeignKey(p => p.CreatedBy)
                  .OnDelete(DeleteBehavior.NoAction)
                  .IsRequired();
            });

            // Appointments
            modelBuilder.Entity<Appointment>(entity =>
            {
                
                    entity.HasKey(e => e.Id);

                    entity.HasOne(e => e.Patient)
                          .WithMany(p => p.Appointments)
                          .HasForeignKey(e => e.PatientId)
                          .OnDelete(DeleteBehavior.NoAction);

                    entity.HasOne(e => e.CreatedByUser)
                          .WithMany()
                          .HasForeignKey(e => e.CreatedBy)
                          .OnDelete(DeleteBehavior.NoAction);


                    entity.HasOne(e => e.Doctor)
                          .WithMany()
                          .HasForeignKey(e => e.DoctorId)
                          .OnDelete(DeleteBehavior.NoAction);

                    entity.Property(u => u.CreatedAt).HasDefaultValueSql("GETDATE()");

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
                entity.HasOne(p => p.CreatedByEmployee)
            .WithMany()
          .HasForeignKey(p => p.CreatedBy)
          .OnDelete(DeleteBehavior.NoAction)
          .IsRequired();
            });


            // RefreshTokens
            modelBuilder.Entity<RefreshToken>(entity =>
            {
                entity.HasKey(rt => rt.Id);
                entity.Property(rt => rt.Token).IsRequired().HasMaxLength(500);
                entity.Property(rt => rt.ExpiresAt).IsRequired();
                entity.Property(rt => rt.CreatedAt).HasDefaultValueSql("GETDATE()");
                entity.Property(rt => rt.CreatedByIp).HasMaxLength(50);
                entity.HasOne(rt => rt.User)
                      .WithMany(u => u.RefreshTokens)
                      .HasForeignKey(rt => rt.UserId)
                      .OnDelete(DeleteBehavior.Cascade)
                      .IsRequired();
            });
            modelBuilder.Entity<Profile>(entity =>
            {
                entity.HasKey(p => p.Id);

                entity.HasOne(p => p.ApplicationUser)
                      .WithOne(u => u.Profile)
                      .HasForeignKey<Profile>(p => p.ApplicationUserId)
                      .OnDelete(DeleteBehavior.Cascade); 

                entity.Property(p => p.Gender).HasMaxLength(10);
                entity.Property(p => p.PhoneNumber).HasMaxLength(20);
                entity.Property(p => p.Bio).HasMaxLength(500);
                entity.Property(p => p.Address).HasMaxLength(300);

            });

            // Enum conversions
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