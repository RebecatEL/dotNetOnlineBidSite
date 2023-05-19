using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using WebProject2.Data;
using WebProject2.Models;
using WebProject2.ViewModels;

namespace WebProject2.Controllers
{
    public class ShopProductsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ShopProductsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: ShopProducts
        public async Task<IActionResult> Index(string shopProductCategory, string search)
        {
            IQueryable<string> categoryQuery = from c in _context.ShopProduct
                                               orderby c.Category
                                               select c.Category;

            var shopProducts = from p in _context.ShopProduct
                           select p;
            if (!string.IsNullOrEmpty(search))
            {
                shopProducts = shopProducts.Where(s => s.Name!.Contains(search));
            }

            if (!string.IsNullOrEmpty(shopProductCategory))
            {
                shopProducts = shopProducts.Where(p => p.Category == shopProductCategory);
            }

            var shopProductCategoryVM = new ShopProductCategoryViewModel
            {
                Categories = new SelectList(await categoryQuery.Distinct().ToListAsync()),
                ShopProducts = await shopProducts.ToListAsync()
            };

            return View(shopProductCategoryVM);
             
        }

        // GET: ShopProducts/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            // get the current user ID and save to ViewBag
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            ViewBag.UserId = userId;

            if (id == null || _context.ShopProduct == null)
            {
                return NotFound();
            }

            var shopProduct = await _context.ShopProduct
                .FirstOrDefaultAsync(m => m.Id == id);
            if (shopProduct == null)
            {
                return NotFound();
            }

            return View(shopProduct);
        }

        // GET: ShopProducts/Create
        public IActionResult Create()
        {
            /*ViewBag.Categories = new SelectList(Enum.GetValues(typeof(ProductCategory)));*/
            return View();
        }

        // POST: ShopProducts/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,ImageUrl,Name,Description,Category,Price,AuctionStartDate,AuctionEndDate,Quantity,SellerUserId")] ShopProduct shopProduct)
        {
            if (ModelState.IsValid)
            {
                _context.Add(shopProduct);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
           /* ViewBag.Categories = new SelectList(Enum.GetValues(typeof(ProductCategory)));*/
            return View(shopProduct);
        }

        // GET: ShopProducts/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.ShopProduct == null)
            {
                return NotFound();
            }

            var shopProduct = await _context.ShopProduct.FindAsync(id);
            if (shopProduct == null)
            {
                return NotFound();
            }
            return View(shopProduct);
        }

        // POST: ShopProducts/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,ImageUrl,Name,Description,Category,Price,AuctionStartDate,AuctionEndDate,Quantity,SellerUserId")] ShopProduct shopProduct)
        {
            if (id != shopProduct.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(shopProduct);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ShopProductExists(shopProduct.Id))
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
            return View(shopProduct);
        }

        // GET: ShopProducts/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.ShopProduct == null)
            {
                return NotFound();
            }

            var shopProduct = await _context.ShopProduct
                .FirstOrDefaultAsync(m => m.Id == id);
            if (shopProduct == null)
            {
                return NotFound();
            }

            return View(shopProduct);
        }

        // POST: ShopProducts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.ShopProduct == null)
            {
                return Problem("Entity set 'ApplicationDbContext.ShopProduct'  is null.");
            }
            var shopProduct = await _context.ShopProduct.FindAsync(id);
            if (shopProduct != null)
            {
                _context.ShopProduct.Remove(shopProduct);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ShopProductExists(int id)
        {
          return (_context.ShopProduct?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
