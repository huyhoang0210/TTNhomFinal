namespace TTNhom.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("SanPham")]
    public partial class SanPham
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public SanPham()
        {
            ChiTietHoaDons = new HashSet<ChiTietHoaDon>();
        }

        [Key]
        public int IDSanPham { get; set; }

        [StringLength(250)]
        public string TenSanPham { get; set; }

        public int? SoLuong { get; set; }

        [StringLength(250)]
        public string HinhAnh { get; set; }

        [Column(TypeName = "ntext")]
        public string MoTa { get; set; }

        public int? Gia { get; set; }

        public int? LoaiSanPham { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ChiTietHoaDon> ChiTietHoaDons { get; set; }

        public virtual DanhMucSanPham DanhMucSanPham { get; set; }
    }
}
