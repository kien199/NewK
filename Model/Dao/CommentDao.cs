﻿using Model.EF;
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
    }
}