using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SKUApp.Data.EFCore.SqlServer.Migrations
{
    /// <inheritdoc />
    public partial class InitialCommit : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "SKUConfigs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    Length = table.Column<long>(type: "bigint", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SKUConfigs", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SKUPartConfigs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    Length = table.Column<int>(type: "int", nullable: false),
                    GenericName = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    IsAlphaNumeric = table.Column<bool>(type: "bit", nullable: false),
                    IsCaseSensitive = table.Column<bool>(type: "bit", nullable: false),
                    AllowPreceedingZero = table.Column<bool>(type: "bit", nullable: false),
                    RestrictConflictingLettersAndCharacters = table.Column<bool>(type: "bit", nullable: false),
                    IncludeSpacerAtTheEnd = table.Column<bool>(type: "bit", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SKUPartConfigs", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SKUPartEntries",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UniqueCode = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SKUPartConfigId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SKUPartEntries", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SKUs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SKUCode = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Image = table.Column<byte[]>(type: "varbinary(max)", nullable: false),
                    SKUConfigId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SKUs", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SKUConfigSequences",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SKUPartConfigId = table.Column<int>(type: "int", nullable: false),
                    SKUConfigId = table.Column<int>(type: "int", nullable: false),
                    Sequence = table.Column<int>(type: "int", nullable: false),
                    RelationshipDescription = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SKUConfigSequences", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SKUConfigSequences_SKUConfigs_SKUConfigId",
                        column: x => x.SKUConfigId,
                        principalTable: "SKUConfigs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SKUConfigSequences_SKUPartConfigs_SKUPartConfigId",
                        column: x => x.SKUPartConfigId,
                        principalTable: "SKUPartConfigs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_SKUConfigSequences_SKUConfigId",
                table: "SKUConfigSequences",
                column: "SKUConfigId");

            migrationBuilder.CreateIndex(
                name: "IX_SKUConfigSequences_SKUPartConfigId",
                table: "SKUConfigSequences",
                column: "SKUPartConfigId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SKUConfigSequences");

            migrationBuilder.DropTable(
                name: "SKUPartEntries");

            migrationBuilder.DropTable(
                name: "SKUs");

            migrationBuilder.DropTable(
                name: "SKUConfigs");

            migrationBuilder.DropTable(
                name: "SKUPartConfigs");
        }
    }
}
