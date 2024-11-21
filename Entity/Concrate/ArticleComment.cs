using Newtonsoft.Json;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entity.Concrate
{
    public class ArticleComment
    {
        [Key]
        [Display(Name = "ID")]
        public int CommentId { get; set; }
        [DataType(DataType.MultilineText)]
        [Display(Name = "Comment")]
        public string Comment { get; set; }
        [ForeignKey("User")]
        public int? UserId { get; set; }
        [Display(Name ="Is Approved")]
        public bool IsApproved { get; set; }
        [ForeignKey("Article")]
        public int? ArticleId { get; set; }
        [Column(TypeName = "date")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString ="{0:dd.MM.yyyy}")]
        [Display(Name = "Comment Date")]
        public DateTime? CommentDate { get; set; }
        [Required(ErrorMessage = "{0} space is required.")]
        [StringLength(30, ErrorMessage = "Must be no more than {1} characters long.")]
        [Display(Name = "Ip Address")]
        public string IpAddress { get; set; }

        public virtual Article Article { get; set; }

        public virtual User User { get; set; }
    }
}
