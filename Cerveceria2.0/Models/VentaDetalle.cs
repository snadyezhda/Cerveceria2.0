
using Cerveceria2._0.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Cerveceria2._0.Models
{
    public class VentaDetalle
    {
        [Key]
        public int Id { get; set; }

        public int ProductId { get; set; }

        public int Quantity { get; set; }


        [Column(TypeName = "decimal(18,4)")]
        public decimal Price { get; set; }


        public int VentaCabeceraId { get; set; }

        public Product GetProduct()
        {
            Product prod;

            using (var context = new ApplicationDbContext())
            {

                prod = context.Product.Where(p => p.Id == ProductId).FirstOrDefault();
            }

            return prod;
        }

        public decimal GetTotal
        {
            get
            {
                return Price * Quantity;

            }
        }


    }
}