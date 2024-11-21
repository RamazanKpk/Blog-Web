using DataModel.BaseModel;
using System;

namespace DataModel.ArticleCommentModels
{
    public class CreateCommentModel : EntityBase
    {
        public int ArticleId {  get; set; }
        public string Comment { get; set; }
        public string User { get; set; }
        //public DateTime Date { get; set; }
    }
}
