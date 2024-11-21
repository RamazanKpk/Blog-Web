using DataModel.ArticleModels;
using Entity.Concrate;
using System.Data.Entity;


namespace DataAccess
{
    public class ArticleContext: DbContext
    {
        public DbSet<Article> Articles { get; set; }
        public DbSet<ArticleComment> ArticleComments { get; set; }
        public DbSet<Category> Categories { get; set; }
        public  DbSet<User> Users { get; set; }
        public DbSet<ArticleImage> ArticleImages { get; set; }
    }
}
