// using System;
// using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using PSMS_API.Models;

namespace PSMS_API.Data;

public partial class PsmsDbContext : DbContext
{
    public PsmsDbContext()
    {
    }

    public PsmsDbContext(DbContextOptions<PsmsDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<AuditLog> AuditLogs { get; set; }

    public virtual DbSet<Brand> Brands { get; set; }

    public virtual DbSet<Category> Categories { get; set; }

    public virtual DbSet<Contract> Contracts { get; set; }

    public virtual DbSet<Customer> Customers { get; set; }

    public virtual DbSet<InstallmentPeriod> InstallmentPeriods { get; set; }

    public virtual DbSet<InstallmentPlan> InstallmentPlans { get; set; }

    public virtual DbSet<InventoryMovement> InventoryMovements { get; set; }

    public virtual DbSet<Notification> Notifications { get; set; }

    public virtual DbSet<Payment> Payments { get; set; }

    public virtual DbSet<Permission> Permissions { get; set; }

    public virtual DbSet<PnlCache> PnlCaches { get; set; }

    public virtual DbSet<Product> Products { get; set; }

    public virtual DbSet<Purchase> Purchases { get; set; }

    public virtual DbSet<PurchaseDetail> PurchaseDetails { get; set; }

    public virtual DbSet<PurchaseReturn> PurchaseReturns { get; set; }

    public virtual DbSet<Role> Roles { get; set; }

    public virtual DbSet<RolePermission> RolePermissions { get; set; }

    public virtual DbSet<Sale> Sales { get; set; }

    public virtual DbSet<SaleDetail> SaleDetails { get; set; }

    public virtual DbSet<SaleReturn> SaleReturns { get; set; }

    public virtual DbSet<Series> Series { get; set; }

    public virtual DbSet<Supplier> Suppliers { get; set; }

    public virtual DbSet<SupplierPayment> SupplierPayments { get; set; }

    public virtual DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder){}
// #warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
//         => optionsBuilder.UseNpgsql("Host=localhost;Database=psms_db;Username=postgres;Password=Sv34");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<AuditLog>(entity =>
        {
            entity.HasKey(e => e.LogId).HasName("audit_logs_pkey");

            entity.ToTable("audit_logs");

            entity.Property(e => e.LogId)
                .UseIdentityAlwaysColumn()
                .HasColumnName("log_id");
            entity.Property(e => e.ActionType)
                .HasMaxLength(30)
                .HasColumnName("action_type");
            entity.Property(e => e.Description).HasColumnName("description");
            entity.Property(e => e.EntityId).HasColumnName("entity_id");
            entity.Property(e => e.EntityName)
                .HasMaxLength(50)
                .HasColumnName("entity_name");
            entity.Property(e => e.IpAddress)
                .HasMaxLength(45)
                .HasColumnName("ip_address");
            entity.Property(e => e.PrevHash)
                .HasMaxLength(64)
                .HasColumnName("prev_hash");
            entity.Property(e => e.RecordHash)
                .HasMaxLength(64)
                .HasColumnName("record_hash");
            entity.Property(e => e.Timestamp)
                .HasDefaultValueSql("now()")
                .HasColumnName("timestamp");
            entity.Property(e => e.UserId).HasColumnName("user_id");

            entity.HasOne(d => d.User).WithMany(p => p.AuditLogs)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("audit_logs_user_id_fkey");
        });

        modelBuilder.Entity<Brand>(entity =>
        {
            entity.HasKey(e => e.BrandId).HasName("brands_pkey");

            entity.ToTable("brands");

            entity.Property(e => e.BrandId)
                .UseIdentityAlwaysColumn()
                .HasColumnName("brand_id");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("now()")
                .HasColumnName("created_at");
            entity.Property(e => e.IsActive)
                .HasDefaultValue(true)
                .HasColumnName("is_active");
            entity.Property(e => e.Name)
                .HasMaxLength(100)
                .HasColumnName("name");
            entity.Property(e => e.UpdatedAt)
                .HasDefaultValueSql("now()")
                .HasColumnName("updated_at");
        });

        modelBuilder.Entity<Category>(entity =>
        {
            entity.HasKey(e => e.CategoryId).HasName("categories_pkey");

            entity.ToTable("categories");

            entity.Property(e => e.CategoryId)
                .UseIdentityAlwaysColumn()
                .HasColumnName("category_id");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("now()")
                .HasColumnName("created_at");
            entity.Property(e => e.Description).HasColumnName("description");
            entity.Property(e => e.IsActive)
                .HasDefaultValue(true)
                .HasColumnName("is_active");
            entity.Property(e => e.Name)
                .HasMaxLength(100)
                .HasColumnName("name");
            entity.Property(e => e.UpdatedAt)
                .HasDefaultValueSql("now()")
                .HasColumnName("updated_at");
        });

        modelBuilder.Entity<Contract>(entity =>
        {
            entity.HasKey(e => e.ContractId).HasName("contract_pkey");

            entity.ToTable("contract");

            entity.HasIndex(e => e.ContractNumber, "contract_contract_number_key").IsUnique();

            entity.Property(e => e.ContractId)
                .UseIdentityAlwaysColumn()
                .HasColumnName("contract_id");
            entity.Property(e => e.ContractNumber)
                .HasMaxLength(50)
                .HasColumnName("contract_number");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("now()")
                .HasColumnName("created_at");
            entity.Property(e => e.CustmrContact)
                .HasMaxLength(50)
                .HasColumnName("custmr_contact");
            entity.Property(e => e.CustmrId).HasColumnName("custmr_id");
            entity.Property(e => e.CustmrIdCard)
                .HasMaxLength(50)
                .HasColumnName("custmr_id_card");
            entity.Property(e => e.CustmrName)
                .HasMaxLength(100)
                .HasColumnName("custmr_name");
            entity.Property(e => e.Notes).HasColumnName("notes");
            entity.Property(e => e.PlanId).HasColumnName("plan_id");
            entity.Property(e => e.SignedDate).HasColumnName("signed_date");
            entity.Property(e => e.Status)
                .HasMaxLength(20)
                .HasDefaultValueSql("'Active'::character varying")
                .HasColumnName("status");
            entity.Property(e => e.UpdatedAt)
                .HasDefaultValueSql("now()")
                .HasColumnName("updated_at");

            entity.HasOne(d => d.Custmr).WithMany(p => p.Contracts)
                .HasForeignKey(d => d.CustmrId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("contract_custmr_id_fkey");

            entity.HasOne(d => d.Plan).WithMany(p => p.Contracts)
                .HasForeignKey(d => d.PlanId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("contract_plan_id_fkey");
        });

        modelBuilder.Entity<Customer>(entity =>
        {
            entity.HasKey(e => e.CustmrId).HasName("customers_pkey");

            entity.ToTable("customers");

            entity.Property(e => e.CustmrId)
                .UseIdentityAlwaysColumn()
                .HasColumnName("custmr_id");
            entity.Property(e => e.Address).HasColumnName("address");
            entity.Property(e => e.Contact)
                .HasMaxLength(50)
                .HasColumnName("contact");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("now()")
                .HasColumnName("created_at");
            entity.Property(e => e.CreditScore).HasColumnName("credit_score");
            entity.Property(e => e.CreditStatus)
                .HasMaxLength(20)
                .HasDefaultValueSql("'Good'::character varying")
                .HasColumnName("credit_status");
            entity.Property(e => e.CustmrName)
                .HasMaxLength(100)
                .HasColumnName("custmr_name");
            entity.Property(e => e.IsActive)
                .HasDefaultValue(true)
                .HasColumnName("is_active");
            entity.Property(e => e.UpdatedAt)
                .HasDefaultValueSql("now()")
                .HasColumnName("updated_at");
        });

        modelBuilder.Entity<InstallmentPeriod>(entity =>
        {
            entity.HasKey(e => e.PeriodId).HasName("installment_periods_pkey");

            entity.ToTable("installment_periods");

            entity.Property(e => e.PeriodId)
                .UseIdentityAlwaysColumn()
                .HasColumnName("period_id");
            entity.Property(e => e.Amount)
                .HasPrecision(12, 2)
                .HasColumnName("amount");
            entity.Property(e => e.DueDate).HasColumnName("due_date");
            entity.Property(e => e.PaidDate).HasColumnName("paid_date");
            entity.Property(e => e.PeriodNo).HasColumnName("period_no");
            entity.Property(e => e.PlanId).HasColumnName("plan_id");
            entity.Property(e => e.Status)
                .HasMaxLength(20)
                .HasDefaultValueSql("'Pending'::character varying")
                .HasColumnName("status");

            entity.HasOne(d => d.Plan).WithMany(p => p.InstallmentPeriods)
                .HasForeignKey(d => d.PlanId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("installment_periods_plan_id_fkey");
        });

        modelBuilder.Entity<InstallmentPlan>(entity =>
        {
            entity.HasKey(e => e.PlanId).HasName("installment_plans_pkey");

            entity.ToTable("installment_plans");

            entity.Property(e => e.PlanId)
                .UseIdentityAlwaysColumn()
                .HasColumnName("plan_id");
            entity.Property(e => e.AmountPerPeriod)
                .HasPrecision(12, 2)
                .HasColumnName("amount_per_period");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("now()")
                .HasColumnName("created_at");
            entity.Property(e => e.Periods).HasColumnName("periods");
            entity.Property(e => e.SaleId).HasColumnName("sale_id");

            entity.HasOne(d => d.Sale).WithMany(p => p.InstallmentPlans)
                .HasForeignKey(d => d.SaleId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("installment_plans_sale_id_fkey");
        });

        modelBuilder.Entity<InventoryMovement>(entity =>
        {
            entity.HasKey(e => e.MovementId).HasName("inventory_movements_pkey");

            entity.ToTable("inventory_movements");

            entity.Property(e => e.MovementId)
                .UseIdentityAlwaysColumn()
                .HasColumnName("movement_id");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("now()")
                .HasColumnName("created_at");
            entity.Property(e => e.ProdId).HasColumnName("prod_id");
            entity.Property(e => e.Qty).HasColumnName("qty");
            entity.Property(e => e.RefId).HasColumnName("ref_id");
            entity.Property(e => e.RefType)
                .HasMaxLength(30)
                .HasColumnName("ref_type");
            entity.Property(e => e.Type)
                .HasMaxLength(10)
                .HasColumnName("type");

            entity.HasOne(d => d.Prod).WithMany(p => p.InventoryMovements)
                .HasForeignKey(d => d.ProdId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("inventory_movements_prod_id_fkey");
        });

        modelBuilder.Entity<Notification>(entity =>
        {
            entity.HasKey(e => e.NotifyId).HasName("notifications_pkey");

            entity.ToTable("notifications");

            entity.Property(e => e.NotifyId)
                .UseIdentityAlwaysColumn()
                .HasColumnName("notify_id");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("now()")
                .HasColumnName("created_at");
            entity.Property(e => e.IsRead).HasColumnName("is_read");
            entity.Property(e => e.Message).HasColumnName("message");
            entity.Property(e => e.RefId).HasColumnName("ref_id");
            entity.Property(e => e.RefType)
                .HasMaxLength(30)
                .HasColumnName("ref_type");
            entity.Property(e => e.Type)
                .HasMaxLength(30)
                .HasColumnName("type");
            entity.Property(e => e.UserId).HasColumnName("user_id");

            entity.HasOne(d => d.User).WithMany(p => p.Notifications)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("notifications_user_id_fkey");
        });

        modelBuilder.Entity<Payment>(entity =>
        {
            entity.HasKey(e => e.PaymentId).HasName("payments_pkey");

            entity.ToTable("payments");

            entity.Property(e => e.PaymentId)
                .UseIdentityAlwaysColumn()
                .HasColumnName("payment_id");
            entity.Property(e => e.Amount)
                .HasPrecision(12, 2)
                .HasColumnName("amount");
            entity.Property(e => e.Method)
                .HasMaxLength(20)
                .HasColumnName("method");
            entity.Property(e => e.PaymentDate)
                .HasDefaultValueSql("CURRENT_DATE")
                .HasColumnName("payment_date");
            entity.Property(e => e.PeriodId).HasColumnName("period_id");
            entity.Property(e => e.SaleId).HasColumnName("sale_id");

            entity.HasOne(d => d.Period).WithMany(p => p.Payments)
                .HasForeignKey(d => d.PeriodId)
                .HasConstraintName("payments_period_id_fkey");

            entity.HasOne(d => d.Sale).WithMany(p => p.Payments)
                .HasForeignKey(d => d.SaleId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("payments_sale_id_fkey");
        });

        modelBuilder.Entity<Permission>(entity =>
        {
            entity.HasKey(e => e.PermissionId).HasName("permission_pkey");

            entity.ToTable("permission");

            entity.HasIndex(e => e.Code, "permission_code_key").IsUnique();

            entity.Property(e => e.PermissionId)
                .UseIdentityAlwaysColumn()
                .HasColumnName("permission_id");
            entity.Property(e => e.Code)
                .HasMaxLength(50)
                .HasColumnName("code");
            entity.Property(e => e.Description).HasColumnName("description");
        });

        modelBuilder.Entity<PnlCache>(entity =>
        {
            entity.HasKey(e => e.CacheId).HasName("pnl_cache_pkey");

            entity.ToTable("pnl_cache");

            entity.Property(e => e.CacheId)
                .UseIdentityAlwaysColumn()
                .HasColumnName("cache_id");
            entity.Property(e => e.GrossProfit)
                .HasPrecision(12, 2)
                .HasComputedColumnSql("(total_revenue - total_cost)", true)
                .HasColumnName("gross_profit");
            entity.Property(e => e.TotalCost)
                .HasPrecision(12, 2)
                .HasColumnName("total_cost");
            entity.Property(e => e.TotalRevenue)
                .HasPrecision(12, 2)
                .HasColumnName("total_revenue");
            entity.Property(e => e.UpdatedAt)
                .HasDefaultValueSql("now()")
                .HasColumnName("updated_at");
        });

        modelBuilder.Entity<Product>(entity =>
        {
            entity.HasKey(e => e.ProdId).HasName("products_pkey");

            entity.ToTable("products");

            entity.Property(e => e.ProdId)
                .UseIdentityAlwaysColumn()
                .HasColumnName("prod_id");
            entity.Property(e => e.BrandId).HasColumnName("brand_id");
            entity.Property(e => e.CategoryId).HasColumnName("category_id");
            entity.Property(e => e.Condition)
                .HasMaxLength(20)
                .HasDefaultValueSql("'New'::character varying")
                .HasColumnName("condition");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("now()")
                .HasColumnName("created_at");
            entity.Property(e => e.IsActive)
                .HasDefaultValue(true)
                .HasColumnName("is_active");
            entity.Property(e => e.MinStockLevel).HasColumnName("min_stock_level");
            entity.Property(e => e.Price)
                .HasPrecision(12, 2)
                .HasColumnName("price");
            entity.Property(e => e.ProdName)
                .HasMaxLength(150)
                .HasColumnName("prod_name");
            entity.Property(e => e.Qty).HasColumnName("qty");
            entity.Property(e => e.SeriesId).HasColumnName("series_id");
            entity.Property(e => e.UpdatedAt)
                .HasDefaultValueSql("now()")
                .HasColumnName("updated_at");

            entity.HasOne(d => d.Brand).WithMany(p => p.Products)
                .HasForeignKey(d => d.BrandId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("products_brand_id_fkey");

            entity.HasOne(d => d.Category).WithMany(p => p.Products)
                .HasForeignKey(d => d.CategoryId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("products_category_id_fkey");

            entity.HasOne(d => d.Series).WithMany(p => p.Products)
                .HasForeignKey(d => d.SeriesId)
                .HasConstraintName("products_series_id_fkey");
        });

        modelBuilder.Entity<Purchase>(entity =>
        {
            entity.HasKey(e => e.PurchaseId).HasName("purchases_pkey");

            entity.ToTable("purchases");

            entity.Property(e => e.PurchaseId)
                .UseIdentityAlwaysColumn()
                .HasColumnName("purchase_id");
            entity.Property(e => e.Balance)
                .HasPrecision(12, 2)
                .HasComputedColumnSql("(total_cost - paid_amount)", true)
                .HasColumnName("balance");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("now()")
                .HasColumnName("created_at");
            entity.Property(e => e.PaidAmount)
                .HasPrecision(12, 2)
                .HasColumnName("paid_amount");
            entity.Property(e => e.PurchaseDate)
                .HasDefaultValueSql("CURRENT_DATE")
                .HasColumnName("purchase_date");
            entity.Property(e => e.SupplrId).HasColumnName("supplr_id");
            entity.Property(e => e.TotalCost)
                .HasPrecision(12, 2)
                .HasColumnName("total_cost");
            entity.Property(e => e.UpdatedAt)
                .HasDefaultValueSql("now()")
                .HasColumnName("updated_at");
            entity.Property(e => e.UserId).HasColumnName("user_id");

            entity.HasOne(d => d.Supplr).WithMany(p => p.Purchases)
                .HasForeignKey(d => d.SupplrId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("purchases_supplr_id_fkey");

            entity.HasOne(d => d.User).WithMany(p => p.Purchases)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("purchases_user_id_fkey");
        });

        modelBuilder.Entity<PurchaseDetail>(entity =>
        {
            entity.HasKey(e => e.PurchaseDetailId).HasName("purchase_details_pkey");

            entity.ToTable("purchase_details");

            entity.Property(e => e.PurchaseDetailId)
                .UseIdentityAlwaysColumn()
                .HasColumnName("purchase_detail_id");
            entity.Property(e => e.Deduction)
                .HasPrecision(12, 2)
                .HasColumnName("deduction");
            entity.Property(e => e.ProdId).HasColumnName("prod_id");
            entity.Property(e => e.PurchaseId).HasColumnName("purchase_id");
            entity.Property(e => e.Qty).HasColumnName("qty");
            entity.Property(e => e.UnitCost)
                .HasPrecision(12, 2)
                .HasColumnName("unit_cost");

            entity.HasOne(d => d.Prod).WithMany(p => p.PurchaseDetails)
                .HasForeignKey(d => d.ProdId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("purchase_details_prod_id_fkey");

            entity.HasOne(d => d.Purchase).WithMany(p => p.PurchaseDetails)
                .HasForeignKey(d => d.PurchaseId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("purchase_details_purchase_id_fkey");
        });

        modelBuilder.Entity<PurchaseReturn>(entity =>
        {
            entity.HasKey(e => e.PurchaseReturnId).HasName("purchase_returns_pkey");

            entity.ToTable("purchase_returns");

            entity.Property(e => e.PurchaseReturnId)
                .UseIdentityAlwaysColumn()
                .HasColumnName("purchase_return_id");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("now()")
                .HasColumnName("created_at");
            entity.Property(e => e.DeductionAmount)
                .HasPrecision(12, 2)
                .HasColumnName("deduction_amount");
            entity.Property(e => e.PurchaseDetailId).HasColumnName("purchase_detail_id");
            entity.Property(e => e.PurchaseId).HasColumnName("purchase_id");
            entity.Property(e => e.Qty).HasColumnName("qty");
            entity.Property(e => e.Reason).HasColumnName("reason");
            entity.Property(e => e.ReturnDate)
                .HasDefaultValueSql("CURRENT_DATE")
                .HasColumnName("return_date");
            entity.Property(e => e.UserId).HasColumnName("user_id");

            entity.HasOne(d => d.PurchaseDetail).WithMany(p => p.PurchaseReturns)
                .HasForeignKey(d => d.PurchaseDetailId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("purchase_returns_purchase_detail_id_fkey");

            entity.HasOne(d => d.Purchase).WithMany(p => p.PurchaseReturns)
                .HasForeignKey(d => d.PurchaseId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("purchase_returns_purchase_id_fkey");

            entity.HasOne(d => d.User).WithMany(p => p.PurchaseReturns)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("purchase_returns_user_id_fkey");
        });

        modelBuilder.Entity<Role>(entity =>
        {
            entity.HasKey(e => e.RoleId).HasName("roles_pkey");

            entity.ToTable("roles");

            entity.HasIndex(e => e.RoleName, "roles_role_name_key").IsUnique();

            entity.Property(e => e.RoleId)
                .UseIdentityAlwaysColumn()
                .HasColumnName("role_id");
            entity.Property(e => e.RoleName)
                .HasMaxLength(50)
                .HasColumnName("role_name");
        });

        modelBuilder.Entity<RolePermission>(entity =>
        {
            entity.HasKey(e => e.RnpId).HasName("role_permission_pkey");

            entity.ToTable("role_permission");

            entity.HasIndex(e => new { e.RoleId, e.PermissionId }, "role_permission_role_id_permission_id_key").IsUnique();

            entity.Property(e => e.RnpId)
                .UseIdentityAlwaysColumn()
                .HasColumnName("rnp_id");
            entity.Property(e => e.PermissionId).HasColumnName("permission_id");
            entity.Property(e => e.RoleId).HasColumnName("role_id");

            entity.HasOne(d => d.Permission).WithMany(p => p.RolePermissions)
                .HasForeignKey(d => d.PermissionId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("role_permission_permission_id_fkey");

            entity.HasOne(d => d.Role).WithMany(p => p.RolePermissions)
                .HasForeignKey(d => d.RoleId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("role_permission_role_id_fkey");
        });

        modelBuilder.Entity<Sale>(entity =>
        {
            entity.HasKey(e => e.SaleId).HasName("sales_pkey");

            entity.ToTable("sales");

            entity.Property(e => e.SaleId)
                .UseIdentityAlwaysColumn()
                .HasColumnName("sale_id");
            entity.Property(e => e.Balance)
                .HasPrecision(12, 2)
                .HasComputedColumnSql("(total_amount - paid_amount)", true)
                .HasColumnName("balance");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("now()")
                .HasColumnName("created_at");
            entity.Property(e => e.CustmrId).HasColumnName("custmr_id");
            entity.Property(e => e.PaidAmount)
                .HasPrecision(12, 2)
                .HasColumnName("paid_amount");
            entity.Property(e => e.SaleDate)
                .HasDefaultValueSql("CURRENT_DATE")
                .HasColumnName("sale_date");
            entity.Property(e => e.TotalAmount)
                .HasPrecision(12, 2)
                .HasColumnName("total_amount");
            entity.Property(e => e.UpdatedAt)
                .HasDefaultValueSql("now()")
                .HasColumnName("updated_at");
            entity.Property(e => e.UserId).HasColumnName("user_id");

            entity.HasOne(d => d.Custmr).WithMany(p => p.Sales)
                .HasForeignKey(d => d.CustmrId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("sales_custmr_id_fkey");

            entity.HasOne(d => d.User).WithMany(p => p.Sales)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("sales_user_id_fkey");
        });

        modelBuilder.Entity<SaleDetail>(entity =>
        {
            entity.HasKey(e => e.SaleDetailId).HasName("sale_details_pkey");

            entity.ToTable("sale_details");

            entity.Property(e => e.SaleDetailId)
                .UseIdentityAlwaysColumn()
                .HasColumnName("sale_detail_id");
            entity.Property(e => e.Discount)
                .HasPrecision(12, 2)
                .HasColumnName("discount");
            entity.Property(e => e.ProdId).HasColumnName("prod_id");
            entity.Property(e => e.Qty).HasColumnName("qty");
            entity.Property(e => e.SaleId).HasColumnName("sale_id");
            entity.Property(e => e.UnitPrice)
                .HasPrecision(12, 2)
                .HasColumnName("unit_price");

            entity.HasOne(d => d.Prod).WithMany(p => p.SaleDetails)
                .HasForeignKey(d => d.ProdId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("sale_details_prod_id_fkey");

            entity.HasOne(d => d.Sale).WithMany(p => p.SaleDetails)
                .HasForeignKey(d => d.SaleId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("sale_details_sale_id_fkey");
        });

        modelBuilder.Entity<SaleReturn>(entity =>
        {
            entity.HasKey(e => e.SaleReturnId).HasName("sale_returns_pkey");

            entity.ToTable("sale_returns");

            entity.Property(e => e.SaleReturnId)
                .UseIdentityAlwaysColumn()
                .HasColumnName("sale_return_id");
            entity.Property(e => e.Qty).HasColumnName("qty");
            entity.Property(e => e.Reason).HasColumnName("reason");
            entity.Property(e => e.RefundAmount)
                .HasPrecision(12, 2)
                .HasColumnName("refund_amount");
            entity.Property(e => e.ReturnDate)
                .HasDefaultValueSql("CURRENT_DATE")
                .HasColumnName("return_date");
            entity.Property(e => e.SaleDetailId).HasColumnName("sale_detail_id");
            entity.Property(e => e.SaleId).HasColumnName("sale_id");
            entity.Property(e => e.UserId).HasColumnName("user_id");

            entity.HasOne(d => d.SaleDetail).WithMany(p => p.SaleReturns)
                .HasForeignKey(d => d.SaleDetailId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("sale_returns_sale_detail_id_fkey");

            entity.HasOne(d => d.Sale).WithMany(p => p.SaleReturns)
                .HasForeignKey(d => d.SaleId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("sale_returns_sale_id_fkey");

            entity.HasOne(d => d.User).WithMany(p => p.SaleReturns)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("sale_returns_user_id_fkey");
        });

        modelBuilder.Entity<Series>(entity =>
        {
            entity.HasKey(e => e.SeriesId).HasName("series_pkey");

            entity.ToTable("series");

            entity.Property(e => e.SeriesId)
                .UseIdentityAlwaysColumn()
                .HasColumnName("series_id");
            entity.Property(e => e.BrandId).HasColumnName("brand_id");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("now()")
                .HasColumnName("created_at");
            entity.Property(e => e.IsActive)
                .HasDefaultValue(true)
                .HasColumnName("is_active");
            entity.Property(e => e.Name)
                .HasMaxLength(100)
                .HasColumnName("name");
            entity.Property(e => e.UpdatedAt)
                .HasDefaultValueSql("now()")
                .HasColumnName("updated_at");

            entity.HasOne(d => d.Brand).WithMany(p => p.Series)
                .HasForeignKey(d => d.BrandId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("series_brand_id_fkey");
        });

        modelBuilder.Entity<Supplier>(entity =>
        {
            entity.HasKey(e => e.SupplrId).HasName("suppliers_pkey");

            entity.ToTable("suppliers");

            entity.Property(e => e.SupplrId)
                .UseIdentityAlwaysColumn()
                .HasColumnName("supplr_id");
            entity.Property(e => e.Address).HasColumnName("address");
            entity.Property(e => e.Categories)
                .HasMaxLength(150)
                .HasColumnName("categories");
            entity.Property(e => e.Contact)
                .HasMaxLength(50)
                .HasColumnName("contact");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("now()")
                .HasColumnName("created_at");
            entity.Property(e => e.IsActive)
                .HasDefaultValue(true)
                .HasColumnName("is_active");
            entity.Property(e => e.Name)
                .HasMaxLength(100)
                .HasColumnName("name");
            entity.Property(e => e.UpdatedAt)
                .HasDefaultValueSql("now()")
                .HasColumnName("updated_at");
        });

        modelBuilder.Entity<SupplierPayment>(entity =>
        {
            entity.HasKey(e => e.SupplrPaymentId).HasName("supplier_payments_pkey");

            entity.ToTable("supplier_payments");

            entity.Property(e => e.SupplrPaymentId)
                .UseIdentityAlwaysColumn()
                .HasColumnName("supplr_payment_id");
            entity.Property(e => e.Amount)
                .HasPrecision(12, 2)
                .HasColumnName("amount");
            entity.Property(e => e.PaymentDate)
                .HasDefaultValueSql("CURRENT_DATE")
                .HasColumnName("payment_date");
            entity.Property(e => e.PurchaseId).HasColumnName("purchase_id");

            entity.HasOne(d => d.Purchase).WithMany(p => p.SupplierPayments)
                .HasForeignKey(d => d.PurchaseId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("supplier_payments_purchase_id_fkey");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.UserId).HasName("users_pkey");

            entity.ToTable("users");

            entity.HasIndex(e => e.Username, "users_username_key").IsUnique();

            entity.Property(e => e.UserId)
                .UseIdentityAlwaysColumn()
                .HasColumnName("user_id");
            entity.Property(e => e.Contact)
                .HasMaxLength(50)
                .HasColumnName("contact");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("now()")
                .HasColumnName("created_at");
            entity.Property(e => e.FullName)
                .HasMaxLength(100)
                .HasColumnName("full_name");
            entity.Property(e => e.IsActive)
                .HasDefaultValue(true)
                .HasColumnName("is_active");
            entity.Property(e => e.PasswordHash)
                .HasMaxLength(255)
                .HasColumnName("password_hash");
            entity.Property(e => e.RoleId).HasColumnName("role_id");
            entity.Property(e => e.UpdatedAt)
                .HasDefaultValueSql("now()")
                .HasColumnName("updated_at");
            entity.Property(e => e.Username)
                .HasMaxLength(50)
                .HasColumnName("username");

            entity.HasOne(d => d.Role).WithMany(p => p.Users)
                .HasForeignKey(d => d.RoleId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("users_role_id_fkey");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
