using DataModel.ArticleCommentModels;
using Newtonsoft.Json;
using System.Configuration;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http.Filters;
using System.Web.Mvc;

namespace Web.Controllers
{
    public class CommentController : Controller
    {
        // GET: Comment
       
        public ActionResult Create(int ArticleId)
        {
            string user;
            if (Login.Login.ActiveUser != null)
            {
                user = Login.Login.ActiveUser.UserName;
                if (user != null)
                {
                    ViewBag.UserName = user;
                }
                else
                {
                    ViewBag.UserName = null;
                    RedirectToAction("login", "Login", new { ReturnUrl = "Article/Details/" + ArticleId + "" });
                }
            }
            TempData["Message"] = "You need to log in to post a comment.";
            return View(new CreateCommentModel { ArticleId = ArticleId });
        }
        [HttpPost]
        [Authorize]
        public async Task<ActionResult> Create(CreateCommentModel model)
        {          
            if (ModelState.IsValid)
            {
                if (model.User == null)
                {
                    model.User = Login.Login.ActiveUser.UserName;
                }

                var result = await CreateComment(model);
                if (result != null)
                {
                    return RedirectToAction("Index", "Article", new {area =""});
                }
                else
                {
                    TempData["Message"] = "You need to log in to post a comment.";
                    return RedirectToAction("login", "Login", new { ReturnUrl = Request.Url.PathAndQuery });
                }
            }
            return View(model);
        }
        public async Task<CreateCommentModel> CreateComment(CreateCommentModel model)
        {
            string apiUrl = ConfigurationManager.AppSettings["PostArticleCommentEndPoint"];
            var jsonContent = JsonConvert.SerializeObject(model);
            var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");
            var createComment = new CreateCommentModel();

            using (HttpClient client = new HttpClient())
            {

                HttpResponseMessage reponse = await client.PostAsync(apiUrl, content);
                if (reponse.IsSuccessStatusCode)
                {
                    string jsonData = await reponse.Content.ReadAsStringAsync();
                    createComment = JsonConvert.DeserializeObject<CreateCommentModel>(jsonData);
                }
            }
            return createComment;
        }
    }
}