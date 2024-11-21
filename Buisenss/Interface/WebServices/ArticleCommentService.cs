using Buisenss.Interface.Abstract;
using DataAccess;
using Entity.Concrate;
using PagedList;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace Buisenss.Interface.Service
{
    public class ArticleCommentService : IAllService<ArticleComment>
    {
        ArticleContext db = new ArticleContext();
        public async Task Add(ArticleComment comment)
        {
            db.ArticleComments.Add(comment);
            await db.SaveChangesAsync();

        }
        public async Task Delete(int CommentId)
        {
            var comment = await db.ArticleComments.FindAsync(CommentId);
            if (comment != null)
            {
                db.ArticleComments.Remove(comment);
                await db.SaveChangesAsync();
            }       
        }

        public Task<ArticleComment> GetById(int? CommentId)
        {
            var comment = db.ArticleComments.FindAsync(CommentId);
            return comment;
        }

        public async Task<IPagedList<ArticleComment>> GetFilterListOrAllList(string Search, int Page)
        {
            var commentList = await db.ArticleComments.ToListAsync();
            var query = from x in commentList select x;
            if (!String.IsNullOrEmpty(Search))
            {
                query = query.Where(x=> x.User.UserName.Contains(Search)||
                x.Article.ArticleTitle.Contains(Search));
            }
            return query.OrderByDescending(x => x.CommentDate).ThenBy(c => c.CommentId).ToPagedList(Page, 5);
            
        }

        public Task<List<ArticleComment>> GetList()
        {
            var commentlist = db.ArticleComments.ToListAsync();
            return commentlist;
        }

        public async Task Update(ArticleComment comment)
        {
            db.Entry(comment).State = EntityState.Modified;
            //ArticleComment articleComment = await db.ArticleComments.FindAsync(comment.CommentId);
            //articleComment.Article.ArticleTitle = comment.Article.ArticleTitle;
            //articleComment.User.UserName = comment.User.UserName;
            //articleComment.IsApproved = comment.IsApproved;
            //articleComment.IpAddress = comment.IpAddress;
            //articleComment.CommentDate = comment.CommentDate;
            await db.SaveChangesAsync();
        }

    }
}
