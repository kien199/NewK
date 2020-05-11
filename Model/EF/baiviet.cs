namespace Model.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("baiviet")]
    public partial class baiviet
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public baiviet()
        {
            binhluans = new HashSet<binhluan>();
        }

        public int id { get; set; }

        [StringLength(255)]
        public string tieude { get; set; }

        [StringLength(255)]
        public string slug { get; set; }

        public string tomtat { get; set; }

        public string noidung { get; set; }

        [StringLength(255)]
        public string thumbnail { get; set; }

        public int? soluotxem { get; set; }

        public bool? noibat { get; set; }

        [Column(TypeName = "date")]
        public DateTime? ngaytao { get; set; }

        [Column(TypeName = "date")]
        public DateTime? ngaycapnhat { get; set; }

        public int? theloai_id { get; set; }

        public bool? hide { get; set; }

        public virtual theloaitin theloaitin { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<binhluan> binhluans { get; set; }
    }
}
