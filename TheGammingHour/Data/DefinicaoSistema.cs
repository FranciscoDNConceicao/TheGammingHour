using Microsoft.AspNetCore.Http.Features;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.VisualBasic.Syntax;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization.Formatters;
using UTAD.LAB._2022.TheGammingHour.Models;

namespace UTAD.LAB._2022.TheGammingHour.Data
{
    public class DefinicaoSistema
    {
        public readonly TheGammingHourDatabase _context;

        public DefinicaoSistema(TheGammingHourDatabase context)
        {
            _context = context;
        }
        public void CreateGroupsMenu()
        {
            _context.Database.EnsureCreated();

            if (_context.Grupos.Any() || _context.Menus.Any())
            {
                return;
            }

            var _grupos = new Grupo[]
            {
                new Grupo{

                    Nome = "Cliente"

                },
                new Grupo
                {

                    Nome = "Admnistrador",
                }
            };
            var _menus = new Menu[]
            {
                new Menu
                {
                    Caption = "Seja bem-vindo ao nosso site",
                    Create = false,
                    Edit = false,
                    Update = false,
                    Tooltip = "Cliente",
                    View = true,
                    Key = 1,
                },
                new Menu
                {
                    Caption = "Administrador Settings",
                    Create = true,
                    Edit = true,
                    Update = true,
                    Tooltip = "Admin",
                    View = true,
                    Key = 2
                }
            };
            
            _context.Menus.AddRange(_menus);
            _context.Grupos.AddRange(_grupos);

            _context.SaveChanges();

            foreach (var g in _context.Grupos)
            {
                foreach (var m in _context.Menus) {
                    if (g.Nome == "Cliente" && m.Key == 1)
                    {
                        GrupoMenu grupoMen = new GrupoMenu();
                        grupoMen.MenuId = m.Id;
                        grupoMen.GrupoId = g.Id;
                        _context.GrupoMenus.Add(grupoMen);
                    }
                    if(g.Nome == "Admnistrador" && m.Key == 2)
                    {
                        GrupoMenu grupoMen2 = new GrupoMenu();
                        grupoMen2.MenuId = m.Id;
                        grupoMen2.GrupoId = g.Id;
                        _context.GrupoMenus.Add(grupoMen2);
                    }
                }
            }
            _context.SaveChanges();
        }
        
        public void CreateCategories()
        {
            _context.Database.EnsureCreated();

            if (_context.Categoria.Any())
            {
                return;
            }

            var _categorias = new Categoria[]
            {
                new Categoria{Nome = "Ação"},
                new Categoria{Nome = "Corrida"},
                new Categoria{Nome = "Desporto"},
                new Categoria{Nome = "RPG"},
                new Categoria{Nome = "Estratégia"},
                new Categoria{Nome = "Indie"},
                new Categoria{Nome = "MOBA"},
                new Categoria{Nome = "Simulação"},
                new Categoria{Nome = "Plataforma"},
            };
            _context.Categoria.AddRange(_categorias);
            _context.SaveChanges();
        }
        public void CreateDefaultGames(){
            _context.Database.EnsureCreated();

            if (_context.Jogos.Any())
            {
                return;
            }

            var _jogos = new Jogo[]
            {
                new Jogo
                {
                    Nome = "Grand Theft Auto V",
                    Data = new DateTime(2013, 7, 24),
                    Preco = 19.99,
                    Pegi = 18,
                    Descricao = "Grand Theft Auto V é um jogo eletrônico de ação-aventura desenvolvido pela Rockstar North e publicado pela Rockstar Games. É o sétimo título principal da série Grand Theft Auto e foi lançado originalmente em 17 de setembro de 2013 para PlayStation 3 e Xbox 360, com remasterizações lançadas em 2014 para PlayStation 4 e Xbox One, em 2015 para Microsoft Windows e em 2022 para PlayStation 5 e Xbox Series X/S. O jogo se passa no estado ficcional de San Andreas, com a história da campanha um jogador seguindo três criminosos e seus esforços para realizarem assaltos sob a pressão de uma agência governamental. O mundo aberto permite que os jogadores naveguem livremente pelas áreas rurais e urbanas de San Andreas. ",
                    Imagem = "GrandTheftAutoV-cap.pngGrandTheftAutoV1.pngGrandTheftAutoV2.pngGrandTheftAutoV3.pngGrandTheftAutoV4.png",
                    Desconto = false,
                    Avaliacao = 0
                },
                new Jogo
                {
                    Nome = "Minecraft",
                    Data = new DateTime(2011, 11, 18),
                    Preco = 18.99,
                    Pegi = 3,
                    Descricao = "Minecraft é um jogo eletrônico dos gêneros sandbox e sobrevivência que não possui objetivos específicos a serem alcançados, permitindo aos jogadores uma grande liberdade de escolha de como jogá-lo. No entanto, existe um sistema de conquistas,[20] conhecido como progressos na edição Java. A jogabilidade é apresentada numa perspectiva em primeira pessoa por padrão, mas os jogadores têm a opção de selecionarem uma visão em terceira pessoa.",
                    Imagem = "Minecraft-cap.pngMinecraft1.pngMinecraft2.pngMinecraft3.pngMinecraft4.png",
                    Desconto = false,
                    Avaliacao = 0
                }

            };
            _context.Jogos.AddRange(_jogos);
            _context.SaveChanges();
            foreach (var g in _context.Jogos)
            {
                foreach (var m in _context.Categoria)
                {
                    if (g.Nome == "Grand Theft Auto V" && m.Nome == "Ação")
                    {
                        CategoriaJogo _categJogo = new CategoriaJogo();
                        _categJogo.CategoriaId = m.Id;
                        _categJogo.JogoId = g.Id;
                        _context.CategoriaJogos.Add(_categJogo);
                    }
                    if (g.Nome == "Minecraft" && m.Nome == "Plataforma")
                    {
                        CategoriaJogo _categJogo = new CategoriaJogo();
                        _categJogo.CategoriaId = m.Id;
                        _categJogo.JogoId = g.Id;
                        _context.CategoriaJogos.Add(_categJogo);
                    }
                }
            }
            _context.SaveChanges();
        }
        
