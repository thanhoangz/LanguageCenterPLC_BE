using LanguageCenterPLC.Data.Entities;
using LanguageCenterPLC.Infrastructure.Interfaces;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System;
using System.IO;
using System.Linq;

namespace LanguageCenterPLC.Data.EF
{
    public class AppDbContext : IdentityDbContext<AppUser,AppRole,Guid>
    {
        public AppDbContext(DbContextOptions options) : base(options)
        {

        }
        public DbSet<AppUser> AppUsers { set; get; }
        public DbSet<AppRole> AppRoles { set; get; }

        public DbSet<AttendanceSheet> AttendanceSheets { set; get; }

        public DbSet<AttendanceSheetDetail> AttendanceSheetDetails { set; get; }

        public DbSet<Classroom> Classrooms { set; get; }

        public DbSet<Contact> Contacts { set; get; }

        public DbSet<Course> Courses { set; get; }

        public DbSet<EndingCoursePoint> EndingCoursePoints { set; get; }

        public DbSet<EndingCoursePointDetail> EndingCoursePointDetails { set; get; }

        public DbSet<Feedback> Feedbacks { set; get; }

        public DbSet<Footer> Footers { set; get; }

        public DbSet<GuestType> GuestTypes { set; get; }

        public DbSet<LanguageClass> LanguageClasses { set; get; }

        public DbSet<Learner> Learners { set; get; }

        public DbSet<Lecturer> Lecturers { set; get; }

        public DbSet<PaySlip> PaySlips { set; get; }

        public DbSet<PaySlipType> PaySlipTypes { set; get; }

        public DbSet<PeriodicPoint> PeriodicPoints { set; get; }

        public DbSet<PeriodicPointDetail> PeriodicPointDetails { set; get; }

        public DbSet<Personnel> Personnels { set; get; }

        public DbSet<Receipt> Receipts { set; get; }

        public DbSet<ReceiptDetail> ReceiptDetails { set; get; }

        public DbSet<ReceiptType> ReceiptTypes { set; get; }

        public DbSet<StudyProcess> StudyProcesses { set; get; }

        public DbSet<SystemConfig> SystemConfigs { set; get; }

        public DbSet<TeachingSchedule> TeachingSchedules { set; get; }

        public DbSet<Timesheet> Timesheets { set; get; }

        public DbSet<Permission> Permissions { set; get; }

        public DbSet<Function> Functions { set; get; }


        public DbSet<Announcement> Announcements { set; get; }
        public DbSet<AnnouncementUser> AnnouncementUsers { set; get; }


        public DbSet<LogTuitionRecord> LogTuitionRecords { set; get; }

        public DbSet<ClassSession> ClassSessions { set; get; }

        public DbSet<TimeShift> TimeShifts { set; get; }

        public DbSet<SalaryPay> SalaryPays { set; get; }

        public DbSet<LogSystem> LogSystems { set; get; }

        public DbSet<AccountForLearner> AccountForLearners { set; get; }

        public DbSet<InforLearner> InforLearners { set; get; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }

        public override int SaveChanges()
        {
            var modified = ChangeTracker.Entries().Where(e => e.State == EntityState.Modified || e.State == EntityState.Added);
            foreach (EntityEntry item in modified)
            {
                var changedOrAddedItem = item.Entity as IDateTracking;
                if (changedOrAddedItem != null)
                {
                    if (item.State == EntityState.Added)
                    {
                        changedOrAddedItem.DateCreated = DateTime.Now;
                    }
                    changedOrAddedItem.DateModified = DateTime.Now;
                }
            }
            return base.SaveChanges();
        }
    }

    public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<AppDbContext>
    {

        public AppDbContext CreateDbContext(string[] args)
        {
            IConfiguration configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json").Build();
            var builder = new DbContextOptionsBuilder<AppDbContext>();
            var connectionString = configuration.GetConnectionString("DefaultConnection");
            builder.UseSqlServer(connectionString);
            return new AppDbContext(builder.Options);
        }
    }
}
