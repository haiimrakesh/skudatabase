using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace skudatabase.MVC.Web.Migrations
{
    /// <inheritdoc />
    public partial class DescriptionFields : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<byte[]>(
                name: "Image",
                table: "SKUs",
                type: "BLOB",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "TEXT");

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "SKUPartConfigs",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<bool>(
                name: "IncludeSpacerAtTheEnd",
                table: "SKUPartConfigs",
                type: "INTEGER",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "SKUConfigId",
                table: "SKUConfigSequences",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<int>(
                name: "Length",
                table: "SKUConfigs",
                type: "INTEGER",
                nullable: false,
                oldClrType: typeof(double),
                oldType: "REAL");

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "SKUConfigs",
                type: "TEXT",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Description",
                table: "SKUPartConfigs");

            migrationBuilder.DropColumn(
                name: "IncludeSpacerAtTheEnd",
                table: "SKUPartConfigs");

            migrationBuilder.DropColumn(
                name: "SKUConfigId",
                table: "SKUConfigSequences");

            migrationBuilder.DropColumn(
                name: "Description",
                table: "SKUConfigs");

            migrationBuilder.AlterColumn<string>(
                name: "Image",
                table: "SKUs",
                type: "TEXT",
                nullable: false,
                oldClrType: typeof(byte[]),
                oldType: "BLOB");

            migrationBuilder.AlterColumn<double>(
                name: "Length",
                table: "SKUConfigs",
                type: "REAL",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "INTEGER");
        }
    }
}