        public void AddProdutoraPlataform()
        {
            _context.Database.EnsureCreated();

            if (_context.Plataformas.Any() || _context.Produtoras.Any())
            {
                return;
            }

            var _plataforma = new Plataforma[]
            {
                new Plataforma{Nome = "Playstation" },
                new Plataforma{Nome = "Steam"},
                new Plataforma{Nome = "Epic Games" },
                new Plataforma{Nome = "Nintendo"},
                new Plataforma{Nome = "Steam" },
                new Plataforma{Nome = "Xbox"}
            };
            var _produtora = new Produtora[]
           {
                new Produtora{Nome = "EA Sports" },
                new Produtora{Nome = "Rockstar Games"},
                new Produtora{Nome = "Microsoft" },
                new Produtora{Nome = "CD Project"},
                new Produtora{Nome = "Ubisoft" },
                new Produtora{Nome = "Naughty Dog"},
                new Produtora{Nome = "Activision" },
                new Produtora{Nome = "Santa Monica Studio"},
                new Produtora{Nome = "Isomanic Games" },
                new Produtora{Nome = "Nintendo"},
                new Produtora{Nome = "Rocksteady Studios" },
                new Produtora{Nome = "Blizzard"}
           };
            _context.Plataformas.AddRange(_plataforma);
            _context.Produtoras.AddRange(_produtora);
            _context.SaveChanges();

            foreach (var g in _context.Jogos)
            {
                foreach (var m in _context.Plataformas)
                {
                    if (g.Nome == "Grand Theft Auto V" && m.Nome == "Playstation")
                    {
                        PlataformaJogo _platJogo = new PlataformaJogo();
                        _platJogo.PlataformaId = m.Id;
                        _platJogo.JogoId = g.Id;
                        _context.PlataformaJogos.Add(_platJogo);
                    }
                    if (g.Nome == "Minecraft" && m.Nome == "Nintendo")
                    {
                        PlataformaJogo _platJogo = new PlataformaJogo();
                        _platJogo.PlataformaId = m.Id;
                        _platJogo.JogoId = g.Id;
                        _context.PlataformaJogos.Add(_platJogo);
                    }
                }
            }
            foreach (var g in _context.Jogos)
            {
                foreach (var m in _context.Produtoras)
                {
                    if (g.Nome == "Grand Theft Auto V" && m.Nome == "Rockstar Games")
                    {
                        ProdutoraJogo _prodJogo = new ProdutoraJogo();
                        _prodJogo.ProdutoraId = m.Id;
                        _prodJogo.JogoId = g.Id;
                        _context.ProdutoraJogos.Add(_prodJogo);
                    }
                    if (g.Nome == "Minecraft" && m.Nome == "Microsoft")
                    {
                        ProdutoraJogo _prodJogo = new ProdutoraJogo();
                        _prodJogo.ProdutoraId = m.Id;
                        _prodJogo.JogoId = g.Id;
                        _context.ProdutoraJogos.Add(_prodJogo);
                    }
                }
            }

            _context.SaveChanges();
        }
    
    }
}
