namespace TTNhom.Models
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class OnlShopDbContext : DbContext
    {
        public OnlShopDbContext()
            : base("name=OnlShopDbContext")
        {
        }

        public virtual DbSet<admin> admins { get; set; }
        public virtual DbSet<ChiTietHoaDon> ChiTietHoaDons { get; set; }
        public virtual DbSet<DanhMucSanPham> DanhMucSanPhams { get; set; }
        public virtual DbSet<HoaDon> HoaDons { get; set; }
        public virtual DbSet<LoaiThu> LoaiThus { get; set; }
        public virtual DbSet<SanPham> SanPhams { get; set; }
        public virtual DbSet<user> users { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<admin>()
                .Property(e => e.TaiKhoan)
                .IsUnicode(false);

            modelBuilder.Entity<admin>()
                .Property(e => e.MatKhau)
                .IsUnicode(false);

            modelBuilder.Entity<admin>()
                .Property(e => e.SDT)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<DanhMucSanPham>()
                .HasMany(e => e.SanPhams)
                .WithOptional(e => e.DanhMucSanPham)
                .HasForeignKey(e => e.LoaiSanPham);

            modelBuilder.Entity<LoaiThu>()
                .HasMany(e => e.DanhMucSanPhams)
                .WithOptional(e => e.LoaiThu1)
                .HasForeignKey(e => e.LoaiThu);

            modelBuilder.Entity<user>()
                .Property(e => e.TaiKhoan)
                .IsUnicode(false);

            modelBuilder.Entity<user>()
                .Property(e => e.MatKhau)
                .IsUnicode(false);

            modelBuilder.Entity<user>()
                .Property(e => e.email)
                .IsUnicode(false);

            modelBuilder.Entity<user>()
                .Property(e => e.SDT)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<user>()
                .HasMany(e => e.HoaDons)
                .WithOptional(e => e.user)
                .HasForeignKey(e => e.KhachHang);
        }
    }
}
