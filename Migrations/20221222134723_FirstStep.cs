using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace UTAD.LAB._2022.TheGammingHour.Migrations
{
    /// <inheritdoc />
    public partial class FirstStep : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            /*migrationBuilder.CreateTable(
                name: "Categoria",
                columns: table => new
                {
                    ID = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nome = table.Column<string>(type: "varchar(250)", unicode: false, maxLength: 250, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categoria", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Grupo",
                columns: table => new
                {
                    ID = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nome = table.Column<string>(type: "varchar(250)", unicode: false, maxLength: 250, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Grupo", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Jogo",
                columns: table => new
                {
                    ID = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nome = table.Column<string>(type: "varchar(250)", unicode: false, maxLength: 250, nullable: false),
                    Data = table.Column<DateTime>(type: "date", nullable: false),
                    Preco = table.Column<double>(type: "float", nullable: false),
                    Pegi = table.Column<int>(type: "int", nullable: false),
                    Descricao = table.Column<string>(type: "text", nullable: false),
                    Imagem = table.Column<string>(type: "text", nullable: false),
                    Desconto = table.Column<bool>(type: "bit", nullable: false),
                    PercentagemDesconto = table.Column<double>(name: "Percentagem_Desconto", type: "float", nullable: true),
                    Avaliacao = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Jogo", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Menu",
                columns: table => new
                {
                    ID = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Caption = table.Column<string>(type: "varchar(250)", unicode: false, maxLength: 250, nullable: false),
                    Create = table.Column<bool>(type: "bit", nullable: false),
                    Edit = table.Column<bool>(type: "bit", nullable: false),
                    Update = table.Column<bool>(type: "bit", nullable: false),
                    View = table.Column<bool>(type: "bit", nullable: false),
                    Tooltip = table.Column<string>(type: "varchar(250)", unicode: false, maxLength: 250, nullable: false),
                    Key = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Menu", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Plataforma",
                columns: table => new
                {
                    ID = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nome = table.Column<string>(type: "varchar(250)", unicode: false, maxLength: 250, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Plataforma", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Produtora",
                columns: table => new
                {
                    ID = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nome = table.Column<string>(type: "varchar(250)", unicode: false, maxLength: 250, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Produtora", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Utilizador",
                columns: table => new
                {
                    ID = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Tipo = table.Column<int>(type: "int", nullable: false),
                    Nome = table.Column<string>(type: "varchar(250)", unicode: false, maxLength: 250, nullable: true),
                    Email = table.Column<string>(type: "varchar(250)", unicode: false, maxLength: 250, nullable: true),
                    Username = table.Column<string>(type: "varchar(250)", unicode: false, maxLength: 250, nullable: false),
                    Password = table.Column<string>(type: "varchar(250)", unicode: false, maxLength: 250, nullable: false),
                    Telefone = table.Column<string>(type: "nvarchar(14)", maxLength: 14, nullable: true),
                    Verificado = table.Column<bool>(type: "bit", nullable: false),
                    Newsletter = table.Column<bool>(type: "bit", nullable: false),
                    GrupoID = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Utilizador", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Utilizador_Grupo_GrupoID",
                        column: x => x.GrupoID,
                        principalTable: "Grupo",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Categoria_Jogo",
                columns: table => new
                {
                    ID = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CategoriaID = table.Column<long>(type: "bigint", nullable: false),
                    JogoID = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categoria_Jogo", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Categoria_Jogo_Categoria_CategoriaID",
                        column: x => x.CategoriaID,
                        principalTable: "Categoria",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Categoria_Jogo_Jogo_JogoID",
                        column: x => x.JogoID,
                        principalTable: "Jogo",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Grupo_Menu",
                columns: table => new
                {
                    ID = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    GrupoID = table.Column<long>(type: "bigint", nullable: false),
                    MenuID = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Grupo_Menu", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Grupo_Menu_Grupo_GrupoID",
                        column: x => x.GrupoID,
                        principalTable: "Grupo",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Grupo_Menu_Menu_MenuID",
                        column: x => x.MenuID,
                        principalTable: "Menu",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Plataforma_Jogo",
                columns: table => new
                {
                    ID = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PlataformaID = table.Column<long>(type: "bigint", nullable: false),
                    JogoID = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Plataforma_Jogo", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Plataforma_Jogo_Jogo_JogoID",
                        column: x => x.JogoID,
                        principalTable: "Jogo",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Plataforma_Jogo_Plataforma_PlataformaID",
                        column: x => x.PlataformaID,
                        principalTable: "Plataforma",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Produtora_Jogo",
                columns: table => new
                {
                    ID = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProdutoraID = table.Column<long>(type: "bigint", nullable: false),
                    JogoID = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Produtora_Jogo", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Produtora_Jogo_Jogo_JogoID",
                        column: x => x.JogoID,
                        principalTable: "Jogo",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Produtora_Jogo_Produtora_ProdutoraID",
                        column: x => x.ProdutoraID,
                        principalTable: "Produtora",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CategoriaFavorita",
                columns: table => new
                {
                    ID = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UtilizadorID = table.Column<long>(type: "bigint", nullable: false),
                    CategoriaID = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CategoriaFavorita", x => x.ID);
                    table.ForeignKey(
                        name: "FK_CategoriaFavorita_Categoria_CategoriaID",
                        column: x => x.CategoriaID,
                        principalTable: "Categoria",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CategoriaFavorita_Utilizador_UtilizadorID",
                        column: x => x.UtilizadorID,
                        principalTable: "Utilizador",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Pagamento",
                columns: table => new
                {
                    ID = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UtilizadorID = table.Column<long>(type: "bigint", nullable: false),
                    Telemovel = table.Column<int>(type: "int", nullable: true),
                    NIF = table.Column<string>(type: "varchar(9)", unicode: false, maxLength: 9, nullable: false),
                    Morada = table.Column<string>(type: "varchar(250)", unicode: false, maxLength: 250, nullable: false),
                    CodigoPostal = table.Column<string>(type: "varchar(8)", unicode: false, maxLength: 8, nullable: false),
                    Entidade = table.Column<int>(type: "int", nullable: true),
                    Referencia = table.Column<int>(type: "int", nullable: true),
                    PayPal = table.Column<bool>(type: "bit", nullable: true),
                    Guarda = table.Column<bool>(type: "bit", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pagamento", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Pagamento_Utilizador_UtilizadorID",
                        column: x => x.UtilizadorID,
                        principalTable: "Utilizador",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Utilizador_Grupo",
                columns: table => new
                {
                    ID = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UtilizadorID = table.Column<long>(type: "bigint", nullable: false),
                    GrupoID = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Utilizador_Grupo", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Utilizador_Grupo_Grupo_GrupoID",
                        column: x => x.GrupoID,
                        principalTable: "Grupo",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Utilizador_Grupo_Utilizador_UtilizadorID",
                        column: x => x.UtilizadorID,
                        principalTable: "Utilizador",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CompraJogo",
                columns: table => new
                {
                    ID = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UtilizadorID = table.Column<long>(type: "bigint", nullable: false),
                    JogoID = table.Column<long>(type: "bigint", nullable: false),
                    PagamentoID = table.Column<long>(type: "bigint", nullable: false),
                    Data = table.Column<DateTime>(type: "date", nullable: false),
                    Preco = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CompraJogo", x => x.ID);
                    table.ForeignKey(
                        name: "FK_CompraJogo_Jogo_JogoID",
                        column: x => x.JogoID,
                        principalTable: "Jogo",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CompraJogo_Pagamento_PagamentoID",
                        column: x => x.PagamentoID,
                        principalTable: "Pagamento",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CompraJogo_Utilizador_UtilizadorID",
                        column: x => x.UtilizadorID,
                        principalTable: "Utilizador",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Categoria_Jogo_CategoriaID",
                table: "Categoria_Jogo",
                column: "CategoriaID");

            migrationBuilder.CreateIndex(
                name: "IX_Categoria_Jogo_JogoID",
                table: "Categoria_Jogo",
                column: "JogoID");

            migrationBuilder.CreateIndex(
                name: "IX_CategoriaFavorita_CategoriaID",
                table: "CategoriaFavorita",
                column: "CategoriaID");

            migrationBuilder.CreateIndex(
                name: "IX_CategoriaFavorita_UtilizadorID",
                table: "CategoriaFavorita",
                column: "UtilizadorID");

            migrationBuilder.CreateIndex(
                name: "IX_CompraJogo_JogoID",
                table: "CompraJogo",
                column: "JogoID");

            migrationBuilder.CreateIndex(
                name: "IX_CompraJogo_PagamentoID",
                table: "CompraJogo",
                column: "PagamentoID");

            migrationBuilder.CreateIndex(
                name: "IX_CompraJogo_UtilizadorID",
                table: "CompraJogo",
                column: "UtilizadorID");

            migrationBuilder.CreateIndex(
                name: "IX_Grupo_Menu_GrupoID",
                table: "Grupo_Menu",
                column: "GrupoID");

            migrationBuilder.CreateIndex(
                name: "IX_Grupo_Menu_MenuID",
                table: "Grupo_Menu",
                column: "MenuID");

            migrationBuilder.CreateIndex(
                name: "IX_Pagamento_UtilizadorID",
                table: "Pagamento",
                column: "UtilizadorID");

            migrationBuilder.CreateIndex(
                name: "IX_Plataforma_Jogo_JogoID",
                table: "Plataforma_Jogo",
                column: "JogoID");

            migrationBuilder.CreateIndex(
                name: "IX_Plataforma_Jogo_PlataformaID",
                table: "Plataforma_Jogo",
                column: "PlataformaID");

            migrationBuilder.CreateIndex(
                name: "IX_Produtora_Jogo_JogoID",
                table: "Produtora_Jogo",
                column: "JogoID");

            migrationBuilder.CreateIndex(
                name: "IX_Produtora_Jogo_ProdutoraID",
                table: "Produtora_Jogo",
                column: "ProdutoraID");

            migrationBuilder.CreateIndex(
                name: "IX_Utilizador_GrupoID",
                table: "Utilizador",
                column: "GrupoID");

            migrationBuilder.CreateIndex(
                name: "IX_Utilizador_Grupo_GrupoID",
                table: "Utilizador_Grupo",
                column: "GrupoID");

            migrationBuilder.CreateIndex(
                name: "IX_Utilizador_Grupo_UtilizadorID",
                table: "Utilizador_Grupo",
                column: "UtilizadorID");*/
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
           /* migrationBuilder.DropTable(
                name: "Categoria_Jogo");

            migrationBuilder.DropTable(
                name: "CategoriaFavorita");

            migrationBuilder.DropTable(
                name: "CompraJogo");

            migrationBuilder.DropTable(
                name: "Grupo_Menu");

            migrationBuilder.DropTable(
                name: "Plataforma_Jogo");

            migrationBuilder.DropTable(
                name: "Produtora_Jogo");

            migrationBuilder.DropTable(
                name: "Utilizador_Grupo");

            migrationBuilder.DropTable(
                name: "Categoria");

            migrationBuilder.DropTable(
                name: "Pagamento");

            migrationBuilder.DropTable(
                name: "Menu");

            migrationBuilder.DropTable(
                name: "Plataforma");

            migrationBuilder.DropTable(
                name: "Jogo");

            migrationBuilder.DropTable(
                name: "Produtora");

            migrationBuilder.DropTable(
                name: "Utilizador");

            migrationBuilder.DropTable(
                name: "Grupo");*/
        }
    }
}
