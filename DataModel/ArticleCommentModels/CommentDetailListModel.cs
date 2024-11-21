using DataModel.BaseModel;
using System.Collections.Generic;

namespace DataModel.ArticleCommentModels
{
    public class CommentDetailListModel : EntityBase
    {
        public string Comment {  get; set; }
        public int ArticleId { get; set; }
        public List<ArticleCommentModel> Comments { get; set; }
    }
}
