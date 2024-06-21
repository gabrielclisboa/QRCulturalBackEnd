using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace QRCulturalBackEnd.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Admins",
                columns: table => new
                {
                    Usuario = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Senha = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Admins", x => x.Usuario);
                });

            migrationBuilder.CreateTable(
                name: "Monumentos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ContextoHistorico = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Endereco = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    HorarioFuncionamento = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Entrada = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Carrosel1 = table.Column<byte[]>(type: "varbinary(max)", nullable: false),
                    Carrosel2 = table.Column<byte[]>(type: "varbinary(max)", nullable: false),
                    Carrosel3 = table.Column<byte[]>(type: "varbinary(max)", nullable: false),
                    ImagemPrincipal = table.Column<byte[]>(type: "varbinary(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Monumentos", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Cards",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Descricao = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Titulo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Imagem = table.Column<byte[]>(type: "varbinary(max)", nullable: false),
                    MonumentoId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cards", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Cards_Monumentos_MonumentoId",
                        column: x => x.MonumentoId,
                        principalTable: "Monumentos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Cards_MonumentoId",
                table: "Cards",
                column: "MonumentoId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Admins");

            migrationBuilder.DropTable(
                name: "Cards");

            migrationBuilder.DropTable(
                name: "Monumentos");
        }
    }
}
