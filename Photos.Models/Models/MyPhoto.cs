using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Photos.Models.Models
{
    public class MyPhoto
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(60)]
        public string Title { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        public string Author { get; set; }

        [Required]
        [Display(Name  = "List Price")]
        [Range(0, 1000)]
        public double Price { get; set; }

        //[Required]
        //[Display(Name = "List Price")]
        //[Range(0, 1000)]
        //public double Price50 { get; set; }

        //[Required]
        //[Display(Name = "List Price")]
        //[Range(0, 1000)]
        //public double Price100 { get; set; }
    }
}
