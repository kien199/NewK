using Model.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Dao
{
    public class PostDao
    {
        Model2 db = null;
        public PostDao()
        {
            db = new Model2();
        }

        public int AddPost(string tieude, string slug, string tomtat, string noidung, string thumbnail, int theloat_id)
        {
            try
            {
                baiviet newPost = new baiviet();
                newPost.tieude = tieude;
                newPost.slug = slug;
                newPost.tomtat = tomtat;
                newPost.noidung = noidung;
                newPost.thumbnail = thumbnail;
                newPost.soluotxem = 0;
                newPost.noibat = false;
                newPost.ngaytao = DateTime.Now;
                newPost.ngaycapnhat = DateTime.Now;
                newPost.hide = false;
                newPost.theloai_id = theloat_id;
                db.baiviets.Add(newPost);
                db.SaveChanges();

                return newPost.id;
            }
            catch (Exception ex)
            {
                return -1;
            }
        }
        public int IncViews(string slug, int theloai_id)
        {
            baiviet post = new baiviet();
            try
            {
                post = db.baiviets.Where(x => x.slug == slug && x.theloai_id == theloai_id).SingleOrDefault();
                post.soluotxem += 1;
                db.SaveChanges();
                return post.soluotxem.Value;
            }
            catch
            {
                return -1;
            }
        }
        public List<baiviet> GetAllPost(int theloai_id)
        {
            List<baiviet> posts = new List<baiviet>();
            try
            {
                posts = db.baiviets.Where(x=>x.theloai_id == theloai_id && x.hide == false).ToList();
                return posts;
            }
            catch (Exception ex)
            {
                return posts;
            }
        }
        public baiviet GetSinglePost(string slug, int theloai_id)
        {
            baiviet post = new baiviet();
            try
            {
                post = db.baiviets.Where(x => x.slug == slug && x.theloai_id == theloai_id).FirstOrDefault();
                return post;
            }
            catch(Exception ex)
            {
                return post;
            }
        }
    }
}
