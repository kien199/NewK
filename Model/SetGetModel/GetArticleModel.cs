using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.SetGetModel
{
    public class GetArticleModel
    {
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
        [StringLength(255)]
        public string theloai_ten { get; set; }

        public bool? hide { get; set; }
    }
}
