using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PoliziaMunicipaleApp.Data;
using PoliziaMunicipaleApp.Models;

namespace PolMunApp.Controllers
{
    public class ViolazioneController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ViolazioneController(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Index()
        {
            return View(await _context.Violazioni.ToListAsync());
        }
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Descrizione,Importo,PuntiDecurtati")] Violazione violazione)
        {
            if (ModelState.IsValid)
            {
                _context.Add(violazione);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(violazione);
        }
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var violazione = await _context.Violazioni.FindAsync(id);
            if (violazione == null)
            {
                return NotFound();
            }
            return View(violazione);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,Importo,Descrizione,PuntiDecurtati")] Violazione violazione)
        {
            if (id != violazione.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(violazione);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ViolazioneExists(violazione.ID))
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
            return View(violazione);  
        }
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var violazione = await _context.Violazioni
                .FirstOrDefaultAsync(m => m.ID == id);
            if (violazione == null)
            {
                return NotFound();
            }

            return View(violazione);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var violazione = await _context.Violazioni.FindAsync(id);
            if (violazione != null)
            {
                _context.Violazioni.Remove(violazione);
                await _context.SaveChangesAsync();
            }
            return Json(new { success = true });
        }

        private bool ViolazioneExists(int id)
        {
            return _context.Violazioni.Any(e => e.ID == id);
        }
    }
}
