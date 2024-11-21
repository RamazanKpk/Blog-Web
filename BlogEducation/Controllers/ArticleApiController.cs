using Buisenss.Interface.Abstract.ApiAbstract;
using DataModel.ArticleModels;
using Entity.Concrate;
using System;
using System.Threading.Tasks;
using System.Web.Http;

namespace BlogEducation.Controllers
{
    public class ArticleApiController : ApiController
    {
        private readonly IAllApiServices<ArticleListModel, CreateArticleListModel,ArticleDetailListModel> _allApiServices;
        public ArticleApiController(IAllApiServices<ArticleListModel, CreateArticleListModel, ArticleDetailListModel> allApiServices)
        {
            _allApiServices = allApiServices;
        }
        public ArticleApiController() { }
        [HttpGet]
        [Route("api/Articleapi/GetArticle")]
        public async Task<IHttpActionResult> GetArticle()
        {
            var result = await _allApiServices.GetList();
            return Json(result);
        }
        [HttpPost]
        [Route("api/Articleapi/PostArticle")]
        public IHttpActionResult PostArticle([FromBody] CreateArticleListModel article)
        {
            var result = _allApiServices.CreateOrUpdate(article);
            if (result != null)
            {
                //return Ok(new { success = true, message = "Article created successfully." });
                return Ok(article);
            }
            else
            {
                return InternalServerError(new Exception("Unable to create article."));
            }
        }

        [HttpGet]
        [Route("api/Articleapi/ArticleDetail")]
        public async Task<IHttpActionResult> ArticleDeatils()
        {
            var result = await _allApiServices.Details();
            return Json(result);
        }
    }
}
