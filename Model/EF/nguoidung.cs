namespace Model.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("nguoidung")]
    public partial class nguoidung
    {
        public int id { get; set; }

        [StringLength(255)]
        public string ten { get; set; }

        [StringLength(255)]
        public string email { get; set; }

        [StringLength(255)]
        public string matkhau { get; set; }

        [StringLength(255)]
        public string ngaysinh { get; set; }

        [StringLength(255)]
        public string sdt { get; set; }

        [Column(TypeName = "date")]
        public DateTime? ngaytao { get; set; }

        [Column(TypeName = "date")]
        public DateTime? ngaycapnhat { get; set; }

        [StringLength(100)]
        public string gioitinh { get; set; }

        public int? vaitro { get; set; }
    }
}
