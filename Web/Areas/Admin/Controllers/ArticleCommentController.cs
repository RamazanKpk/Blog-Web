using Buisenss.Interface.Abstract;
using Entity.Concrate;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Web.Areas.Admin.Controllers
{
    public class ArticleCommentController : Controller
    {
        private readonly IAllService<ArticleComment> _allService;
        private readonly IAllService<Article> _articleListService;
        private readonly IAllService<User> _userLsitService;
        public ArticleCommentController(IAllService<ArticleComment> allService, IAllService<Article> articleListService, IAllService<User> userLsitService)
        {
            _allService = allService;
            _articleListService = articleListService;
            _userLsitService = userLsitService;
        }
        // GET: Admin/ArticleComment
        public async Task<ActionResult> Index(string Search = null, int Page = 1)
        {
            var result = await _allService.GetFilterListOrAllList(Search, Page);
            return View(result);
        }
        public async Task<ActionResult> Delete(int Id)
        {
            await _allService.Delete(Id);
            return RedirectToAction("Index");
        }
        [HttpGet]
        public async Task<ActionResult> Edit(int Id)
        {
            ArticleComment comment = await _allService.GetById(Id);
            List<User> users = await _userLsitService.GetList();
            List<Article> articles = await _articleListService.GetList();
            string selectedUserName = users.FirstOrDefault(x=>x.UserId == comment.UserId)?.UserName;
            string selectedArticleName = articles.FirstOrDefault(x => x.ArticleId == comment.ArticleId)?.ArticleTitle;
            if (!string.IsNullOrEmpty(selectedArticleName))
            {
                ViewBag.SelectedArticleTitle = selectedArticleName;
            }
            if (!string.IsNullOrEmpty(selectedUserName))
            {
                ViewBag.SelectedUserName = selectedUserName;
            }
            ViewBag.CommentUser = comment.UserId;
            ViewBag.CommentArticle = comment.ArticleId;
            return View(comment);
        }
        [HttpPost]
        public async Task<ActionResult> Edit(ArticleComment articleComment)
        {
            if (ModelState.IsValid)
            {

                if (articleComment.ArticleId != null && articleComment.UserId != null)
                {
                    await _allService.Update(articleComment);
                    return RedirectToAction("Index");
                }
                else
                {
                    ModelState.AddModelError("", "ArticleId and/or UserId cannot be null.");
                }
            }
            return View(articleComment);
        }

        public async Task<ActionResult> Details(int Id)
        {
            return View(await _allService.GetById(Id));
        }


        //[HttpGet]
        //public async Task<JsonResult> ReadComment(int commentId)
        //{
        //    ArticleComment articleComment = await _allService.GetById(commentId);

        //    if (articleComment == null)
        //    {
        //        return null;
        //    }
        //    else
        //    {
        //        var data = new
        //        {
        //            c_UserName = articleComment.User.UserName,
        //            c_ArticleTitle = articleComment.Article.ArticleTitle,
        //            c_CommentDate = articleComment.CommentDate,
        //            c_IsApproved = articleComment.IsApproved,
        //            c_IpAddress = articleComment.IpAddress,
        //        };
        //        return Json(data, JsonRequestBehavior.AllowGet);
        //    }
        //}

        //public async Task<ActionResult> Approval(int Id, bool State)
        //{
        //    var comment = await _allService.GetById(Id);
        //    comment.IsApproved = State;
        //    string message;
        //    if (State)
        //    {
        //        message = "Comment is approved!";
        //    }
        //    else
        //    {
        //        message = "Comment approval removed!";
        //    }
        //    return Json(message);
        //}
    }
}