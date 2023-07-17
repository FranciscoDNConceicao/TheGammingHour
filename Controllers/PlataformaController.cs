using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using UTAD.LAB._2022.TheGammingHour.Data;
using UTAD.LAB._2022.TheGammingHour.Models;

namespace UTAD.LAB._2022.TheGammingHour.Controllers
{
    public class PlataformaController : Controller
    {
        private readonly TheGammingHourDatabase _context;

        public PlataformaController(TheGammingHourDatabase context)
        {
            _context = context;
        }

        // GET: Plataforma
        public async Task<IActionResult> Index()
        {
              return View(await _context.Plataformas.ToListAsync());
        }

        // GET: Plataforma/Details/5
        public async Task<IActionResult> Details(long? id)
        {
            if (id == null || _context.Plataformas == null)
            {
                return NotFound();
            }

            var plataforma = await _context.Plataformas
                .FirstOrDefaultAsync(m => m.Id == id);
            if (plataforma == null)
            {
                return NotFound();
            }

            return View(plataforma);
        }

        // GET: Plataforma/Create
        public IActionResult Create()
        {
            
            return View();
        }

        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("plataforma, platFile")] PlataformaInfo plataformaInfo)
        {
            if (plataformaInfo.plataforma != null && plataformaInfo.platFile != null)
            {
                Plataforma plataforma = new Plataforma();
                string nomeFicheiro = plataformaInfo.plataforma.Nome.Replace(" ", "");
                plataforma.Nome = plataformaInfo.plataforma.Nome;
                string pathFicheiro = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\Imagens\\Plataformas");

                string FileNamePath = Path.Combine(pathFicheiro, plataformaInfo.platFile.FileName);

                using (var stream = new FileStream(FileNamePath, FileMode.Create))
                {
                    plataformaInfo.platFile.CopyTo(stream);
                }

                System.IO.File.Move(FileNamePath, pathFicheiro + "\\" + nomeFicheiro + ".png");

                _context.Add(plataforma);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(plataformaInfo);
        }

        // GET: Plataforma/Edit/5
        public async Task<IActionResult> Edit(long? id)
        {
            if (id == null || _context.Plataformas == null)
            {
                return NotFound();
            }
            var plataforma = await _context.Plataformas.FindAsync(id);

            PlataformaInfo plataformaInfo = new PlataformaInfo();
            plataformaInfo.plataforma = plataforma;
            plataformaInfo.id = id;

            
            
            if (plataformaInfo == null)
            {
                return NotFound();
            }
            return View(plataformaInfo);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long id, [Bind("id, plataforma, platFile")] PlataformaInfo plataformaInfo)
        {
            plataformaInfo.id = id;
            
            if(plataformaInfo != null && plataformaInfo.platFile != null && plataformaInfo.plataforma.Nome != " ") {

                Plataforma plataformaa = new Plataforma();

                string nomeFicheiro = plataformaInfo.plataforma.Nome.Replace(" ", "");
                string pathFicheiro = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\Imagens\\Plataformas");

                string FileNamePath = Path.Combine(pathFicheiro, plataformaInfo.platFile.FileName);

                plataformaa = plataformaInfo.plataforma;
                using (var stream = new FileStream(FileNamePath, FileMode.Create))
                {
                    plataformaInfo.platFile.CopyTo(stream);

                }
                if (System.IO.File.Exists(pathFicheiro + "\\" + nomeFicheiro + ".png"))
                {
                    System.IO.File.Delete(pathFicheiro + "\\" + nomeFicheiro + ".png");
                }

                System.IO.File.Move(FileNamePath, pathFicheiro + "\\" + nomeFicheiro + ".png");
                _context.Update(plataformaa);
                await _context.SaveChangesAsync();
              
               
                return RedirectToAction(nameof(Index));
            }
            return View(plataformaInfo);
        }

        // GET: Plataforma/Delete/5
        public async Task<IActionResult> Delete(long? id)
        {
            if (id == null || _context.Plataformas == null)
            {
                return NotFound();
            }

            var plataforma = await _context.Plataformas
                .FirstOrDefaultAsync(m => m.Id == id);
            if (plataforma == null)
            {
                return NotFound();
            }

            return View(plataforma);
        }

        // POST: Plataforma/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(long id)
        {
            if (_context.Plataformas == null)
            {
                return Problem("Entity set 'TheGammingHourDatabase.Plataformas'  is null.");
            }
            var plataforma = await _context.Plataformas.FindAsync(id);
            if (plataforma != null)
            {

                PlataformaJogo platJogo = new PlataformaJogo();
                platJogo = _context.PlataformaJogos.FirstOrDefault(m => m.JogoId == id);
                _context.PlataformaJogos.Remove(platJogo);
                _context.Plataformas.Remove(plataforma);
                string nomeFicheiro = plataforma.Nome.Replace(" ", "");
                string pathFicheiro = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\Imagens\\Plataformas");
                if (System.IO.File.Exists(pathFicheiro + "\\" + nomeFicheiro + ".png"))
                {
                    System.IO.File.Delete(pathFicheiro + "\\" + nomeFicheiro + ".png");
                }
            }
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PlataformaExists(long id)
        {
          return _context.Plataformas.Any(e => e.Id == id);
        }
    }
}
