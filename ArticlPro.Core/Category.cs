using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArticlPro.Core
{
    [Table("Categories")]
    public class Category
    {
        [Required]
        [Display(Name = "المعرف")]
        public int Id { get; set; }
        [Required(ErrorMessage ="هذا الحقل مطلوب")]
        [Display(Name = "اسم الصنف")]
        [MaxLength(50, ErrorMessage = "الحد الاقصى 50 حرف")]
        [MinLength(2, ErrorMessage = "الحد الادنى 3 حرف")]
        [DataType(DataType.Text)]
        public string Name { get; set; }

        //Navigation Property
        public virtual List<AuthorPost> AuthorPosts { get; set; }

    }
}
