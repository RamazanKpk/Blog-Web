using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Entity.Concrate
{
    public class User
    {
        [Key]
        [Display(Name = "ID")]
        public int UserId { get; set; }
        [Required(ErrorMessage = "{0} space is required.")]
        [StringLength(30, ErrorMessage = "Must be no more than {1} characters long.")]
        [Display(Name = "User Name")]
        public string UserName { get; set; }
        [Required(ErrorMessage = "{0} space is required.")]
        [EmailAddress(ErrorMessage = "Enter a valid e-mail address.")]
        [Display(Name = "E-Mail Address")]
        public string EMail { get; set; }
        [DataType(DataType.Password)]
        [Required(ErrorMessage = "{0} space is required.")]
        [StringLength(10, MinimumLength =4, ErrorMessage = "The password must be at least {2}, at least must be no more than {1} characters long.")]
        [Display(Name ="Password")]
        public string Password { get; set; }
        [Display(Name ="Admin Title")]
        public bool IsAdmin { get; set; }
        [Display(Name ="Is Active")]
        public bool IsActive { get; set; }
        //public virtual ICollection<Article> Articles { get; set; }
    }
}