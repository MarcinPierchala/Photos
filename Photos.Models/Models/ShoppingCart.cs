using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Photos.Models.Models
{
    public class ShoppingCart
    {
        public int Id { get; set; }
        public int PhotoId { get; set; }

        [ForeignKey("PhotoId")]
        [ValidateNever]
        public MyPhoto MyPhoto { get; set; }

        public string ApplicationUserId { get; set; }

        [ForeignKey("ApplicationUserId")]
        [ValidateNever]
        public ApplicationUser ApplicationUser { get; set; }

        //[NotMapped]
        //public double Price { get; set; }

    }
}
