using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace FoodMenuAPITest.Models
{
    public partial class AndroidFoodMenuDBContext : DbContext
    {
        public AndroidFoodMenuDBContext()
        {
        }

        public AndroidFoodMenuDBContext(DbContextOptions<AndroidFoodMenuDBContext> options)
            : base(options)
        {
        }

        public virtual DbSet<AdminTable> AdminTable { get; set; }
        public virtual DbSet<AndroidTableNumber> AndroidTableNumber { get; set; }
        public virtual DbSet<CancelledOrderTable> CancelledOrderTable { get; set; }
        public virtual DbSet<CashierIdlist> CashierIdlist { get; set; }
        public virtual DbSet<CashierInfoList> CashierInfoList { get; set; }
        public virtual DbSet<CashierLoginCredentials> CashierLoginCredentials { get; set; }
        public virtual DbSet<DailySalesTable> DailySalesTable { get; set; }
        public virtual DbSet<DefaultOrderNumberTable> DefaultOrderNumberTable { get; set; }
        public virtual DbSet<DefaultTaxTable> DefaultTaxTable { get; set; }
        public virtual DbSet<DfltCashierId> DfltCashierId { get; set; }
        public virtual DbSet<DiscountTable> DiscountTable { get; set; }
        public virtual DbSet<FoodCategory> FoodCategory { get; set; }
        public virtual DbSet<FoodMenu> FoodMenu { get; set; }
        public virtual DbSet<InvoiceTable> InvoiceTable { get; set; }
        public virtual DbSet<LoginTable> LoginTable { get; set; }
        public virtual DbSet<SeniorCitizensPwdsDiscountTable> SeniorCitizensPwdsDiscountTable { get; set; }
        public virtual DbSet<TemporaryInvoiceTable> TemporaryInvoiceTable { get; set; }
        public virtual DbSet<TransactionDefaultId> TransactionDefaultId { get; set; }
        public virtual DbSet<UserList> UserList { get; set; }
        public virtual DbSet<VoidTable> VoidTable { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer("Server=DESKTOP-2N78NV2;Database=Android Food Menu DB;Persist Security Info=True;User ID=Restaurant Admin;Password=foodsotasty1999");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AdminTable>(entity =>
            {
                entity.HasNoKey();

                entity.Property(e => e.AdminId).HasColumnName("AdminID");

                entity.Property(e => e.AdminPhoto).HasColumnType("image");

                entity.Property(e => e.Role).HasMaxLength(7);

                entity.Property(e => e.Status).HasMaxLength(8);
            });

            modelBuilder.Entity<AndroidTableNumber>(entity =>
            {
                entity.HasNoKey();
            });

            modelBuilder.Entity<CancelledOrderTable>(entity =>
            {
                entity.HasNoKey();

                entity.Property(e => e.CancelledOrderDate).HasColumnType("date");

                entity.Property(e => e.CancelledOrderDay).HasMaxLength(50);

                entity.Property(e => e.CancelledOrderDeviceId).HasColumnName("CancelledOrderDeviceID");

                entity.Property(e => e.CancelledOrderId).HasColumnName("CancelledOrderID");

                entity.Property(e => e.CancelledOrderItem).HasMaxLength(100);

                entity.Property(e => e.CancelledOrderMonth).HasMaxLength(50);

                entity.Property(e => e.CancelledOrderOgpayment).HasColumnName("CancelledOrderOGPayment");

                entity.Property(e => e.CancelledOrderYear).HasMaxLength(50);
            });

            modelBuilder.Entity<CashierIdlist>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("CashierIDList");

                entity.Property(e => e.CashierId).HasColumnName("CashierID");
            });

            modelBuilder.Entity<CashierInfoList>(entity =>
            {
                entity.HasKey(e => e.CashierId);

                entity.Property(e => e.CashierId).HasColumnName("CashierID");

                entity.Property(e => e.Userpic).HasColumnType("image");
            });

            modelBuilder.Entity<CashierLoginCredentials>(entity =>
            {
                entity.HasNoKey();

                entity.Property(e => e.EmpId).HasColumnName("EmpID");

                entity.Property(e => e.Role).HasMaxLength(7);

                entity.Property(e => e.Status).HasMaxLength(8);
            });

            modelBuilder.Entity<DailySalesTable>(entity =>
            {
                entity.HasKey(e => e.DstransactionNumber);

                entity.Property(e => e.DstransactionNumber)
                    .HasColumnName("DSTransactionNumber")
                    .ValueGeneratedNever();

                entity.Property(e => e.Dschange).HasColumnName("DSChange");

                entity.Property(e => e.Dsdate)
                    .HasColumnName("DSDate")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.DsdeviceId).HasColumnName("DSDeviceID");

                entity.Property(e => e.Dsitem)
                    .HasColumnName("DSItem")
                    .HasMaxLength(100);

                entity.Property(e => e.Dspayment).HasColumnName("DSPayment");

                entity.Property(e => e.Dsquantity).HasColumnName("DSQuantity");

                entity.Property(e => e.Dstax).HasColumnName("DSTax");

                entity.Property(e => e.Dstime)
                    .HasColumnName("DSTime")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Dstotal).HasColumnName("DSTotal");
            });

            modelBuilder.Entity<DefaultOrderNumberTable>(entity =>
            {
                entity.HasKey(e => e.OrderNumber);

                entity.Property(e => e.OrderNumber).ValueGeneratedNever();
            });

            modelBuilder.Entity<DefaultTaxTable>(entity =>
            {
                entity.HasNoKey();
            });

            modelBuilder.Entity<DfltCashierId>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("DfltCashierID");

                entity.Property(e => e.DefaultId).HasColumnName("DefaultID");
            });

            modelBuilder.Entity<DiscountTable>(entity =>
            {
                entity.HasNoKey();

                entity.Property(e => e.DiscountName).HasMaxLength(50);

                entity.Property(e => e.DiscountPrice).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.DiscountType).HasMaxLength(50);
            });

            modelBuilder.Entity<FoodCategory>(entity =>
            {
                entity.HasNoKey();

                entity.Property(e => e.Category)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.Image).HasColumnType("image");
            });

            modelBuilder.Entity<FoodMenu>(entity =>
            {
                entity.HasNoKey();

                entity.Property(e => e.FoodAvailability).HasMaxLength(11);

                entity.Property(e => e.FoodCategory).HasMaxLength(100);

                entity.Property(e => e.FoodId)
                    .IsRequired()
                    .HasColumnName("FoodID")
                    .HasMaxLength(50);

                entity.Property(e => e.FoodName).HasMaxLength(100);

                entity.Property(e => e.FoodPic).HasColumnType("image");

                entity.Property(e => e.FoodPrice).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.FoodSrp)
                    .HasColumnName("FoodSRP")
                    .HasColumnType("decimal(18, 2)");

                entity.Property(e => e.FoodVat)
                    .HasColumnName("FoodVAT")
                    .HasColumnType("decimal(18, 2)");
            });

            modelBuilder.Entity<InvoiceTable>(entity =>
            {
                entity.HasNoKey();

                entity.Property(e => e.InvoiceCashier).HasMaxLength(50);

                entity.Property(e => e.InvoiceChange).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.InvoiceDate).HasColumnType("date");

                entity.Property(e => e.InvoiceDay).HasMaxLength(50);

                entity.Property(e => e.InvoiceDeviceId).HasColumnName("InvoiceDeviceID");

                entity.Property(e => e.InvoiceHolidayDiscount).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.InvoiceItem).HasMaxLength(100);

                entity.Property(e => e.InvoiceItemTax).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.InvoiceMonth).HasMaxLength(50);

                entity.Property(e => e.InvoiceOrderCommand).HasMaxLength(50);

                entity.Property(e => e.InvoicePayment).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.InvoicePrice).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.InvoicePriceAmount).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.InvoiceSeniorCitizenPwddiscount)
                    .HasColumnName("InvoiceSeniorCitizenPWDDiscount")
                    .HasColumnType("decimal(18, 2)");

                entity.Property(e => e.InvoiceSubTotal).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.InvoiceTotal).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.InvoiceTotalTax).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.InvoiceYear).HasMaxLength(50);
            });

            modelBuilder.Entity<LoginTable>(entity =>
            {
                entity.HasNoKey();

                entity.Property(e => e.LoginDate).HasColumnType("date");

                entity.Property(e => e.LoginId)
                    .IsRequired()
                    .HasColumnName("LoginID")
                    .HasMaxLength(50);

                entity.Property(e => e.LoginTime)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.LogoutTime)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<SeniorCitizensPwdsDiscountTable>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("SeniorCitizensPWDsDiscountTable");

                entity.Property(e => e.SpecialDiscountName).HasMaxLength(50);

                entity.Property(e => e.SpecialDiscountPrice).HasColumnType("decimal(18, 2)");
            });

            modelBuilder.Entity<TemporaryInvoiceTable>(entity =>
            {
                entity.HasNoKey();

                entity.Property(e => e.TempDeviceId).HasColumnName("TempDeviceID");

                entity.Property(e => e.TempInvoiceDay).HasMaxLength(50);

                entity.Property(e => e.TempInvoiceItem).HasMaxLength(100);

                entity.Property(e => e.TempInvoiceMonth).HasMaxLength(50);

                entity.Property(e => e.TempInvoiceSubTotal).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.TempInvoiceYear).HasMaxLength(50);

                entity.Property(e => e.TempOrderCommand).HasMaxLength(50);

                entity.Property(e => e.TempOrderNumber).ValueGeneratedOnAdd();

                entity.Property(e => e.TempOrderPrice).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.TempOrderPriceAmount).HasColumnType("decimal(18, 2)");
            });

            modelBuilder.Entity<TransactionDefaultId>(entity =>
            {
                entity.HasKey(e => e.Transactionno);

                entity.ToTable("TransactionDefaultID");

                entity.Property(e => e.Transactionno).ValueGeneratedNever();
            });

            modelBuilder.Entity<UserList>(entity =>
            {
                entity.HasNoKey();

                entity.HasIndex(e => e.UserId)
                    .HasName("IX_UserList")
                    .IsUnique();

                entity.Property(e => e.UserId).HasColumnName("UserID");

                entity.Property(e => e.UserRole).HasMaxLength(7);

                entity.Property(e => e.UserStatus).HasMaxLength(8);
            });

            modelBuilder.Entity<VoidTable>(entity =>
            {
                entity.HasNoKey();

                entity.Property(e => e.VoidDeviceId).HasColumnName("VoidDeviceID");

                entity.Property(e => e.VoidInvoiceDay).HasMaxLength(50);

                entity.Property(e => e.VoidInvoiceItem).HasMaxLength(100);

                entity.Property(e => e.VoidInvoiceItemVat).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.VoidInvoiceMonth).HasMaxLength(50);

                entity.Property(e => e.VoidInvoiceVatTotal).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.VoidInvoiceYear)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.VoidOrderPrice).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.VoidOrderPriceAmount).HasColumnType("decimal(18, 2)");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
