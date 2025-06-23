using DAL.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Reflection.Emit;

public class EasypayContext : DbContext
{
    public DbSet<Employee> Employees => Set<Employee>();
    public DbSet<Payroll> Payrolls => Set<Payroll>();
    public DbSet<LeaveRequest> LeaveRequests => Set<LeaveRequest>();
    public DbSet<User> Users => Set<User>();
    public DbSet<Benefit> Benefits => Set<Benefit>();
    public DbSet<AuditLog> AuditLogs => Set<AuditLog>();
    public DbSet<PayrollConfig> PayrollConfigs => Set<PayrollConfig>();
    public DbSet<Notification> Notifications => Set<Notification>();
    public DbSet<Timesheet> Timesheets => Set<Timesheet>();

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        //To configure a connection string
        if (!optionsBuilder.IsConfigured)
        {
            optionsBuilder.UseSqlServer(DatabaseHelper.GetConnectionString());
        }
        base.OnConfiguring(optionsBuilder);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {

        modelBuilder.Entity<Employee>()
            .HasIndex(e => e.Email)
            .IsUnique();

        modelBuilder.Entity<LeaveRequest>()
            .HasOne(l => l.Employee)
            .WithMany(e => e.LeaveRequests)
            .HasForeignKey(l => l.EmployeeId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<Payroll>()
        .Property(p => p.Salary)
        .HasPrecision(18, 2);

        modelBuilder.Entity<PayrollConfig>(entity =>
        {
            entity.Property(p => p.Allowances).HasPrecision(18, 2);
            entity.Property(p => p.Deductions).HasPrecision(18, 2);
            entity.Property(p => p.TaxRate).HasPrecision(5, 2);
        });
    }
}