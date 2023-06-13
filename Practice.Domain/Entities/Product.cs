using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Practice.Domain.Entities
{
    public class Product
    {
        [Key]
        public int ProductId { get; set; }

        [Required(ErrorMessage = "Product Name can't be blank")]
        public string ProductName { get; set; }

        [Required(ErrorMessage = "Product Description can't be blank")]
        public string ProductDescription { get; set; }

        [Required(ErrorMessage = "Product Price can't be blank")]
        public string ProductPrice { get; set; }

        [Required(ErrorMessage = "Please choose Category")]
        public int CategoryId { get; set; }

        [ForeignKey("CategoryId")]
        public Category Category { get; set; }
    }
}
