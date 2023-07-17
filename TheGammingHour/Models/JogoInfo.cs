using Microsoft.CodeAnalysis;
using System.Security.Policy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.ComponentModel.DataAnnotations;
using UTAD.LAB._2022.TheGammingHour.Data;
namespace UTAD.LAB._2022.TheGammingHour.Models;


[Keyless]
public class JogoInfo
{
    
    private readonly TheGammingHourDatabase _context;

    public Jogo Jogo { get; set; }
    public Categoria Categoria { get; set; }
    public Produtora Produtora { get; set; }
    public Plataforma Plataforma { get; set; }
    public List<string> links { get; set; } = new List<string>();
    public IFormFile ImagemCapa { get; set; }
    public List<IFormFile> LinkImages { get; set; } = new List<IFormFile>();

    public JogoInfo()
    {
       
    }

    public JogoInfo(Jogo _jogo, Categoria _categoria, Produtora _produtora, Plataforma _plataforma, List<string> _links, TheGammingHourDatabase context) 
    {
        Jogo = _jogo;
        Categoria = _categoria;
        Produtora = _produtora;
        Plataforma = _plataforma;
        links = _links;
        _context = context;

    }
    
}
