using Buisenss.Interface.Abstract;
using Entity.Concrate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Web.Areas.Admin.Controllers
{
    public class ArticleController : Controller
    {
        private readonly IAllService<Article> _allService;
        private readonly IArticleService _articleService;
        public ArticleController(IAllService<Article> allService, IArticleService articleService)
        {
            _allService = allService;
            _articleService = articleService;
        }
        // GET: Admin/Article
        public async Task<ActionResult> Index(string Search, int Page = 1)
        {
            var result = await _allService.GetFilterListOrAllList(Search,Page);
            var articleList = result.ToList();
            if (result != null)
            {
                foreach (var article in result)
                {
                    var cleanContent = ConvertSpecialCharacters(RemoveHtmlTags(article.ArticleContent));
                    article.ArticleContent = cleanContent;
                }
            }
            return View(result);
        }
        public ActionResult Create()
        {
            var userName = Login.Login.ActiveUser.UserName;
            List<Category> categories = _articleService.CategoryList();
            ViewBag.CategoryId = new SelectList(categories, "CategoryId", "CategoryName");
            ViewBag.UserName = userName;
            return View();
        }
        [ValidateInput(false)]
        [HttpPost]
        public async Task<ActionResult> Create(Article article)
        {
            if (ModelState.IsValid)
            {
                article.User.UserName = Login.Login.ActiveUser.UserName;
                await _allService.Add(article);
                return RedirectToAction("Index");
            }
            return View(article);
        }

        [HttpGet]
        public async Task<ActionResult> Edit(int Id)
        {
            Article article = await _allService.GetById(Id);
            List<Category> categories = _articleService.CategoryList();
            string selectedCategoryName = categories.FirstOrDefault(x=> x.CategoryId == article.CategoryId)?.CategoryName;
            SelectList selectLists = new SelectList(categories, "CategoryId", "CategoryName");

            ViewBag.CategoryId = selectLists;
            if (!string.IsNullOrEmpty(selectedCategoryName))
            {
                ViewBag.SelectedCategoryName = selectedCategoryName;
            }

            return View(article);
        }
        [ValidateInput(false)]
        [HttpPost]
        public async Task<ActionResult> Edit(Article article)
        {
            await _allService.Update(article);
            return RedirectToAction("Index");
        }
        public async Task<ActionResult> Delete(int Id)
        {
            await _allService.Delete(Id);
            return RedirectToAction("Index");
        }
        public async Task<ActionResult> Details(int Id)
        {
            await _articleService.Details(Id);
            return RedirectToAction("Index");
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
    }
}