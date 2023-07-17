using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Web;
using UTAD.LAB._2022.TheGammingHour.Data;
using UTAD.LAB._2022.TheGammingHour.Models;
using Microsoft.AspNetCore.Authorization;
using System.Globalization;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore.Query.Internal;
using Microsoft.AspNetCore.Razor.Language.Extensions;

namespace UTAD.LAB._2022.TheGammingHour.Controllers
{
    public class JogoController : Controller
    {
        private readonly TheGammingHourDatabase _context;
        private readonly IHostEnvironment _he;

        public IActionResult Biblioteca()
        {
            if (!User.Identity.IsAuthenticated)
            {
                return NotFound();
            }
            
            CompraJogoInfo compraJogo = new CompraJogoInfo();
            Utilizador user = new Utilizador();
            user = _context.Utilizadors.FirstOrDefault(m => m.Username == User.Identity.Name);
            foreach (CompraJogo compra in _context.CompraJogos) { 
                if(compra.UtilizadorId == user.Id)
                {
                    JogoInfo jogoInfoBought = new JogoInfo();
                    PlataformaJogo plat = new PlataformaJogo();
                    compraJogo.comprajogo.Add(compra);

                    foreach (Jogo jogo in _context.Jogos)
                    {
                        if(jogo.Id == compra.JogoId)
                        {
                            jogoInfoBought.Jogo = jogo;
                        }
                    }
                    plat = _context.PlataformaJogos.FirstOrDefault(m => m.JogoId == jogoInfoBought.Jogo.Id);
                    jogoInfoBought.Plataforma = _context.Plataformas.FirstOrDefault(m => m.Id == plat.PlataformaId);

                    string link = jogoInfoBought.Jogo.Imagem;
                    string tempString = "";
                    String[] strlist = link.Split(".png", StringSplitOptions.RemoveEmptyEntries);
                    foreach (string phrase in strlist)
                    {
                        tempString = "<img class='image-carrousel' src='" + @Url.Content("~" + "/Imagens/Jogos/" + jogoInfoBought.Jogo.Nome.Replace(" ", "") + "/" + phrase.Replace(" ", "") + ".png") + "' />";
                        jogoInfoBought.links.Add(tempString);
                    }

                    tempString = "<img  id='ico-plataforma' src='" + @Url.Content("~" + "/Imagens/Plataformas/" + jogoInfoBought.Plataforma.Nome.Replace(" ", "") + ".png") + "' />";

                    jogoInfoBought.links.Add(tempString);
                    compraJogo.jogos.Add(jogoInfoBought);
                }

            }

            return View(compraJogo);
        }

        public JogoController(TheGammingHourDatabase context, IHostEnvironment e)
        {
            _context = context;
            _he = e;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _context.Jogos.ToListAsync());
        }


        public IActionResult CompraJogo(long id)
        {
            if (User.Identity.IsAuthenticated)
            {
                Jogo jogoBought = new Jogo();
                Utilizador UserBought = new Utilizador();
                Pagamento Pagamento = new Pagamento();
                UserBought = _context.Utilizadors.FirstOrDefault(m => m.Username == User.Identity.Name);
                jogoBought = _context.Jogos.FirstOrDefault(m => m.Id == id);
                if (UserBought == null)
                {
                    return NotFound();
                }
                foreach(CompraJogo compra in _context.CompraJogos)
                {
                    if(compra.JogoId == id && compra.UtilizadorId == UserBought.Id)
                    {
                        return RedirectToAction("Index", "Home");
                    }
                }
                Pagamento.UtilizadorId = UserBought.Id;
                Pagamento.CodigoPostal = "0000-000";
                Pagamento.Morada = "ooooooooooo";
                Pagamento.Nif = "AAAAAAAA";
                _context.Pagamentos.Add(Pagamento);
                _context.SaveChanges();
                
                
                CompraJogo compraJogo = new CompraJogo();
                compraJogo.Data = DateTime.Today;
                if (jogoBought.Desconto)
                {
                    jogoBought.Preco = Convert.ToDouble(Math.Round(Convert.ToDecimal(jogoBought.Preco * jogoBought.PercentagemDesconto), 2));
                }
                compraJogo.Preco = jogoBought.Preco;
                compraJogo.UtilizadorId = UserBought.Id;
                compraJogo.JogoId = jogoBought.Id;
                compraJogo.PagamentoId = _context.Pagamentos.FirstOrDefault(m => m.UtilizadorId == UserBought.Id).Id;

                _context.CompraJogos.Add(compraJogo);
                _context.SaveChanges();

            }
            return RedirectToPage("/Account/Login", new { area = "Identity" });
        }


