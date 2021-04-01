using Microsoft.AspNetCore.Http;
using Cerveceria2._0.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using Cerveceria2._0.Models;

namespace Cerveceria2._0.Models
{
    public class Product
    {

        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Por favor, ingrese una descripción.")]
        [MaxLength(150, ErrorMessage = "El máximo de caracteres es 150.")]
        [Display(Name = "Descripción")]
        public string Description { get; set; }

        [Required(ErrorMessage = "Por favor, ingrese un títlo.")]
        [MaxLength(50, ErrorMessage = "El máximo de caracteres es 50.")]
        [Display(Name = "Titulo del producto")]
        public string Title { get; set; }

        [Display(Name = "Código del Producto")]
        [Required(ErrorMessage = "Por favor, ingrese un código.")]
        [MaxLength(20)]
        public string Code { get; set; }


        [Display(Name = "Precio")]
        [Required(ErrorMessage = "Por favor, ingrese un precio.")]
        [Column(TypeName = "decimal(18,4)")]
        //[RegularExpression(@"^\d+\.\d{0,2}$")]
        //[Range(0, 9999999999999999.99)]
        public decimal Price { get; set; }


        [Display(Name = "Categoria")]
        [Required(ErrorMessage = "Por favor, seleccione una categoría.")]
        public int CategoryId { get; set; }

        public string ImageName { get; set; }


        public Category GetCategory()
        {
            Category cat;

            using (var _context = new ApplicationDbContext())
            {

                cat = _context.Category.Where(c => c.Id == CategoryId).FirstOrDefault();

            }
            return cat;

        }


    }
}
