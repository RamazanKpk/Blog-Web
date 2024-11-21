using Buisenss.Interface.Abstract;
using Buisenss.Interface.Abstract.ApiAbstract;
using DataModel.ArticleCommentModels;
using Entity.Concrate;
using System;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.UI.WebControls;

namespace BlogEducation.Controllers
{

    public class CommentApiController : ApiController
    {
        private readonly IAllApiServices<ArticleCommentModel, CreateCommentModel, CommentDetailListModel> _allApiServices;
        public CommentApiController(IAllApiServices<ArticleCommentModel, CreateCommentModel, CommentDetailListModel> allApiServices)
        {
            _allApiServices = allApiServices;
        }
        public CommentApiController() { }
        [HttpGet]
        [Route("api/Commentapi/GetArticleComment")]
        public async Task<IHttpActionResult> GetArticleComment()
        {
            var result = await _allApiServices.GetList();
            return Json(result);
        }
        [HttpPost]
        [Route("api/Commentapi/PostArticleComment")]
        public IHttpActionResult PostArticleComment([FromBody] CreateCommentModel articleComment)
        {
            var result = _allApiServices.CreateOrUpdate(articleComment);
            if (result != null)
            {
                //return Ok(new { success = true, message = "Article created successfully." });
                return Ok(articleComment);
            }
            else
            {
                return InternalServerError(new Exception("Unable to create article."));
            }
        }
        [HttpGet]
        [Route("api/Commentapi/DetailComments")]
        public async Task<IHttpActionResult> GetDetailComment()
        {
            var result = await _allApiServices.Details();
            return Json(result);
        }
    }
}
