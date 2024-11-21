using DataModel.BaseModel;
using System;

namespace DataModel.ArticleModels
{
    public class CreateArticleListModel : EntityBase
    {
        public string Title { get; set; }
        public string Content { get; set; }
        public int CategoryId { get; set; }
        public string User {  get; set; }
        public DateTime Date { get; set; }


    }
}
