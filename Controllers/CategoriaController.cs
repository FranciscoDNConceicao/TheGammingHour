using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using UTAD.LAB._2022.TheGammingHour.Data;
using UTAD.LAB._2022.TheGammingHour.Models;

namespace UTAD.LAB._2022.TheGammingHour.Controllers
{
    public class CategoriaController : Controller
    {
        private readonly TheGammingHourDatabase _context;

        public CategoriaController(TheGammingHourDatabase context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _context.Categoria.ToListAsync());
        }


        public async Task<IActionResult> PaginaPrincipal(long? id)
        {
            CultureInfo.DefaultThreadCurrentCulture = new CultureInfo("pt-PT");
            if (id == null)
            {
                return NotFound();
            }
            ModelIndex _modelIndex = new ModelIndex();
            JogoInfo _jogoInfo = new JogoInfo();

            _modelIndex.Categorias.Add(_context.Categoria.FirstOrDefault(m => m.Id == id));


            foreach (Categoria categ in _context.Categoria)
            {
                if (categ.Id != id)
                {
                    _modelIndex.Categorias.Add(categ);
          
                }
            }
            foreach (CategoriaJogo categJog in _context.CategoriaJogos)
            {
                _jogoInfo = new JogoInfo();
                if (id == categJog.CategoriaId)
                {
                    _jogoInfo.Jogo = _context.Jogos.FirstOrDefault(m => m.Id == categJog.JogoId);
                    _jogoInfo.Categoria = _context.Categoria.FirstOrDefault(m => m.Id == categJog.CategoriaId);

                    foreach (PlataformaJogo platJogo in _context.PlataformaJogos)
                    {
                        if(_jogoInfo.Jogo == null)
                        {
                            break;
                        }
                        if(platJogo.JogoId == _jogoInfo.Jogo.Id)
                        {
                            _jogoInfo.Plataforma = _context.Plataformas.FirstOrDefault(m => m.Id == platJogo.PlataformaId);
                        }
                    }

                    string link = _jogoInfo.Jogo.Imagem;
                    string tempString = "";
                    String[] strlist = link.Split(".png", StringSplitOptions.RemoveEmptyEntries);
                    foreach (string phrase in strlist)
                    {
                        tempString = "<img src='" + @Url.Content("~" + "/Imagens/Jogos/" + _jogoInfo.Jogo.Nome.Replace(" ", "") + "/" + phrase.Replace(" ", "") + ".png") + "' />";
                        _jogoInfo.links.Add(tempString);
                    }
                    tempString = "<img id='ico-plataforma' src='" + @Url.Content("~" + "/Imagens/Plataformas/" + _jogoInfo.Plataforma.Nome.Replace(" ", "") + ".png") + "' />";

                    _jogoInfo.links.Add(tempString);
                    _modelIndex.JogosInfo.Add(_jogoInfo);
                }
            }
            return View(_modelIndex);
        }

        public async Task<IActionResult> Details(long? id)
        {
            if (id == null || _context.Categoria == null)
            {
                return NotFound();
            }

            var categoria = await _context.Categoria
                .FirstOrDefaultAsync(m => m.Id == id);
            if (categoria == null)
            {
                return NotFound();
            }

            return View(categoria);
        }

        public IActionResult Create()
        {
            return View();
        }

        // POST: Categoria/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Nome")] Categoria categoria)
        {
            if (ModelState.IsValid)
            {
                _context.Add(categoria);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(categoria);
        }

        // GET: Categoria/Edit/5
        public async Task<IActionResult> Edit(long? id)
        {
            if (id == null || _context.Categoria == null)
            {
                return NotFound();
            }

            var categoria = await _context.Categoria.FindAsync(id);
            if (categoria == null)
            {
                return NotFound();
            }
            return View(categoria);
        }

        // POST: Categoria/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long id, [Bind("Id,Nome")] Categoria categoria)
        {
            if (id != categoria.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(categoria);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CategoriaExists(categoria.Id))
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
            return View(categoria);
        }

        // GET: Categoria/Delete/5
        public async Task<IActionResult> Delete(long? id)
        {
            if (id == null || _context.Categoria == null)
            {
                return NotFound();
            }

            var categoria = await _context.Categoria
                .FirstOrDefaultAsync(m => m.Id == id);
            if (categoria == null)
            {
                return NotFound();
            }

            return View(categoria);
        }

        // POST: Categoria/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(long id)
        {
            if (_context.Categoria == null)
            {
                return Problem("Entity set 'TheGammingHourDatabase.Categoria'  is null.");
            }
            var categoria = await _context.Categoria.FindAsync(id);
            if (categoria != null)
            {
                CategoriaJogo categJogo = new CategoriaJogo();
                categJogo = _context.CategoriaJogos.FirstOrDefault(m => m.CategoriaId == id);
                _context.CategoriaJogos.Remove(categJogo);
                _context.Categoria.Remove(categoria);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CategoriaExists(long id)
        {
          return _context.Categoria.Any(e => e.Id == id);
        }
    }
}
