using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FiapOrangeRoute.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "T_OR_TAG",
                columns: table => new
                {
                    id_tag = table.Column<int>(type: "NUMBER(10)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    nm_tag = table.Column<string>(type: "NVARCHAR2(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_T_OR_TAG", x => x.id_tag);
                });

            migrationBuilder.CreateTable(
                name: "T_OR_TIPO_USUARIO",
                columns: table => new
                {
                    id_tipo_usuario = table.Column<int>(type: "NUMBER(10)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    nm_tipo_usuario = table.Column<string>(type: "NVARCHAR2(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_T_OR_TIPO_USUARIO", x => x.id_tipo_usuario);
                });

            migrationBuilder.CreateTable(
                name: "T_OR_TRILHA_CARREIRA",
                columns: table => new
                {
                    id_trilha_carreira = table.Column<int>(type: "NUMBER(10)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    tt_trilha_carreira = table.Column<string>(type: "NVARCHAR2(150)", maxLength: 150, nullable: false),
                    cd_trilha_carreira = table.Column<string>(type: "NVARCHAR2(2000)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_T_OR_TRILHA_CARREIRA", x => x.id_trilha_carreira);
                });

            migrationBuilder.CreateTable(
                name: "T_OR_USUARIO",
                columns: table => new
                {
                    id_usuario = table.Column<int>(type: "NUMBER(10)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    nm_usuario = table.Column<string>(type: "NVARCHAR2(100)", maxLength: 100, nullable: false),
                    Email = table.Column<string>(type: "NVARCHAR2(150)", maxLength: 150, nullable: false),
                    Senha = table.Column<string>(type: "NVARCHAR2(150)", maxLength: 150, nullable: false),
                    Foto = table.Column<byte[]>(type: "RAW(2000)", nullable: true),
                    at_usuario = table.Column<string>(type: "NVARCHAR2(1)", maxLength: 1, nullable: true),
                    id_tipo_usuario = table.Column<int>(type: "NUMBER(10)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_T_OR_USUARIO", x => x.id_usuario);
                    table.ForeignKey(
                        name: "FK_T_OR_USUARIO_T_OR_TIPO_USUARIO_id_tipo_usuario",
                        column: x => x.id_tipo_usuario,
                        principalTable: "T_OR_TIPO_USUARIO",
                        principalColumn: "id_tipo_usuario",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "T_OR_LINK",
                columns: table => new
                {
                    id_link = table.Column<int>(type: "NUMBER(10)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    tt_link = table.Column<string>(type: "NVARCHAR2(150)", maxLength: 150, nullable: false),
                    cd_link = table.Column<string>(type: "NVARCHAR2(2000)", nullable: true),
                    IdTrilhaCarreira = table.Column<int>(type: "NUMBER(10)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_T_OR_LINK", x => x.id_link);
                    table.ForeignKey(
                        name: "FK_T_OR_LINK_T_OR_TRILHA_CARREIRA_IdTrilhaCarreira",
                        column: x => x.IdTrilhaCarreira,
                        principalTable: "T_OR_TRILHA_CARREIRA",
                        principalColumn: "id_trilha_carreira",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "T_OR_TAG_CARREIRA",
                columns: table => new
                {
                    id_tag_carreira = table.Column<int>(type: "NUMBER(10)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    IdTrilhaCarreira = table.Column<int>(type: "NUMBER(10)", nullable: false),
                    IdTag = table.Column<int>(type: "NUMBER(10)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_T_OR_TAG_CARREIRA", x => x.id_tag_carreira);
                    table.ForeignKey(
                        name: "FK_T_OR_TAG_CARREIRA_T_OR_TAG_IdTag",
                        column: x => x.IdTag,
                        principalTable: "T_OR_TAG",
                        principalColumn: "id_tag",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_T_OR_TAG_CARREIRA_T_OR_TRILHA_CARREIRA_IdTrilhaCarreira",
                        column: x => x.IdTrilhaCarreira,
                        principalTable: "T_OR_TRILHA_CARREIRA",
                        principalColumn: "id_trilha_carreira",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "T_OR_COMENTARIO",
                columns: table => new
                {
                    id_comentario = table.Column<int>(type: "NUMBER(10)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    cd_comentario = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false),
                    at_comentario = table.Column<string>(type: "NVARCHAR2(1)", maxLength: 1, nullable: true),
                    IdUsuario = table.Column<int>(type: "NUMBER(10)", nullable: false),
                    IdTrilhaCarreira = table.Column<int>(type: "NUMBER(10)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_T_OR_COMENTARIO", x => x.id_comentario);
                    table.ForeignKey(
                        name: "FK_T_OR_COMENTARIO_T_OR_TRILHA_CARREIRA_IdTrilhaCarreira",
                        column: x => x.IdTrilhaCarreira,
                        principalTable: "T_OR_TRILHA_CARREIRA",
                        principalColumn: "id_trilha_carreira",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_T_OR_COMENTARIO_T_OR_USUARIO_IdUsuario",
                        column: x => x.IdUsuario,
                        principalTable: "T_OR_USUARIO",
                        principalColumn: "id_usuario",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "T_OR_FAVORITO",
                columns: table => new
                {
                    id_favorito = table.Column<int>(type: "NUMBER(10)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    IdUsuario = table.Column<int>(type: "NUMBER(10)", nullable: false),
                    IdTrilhaCarreira = table.Column<int>(type: "NUMBER(10)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_T_OR_FAVORITO", x => x.id_favorito);
                    table.ForeignKey(
                        name: "FK_T_OR_FAVORITO_T_OR_TRILHA_CARREIRA_IdTrilhaCarreira",
                        column: x => x.IdTrilhaCarreira,
                        principalTable: "T_OR_TRILHA_CARREIRA",
                        principalColumn: "id_trilha_carreira",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_T_OR_FAVORITO_T_OR_USUARIO_IdUsuario",
                        column: x => x.IdUsuario,
                        principalTable: "T_OR_USUARIO",
                        principalColumn: "id_usuario",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_T_OR_COMENTARIO_IdTrilhaCarreira",
                table: "T_OR_COMENTARIO",
                column: "IdTrilhaCarreira");

            migrationBuilder.CreateIndex(
                name: "IX_T_OR_COMENTARIO_IdUsuario",
                table: "T_OR_COMENTARIO",
                column: "IdUsuario");

            migrationBuilder.CreateIndex(
                name: "IX_T_OR_FAVORITO_IdTrilhaCarreira",
                table: "T_OR_FAVORITO",
                column: "IdTrilhaCarreira");

            migrationBuilder.CreateIndex(
                name: "IX_T_OR_FAVORITO_IdUsuario",
                table: "T_OR_FAVORITO",
                column: "IdUsuario");

            migrationBuilder.CreateIndex(
                name: "IX_T_OR_LINK_IdTrilhaCarreira",
                table: "T_OR_LINK",
                column: "IdTrilhaCarreira");

            migrationBuilder.CreateIndex(
                name: "IX_T_OR_TAG_CARREIRA_IdTag",
                table: "T_OR_TAG_CARREIRA",
                column: "IdTag");

            migrationBuilder.CreateIndex(
                name: "IX_T_OR_TAG_CARREIRA_IdTrilhaCarreira",
                table: "T_OR_TAG_CARREIRA",
                column: "IdTrilhaCarreira");

            migrationBuilder.CreateIndex(
                name: "IX_T_OR_USUARIO_id_tipo_usuario",
                table: "T_OR_USUARIO",
                column: "id_tipo_usuario");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "T_OR_COMENTARIO");

            migrationBuilder.DropTable(
                name: "T_OR_FAVORITO");

            migrationBuilder.DropTable(
                name: "T_OR_LINK");

            migrationBuilder.DropTable(
                name: "T_OR_TAG_CARREIRA");

            migrationBuilder.DropTable(
                name: "T_OR_USUARIO");

            migrationBuilder.DropTable(
                name: "T_OR_TAG");

            migrationBuilder.DropTable(
                name: "T_OR_TRILHA_CARREIRA");

            migrationBuilder.DropTable(
                name: "T_OR_TIPO_USUARIO");
        }
    }
}
