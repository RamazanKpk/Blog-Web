using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entity.Concrate
{
    public class Article
    {
        [Key]
        [Display(Name = "ID")]
        public int ArticleId { get; set; }
        [Display(Name ="Article Title")]
        public string ArticleTitle { get; set; }
        [Display(Name ="Artficle Content")]
        public string ArticleContent { get; set; } //text editor kullanılacak (CK Editor)
        [Column(TypeName = "date")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString ="{0:dd.MM.yyyy}")]
        [Display(Name = "Aricle Date")]
        public DateTime ArticleDate { get; set; }
        [Display(Name ="Is Active")]
        public bool IsActive { get; set; }
        [ForeignKey("Category")]
        public int? CategoryId { get; set; }
        public virtual Category Category { get; set; }
        [ForeignKey("User")]
        public  int? UserId { get; set; }
        public virtual User User { get; set; }
        public virtual ICollection<ArticleComment> ArticleComments { get; set; }
        public virtual ICollection<ArticleImage> ArticlePhotos { get; set; }
    }
}
