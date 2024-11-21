using Buisenss.Interface.Abstract.ApiAbstract;
using DataAccess;
using DataModel.ArticleCommentModels;
using DataModel.ArticleModels;
using DataModel.CategoryModels;
using Entity.Concrate;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace Buisenss.Interface.ApiServices
{
    public class ArticleApiService : IAllApiServices<ArticleListModel, CreateArticleListModel, ArticleDetailListModel>
    {
        ArticleContext db = new ArticleContext();
        public async Task<List<ArticleListModel>> GetList()
        {
            var result = await db.Articles.Where(x => x.IsActive)
                .Select(s => new ArticleListModel()
                {
                    Date = s.ArticleDate,
                    Id = s.ArticleId,
                    Title = s.ArticleTitle,
                    CategoryName = s.Category.CategoryName,
                    Summary = s.ArticleContent
                }).ToListAsync();
            return result;
        }

        public async Task CreateOrUpdate(CreateArticleListModel model)
        {
            //GenericResponse
            // success
            // message
            // data
            var user = FindName(model.User);
            int userId = user.UserId;
            var articles = new Article
            {
                ArticleTitle = model.Title,
                ArticleDate = model.Date,
                ArticleContent = model.Content,
                IsActive = false,
                CategoryId = model.CategoryId,
                UserId = userId,
            };
            db.Articles.Add(articles);
            await db.SaveChangesAsync();
        }
        public async Task<List<ArticleDetailListModel>> Details()
        {
            var result = await db.Articles.Where(x => x.IsActive)
                .Include(x=> x.User)
                .Select(s => new ArticleDetailListModel()
            {
                Id = s.ArticleId,
                Content = s.ArticleContent,
                User = s.User.UserName,
                Categories = db.Categories.Where(g => g.IsActive)
                .Select(a => new CategoryListModel
                {
                    Name = a.CategoryName,
                    Id = a.CategoryId,
                    Articles = db.Articles.Where(v => v.ArticleId == s.ArticleId && v.CategoryId == a.CategoryId)
                    .Select(e => new ArticleListModel
                    {
                        Id = e.ArticleId,
                        CategoryName = e.Category.CategoryName,
                        Date = e.ArticleDate,
                        Title = e.ArticleTitle,
                    }).ToList(),
                } ).ToList(),
                Comments = db.ArticleComments.Where(x=> x.IsApproved && x.ArticleId == s.ArticleId)
                .Select(c=> new CommentDetailListModel
                {
                    Id = c.CommentId,
                    Comment = c.Comment,
                    Comments = db.ArticleComments.Where (b=> b.CommentId == c.CommentId)
                    .Select(d => new ArticleCommentModel {
                        Id = d.CommentId,
                        Date = d.CommentDate,
                        UserName = d.User.UserName,
                    }).ToList(),
                }).ToList(),
            }).ToListAsync();
            return result;
        }
        public User FindName(string name)
        {
            var user =  db.Users.Where(x=> x.UserName == name).FirstOrDefault();
            return user;
        }
    }
}
