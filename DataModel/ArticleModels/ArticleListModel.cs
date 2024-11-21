using DataModel.BaseModel;
using System;
using System.ComponentModel.DataAnnotations;

namespace DataModel.ArticleModels
{
    public class ArticleListModel : EntityBase
    {
        public string Title { get; set; }
        public string CategoryName { get; set; }
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime Date { get; set; }
        public string Summary { get; set; }
    }
}
