using DataAccess;
using DataModel.ArticleCommentModels;
using DataModel.CategoryModels;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Web.Controllers
{
    public class PartialController : Controller
    {
        // GET: Partial
        public PartialViewResult Menu()
        {
            if (Login.Login.ActiveUser != null)
            {
                var userName = Login.Login.ActiveUser.UserName;
                ViewBag.User = userName;
                if (Login.Login.ActiveUser.IsAdmin)
                {
                    ViewBag.IsAdmin = true;
                }
                else
                {
                    ViewBag.IsAdmin = false;
                }
            }
            else
            {
                ViewBag.IsAdmin = false;
            }

            var categories = GetCategory();
            return PartialView("_Menu", categories.ToList());
        }
        public PartialViewResult Footer()
        {
            var lastComment = DetailComments();
            return PartialView("_Footer", lastComment.ToList());
        }
        [HttpGet]
        public List<CategoryListModel> GetCategory()
        {
            List<CategoryListModel> categories = new List<CategoryListModel>();
            string api = ConfigurationManager.AppSettings["GetCategoryEndPoint"].ToString();
            using (HttpClient client = new HttpClient())
            {
                HttpResponseMessage response =  client.GetAsync(api).Result;
                if (response.IsSuccessStatusCode)
                {
                    string json =  response.Content.ReadAsStringAsync().Result;
                    categories = JsonConvert.DeserializeObject<List<CategoryListModel>>(json);
                }
            }
            return categories;
        }
        [HttpGet]
        public List<CommentDetailListModel> DetailComments()
        {
            List<CommentDetailListModel> comments = new List<CommentDetailListModel>();
            string api = ConfigurationManager.AppSettings["CommentDetailEndPoint"].ToString();
            using (HttpClient client = new HttpClient())
            {
                HttpResponseMessage response = client.GetAsync(api).Result;
                if (response.IsSuccessStatusCode)
                {
                    string json =  response.Content.ReadAsStringAsync().Result;
                    comments = JsonConvert.DeserializeObject<List<CommentDetailListModel>>(json);
                }
            }
            return comments;
        }
    }
}