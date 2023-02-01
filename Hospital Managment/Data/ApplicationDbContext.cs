using Hospital_Managment.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Hospital_Managment.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        
        public DbSet<Patient> Patients { get; set; }
        public DbSet<ContactUs> ContactUs{ get; set; }
        public DbSet<Doctor> Doctors { get; set; }
        public DbSet<InsuranceCompany> InsuranceCompanies { get; set; }
        public DbSet<Appointment> Appointments { get; set; }
        public DbSet<Prescription> Prescriptions { get; set; }
        public DbSet<TestResult> TestResults { get; set; }
        public DbSet<Treatment> Treatments { get; set; }
        public DbSet<Hospitalization> Hospitalizations { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet<MedicalEquipment> MedicalEquipment { get; set; }
        public DbSet<Supply> Supplies { get; set; }
        public DbSet<Nurse> Nurses { get; set; }
        public DbSet<Receptionist> Receptionists { get; set; }
        public DbSet<Bill> Bills { get; set; }
        public DbSet<Payment> Payments { get; set; }
        public DbSet<PatientInsuranceCompany> PatientInsuranceCompanies { get; set; }
        public DbSet<DoctorAppointment> DoctorAppointments { get; set; }
        public DbSet<ApplicationUser> ApplicationUsers { get; set; }
        public DbSet<Appointmenti> Appointmentis{ get; set; }
        public DbSet<PharmacyProduct> PharmacyProducts{ get; set; }
        public DbSet<ShoppingCart>  ShoppingCarts{ get; set; }
        public DbSet<OrderHeader>  OrderHeader{ get; set; }
        public DbSet<OrderDetails>  OrderDetail{ get; set; }
        public DbSet<Appointments> appointmentsList { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<PatientInsuranceCompany>()
                .HasKey(pc => new { pc.PatientId, pc.InsuranceCompanyId });
            modelBuilder.Entity<PatientInsuranceCompany>()
                .HasOne(pc => pc.Patient)
                .WithMany(p => p.PatientInsuranceCompanies)
                .HasForeignKey(pc => pc.PatientId);
            modelBuilder.Entity<PatientInsuranceCompany>()
                .HasOne(pc => pc.InsuranceCompany)
                .WithMany(c => c.PatientInsuranceCompanies)
                .HasForeignKey(pc => pc.InsuranceCompanyId);

            modelBuilder.Entity<DoctorAppointment>()
                .HasKey(da => new { da.DoctorId, da.AppointmentId });
            modelBuilder.Entity<DoctorAppointment>()
                .HasOne(da => da.Doctor)
                .WithMany(d => d.DoctorAppointments)
                .HasForeignKey(da => da.DoctorId).OnDelete(DeleteBehavior.ClientSetNull);
            modelBuilder.Entity<DoctorAppointment>()
                .HasOne(da => da.Appointment)
                .WithMany(a => a.DoctorAppointments)
                .HasForeignKey(da => da.AppointmentId);
            modelBuilder.Entity<Doctor>(entity =>
            {
                entity.ToTable("Doctors");
                entity.HasKey(e => e.DoctorId);
                entity.Property(e => e.DoctorId).ValueGeneratedOnAdd();
                entity.Property(e => e.FirstName).IsRequired().HasMaxLength(50);
                entity.Property(e => e.LastName).IsRequired().HasMaxLength(50);
                entity.Property(e => e.Specialty).IsRequired().HasMaxLength(50);
                entity.Property(e => e.PhoneNumber).HasMaxLength(20);
                entity.Property(e => e.Email).HasMaxLength(50);
                entity.HasMany(e => e.Appointments).WithOne(e => e.Doctor).HasForeignKey(e => e.DoctorId);
                entity.HasMany(e => e.Treatments).WithOne(e => e.Doctor).HasForeignKey(e => e.DoctorId);
                entity.HasMany(e => e.Hospitalizations).WithOne(e => e.Doctor).HasForeignKey(e => e.DoctorId);
               
            });
            modelBuilder.Entity<Appointment>(entity =>
            {
                entity.ToTable("Appointments");
                entity.HasKey(e => e.AppointmentId);
                entity.Property(e => e.AppointmentId).ValueGeneratedOnAdd();
                entity.HasOne(e => e.Patient).WithMany(e => e.Appointments).HasForeignKey(e => e.PatientId).OnDelete(DeleteBehavior.ClientSetNull);
                entity.HasOne(e => e.Doctor).WithMany(e => e.Appointments).HasForeignKey(e => e.DoctorId);

            });
            modelBuilder.Entity<Payment>(entity =>
            {
                entity.ToTable("Payments");
                entity.HasKey(e => e.PaymentId);
                entity.Property(e => e.PaymentId).ValueGeneratedOnAdd();
                entity.HasOne(e => e.Patient).WithMany(e => e.Payment).HasForeignKey(e => e.PatientId).OnDelete(DeleteBehavior.ClientSetNull);
                entity.HasOne(e => e.Bill).WithMany(e => e.Payments).HasForeignKey(e => e.PatientId).OnDelete(DeleteBehavior.ClientSetNull);



            });
            modelBuilder.Entity<TestResult>(entity =>
            {
                entity.ToTable("TestResults");
                entity.HasKey(e => e.Id);
               
                entity.HasOne(e => e.Patient).WithMany(e => e.TestResults).HasForeignKey(e => e.PatientId);
            });
            modelBuilder.Entity<Prescription>(entity =>
            {
                entity.ToTable("Prescriptions");
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Id).ValueGeneratedOnAdd();
               ;
                entity.HasOne(e => e.Patient).WithMany(e => e.Prescriptions).HasForeignKey(e => e.PatientId);
                entity.HasOne(e => e.Doctor).WithMany(e => e.Prescription).HasForeignKey(e => e.DoctorId).OnDelete(DeleteBehavior.ClientNoAction);
            });

          modelBuilder.Entity<IdentityUserLogin<string>>(entity =>
            {
                entity.ToTable("UserLogins");
                entity.HasIndex(a=>a.UserId);
                entity.HasKey(a=> new { a.LoginProvider, a.ProviderKey });
            });
            modelBuilder.Entity<IdentityUserRole<string>>(b => { 
            
                b.HasKey(r => new { r.UserId, r.RoleId });
                b.HasIndex(a => a.RoleId);
                b.ToTable("UserRole");
            });
            
               modelBuilder.Entity<IdentityUserToken<string>>(entity =>
               {
                   entity.ToTable("UserToken");
                   entity.HasKey(r => new { r.UserId, r.LoginProvider,r.Name });
               });
        }


     

    }
}