using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entity.Concrate
{
    public class ArticleImage
    {
        [Key]
        [Display(Name = "ID")]
        public int ImageId { get; set; }
        [Required(ErrorMessage = "{0} space is required.")]
        [StringLength(50, ErrorMessage = "Must be no more than {1} characters long.")]
        [Display(Name = "Image Name")]
        public string ImageName { get; set; }
        [ForeignKey("Article")]
        public int? ArticleId { get; set; }
        [Display(Name ="Default Image")]
        public bool IsDefaultImage { get; set; }
        public virtual  Article Article { get; set; }
    }
}
