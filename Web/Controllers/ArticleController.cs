using DataModel.ArticleModels;
using DataModel.CategoryModels;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Web.Mvc;
using Web.Login;

namespace Web.Controllers
{
    public class ArticleController : Controller
    {
        // GET: Article
     
        public async Task<ActionResult> Index()
        {
            var articleList = await GetArticle();
            var articles = articleList.OrderByDescending(x => x.Date).ToList();
            if (articleList != null)
            {

                foreach (var article in articleList)
                {
                    //article.Summary = ConvertSpecialCharacters(RemoveHtmlTags(article.Summary));
                    // s.ArticleContent.Length <= 200 ? s.ArticleContent :
                    //s.ArticleContent.Substring(0,200) + "...",
                    var summaryText = ConvertSpecialCharacters(RemoveHtmlTags(article.Summary));
                    article.Summary = summaryText.Length <= 200 ? summaryText : $"{summaryText.Substring(0, 200)}...";
                }
                return View(articles);
            }
            else
            {
                return HttpNotFound();
            }

        }
        public async Task<ActionResult> Category(string category)
        {
            var articleList = await GetArticle();
            var article = articleList.Where(x => x.CategoryName == category);
            return View(article);
        }
        //[LoginFilter]
        [Authorize]
        public async Task<ActionResult> Create()
        {
            var categories = await GetCategory();
            ViewBag.CategoryId = new SelectList(categories, "Id", "Name");
            TempData["Message"] = "You need to log in to create new article.";
            var userName = User.Identity.Name;
            //var userName = Login.Login.ActiveUser.UserName;
            ViewBag.UserName = userName;
            return View();
        }
        [ValidateInput(false)]
        [HttpPost]
        public async Task<ActionResult> Create(CreateArticleListModel model)
        {
            if (ModelState.IsValid)
            {
                model.User = Login.Login.ActiveUser.UserName;
                model.Date = DateTime.Now;
                var result = await CreateArticles(model);
                if (result != null)
                {
                    return RedirectToAction("Index");
                }
                ModelState.AddModelError("", "Unable to create article.");           
            }
            var categories = await GetCategory();
            ViewBag.CategoryId = new SelectList(categories, "Id", "Name");
            ViewBag.UserName = User.Identity.Name;
            return View(model);
        }
        public async Task<ActionResult> Details(int Id)
        {                    
            var detailList = await ArticleDetails();
            ViewBag.Categories = detailList.Select(c => c.Categories).ToList();
            var aricleList = detailList.Where(x => x.Id == Id).FirstOrDefault();
            if (aricleList != null)
            {
                var cleanContent = RemoveHtmlTags(aricleList.Content);
                var clearContent = ConvertSpecialCharacters(cleanContent);
                return View(aricleList);
            }
            else
            {
                return HttpNotFound();
            }
        }
        
        public async Task<List<ArticleListModel>> GetArticle()
        {
            List<ArticleListModel> articles = new List<ArticleListModel>();
            string api = ConfigurationManager.AppSettings["GetArticleEndPoint"].ToString();
            using (HttpClient client = new HttpClient())
            {
                HttpResponseMessage response = await client.GetAsync(api);
                if (response.IsSuccessStatusCode)
                {
                    string json = await response.Content.ReadAsStringAsync();
                    articles = JsonConvert.DeserializeObject<List<ArticleListModel>>(json);
                }
            }
            return articles;
        }


        public async Task<List<ArticleDetailListModel>> ArticleDetails()
        {
            List<ArticleDetailListModel> articleDetail = new List<ArticleDetailListModel>();
            string api = ConfigurationManager.AppSettings["ArticleDetailEndPoint"].ToString();
            using (HttpClient client = new HttpClient())
            {
                HttpResponseMessage response = await client.GetAsync(api);
                if (response.IsSuccessStatusCode)
                {
                    string json = await response.Content.ReadAsStringAsync();
                    articleDetail = JsonConvert.DeserializeObject<List<ArticleDetailListModel>>(json);
                }
            }
            return articleDetail;
        }
        public string RemoveHtmlTags(string html)
        {
            return Regex.Replace(html, @"<[^>]*>", String.Empty);
        }
        public string ConvertSpecialCharacters(string text)
        {
            text = text.Replace("&nbsp;", " ");
            return text;
        }
        public async Task<List<CategoryListModel>> GetCategory()
        {
            List<CategoryListModel> categories = new List<CategoryListModel>();
            string api = ConfigurationManager.AppSettings["GetCategoryEndPoint"].ToString();
            using (HttpClient client = new HttpClient())
            {
                HttpResponseMessage response = await client.GetAsync(api);
                if (response.IsSuccessStatusCode)
                {
                    string json = await response.Content.ReadAsStringAsync();
                    categories = JsonConvert.DeserializeObject<List<CategoryListModel>>(json);
                }
            }
            return categories;
        }
        public async Task<CreateArticleListModel> CreateArticles(CreateArticleListModel model)
        {
            string apiUrl = ConfigurationManager.AppSettings["PostArticleEndPoint"];
            var jsonContent = JsonConvert.SerializeObject(model);
            var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");
            var articleDataList = new CreateArticleListModel();

            using (HttpClient client = new HttpClient())
            {

                HttpResponseMessage reponse = await client.PostAsync(apiUrl, content);
                if (reponse.IsSuccessStatusCode)
                {
                    string jsonData = await reponse.Content.ReadAsStringAsync();
                    articleDataList = JsonConvert.DeserializeObject<CreateArticleListModel>(jsonData);
                }
            }
            return articleDataList;
        }
    }
}