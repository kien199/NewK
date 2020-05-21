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
                if (thumbnail == "http://localhost:44333/Asset_Admin/images/")
                {
                    newPost.thumbnail = "http://localhost:44333/Asset_Admin/images/default-new-image.png";
                }
                else
                {
                    newPost.thumbnail = thumbnail;
                }
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
        public int EditPost(string tieude, string slug, string tomtat, string noidung, string thumbnail, int theloat_id, int id)
        {
            try
            {
                baiviet editPost = db.baiviets.Where(x => x.id == id).SingleOrDefault();
                editPost.tieude = tieude;
                editPost.slug = slug;
                editPost.tomtat = tomtat;
                editPost.noidung = noidung;
                if (thumbnail != "http://localhost:44333/Asset_Admin/images/")
                {
                    editPost.thumbnail = thumbnail;
                }
                editPost.ngaycapnhat = DateTime.Now;
                editPost.theloai_id = theloat_id;

                db.SaveChanges();

                return editPost.id;
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
                posts = db.baiviets.Where(x => x.theloai_id == theloai_id && x.hide == false).ToList();
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
            catch (Exception ex)
            {
                return post;
            }
        }
        //hàm cho việc sửa bài viết trang admin
        public baiviet GetSingleArticle(int id)
        {
            baiviet post = new baiviet();
            try
            {
                post = db.baiviets.Where(x => x.id == id).SingleOrDefault();
                return post;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        //Lấy dữ liệu để phân trang
        public IEnumerable<baiviet> GetPagedListPost(int page, int pageSize)
        {
            try
            {
                var posts = db.baiviets.OrderByDescending(x => x.ngaycapnhat).ToList().ToPagedList(page, pageSize);
                return posts;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<GetArticleModel> SearchPost(string search)
        {
            List<GetArticleModel> posts = new List<GetArticleModel>();
            try
            {
                posts = db.Database.SqlQuery<GetArticleModel>("select * from baiviet where baiviet.tieude LIKE N'%" + search + "%'").ToList();
                foreach (var item in posts)
                {
                    item.theloai_ten = db.Database.SqlQuery<string>("select theloaitin.ten from theloaitin where theloaitin.id = " + item.theloai_id).SingleOrDefault();
                }
                return posts;
            }
            catch (Exception ex)
            {
                return posts;
            }
        }

        public bool DeletePost(int postId)
        {
            baiviet post = new baiviet();
            try
            {
                post = db.baiviets.Where(x => x.id == postId).SingleOrDefault();
                db.baiviets.Remove(post);
                db.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
