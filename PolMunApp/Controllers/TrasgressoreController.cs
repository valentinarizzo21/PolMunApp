using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PoliziaMunicipaleApp.Data;
using PoliziaMunicipaleApp.Models;

namespace PolMunApp.Controllers
{
    public class TrasgressoreController : Controller
    {
        private readonly ApplicationDbContext _context;

        public TrasgressoreController(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Index()
        {
            var trasgressori = await _context.Trasgressori.ToListAsync();
            return View(trasgressori);
        }
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Nome,Cognome,Indirizzo,CodiceFiscale, Telefono, Email")] Trasgressore trasgressore)
        {
            if (ModelState.IsValid)
            {
                _context.Add(trasgressore);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(trasgressore);
        }
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var trasgressore = await _context.Trasgressori.FindAsync(id);
            if (trasgressore == null)
            {
                return NotFound();
            }
            return View(trasgressore);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,Nome,Cognome,Indirizzo,CodiceFiscale,Telefono,Email")] Trasgressore trasgressore)
        {
            if (id != trasgressore.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(trasgressore);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TrasgressoreExists(trasgressore.ID))
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
            return View(trasgressore);
        }
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var trasgressore = await _context.Trasgressori
                .FirstOrDefaultAsync(m => m.ID == id);
            if (trasgressore == null)
            {
                return NotFound();
            }

            return View(trasgressore);
        }

        public async Task<IActionResult> DeleteDirect(int id)
        {
            var trasgressore = await _context.Trasgressori.FindAsync(id);

            if (trasgressore == null)
            {
                return NotFound();
            }

            try
            {
                _context.Trasgressori.Remove(trasgressore);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                return BadRequest("Impossibile eliminare il trasgressore.");
            }

            return RedirectToAction(nameof(Index));
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var trasgressore = await _context.Trasgressori.FindAsync(id);
            _context.Trasgressori.Remove(trasgressore);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TrasgressoreExists(int id)
        {
            return _context.Trasgressori.Any(e => e.ID == id);
        }
    }
}
