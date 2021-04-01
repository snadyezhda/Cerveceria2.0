using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Cerveceria2._0.Data;
using Cerveceria2._0.Helpers;
using Cerveceria2._0.Models;

namespace Cerveceria2._0.Controllers
{
    public class VentaCabecerasController : Controller
    {
        private readonly ApplicationDbContext _context;

        private readonly ApplicationUser userManager;

        public VentaCabecerasController(ApplicationDbContext context)
        {
            _context = context;
            userManager = new ApplicationUser();
        }

        // GET: VentaCabeceras
        public async Task<IActionResult> Index()
        {
            return View(await _context.VentaCabecera.ToListAsync());
        }

        //// GET: VentaCabeceras
        //[HttpPost]
        //public async Task<IActionResult> Index(int Categoriaid)
        //{

        //    return View();
        //}

        // GET: VentaCabeceras/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ventaCabecera = await _context.VentaCabecera
                .FirstOrDefaultAsync(m => m.Id == id);
            if (ventaCabecera == null)
            {
                return NotFound();
            }

            ventaCabecera.Details = _context.VentaDetalle.Where(vd => vd.VentaCabeceraId == id).ToList();

            if (ventaCabecera.Details == null)
                ventaCabecera.Details = new List<VentaDetalle>();
            ViewBag.TotalVentas = ventaCabecera.Details.Sum(p => p.Price * p.Quantity);


            return View(ventaCabecera);
        }

        // GET: VentaCabeceras/Create
        public IActionResult Create(int idCategory, string strFilter)
        {
            if (ListProducts == null) {
                ListProducts = new List<ShopCartTemp>();
            }
            ViewBag.CategoryList = Helpers.Functions.GetCategorys(true);
            var cabecera = new VentaCabecera(idCategory, strFilter);
            return View(cabecera);
        }

        // POST: VentaCabeceras/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Date,IdUsuario")] VentaCabecera ventaCabecera)
        {
            if (ModelState.IsValid && ListProducts.Count > 0)
            {

                if (User.Identity.IsAuthenticated)
                {
                    //var user = userManager.Id;  //NO SIRVE
                    var user = User.FindFirstValue(ClaimTypes.NameIdentifier); //SI Funciona!
                    ventaCabecera.IdUsuario = user;

                    var ListDetalle = new List<VentaDetalle>();
                    foreach (var item in ListProducts)
                    {
                        var ventaDetail = new VentaDetalle();

                        ventaDetail.ProductId = item.IdProduct;
                        ventaDetail.Quantity = item.Quantity;
                        ventaDetail.Price = _context.Product.Where(p => p.Id == item.IdProduct).FirstOrDefault().Price;
                        ListDetalle.Add(ventaDetail);
                    }
                    ventaCabecera.Details = ListDetalle;



                    _context.Add(ventaCabecera);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
            }
            return View(ventaCabecera);
        }

        // GET: VentaCabeceras/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ventaCabecera = await _context.VentaCabecera.FindAsync(id);
            if (ventaCabecera == null)
            {
                return NotFound();
            }
            return View(ventaCabecera);
        }

        // POST: VentaCabeceras/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Date,IdUsuario")] VentaCabecera ventaCabecera)
        {
            if (id != ventaCabecera.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(ventaCabecera);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!VentaCabeceraExists(ventaCabecera.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(ventaCabecera);
        }

        // GET: VentaCabeceras/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ventaCabecera = await _context.VentaCabecera
                .FirstOrDefaultAsync(m => m.Id == id);
            if (ventaCabecera == null)
            {
                return NotFound();
            }

            return View(ventaCabecera);
        }

        // POST: VentaCabeceras/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var ventaCabecera = await _context.VentaCabecera.FindAsync(id);
            _context.VentaCabecera.Remove(ventaCabecera);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool VentaCabeceraExists(int id)
        {
            return _context.VentaCabecera.Any(e => e.Id == id);
        }


        private static List<ShopCartTemp> ListProducts;
        public ShopCartTemp AddProductToCart(int IdProduct)
        {

            if (ListProducts == null)
            {
                ListProducts = new List<ShopCartTemp>();
            }

            var productInList = ListProducts.Where(p => p.IdProduct == IdProduct).FirstOrDefault();

            if (productInList != null)
            {
                productInList.Quantity++;
            }
            else
            {
                productInList = new ShopCartTemp() { IdProduct = IdProduct, Quantity = 1 };
                ListProducts.Add(productInList);
            }

            productInList.TotalProducto = _context.Product.Where(p => p.Id == IdProduct).FirstOrDefault().Price * productInList.Quantity;

            ShopCartTemp ret = GetProductShopCart(IdProduct);

            return ret;
        }

        public ShopCartTemp GetProductShopCart(int IdProduct)
        {

            return ListProducts.Where(p => p.IdProduct == IdProduct).FirstOrDefault();
        }

        public decimal GetImporteTotal()
        {
            if(ListProducts == null) { return 0; }
                var total = ListProducts.Sum(t => t.TotalProducto);
            return total;
        }

        public class ShopCartTemp
        {
            public int IdProduct { get; set; }
            public int Quantity { get; set; }

            public decimal TotalProducto { get; set; }
        }




    }
}