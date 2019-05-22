namespace TTNhom.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("LoaiThu")]
    public partial class LoaiThu
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public LoaiThu()
        {
            DanhMucSanPhams = new HashSet<DanhMucSanPham>();
        }

        [Key]
        public int IDLoaiThu { get; set; }

        [StringLength(50)]
        public string TenLoaiThu { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<DanhMucSanPham> DanhMucSanPhams { get; set; }
    }
}
