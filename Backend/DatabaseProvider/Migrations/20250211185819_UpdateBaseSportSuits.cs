using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DatabaseProvider.Migrations
{
    /// <inheritdoc />
    public partial class UpdateBaseSportSuits : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "BaseSportSuit_FkBaseBelts_fkey",
                table: "BaseSportSuit");

            migrationBuilder.DropForeignKey(
                name: "BaseSportSuit_FkBaseNecklines_fkey",
                table: "BaseSportSuit");

            migrationBuilder.DropForeignKey(
                name: "BaseSportSuit_FkBasePantsCuffsLeft_fkey",
                table: "BaseSportSuit");

            migrationBuilder.DropForeignKey(
                name: "BaseSportSuit_FkBasePantsCuffsRight_fkey",
                table: "BaseSportSuit");

            migrationBuilder.DropForeignKey(
                name: "BaseSportSuit_FkBasePants_fkey",
                table: "BaseSportSuit");

            migrationBuilder.DropForeignKey(
                name: "BaseSportSuit_FkBaseSleeveCuffsLeft_fkey",
                table: "BaseSportSuit");

            migrationBuilder.DropForeignKey(
                name: "BaseSportSuit_FkBaseSleeveCuffsRight_fkey",
                table: "BaseSportSuit");

            migrationBuilder.DropForeignKey(
                name: "BaseSportSuit_FkBaseSleeves_fkey",
                table: "BaseSportSuit");

            migrationBuilder.DropForeignKey(
                name: "BaseSportSuit_FkBaseSweaters_fkey",
                table: "BaseSportSuit");

            migrationBuilder.DropPrimaryKey(
                name: "BaseSportSuit_pkey",
                table: "BaseSportSuit");

            migrationBuilder.RenameTable(
                name: "BaseSportSuit",
                newName: "BaseSportSuits");

            migrationBuilder.RenameIndex(
                name: "IX_BaseSportSuit_FkBaseSweaters",
                table: "BaseSportSuits",
                newName: "IX_BaseSportSuits_FkBaseSweaters");

            migrationBuilder.RenameIndex(
                name: "IX_BaseSportSuit_FkBaseSleeves",
                table: "BaseSportSuits",
                newName: "IX_BaseSportSuits_FkBaseSleeves");

            migrationBuilder.RenameIndex(
                name: "IX_BaseSportSuit_FkBaseSleeveCuffsRight",
                table: "BaseSportSuits",
                newName: "IX_BaseSportSuits_FkBaseSleeveCuffsRight");

            migrationBuilder.RenameIndex(
                name: "IX_BaseSportSuit_FkBaseSleeveCuffsLeft",
                table: "BaseSportSuits",
                newName: "IX_BaseSportSuits_FkBaseSleeveCuffsLeft");

            migrationBuilder.RenameIndex(
                name: "IX_BaseSportSuit_FkBasePantsCuffsRight",
                table: "BaseSportSuits",
                newName: "IX_BaseSportSuits_FkBasePantsCuffsRight");

            migrationBuilder.RenameIndex(
                name: "IX_BaseSportSuit_FkBasePantsCuffsLeft",
                table: "BaseSportSuits",
                newName: "IX_BaseSportSuits_FkBasePantsCuffsLeft");

            migrationBuilder.RenameIndex(
                name: "IX_BaseSportSuit_FkBasePants",
                table: "BaseSportSuits",
                newName: "IX_BaseSportSuits_FkBasePants");

            migrationBuilder.RenameIndex(
                name: "IX_BaseSportSuit_FkBaseNecklines",
                table: "BaseSportSuits",
                newName: "IX_BaseSportSuits_FkBaseNecklines");

            migrationBuilder.RenameIndex(
                name: "IX_BaseSportSuit_FkBaseBelts",
                table: "BaseSportSuits",
                newName: "IX_BaseSportSuits_FkBaseBelts");

            migrationBuilder.AddPrimaryKey(
                name: "BaseSportSuits_pkey",
                table: "BaseSportSuits",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "BaseSportSuits_FkBaseBelts_fkey",
                table: "BaseSportSuits",
                column: "FkBaseBelts",
                principalTable: "BaseBelts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "BaseSportSuits_FkBaseNecklines_fkey",
                table: "BaseSportSuits",
                column: "FkBaseNecklines",
                principalTable: "BaseNecklines",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "BaseSportSuits_FkBasePantsCuffsLeft_fkey",
                table: "BaseSportSuits",
                column: "FkBasePantsCuffsLeft",
                principalTable: "BasePantsCuffs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "BaseSportSuits_FkBasePantsCuffsRight_fkey",
                table: "BaseSportSuits",
                column: "FkBasePantsCuffsRight",
                principalTable: "BasePantsCuffs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "BaseSportSuits_FkBasePants_fkey",
                table: "BaseSportSuits",
                column: "FkBasePants",
                principalTable: "BasePants",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "BaseSportSuits_FkBaseSleeveCuffsLeft_fkey",
                table: "BaseSportSuits",
                column: "FkBaseSleeveCuffsLeft",
                principalTable: "BaseSleeveCuffs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "BaseSportSuits_FkBaseSleeveCuffsRight_fkey",
                table: "BaseSportSuits",
                column: "FkBaseSleeveCuffsRight",
                principalTable: "BaseSleeveCuffs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "BaseSportSuits_FkBaseSleeves_fkey",
                table: "BaseSportSuits",
                column: "FkBaseSleeves",
                principalTable: "BaseSleeves",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "BaseSportSuits_FkBaseSweaters_fkey",
                table: "BaseSportSuits",
                column: "FkBaseSweaters",
                principalTable: "BaseSweaters",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "BaseSportSuits_FkBaseBelts_fkey",
                table: "BaseSportSuits");

            migrationBuilder.DropForeignKey(
                name: "BaseSportSuits_FkBaseNecklines_fkey",
                table: "BaseSportSuits");

            migrationBuilder.DropForeignKey(
                name: "BaseSportSuits_FkBasePantsCuffsLeft_fkey",
                table: "BaseSportSuits");

            migrationBuilder.DropForeignKey(
                name: "BaseSportSuits_FkBasePantsCuffsRight_fkey",
                table: "BaseSportSuits");

            migrationBuilder.DropForeignKey(
                name: "BaseSportSuits_FkBasePants_fkey",
                table: "BaseSportSuits");

            migrationBuilder.DropForeignKey(
                name: "BaseSportSuits_FkBaseSleeveCuffsLeft_fkey",
                table: "BaseSportSuits");

            migrationBuilder.DropForeignKey(
                name: "BaseSportSuits_FkBaseSleeveCuffsRight_fkey",
                table: "BaseSportSuits");

            migrationBuilder.DropForeignKey(
                name: "BaseSportSuits_FkBaseSleeves_fkey",
                table: "BaseSportSuits");

            migrationBuilder.DropForeignKey(
                name: "BaseSportSuits_FkBaseSweaters_fkey",
                table: "BaseSportSuits");

            migrationBuilder.DropPrimaryKey(
                name: "BaseSportSuits_pkey",
                table: "BaseSportSuits");

            migrationBuilder.RenameTable(
                name: "BaseSportSuits",
                newName: "BaseSportSuit");

            migrationBuilder.RenameIndex(
                name: "IX_BaseSportSuits_FkBaseSweaters",
                table: "BaseSportSuit",
                newName: "IX_BaseSportSuit_FkBaseSweaters");

            migrationBuilder.RenameIndex(
                name: "IX_BaseSportSuits_FkBaseSleeves",
                table: "BaseSportSuit",
                newName: "IX_BaseSportSuit_FkBaseSleeves");

            migrationBuilder.RenameIndex(
                name: "IX_BaseSportSuits_FkBaseSleeveCuffsRight",
                table: "BaseSportSuit",
                newName: "IX_BaseSportSuit_FkBaseSleeveCuffsRight");

            migrationBuilder.RenameIndex(
                name: "IX_BaseSportSuits_FkBaseSleeveCuffsLeft",
                table: "BaseSportSuit",
                newName: "IX_BaseSportSuit_FkBaseSleeveCuffsLeft");

            migrationBuilder.RenameIndex(
                name: "IX_BaseSportSuits_FkBasePantsCuffsRight",
                table: "BaseSportSuit",
                newName: "IX_BaseSportSuit_FkBasePantsCuffsRight");

            migrationBuilder.RenameIndex(
                name: "IX_BaseSportSuits_FkBasePantsCuffsLeft",
                table: "BaseSportSuit",
                newName: "IX_BaseSportSuit_FkBasePantsCuffsLeft");

            migrationBuilder.RenameIndex(
                name: "IX_BaseSportSuits_FkBasePants",
                table: "BaseSportSuit",
                newName: "IX_BaseSportSuit_FkBasePants");

            migrationBuilder.RenameIndex(
                name: "IX_BaseSportSuits_FkBaseNecklines",
                table: "BaseSportSuit",
                newName: "IX_BaseSportSuit_FkBaseNecklines");

            migrationBuilder.RenameIndex(
                name: "IX_BaseSportSuits_FkBaseBelts",
                table: "BaseSportSuit",
                newName: "IX_BaseSportSuit_FkBaseBelts");

            migrationBuilder.AddPrimaryKey(
                name: "BaseSportSuit_pkey",
                table: "BaseSportSuit",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "BaseSportSuit_FkBaseBelts_fkey",
                table: "BaseSportSuit",
                column: "FkBaseBelts",
                principalTable: "BaseBelts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "BaseSportSuit_FkBaseNecklines_fkey",
                table: "BaseSportSuit",
                column: "FkBaseNecklines",
                principalTable: "BaseNecklines",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "BaseSportSuit_FkBasePantsCuffsLeft_fkey",
                table: "BaseSportSuit",
                column: "FkBasePantsCuffsLeft",
                principalTable: "BasePantsCuffs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "BaseSportSuit_FkBasePantsCuffsRight_fkey",
                table: "BaseSportSuit",
                column: "FkBasePantsCuffsRight",
                principalTable: "BasePantsCuffs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "BaseSportSuit_FkBasePants_fkey",
                table: "BaseSportSuit",
                column: "FkBasePants",
                principalTable: "BasePants",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "BaseSportSuit_FkBaseSleeveCuffsLeft_fkey",
                table: "BaseSportSuit",
                column: "FkBaseSleeveCuffsLeft",
                principalTable: "BaseSleeveCuffs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "BaseSportSuit_FkBaseSleeveCuffsRight_fkey",
                table: "BaseSportSuit",
                column: "FkBaseSleeveCuffsRight",
                principalTable: "BaseSleeveCuffs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "BaseSportSuit_FkBaseSleeves_fkey",
                table: "BaseSportSuit",
                column: "FkBaseSleeves",
                principalTable: "BaseSleeves",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "BaseSportSuit_FkBaseSweaters_fkey",
                table: "BaseSportSuit",
                column: "FkBaseSweaters",
                principalTable: "BaseSweaters",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
