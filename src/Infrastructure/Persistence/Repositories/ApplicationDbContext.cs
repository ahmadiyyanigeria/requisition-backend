using System.Reflection;
using Domain.Entities.Aggregates.CashAdvanceAggregate;
using Domain.Entities.Aggregates.GrantAggregate;
using Domain.Entities.Aggregates.PurchaseOrderAggregate;
using Domain.Entities.Aggregates.RequisitionAggregate;
using Domain.Entities.Aggregates.SubmitterAggregate;
using Domain.Entities.Common;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence.Repositories;
public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    { }

    // DbSet for PurchaseOrder aggregate
    public DbSet<PurchaseOrder> PurchaseOrders { get; set; }
    public DbSet<PurchaseOrderItem> PurchaseOrderItems { get; set; }
    public DbSet<Vendor> Vendors { get; set; }

    // DbSet for CashAdvance aggregate
    /* public DbSet<CashAdvance> CashAdvances { get; set; }
     public DbSet<RetirementEntry> RetirementEntries { get; set; }
     public DbSet<RefundEntry> RefundEntries { get; set; }
     public DbSet<ReimbursementEntry> ReimbursementEntries { get; set; }*/

    // DbSet for Grant aggregate
    public DbSet<Grant> Grants { get; set; }

    // DbSet for Requisition 
    public DbSet<Requisition> Requisitions { get; set; }
    public DbSet<RequisitionItem> RequisitionItems { get; set; }
    public DbSet<Attachment> Attachments { get; set; }
    public DbSet<ApprovalFlow> ApprovalFlows { get; set; }
    public DbSet<ApprovalStep> ApprovalSteps { get; set; }

    public DbSet<BankAccount> BankAccounts { get; set; }
    public DbSet<Role> Roles { get; set; }
    public DbSet<Submitter> Submitters { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.HasCollation("case_insensitive", locale: "en-u-ks-primary", provider: "icu", deterministic: false);
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }

    protected override void ConfigureConventions(ModelConfigurationBuilder configurationBuilder)
    {
        base.ConfigureConventions(configurationBuilder);
        configurationBuilder.Properties<string>().UseCollation("case_insensitive");
    }
}