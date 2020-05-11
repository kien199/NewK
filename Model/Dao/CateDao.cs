using Model.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Dao
{
    public class CateDao
    {
        Model2 db = null;
        public CateDao()
        {
            db = new Model2();
        }

        public int AddCate(string ten, string slug)
        {
            try
            {
                theloaitin newCate = new theloaitin();
                newCate.ten = ten;
                newCate.slug = slug;
                newCate.ngaytao = DateTime.Now;
                newCate.ngaycapnhat = DateTime.Now;

                db.theloaitins.Add(newCate);
                db.SaveChanges();

                return newCate.id;
            }
            catch (Exception ex)
            {
                return -1;
            }
        }
        public int GetId(string slug)
        {
            int cate_id = -1;
            try
            {
                var cate = db.theloaitins.Where(x => x.slug == slug).FirstOrDefault();
                if (cate != null)
                {
                    cate_id = cate.id;
                }
                return cate_id;
            }
            catch (Exception ex)
            {
                return cate_id;
            }
        }
        public string GetName(int id)
        {
            string ten = "";
            try
            {
                var cate = db.theloaitins.Where(x => x.id == id).FirstOrDefault();
                if (cate != null)
                {
                    ten = cate.ten;
                }
                return ten;
            }
            catch (Exception ex)
            {
                return ten;
            }
        }
        public string GetSlug(int id)
        {
            string slug = "";
            try
            {
                var cate = db.theloaitins.Where(x => x.id == id).FirstOrDefault();
                if (cate != null)
                {
                    slug = cate.slug;
                }
                return slug;
            }
            catch (Exception ex)
            {
                return slug;
            }
        }
        public List<theloaitin> GetAllCate()
        {
            List<theloaitin> cates = new List<theloaitin>();
            try
            {
                cates = db.Database.SqlQuery<theloaitin>("select * from theloaitin").ToList();
                return cates;
            }
            catch
            {
                return cates;
            }
        }
        public theloaitin GetSingleCate(int cateId)
        {
            theloaitin cate = new theloaitin();
            try
            {
                cate = db.theloaitins.Where(x => x.id == cateId).SingleOrDefault();
                return cate;
            }
            catch
            {
                return cate;
            }
        }
    }
}
