using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Entity.Concrate
{
    public class Category
    {
        [Key]
        [Display(Name = "ID")]
        public int CategoryId { get; set; }
        [Required(ErrorMessage = "{0} space is required.")]
        [StringLength(30, ErrorMessage = "Must be no more than {1} characters long.")]
        [Display(Name = "Category Name")]
        public string CategoryName { get; set; }
        [DataType(DataType.MultilineText)]
        [Display(Name = "Description")]
        public string Description { get; set; }
        [Display(Name ="Is Active")]
        public bool IsActive { get; set; }
        public virtual ICollection<Article> Articles { get; set;}

    }
}