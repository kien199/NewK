using Model.EF;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.SetGetModel
{
    public class GetCommentModel
    {
        public int id { get; set; } //Id bài viết
        [StringLength(255)]
        public string tieude { get; set; } // Tiêu đề bài viết

        public List<binhluan> binhluans { get; set; }
    }
    //public class GetSingleComment
    //{
    //    public int id { get; set; }

    //    [StringLength(255)]
    //    public string noidung { get; set; }

    //    [StringLength(255)]
    //    public string tennguoidang { get; set; }
    //}
}
