using HtmlAgilityPack;
using Model.Dao;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;

namespace Baochi.Areas.Client.Controllers
{
    public class CrawlerController : Controller
    {
        //// GET: Client/Crawler
        //public ActionResult Index()
        //{
        //    return View();
        //}
        public void Crawler(string url)
        {
            try
            {
                string[] cates = { "Mobile", "Esport", "Khám phá", "Manga Film", "Tin tức game", "Cộng đồng mạng" };
               
                foreach (var item in cates)
                {
                    var cate_id = new CateDao().AddCate(item, ToUrlSlug(item));
                    if (cate_id == -1)
                    {
                        Console.WriteLine("Lỗi " + item);
                    }
                    else
                    {
                        Console.WriteLine("Thêm " + item);
                    }
                }
                for (int j = 0; j < cates.Length; j++)
                {

                    var cate_slug = ToUrlSlug(cates[j]);
                    if (cates[j] == "Tin tức game" || cates[j] == "Cộng đồng mạng")
                    {
                        CrawlerSingleCate(url + "/" + cate_slug + ".htm", j+1);
                    }
                    else
                    {
                        CrawlerSingleCate(url + "/" + cate_slug + ".chn", j+1);
                    }
                    //https://gamek.vn/tin-tuc-game.htm
                    //https://gamek.vn/kham-pha.chn
                }
            }
            catch (Exception ex)
            {

            }
        }
        public void CrawlerSingleCate(string url, int theloai_id)
        {
            try
            {
                // Lấy full html từ web gamek
                var client = new WebClient();
                client.Encoding = Encoding.UTF8;
                var html = client.DownloadString(url);

                //Lọc thông tin
                var htmlDocument = new HtmlDocument();
                htmlDocument.LoadHtml(html);

                var html_posts = htmlDocument.DocumentNode.Descendants("li")
                    .Where(node => node.GetAttributeValue("class", "")
                    .Equals("top")).ToList();
                foreach (var post in html_posts)
                {
                    var post_link = post.Descendants("a").FirstOrDefault()
                        ?.ChildAttributes("href")?.FirstOrDefault()?.Value;

                    string tieude = post.Descendants("h3").FirstOrDefault().InnerText;
                    string slug = ToUrlSlug(tieude);
                    string tomtat = post.Descendants("p").FirstOrDefault().InnerText;
                    string noidung = GetContent("https://gamek.vn" + post_link);
                    string thumbnail = post.Descendants("img").FirstOrDefault()
                        ?.ChildAttributes("src")?.FirstOrDefault()?.Value;

                    var newPost_id = new PostDao().AddPost(tieude, slug, tomtat, noidung, thumbnail, theloai_id);
                    if(newPost_id == -1)
                    {
                        //lỗi
                        Console.WriteLine("Lỗi " + tieude);
                    }
                    else
                    {
                        //thành công
                        Console.WriteLine("Thêm" + tieude);
                    }
                }
            }
            catch (Exception ex)
            {
                CrawlerSingleCate(url,theloai_id);
            }
        }
        public string GetContent(string url)
        {
            // Lấy full html từ link của bài viết
            var client = new WebClient();
            client.Encoding = Encoding.UTF8;
            var html = client.DownloadString(url);

            //Lọc thông tin
            var htmlDocument = new HtmlDocument();
            htmlDocument.LoadHtml(html);

            var remove_content = htmlDocument.DocumentNode.Descendants("div")
                .Where(node => node.GetAttributeValue("class", "").Equals("VCSortableInPreviewMode link-content-footer IMSCurrentEditorEditObject", StringComparison.InvariantCultureIgnoreCase) ||
                node.GetAttributeValue("class", "").Equals("link-source-wrapper is-web clearfix mb20", StringComparison.InvariantCultureIgnoreCase)).ToList();
            foreach (var n in remove_content)
            {
                n.Remove();
            }

            var html_single_post = htmlDocument.DocumentNode.Descendants("div")
                .Where(node => node.GetAttributeValue("class", "")
                .Equals("rightdetail")).ToList();
            var content = html_single_post[0].InnerHtml;

            return content;
        }
        public static string ToUrlSlug(string value)
        {

            //First to lower case
            value = value.ToLowerInvariant();

            // Bỏ dấu Tiếng Việt
            Regex regex = new Regex("\\p{IsCombiningDiacriticalMarks}+");
            string temp = value.Normalize(NormalizationForm.FormD);
            value = regex.Replace(temp, String.Empty).Replace('\u0111', 'd').Replace('\u0110', 'D');

            //Remove all accents
            var bytes = Encoding.GetEncoding("Cyrillic").GetBytes(value);
            value = Encoding.ASCII.GetString(bytes);

            //Replace spaces
            value = Regex.Replace(value, @"\s", "-", RegexOptions.Compiled);

            //Remove invalid chars
            value = Regex.Replace(value, @"[^a-z0-9\s-_]", "", RegexOptions.Compiled);

            //Trim dashes from end
            value = value.Trim('-', '_');

            //Replace double occurences of - or _
            value = Regex.Replace(value, @"([-_]){2,}", "$1", RegexOptions.Compiled);

            return value;
        }
    }
}