using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Rendering;
using PoliziaMunicipaleApp.Data;
using PoliziaMunicipaleApp.Models;

namespace PolMunApp.Controllers
{
    public class VerbaleController : Controller
    {
        private readonly ApplicationDbContext _context;

        public VerbaleController(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Index()
        {
            var verbali = _context.Verbali.Include(v => v.Trasgressore).Include(v => v.Violazione);
            return View(await verbali.ToListAsync());
        }
        public IActionResult Create()
        {
            ViewData["TrasgressoreId"] = new SelectList(_context.Trasgressori, "Id", "Nome");
            ViewData["ViolazioneId"] = new SelectList(_context.Violazioni, "Id", "Descrizione");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,TrasgressoreId,ViolazioneId,DataViolazione,Importo,DecurtamentoPunti")] Verbale verbale)
        {
            if (ModelState.IsValid)
            {
                _context.Add(verbale);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["TrasgressoreId"] = new SelectList(_context.Trasgressori, "Id", "Nome", verbale.TrasgressoreID);
            ViewData["ViolazioneId"] = new SelectList(_context.Violazioni, "Id", "Descrizione", verbale.ViolazioneID);
            return View(verbale);
        }
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var verbale = await _context.Verbali.FindAsync(id);
            if (verbale == null)
            {
                return NotFound();
            }
            ViewData["TrasgressoreId"] = new SelectList(_context.Trasgressori, "Id", "Nome", verbale.TrasgressoreID);
            ViewData["ViolazioneId"] = new SelectList(_context.Violazioni, "Id", "Descrizione", verbale.ViolazioneID);
            return View(verbale);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,TrasgressoreId,ViolazioneId,DataViolazione,Importo,DecurtamentoPunti")] Verbale verbale)
        {
            if (id != verbale.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(verbale);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!VerbaleExists(verbale.ID))
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
            ViewData["TrasgressoreId"] = new SelectList(_context.Trasgressori, "Id", "Nome", verbale.TrasgressoreID);
            ViewData["ViolazioneId"] = new SelectList(_context.Violazioni, "Id", "Descrizione", verbale.ViolazioneID);
            return View(verbale);
        } 
       public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var verbale = await _context.Verbali
                .Include(v => v.Trasgressore)
                .Include(v => v.Violazione)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (verbale == null)
            {
                return NotFound();
            }

            return View(verbale);
        }

     
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var verbale = await _context.Verbali.FindAsync(id);
            _context.Verbali.Remove(verbale);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        private bool VerbaleExists(int id)
        {
            return _context.Verbali.Any(e => e.ID == id);
        }
    }
}
