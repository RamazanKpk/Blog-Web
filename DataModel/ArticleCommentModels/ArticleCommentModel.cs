using DataModel.BaseModel;
using System;

namespace DataModel.ArticleCommentModels
{
    public class ArticleCommentModel : EntityBase
    {
        public string UserName { get; set; }
        public DateTime? Date { get; set; }
    }
}
