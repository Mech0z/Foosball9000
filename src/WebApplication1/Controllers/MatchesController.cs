using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Mvc;
using Microsoft.AspNet.Mvc.Rendering;
using Microsoft.Data.Entity;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class MatchesController : Controller
    {
        private ApplicationDbContext _context;

        public MatchesController(ApplicationDbContext context)
        {
            _context = context;    
        }

        // GET: Matches
        public async Task<IActionResult> Index()
        {
            return View(await _context.FoosballMatch.ToListAsync());
        }

        // GET: Matches/Details/5
        public async Task<IActionResult> Details(long? id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }

            FoosballMatch foosballMatch = await _context.FoosballMatch.SingleAsync(m => m.ID == id);
            if (foosballMatch == null)
            {
                return HttpNotFound();
            }

            return View(foosballMatch);
        }

        // GET: Matches/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Matches/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(FoosballMatch foosballMatch)
        {
            if (ModelState.IsValid)
            {
                _context.FoosballMatch.Add(foosballMatch);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(foosballMatch);
        }

        // GET: Matches/Edit/5
        public async Task<IActionResult> Edit(long? id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }

            FoosballMatch foosballMatch = await _context.FoosballMatch.SingleAsync(m => m.ID == id);
            if (foosballMatch == null)
            {
                return HttpNotFound();
            }
            return View(foosballMatch);
        }

        // POST: Matches/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(FoosballMatch foosballMatch)
        {
            if (ModelState.IsValid)
            {
                _context.Update(foosballMatch);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(foosballMatch);
        }

        // GET: Matches/Delete/5
        [ActionName("Delete")]
        public async Task<IActionResult> Delete(long? id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }

            FoosballMatch foosballMatch = await _context.FoosballMatch.SingleAsync(m => m.ID == id);
            if (foosballMatch == null)
            {
                return HttpNotFound();
            }

            return View(foosballMatch);
        }

        // POST: Matches/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(long id)
        {
            FoosballMatch foosballMatch = await _context.FoosballMatch.SingleAsync(m => m.ID == id);
            _context.FoosballMatch.Remove(foosballMatch);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }
    }
}
