using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace PhotosForSale.Models
{
    public class Category
    {
        [Key]

        public int? Id { get; set; }

        [Required]
        [MaxLength(40)]
        [DisplayName("Category Name")]
        public string CategoryName { get; set; }

        [DisplayName("Display Order")]
        [Range(1, 20)]
        public int DisplayOrder { get; set; }
    }
}
