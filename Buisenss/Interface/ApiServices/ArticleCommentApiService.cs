using Buisenss.Interface.Abstract.ApiAbstract;
using DataAccess;
using DataModel.ArticleCommentModels;
using Entity.Concrate;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace Buisenss.Interface.ApiServices
{
    public class ArticleCommentApiService : IAllApiServices<ArticleCommentModel, CreateCommentModel, CommentDetailListModel>
    {
        ArticleContext db = new ArticleContext();
        public async Task CreateOrUpdate(CreateCommentModel comment)
        {
            var user = FindName(comment.User);
            int userId = user.UserId;
            var comments = new ArticleComment
            {
                ArticleId = comment.ArticleId,
                Comment = comment.Comment,
                CommentDate = DateTime.Now,
                IsApproved = false,
                UserId = userId,
                IpAddress = HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"]
            };
            db.ArticleComments.Add(comments);
            await db.SaveChangesAsync();
        }

        public async Task<List<CommentDetailListModel>> Details()
        {
            var result = await db.ArticleComments.Where(x => x.IsApproved)
                .Select(c => new CommentDetailListModel()
                {
                    Id = c.CommentId,
                    Comment = c.Comment,
                    ArticleId = c.Article.ArticleId,
                    Comments = db.ArticleComments.Where(a => a.CommentId == c.CommentId)
                    .Select(d => new ArticleCommentModel()
                    {
                        Date = d.CommentDate,
                        Id = d.CommentId,
                        UserName = d.User.UserName,
                    }).ToList(),
                }).OrderByDescending(k => k.Comments.FirstOrDefault().Date).ToListAsync();
            return result;
        }

        public async Task<List<ArticleCommentModel>> GetList()
        {
            var result = await db.ArticleComments.Where(x => x.IsApproved)
                .Select(c => new ArticleCommentModel()
                {
                    Id = c.CommentId,
                    Date = c.CommentDate,
                    UserName = c.User.UserName
                }).ToListAsync();
            return result;
        }
        public User FindName(string name)
        {
            var user = db.Users.Where(x => x.UserName == name).FirstOrDefault();
            return user;
        }
    }
    
}
