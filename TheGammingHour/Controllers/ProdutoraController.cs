using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.CodeAnalysis;
using Microsoft.EntityFrameworkCore;
using UTAD.LAB._2022.TheGammingHour.Data;
using UTAD.LAB._2022.TheGammingHour.Models;

namespace UTAD.LAB._2022.TheGammingHour.Controllers
{
    public class ProdutoraController : Controller
    {
        private readonly TheGammingHourDatabase _context;

        public ProdutoraController(TheGammingHourDatabase context)
        {
            _context = context;
        }

        // GET: Produtora
        public async Task<IActionResult> Index()
        {
              return View(await _context.Produtoras.ToListAsync());
        }

        // GET: Produtora/Details/5
        public async Task<IActionResult> Details(long? id)
        {
            if (id == null || _context.Produtoras == null)
            {
                return NotFound();
            }

            var produtora = await _context.Produtoras
                .FirstOrDefaultAsync(m => m.Id == id);
            if (produtora == null)
            {
                return NotFound();
            }

            return View(produtora);
        }

        // GET: Produtora/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Produtora/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("produtora, platFile")] ProdutoraInfo produtoraInfo)
        {
            if (produtoraInfo.produtora != null && produtoraInfo.platFile != null)
            {
                Produtora produtora = new Produtora();
                string nomeFicheiro = produtoraInfo.produtora.Nome.Replace(" ", "");
                produtora.Nome = produtoraInfo.produtora.Nome;
                string pathFicheiro = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\Imagens\\Produtora");

                string FileNamePath = Path.Combine(pathFicheiro, produtoraInfo.platFile.FileName);

                using (var stream = new FileStream(FileNamePath, FileMode.Create))
                {
                    produtoraInfo.platFile.CopyTo(stream);
                }

                System.IO.File.Move(FileNamePath, pathFicheiro + "\\" + nomeFicheiro + ".png");

                _context.Add(produtora);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(produtoraInfo);
        }

        // GET: Produtora/Edit/5
        public async Task<IActionResult> Edit(long? id)
        {
            if (id == null || _context.Produtoras == null)
            {
                return NotFound();
            }
            var produtoras = await _context.Produtoras.FindAsync(id);

            ProdutoraInfo produtoraInfo = new ProdutoraInfo();
            produtoraInfo.produtora = produtoras;
            produtoraInfo.id = id;



            if (produtoraInfo == null)
            {
                return NotFound();
            }
            return View(produtoraInfo);
        }

        // POST: Produtora/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long id, [Bind("id, produtora, platFile")] ProdutoraInfo produtoraInfo)
        {
            produtoraInfo.id = id;

            if (produtoraInfo != null && produtoraInfo.platFile != null && produtoraInfo.produtora.Nome != " ")
            {

                Produtora produtoraa = new Produtora();

                string nomeFicheiro = produtoraInfo.produtora.Nome.Replace(" ", "");
                string pathFicheiro = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\Imagens\\Produtora");

                string FileNamePath = Path.Combine(pathFicheiro, produtoraInfo.platFile.FileName);

                produtoraa = produtoraInfo.produtora;
                using (var stream = new FileStream(FileNamePath, FileMode.Create))
                {
                    produtoraInfo.platFile.CopyTo(stream);

                }
                if (System.IO.File.Exists(pathFicheiro + "\\" + nomeFicheiro + ".png"))
                {
                    System.IO.File.Delete(pathFicheiro + "\\" + nomeFicheiro + ".png");
                }

                System.IO.File.Move(FileNamePath, pathFicheiro + "\\" + nomeFicheiro + ".png");
                _context.Update(produtoraa);
                await _context.SaveChangesAsync();


                return RedirectToAction(nameof(Index));
            }
            return View(produtoraInfo);
        }

        // GET: Produtora/Delete/5
        public async Task<IActionResult> Delete(long? id)
        {
            if (id == null || _context.Produtoras == null)
            {
                return NotFound();
            }

            var produtora = await _context.Produtoras
                .FirstOrDefaultAsync(m => m.Id == id);
            if (produtora == null)
            {
                return NotFound();
            }

            return View(produtora);
        }

        // POST: Produtora/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(long id)
        {
            if (_context.Produtoras == null)
            {
                return Problem("Entity set 'TheGammingHourDatabase.Produtoras'  is null.");
            }
            var produtora = await _context.Produtoras.FindAsync(id);
            if (produtora != null)
            {
                ProdutoraJogo prodJog = new ProdutoraJogo();
                prodJog = _context.ProdutoraJogos.FirstOrDefault(m => m.ProdutoraId == id);
                _context.ProdutoraJogos.Remove(prodJog);
                _context.Produtoras.Remove(produtora);
            }
            string nomeFicheiro = produtora.Nome.Replace(" ", "");
            string pathFicheiro = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\Imagens\\Produtora");
            if (System.IO.File.Exists(pathFicheiro + "\\" + nomeFicheiro + ".png"))
            {
                System.IO.File.Delete(pathFicheiro + "\\" + nomeFicheiro + ".png");
            }
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProdutoraExists(long id)
        {
          return _context.Produtoras.Any(e => e.Id == id);
        }
    }
}
