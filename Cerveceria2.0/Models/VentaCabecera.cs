using Microsoft.AspNetCore.Identity;
using Cerveceria2._0.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Cerveceria2._0.Models
{
    public class VentaCabecera
    {
        private ApplicationDbContext context;
        private readonly ApplicationUser userManager;

        [NotMapped]
        public int idCategoryFilter { get; set; }

        [NotMapped]
        public string FilterProductStr { get; set; }

        public VentaCabecera()
        {

            context = new ApplicationDbContext();
            userManager = new ApplicationUser();
        }

        public VentaCabecera(int idCategoria, string strfilter)
        {
            idCategoryFilter = idCategoria;
            FilterProductStr = strfilter;
            context = new ApplicationDbContext();
        }


        [Key]
        public int Id { get; set; }

        public DateTime Date { get; set; } = DateTime.Now;

        public string IdUsuario { get; set; }

        public List<VentaDetalle> Details { get; set; }



        public IdentityUser GetUserName()
        {
            var users = context.Users.ToList();
            var user = context.Users.Where(u => u.Id == IdUsuario).FirstOrDefault();
            return user;
            //return _conte userManager;
        }


        [NotMapped]
        public List<Product> Products
        {

            get
            {

                List<Product> lstProductFilter;

                if (idCategoryFilter > 0)
                {
                    lstProductFilter = context.Product.Where(p => p.CategoryId == idCategoryFilter).ToList();
                }
                else
                {
                    lstProductFilter = context.Product.ToList();
                }

                if (!string.IsNullOrEmpty(FilterProductStr))
                {

                    lstProductFilter = lstProductFilter.Where(p =>
                        p.Description.ToLower().Contains(FilterProductStr.ToLower()) ||
                        p.Title.ToLower().Contains(FilterProductStr.ToLower()
                        )).ToList();
                }

                return lstProductFilter;

            }

        }

    }
}