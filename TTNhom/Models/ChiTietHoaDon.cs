namespace TTNhom.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("ChiTietHoaDon")]
    public partial class ChiTietHoaDon
    {
        [Key]
        public int IDChiTietDonHang { get; set; }

        public int? IDHoaDon { get; set; }

        public int? IDSanPham { get; set; }

        public int? SoLuong { get; set; }

        public int? DonGia { get; set; }

        public virtual HoaDon HoaDon { get; set; }

        public virtual SanPham SanPham { get; set; }
    }
}
