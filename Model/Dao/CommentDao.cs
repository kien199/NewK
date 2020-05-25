using Model.EF;
using Model.SetGetModel;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Dao
{
    public class CommentDao
    {
        Model2 db = null;
        public CommentDao()
        {
            db = new Model2();
        }
        public List<GetCommentModel> SearchComment(string search)
        {
            List<GetCommentModel> comments = new List<GetCommentModel>();
            try
            {
                comments = db.Database.SqlQuery<GetCommentModel>("select baiviet.id, baiviet.tieude from baiviet where baiviet.tieude LIKE N'%" + search + "%'").ToList();

                foreach (var item in comments)
                {
                    item.binhluans = db.Database.SqlQuery<binhluan>("select * from binhluan where binhluan.baiviet_id=" + item.id).ToList();
                }
                return comments;
            }
            catch (Exception ex)
            {
                return comments;
            }
        }

        public binhluan AddComment(string noidung, string tennguoidang, int baivietId)
        {
            try
            {
                binhluan newCmt = new binhluan();
                newCmt.noidung = noidung;
                newCmt.tennguoidang = tennguoidang;
                newCmt.baiviet_id = baivietId;
                newCmt.ngaytao = DateTime.Now;
                newCmt.ngaycapnhat = DateTime.Now;
                db.binhluans.Add(newCmt);
                db.SaveChanges();

                return newCmt;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public List<binhluan> GetAllComment(int postId)
        {
            List<binhluan> comments = new List<binhluan>();
            try
            {
                comments = db.binhluans.Where(x => x.baiviet_id == postId).ToList();
                return comments;
            }
            catch  ( Exception ex){
                return comments;
            }
        }
        public IEnumerable<binhluan> GetPagedListComment(int page, int pageSize)
        {
            try
            {
                var comments = db.binhluans.ToList().ToPagedList(page, pageSize);
                return comments;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public bool DeleteComment(int commentId)
        {
            binhluan comment = new binhluan();
            try
            {
                comment = db.binhluans.Where(x => x.id == commentId).SingleOrDefault();
                db.binhluans.Remove(comment);
                db.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }
        public int CountComment()
        {
            try
            {
                return db.binhluans.Count();
            }
            catch (Exception ex)
            {
                return -1;
            }
        }
    }

}
