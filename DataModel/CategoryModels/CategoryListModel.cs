using DataModel.ArticleModels;
using DataModel.BaseModel;
using System.Collections.Generic;

namespace DataModel.CategoryModels
{
    public class CategoryListModel : EntityBase
    {
        public string Name { get; set; }
        public List<ArticleListModel> Articles { get; set; }
    }
}