using Model.EF;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Dao
{
    public class UserDao
    {
        Model2 db = null;
        public UserDao()
        {
            db = new Model2();
        }
        public nguoidung GetByEmail(string email)
        {
            var user = db.nguoidungs.Where(x => x.email == email).SingleOrDefault();
            user.matkhau = "Hacker à :))";
            return user;
        }
        public nguoidung GetById(int userId)
        {
            var user = db.nguoidungs.Where(x => x.id == userId).SingleOrDefault();
            user.matkhau = "Hacker à :))";
            return user;
        }
        public string GetName(string email)
        {

            var user = db.nguoidungs.Where(x => x.email == email).SingleOrDefault();
            if (user == null)
            {
                return "Không tìm được";
            }
            else
            {
                return user.ten;
            }
        }
        public int Login(string email, string password)
        {
            var result = db.nguoidungs.Where(x => x.email == email).SingleOrDefault();
            if (result == null)
            {
                return 0; //Sai tài khoản
            }
            else
            {
                if (result.matkhau == password)
                {
                    return 1; //Thành công
                }
                else
                {
                    return -1;//Sai mật khẩu
                }
            }
        }
        public IEnumerable<nguoidung> GetPagedListUser(int page, int pageSize)
        {
            try
            {
                var users = db.nguoidungs.ToList().ToPagedList(page, pageSize);
                foreach (var item in users)
                {
                    item.matkhau = "Hacker à :))";
                }
                return users;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int AddUser(string name, string email, string password)
        {
            var test = db.nguoidungs.Where(x => x.email == email).SingleOrDefault();
            if (test == null)
            {
                nguoidung user = new nguoidung();
                user.ten = name;
                user.email = email;
                user.matkhau = password;
                user.ngaytao = DateTime.Now;
                user.ngaycapnhat = DateTime.Now;
                user.vaitro = 1; //Admin
                db.nguoidungs.Add(user);
                db.SaveChanges();
                return user.id;
            }
            else
            {
                return -1;
            }
        }
        public List<nguoidung> SearchUser(string search)
        {
            List<nguoidung> users = new List<nguoidung>();
            try
            {
                users = db.Database.SqlQuery<nguoidung>("select * from nguoidung where nguoidung.ten LIKE N'%" + search + "%'").ToList();
                foreach (var item in users)
                {
                    item.matkhau = "Hacker à :)))";
                }
                return users;
            }
            catch (Exception ex)
            {
                return users;
            }
        }
        public bool DeleteUser(int userId)
        {
            nguoidung user = new nguoidung();
            try
            {
                user = db.nguoidungs.Where(x => x.id == userId).SingleOrDefault();
                db.nguoidungs.Remove(user);
                db.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }
        public int EditUser(string username, string phone, string gender, string birth, int role, int id)
        {
            try
            {
                nguoidung editUser = db.nguoidungs.Where(x => x.id == id).SingleOrDefault();
                editUser.ten = username;
                editUser.sdt = phone;
                editUser.gioitinh = gender;
                editUser.ngaysinh = birth;
                editUser.vaitro = role;
                editUser.ngaycapnhat = DateTime.Now;

                db.SaveChanges();

                return editUser.id;
            }
            catch (Exception ex)
            {
                return -1;
            }
        }
        public int CountUser()
        {
            try
            {
                return db.nguoidungs.Count();
            }
            catch (Exception ex)
            {
                return -1;
            }
        }
    }
}
