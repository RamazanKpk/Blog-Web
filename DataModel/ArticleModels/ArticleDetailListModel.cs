
using DataModel.ArticleCommentModels;
using DataModel.BaseModel;
using DataModel.CategoryModels;
using System.Collections.Generic;

namespace DataModel.ArticleModels
{
    public class ArticleDetailListModel : EntityBase
    {
        public string Content { get; set; }
        public List<CategoryListModel> Categories { get; set; }
        public string User {  get; set; }
        public List<CommentDetailListModel> Comments { get; set; }
    }
}
