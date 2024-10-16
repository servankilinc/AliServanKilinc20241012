using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Model.Entities;

namespace DataAccess.Contexts;

public class AppBaseDbContext : IdentityDbContext<User, IdentityRole<Guid>, Guid>
{
    public AppBaseDbContext(DbContextOptions<AppBaseDbContext> options) : base(options)
    {
    }

    public override DbSet<User> Users { get; set; }
    public DbSet<Account> Accounts { get; set; }
    public DbSet<AccountType> AccountTypes { get; set; }
    public DbSet<Transfer> Transfers { get; set; }
    public DbSet<TransferType> TransferTypes { get; set; }




    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);


        modelBuilder.Entity<Account>(a =>
        {
            a.ToTable("Accounts");
            a.HasKey(a => a.Id);
            a.HasAlternateKey(a => a.AccountNo);
            a.Property(a => a.Id).HasColumnName("Id");
            a.Property(a => a.AccountNo).HasColumnName("AccountNo");
            a.Property(a => a.AccountTypeId).HasColumnName("AccountTypeId");
            a.Property(a => a.UserId).HasColumnName("UserId");
            a.Property(a => a.CreatedDate).HasColumnName("CreatedDate");
            a.Property(a => a.VerificationDate).HasColumnName("VerificationDate");
            a.Property(a => a.IsVerified).HasColumnName("IsVerified");
            a.Property(a => a.Balance).HasColumnName("Balance");

            a.HasOne(a => a.AccountType)
                .WithMany(at => at.Accounts)
                .HasForeignKey(a => a.AccountTypeId)
                .OnDelete(DeleteBehavior.Restrict);

            a.HasOne(a => a.User)
               .WithMany(u => u.Accounts)
               .HasForeignKey(a=> a.UserId)
               .OnDelete(DeleteBehavior.Restrict);

            a.HasMany(a => a.ShippingProcesses)
               .WithOne(t => t.SenderAccount)
               .HasForeignKey(t=> t.SenderAccountId)
               .OnDelete(DeleteBehavior.Restrict);

            a.HasMany(a => a.PurchaseProcesses)
               .WithOne(t => t.ReceivingAccount)
               .HasForeignKey(t => t.RecipientAccountId)
               .OnDelete(DeleteBehavior.Restrict);
        });
        modelBuilder.Entity<Account>().HasQueryFilter(f => !f.IsDeleted);


        modelBuilder.Entity<AccountType>(at =>
        {
            at.ToTable("AccountTypes");
            at.HasKey(at => at.Id);
            at.Property(at => at.Id).HasColumnName("Id");
            at.Property(at => at.Name).HasColumnName("Name");

            at.HasMany(at => at.Accounts)
               .WithOne(a => a.AccountType)
               .HasForeignKey(a => a.AccountTypeId)
               .OnDelete(DeleteBehavior.Restrict);
        });
        modelBuilder.Entity<AccountType>().HasQueryFilter(f => !f.IsDeleted);

        modelBuilder.Entity<Transfer>(t =>
        {
            t.ToTable("Transfers");
            t.HasKey(t => t.Id);
            t.Property(t => t.Id).HasColumnName("Id");
            t.Property(t => t.TransferTypeId).HasColumnName("TransferTypeId");
            t.Property(t => t.SenderAccountId).HasColumnName("SenderAccountId");
            t.Property(t => t.RecipientAccountId).HasColumnName("RecipientAccountId");
            t.Property(t => t.SenderUserId).HasColumnName("SenderUserId");
            t.Property(t => t.RecipientUserId).HasColumnName("RecipientUserId");
            t.Property(t => t.Date).HasColumnName("Date");
            t.Property(t => t.Status).HasColumnName("Status");
            t.Property(t => t.RejectionDetailDescription).HasColumnName("RejectionDetailDescription");
            t.Property(t => t.Amount).HasColumnName("Amount");
            t.Property(t => t.Description).HasColumnName("Description");

            t.HasOne(t => t.TransferType)
                .WithMany(tt=> tt.Transfers)
                .HasForeignKey(t => t.TransferTypeId)
                .OnDelete(DeleteBehavior.Restrict);

            t.HasOne(t => t.SenderAccount)
                .WithMany(a => a.ShippingProcesses)
                .HasForeignKey(t => t.SenderAccountId)
                .OnDelete(DeleteBehavior.Restrict);

            t.HasOne(t => t.ReceivingAccount)
                .WithMany(a => a.PurchaseProcesses)
                .HasForeignKey(t => t.RecipientAccountId)
                .OnDelete(DeleteBehavior.Restrict);

            t.HasOne(t => t.SenderUser)
                .WithMany(u => u.ShippingProcesses)
                .HasForeignKey(t => t.SenderUserId)
                .OnDelete(DeleteBehavior.Restrict);

            t.HasOne(t => t.ReceivingUser)
                .WithMany(u => u.PurchaseProcesses)
                .HasForeignKey(t => t.RecipientUserId)
                .OnDelete(DeleteBehavior.Restrict);
        });
        modelBuilder.Entity<Transfer>().HasQueryFilter(f => !f.IsDeleted);



        modelBuilder.Entity<TransferType>(tt =>
        {
            tt.ToTable("TransferTypes");
            tt.HasKey(tt => tt.Id);
            tt.Property(tt => tt.Id).HasColumnName("Id");
            tt.Property(tt => tt.Name).HasColumnName("Name");

            tt.HasMany(tt => tt.Transfers)
               .WithOne(t => t.TransferType)
               .HasForeignKey(t => t.TransferTypeId)
               .OnDelete(DeleteBehavior.Restrict);
        });
        modelBuilder.Entity<TransferType>().HasQueryFilter(f => !f.IsDeleted);



        modelBuilder.Entity<User>(u =>
        {
            u.ToTable("Users");
            u.Property(u => u.FirstName).HasColumnName("FirstName");
            u.Property(u => u.LastName).HasColumnName("LastName");
            u.Property(u => u.RegistrationDate).HasColumnName("RegistrationDate");

            u.HasMany(u => u.Accounts)
                .WithOne(a => a.User)
                .HasForeignKey(a => a.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            u.HasMany(u => u.ShippingProcesses)
               .WithOne(t => t.SenderUser)
               .HasForeignKey(t => t.SenderUserId)
               .OnDelete(DeleteBehavior.Restrict);

            u.HasMany(u => u.PurchaseProcesses)
               .WithOne(t => t.ReceivingUser)
               .HasForeignKey(t => t.RecipientUserId)
               .OnDelete(DeleteBehavior.Restrict);
        });
        modelBuilder.Entity<User>().HasQueryFilter(f => !f.IsDeleted);


        modelBuilder.Entity<IdentityRole<Guid>>(entity => { entity.ToTable("Roles"); });

        modelBuilder.Entity<IdentityUserClaim<Guid>>(entity => { entity.ToTable("UserClaims"); });

        modelBuilder.Entity<IdentityUserLogin<Guid>>(entity => { entity.ToTable("UserLogins"); });

        modelBuilder.Entity<IdentityRoleClaim<Guid>>(entity => { entity.ToTable("RoleClaims"); });

        modelBuilder.Entity<IdentityUserRole<Guid>>(entity => { entity.ToTable("UserRoles"); });

        modelBuilder.Entity<IdentityUserToken<Guid>>(entity => { entity.ToTable("UserTokens"); });
    }
}