        [Route("PaginaPrincipal/{id:long}")]
        public async Task<IActionResult> PaginaPrincipal(long? id)
        {
            CultureInfo.DefaultThreadCurrentCulture = new CultureInfo("pt-PT");
            if (id == null || _context.Jogos == null)
            {
                return NotFound();
            }

            var jogo = await _context.Jogos.FirstOrDefaultAsync(m => m.Id == id);
            if (jogo == null)
            {
                return NotFound();
            }
            JogoInfo jogoModel = new JogoInfo();
            jogoModel.Jogo = jogo;

            foreach (ProdutoraJogo prodjog in _context.ProdutoraJogos)
            {
                if (jogo.Id == prodjog.JogoId)
                {
                    jogoModel.Produtora = await _context.Produtoras.FirstOrDefaultAsync(m => m.Id == prodjog.ProdutoraId);
                }
            }
            foreach (CategoriaJogo catjog in _context.CategoriaJogos)
            {
                if (jogo.Id == catjog.JogoId)
                {
                    jogoModel.Categoria = await _context.Categoria.FirstOrDefaultAsync(m => m.Id == catjog.CategoriaId);
                }
            }
            foreach (PlataformaJogo platjog in _context.PlataformaJogos)
            {
                if (jogo.Id == platjog.JogoId)
                {
                    jogoModel.Plataforma = await _context.Plataformas.FirstOrDefaultAsync(m => m.Id == platjog.PlataformaId);
                }
            }
            string link = jogoModel.Jogo.Imagem;
            string tempString = "";
            String[] strlist = link.Split(".png", StringSplitOptions.RemoveEmptyEntries);
            foreach (string phrase in strlist)
            {
                tempString = "<img class='image-carrousel' src='" + @Url.Content("~" + "/Imagens/Jogos/" + jogoModel.Jogo.Nome.Replace(" ", "") + "/" + phrase.Replace(" ", "") + ".png") + "' />";
                jogoModel.links.Add(tempString);
            }

            tempString = "<img src='" + @Url.Content("~" + "/Imagens/Produtora/" + jogoModel.Produtora.Nome.Replace(" ", "") + ".png") + "' />";

            jogoModel.links.Add(tempString);
            return View(jogoModel);
        }


        public async Task<IActionResult> ListaClassificação()
        {
            ModelIndex ModelIndexOrdenado = new ModelIndex();
            CultureInfo.DefaultThreadCurrentCulture = new CultureInfo("pt-PT");
            IEnumerable<Jogo> JogosOrdenados = _context.Jogos.OrderByDescending(Jogo => Jogo.Avaliacao);

            foreach (Jogo _jogo in JogosOrdenados)
            {
                JogoInfo jogoInfo = new JogoInfo();

                jogoInfo.Jogo = _jogo;
                string link = jogoInfo.Jogo.Imagem;
                string tempString = "";
                String[] strlist = link.Split(".png", StringSplitOptions.RemoveEmptyEntries);
                foreach (string phrase in strlist)
                {
                    tempString = "<img src='" + @Url.Content("~" + "/Imagens/Jogos/" + jogoInfo.Jogo.Nome.Replace(" ", "") + "/" + phrase.Replace(" ", "") + ".png") + "' />";
                    jogoInfo.links.Add(tempString);
                }
                foreach (CategoriaJogo catjog in _context.CategoriaJogos)
                {
                    if (_jogo.Id == catjog.JogoId)
                    {
                        jogoInfo.Categoria = await _context.Categoria.FirstOrDefaultAsync(m => m.Id == catjog.CategoriaId);
                    }
                }
                ModelIndexOrdenado.JogosInfo.Add(jogoInfo);
            }

            return View(ModelIndexOrdenado);
        }


