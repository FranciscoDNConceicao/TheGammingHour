using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using UTAD.LAB._2022.TheGammingHour.Data;
using UTAD.LAB._2022.TheGammingHour.Models;

namespace UTAD.LAB._2022.TheGammingHour.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly TheGammingHourDatabase _context;
        public HomeController(ILogger<HomeController> logger, TheGammingHourDatabase context)
        {
            _logger = logger;
            _context = context;
        }

        public IActionResult PaginaDescontos()
        {
            List<JogoInfo> jogoinfoList = new List<JogoInfo>();
            foreach(Jogo jogo in _context.Jogos)
            {
                JogoInfo jogoinfo = new JogoInfo();
                if(jogo.Desconto == true)
                {
                    jogoinfo.Jogo = jogo;
                    PlataformaJogo plat = new PlataformaJogo();
                    
                    plat = _context.PlataformaJogos.FirstOrDefault(m => m.JogoId == jogoinfo.Jogo.Id);
                    jogoinfo.Plataforma = _context.Plataformas.FirstOrDefault(m => m.Id == plat.PlataformaId);

                    string link = jogoinfo.Jogo.Imagem;
                    string tempString = "";
                    String[] strlist = link.Split(".png", StringSplitOptions.RemoveEmptyEntries);
                    foreach (string phrase in strlist)
                    {
                        tempString = "<img class='image-carrousel' src='" + @Url.Content("~" + "/Imagens/Jogos/" + jogoinfo.Jogo.Nome.Replace(" ", "") + "/" + phrase.Replace(" ", "") + ".png") + "' />";
                        jogoinfo.links.Add(tempString);
                    }

                    tempString = "<img  id='ico-plataforma' src='" + @Url.Content("~" + "/Imagens/Plataformas/" + jogoinfo.Plataforma.Nome.Replace(" ", "") + ".png") + "' />";

                    jogoinfo.links.Add(tempString);
                    jogoinfoList.Add(jogoinfo);
                }
            }

            return View(jogoinfoList);
        }
        public IActionResult Index()
        {
            ModelIndex modelIndex = new ModelIndex();
            CultureInfo.DefaultThreadCurrentCulture = new CultureInfo("pt-PT");
            IEnumerable<Jogo> JogosOrdenados = _context.Jogos.OrderByDescending(Jogo => Jogo.Avaliacao);

            int i = 0;
            int numerator = 0;
            foreach (Jogo _game in JogosOrdenados)
            {
                JogoInfo _jogoInfo = new JogoInfo();
                if (numerator == 8)
                {
                    break;
                }
                _jogoInfo.Jogo = _game;
                
                foreach (PlataformaJogo platjog in _context.PlataformaJogos)
                {
                    if (_game.Id == platjog.JogoId)
                    {
                        foreach (Plataforma plat in _context.Plataformas)
                        {
                            if (plat.Id == platjog.PlataformaId)
                            {
                                _jogoInfo.Plataforma = plat;
                            }
                        }
                    }
                    i++;
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
                numerator++;
                modelIndex.JogosInfo.Add(_jogoInfo);
            }
            foreach(Categoria _categ in _context.Categoria)
            {
                modelIndex.Categorias.Add(_categ);
            }

            return View(modelIndex);
        }
        

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}