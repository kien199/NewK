namespace Model.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("binhluan")]
    public partial class binhluan
    {
        public int id { get; set; }

        [StringLength(255)]
        public string noidung { get; set; }

        [StringLength(255)]
        public string tennguoidang { get; set; }

        [Column(TypeName = "date")]
        public DateTime? ngaytao { get; set; }

        [Column(TypeName = "date")]
        public DateTime? ngaycapnhat { get; set; }

        public int? baiviet_id { get; set; }

        public virtual baiviet baiviet { get; set; }
    }
}