        public async Task<IActionResult> Details(long? id)
        {

            if (id == null || _context.Jogos == null)
            {
                return NotFound();
            }

            var jogo = await _context.Jogos
                .FirstOrDefaultAsync(m => m.Id == id);
            if (jogo == null)
            {
                return NotFound();
            }

            return View(jogo);
        }


        public IActionResult Search(string searchName) {
            if (!String.IsNullOrEmpty(searchName))
            {
                foreach (Jogo _jogos in _context.Jogos)
                {
                    if (_jogos.Nome == searchName || _jogos.Nome.ToLower() == searchName || _jogos.Nome.ToUpper() == searchName || _jogos.Nome.Replace(" ", "") == searchName || _jogos.Nome.Replace(" ", "").ToUpper() == searchName || _jogos.Nome.Replace(" ", "").ToLower() == searchName)
                    {
                        return RedirectToAction("PaginaPrincipal", new { id = _jogos.Id });
                    }
                }
            }
            return RedirectToAction("Index", "Home");
        }

        [Authorize(Roles = "Admnistrador")]
        public IActionResult Create()
        {

            ViewBag.Categoria = _context.Categoria.Select(c => new SelectListItem()
            { Text = c.Nome, Value = c.Id.ToString() }).ToList();

            ViewBag.Plataforma = _context.Plataformas.Select(c => new SelectListItem()
            { Text = c.Nome, Value = c.Id.ToString() }).ToList();

            ViewBag.Produtora = _context.Produtoras.Select(c => new SelectListItem()
            { Text = c.Nome, Value = c.Id.ToString() }).ToList();

            CultureInfo.DefaultThreadCurrentCulture = new CultureInfo("en-US");

            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admnistrador")]
        public async Task<IActionResult> Create([Bind("Jogo, Categoria, Plataforma, Produtora, ImagemCapa, LinkImages")] JogoInfo JogoInformacao)
        {
            if (JogoInformacao.LinkImages.Count == 4 && JogoInformacao.Jogo.Nome != null && JogoInformacao.ImagemCapa != null)
            {
                Jogo jogoTemp = new Jogo();
                CategoriaJogo catJog = new CategoriaJogo();
                PlataformaJogo platJog = new PlataformaJogo();
                ProdutoraJogo prodJog = new ProdutoraJogo();

                jogoTemp.Nome = JogoInformacao.Jogo.Nome;
                jogoTemp.Avaliacao = JogoInformacao.Jogo.Avaliacao;
                jogoTemp.Preco = JogoInformacao.Jogo.Preco;
                jogoTemp.Data = JogoInformacao.Jogo.Data;
                jogoTemp.Pegi = JogoInformacao.Jogo.Pegi;
                jogoTemp.PercentagemDesconto = JogoInformacao.Jogo.PercentagemDesconto;
                jogoTemp.Desconto = JogoInformacao.Jogo.Desconto;
                jogoTemp.Descricao = JogoInformacao.Jogo.Descricao;

                string fileName = "";
                int i = 0;
                string nomeFicheiro = JogoInformacao.Jogo.Nome.Replace(" ", "");
                string pathFicheiro = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\Imagens\\Jogos");
                string pathName = Path.Combine(pathFicheiro, nomeFicheiro);
                Directory.CreateDirectory(pathName);

                string FileCapaNamePath = Path.Combine(pathName, JogoInformacao.ImagemCapa.FileName);
                using (var streamteste = new FileStream(FileCapaNamePath, FileMode.Create))
                {
                    JogoInformacao.ImagemCapa.CopyTo(streamteste);
                }
                fileName = nomeFicheiro + "-cap.png";
                jogoTemp.Imagem += fileName;
                System.IO.File.Move(FileCapaNamePath, pathName + "\\" + fileName);
                foreach (var file in JogoInformacao.LinkImages)
                {

                    string FileNamePath = Path.Combine(pathName, file.FileName);

                    using (var stream = new FileStream(FileNamePath, FileMode.Create))
                    {
                        file.CopyTo(stream);
                      
                        fileName = nomeFicheiro + i.ToString() + ".png";
                        jogoTemp.Imagem += fileName;
                    }
                    System.IO.File.Move(FileNamePath, pathName + "\\" + fileName);
                    i++;
                }
                _context.Jogos.Add(jogoTemp);
                _context.SaveChanges();

                var jogoId = await _context.Jogos.FirstOrDefaultAsync(m => m.Nome == jogoTemp.Nome);

                catJog.CategoriaId = JogoInformacao.Categoria.Id;
                catJog.JogoId = jogoId.Id;

                platJog.PlataformaId = JogoInformacao.Plataforma.Id;
                platJog.JogoId = jogoId.Id;

                prodJog.ProdutoraId = JogoInformacao.Produtora.Id;
                prodJog.JogoId = jogoId.Id;

                _context.CategoriaJogos.Add(catJog);
                _context.PlataformaJogos.Add(platJog);
                _context.ProdutoraJogos.Add(prodJog);
                _context.SaveChanges();

                CultureInfo.DefaultThreadCurrentCulture = new CultureInfo("pt-PT");
                return RedirectToAction(nameof(Index));
            }
            CultureInfo.DefaultThreadCurrentCulture = new CultureInfo("pt-PT");
            return View(JogoInformacao);
        }


        [Authorize(Roles = "Admnistrador")]
        public async Task<IActionResult> Edit(long? id)
        {

            if (id == null || _context.Jogos == null)
            {
                return NotFound();
            }
            JogoInfo jogoInfo = new JogoInfo();

            var jogo = await _context.Jogos.FindAsync(id);
            jogoInfo.Jogo = jogo;

            ProdutoraJogo prodJog = new ProdutoraJogo();
            prodJog = _context.ProdutoraJogos.FirstOrDefault(m => m.JogoId == id);
            jogoInfo.Produtora = _context.Produtoras.FirstOrDefault(m => m.Id == prodJog.ProdutoraId);

            CategoriaJogo categJog = new CategoriaJogo();
            categJog = _context.CategoriaJogos.FirstOrDefault(m => m.JogoId == id);
            jogoInfo.Categoria = _context.Categoria.FirstOrDefault(m => m.Id == categJog.CategoriaId);

            PlataformaJogo platJog = new PlataformaJogo();
            platJog = _context.PlataformaJogos.FirstOrDefault(m => m.JogoId == id);
            jogoInfo.Plataforma = _context.Plataformas.FirstOrDefault(m => m.Id == platJog.PlataformaId);
            if (jogo == null)
            {
                return NotFound();
            }
            ViewBag.Categoria = _context.Categoria.Select(c => new SelectListItem()
            { Text = c.Nome, Value = c.Id.ToString() }).ToList();

            ViewBag.Plataforma = _context.Plataformas.Select(c => new SelectListItem()
            { Text = c.Nome, Value = c.Id.ToString() }).ToList();

            ViewBag.Produtora = _context.Produtoras.Select(c => new SelectListItem()
            { Text = c.Nome, Value = c.Id.ToString() }).ToList();
            CultureInfo.DefaultThreadCurrentCulture = new CultureInfo("en-US");
            return View(jogoInfo);
        }


        [HttpPost]
        [Authorize(Roles = "Admnistrador")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long id, [Bind("Jogo, Categoria, Produtora, Plataforma")] JogoInfo JogoInformacao)
        {
            if (JogoInformacao != null && id != null && JogoInformacao.Jogo.Nome != null) {
                Jogo jogoTemp = new Jogo();
                CategoriaJogo catJog = new CategoriaJogo();
                PlataformaJogo platJog = new PlataformaJogo();
                ProdutoraJogo prodJog = new ProdutoraJogo();

                jogoTemp.Nome = JogoInformacao.Jogo.Nome;
                jogoTemp.Avaliacao = JogoInformacao.Jogo.Avaliacao;
                jogoTemp.Preco = JogoInformacao.Jogo.Preco;
                jogoTemp.Data = JogoInformacao.Jogo.Data;
                jogoTemp.Pegi = JogoInformacao.Jogo.Pegi;
                jogoTemp.PercentagemDesconto = JogoInformacao.Jogo.PercentagemDesconto;
                jogoTemp.Desconto = JogoInformacao.Jogo.Desconto;
                jogoTemp.Descricao = JogoInformacao.Jogo.Descricao;
                jogoTemp.Imagem = "";
                for(int i = 0; i<5; i++)
                {
                    if (i == 0)
                    {
                        jogoTemp.Imagem += jogoTemp.Nome.Replace(" ", "") +"-cap.png";
                    }
                    else
                    {
                        jogoTemp.Imagem += jogoTemp.Nome.Replace(" ", "") + i.ToString() + ".png";
                    }
                }

                jogoTemp.Id = id;
                _context.Jogos.Update(jogoTemp);
                _context.SaveChanges();

                var jogoId = await _context.Jogos.FirstOrDefaultAsync(m => m.Nome == jogoTemp.Nome);

                catJog = _context.CategoriaJogos.FirstOrDefault(m => m.JogoId == id);
                catJog.CategoriaId = JogoInformacao.Categoria.Id;
                

                platJog = _context.PlataformaJogos.FirstOrDefault(m => m.JogoId == id);
                platJog.PlataformaId = JogoInformacao.Plataforma.Id;


                prodJog = _context.ProdutoraJogos.FirstOrDefault(m => m.JogoId == id);
                prodJog.ProdutoraId = JogoInformacao.Produtora.Id;

                _context.CategoriaJogos.Update(catJog);
                _context.PlataformaJogos.Update(platJog);
                _context.ProdutoraJogos.Update(prodJog);
                _context.SaveChanges();

                CultureInfo.DefaultThreadCurrentCulture = new CultureInfo("pt-PT");

                return RedirectToAction(nameof(Index));
            }
            CultureInfo.DefaultThreadCurrentCulture = new CultureInfo("pt-PT");
            return View(JogoInformacao);
        }
        // GET: Jogo/Delete/5
        [Authorize(Roles = "Admnistrador")]
        public async Task<IActionResult> Delete(long? id)
        {
            if (id == null || _context.Jogos == null)
            {
                return NotFound();
            }

            var jogo = await _context.Jogos
                .FirstOrDefaultAsync(m => m.Id == id);
            if (jogo == null)
            {
                return NotFound();
            }

            return View(jogo);
        }

        // POST: Jogo/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admnistrador")]
        public async Task<IActionResult> DeleteConfirmed(long id)
        {
            if (_context.Jogos == null)
            {
                return Problem("Entity set 'TheGammingHourDatabase.Jogos'  is null.");
            }
            var jogo = await _context.Jogos.FindAsync(id);
            if (jogo != null)
            {
                PlataformaJogo platJogo = new PlataformaJogo();
                platJogo = _context.PlataformaJogos.FirstOrDefault(m => m.JogoId == id);
                _context.PlataformaJogos.Remove(platJogo);

                CategoriaJogo categJogo = new CategoriaJogo();
                categJogo = _context.CategoriaJogos.FirstOrDefault(m => m.JogoId == id);
                _context.CategoriaJogos.Remove(categJogo);

                ProdutoraJogo prodJog  = new ProdutoraJogo();
                prodJog = _context.ProdutoraJogos.FirstOrDefault(m => m.JogoId == id);
                _context.ProdutoraJogos.Remove(prodJog);

                Directory.Delete(Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\Imagens\\Jogos\\" + jogo.Nome.Replace(" ", "")), true);

                _context.Jogos.Remove(jogo);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool JogoExists(long id)
        {
          return _context.Jogos.Any(e => e.Id == id);
        }
    }
}
