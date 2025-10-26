using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ch4rniauski.BankApp.CreditCards.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CreditCards",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    CardNumber = table.Column<string>(type: "character varying(19)", maxLength: 19, nullable: false),
                    CvvHash = table.Column<string>(type: "text", nullable: false),
                    PinCodeHash = table.Column<string>(type: "text", nullable: false),
                    CardType = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false),
                    Balance = table.Column<decimal>(type: "numeric", nullable: false),
                    ExpirationMonth = table.Column<byte>(type: "smallint", nullable: false),
                    ExpirationYear = table.Column<short>(type: "smallint", nullable: false),
                    IsBlocked = table.Column<bool>(type: "boolean", nullable: false),
                    CardHolderName = table.Column<string>(type: "character varying(150)", maxLength: 150, nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    CardHolderId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CreditCards", x => x.Id);
                    table.CheckConstraint("CK_CreditCards_Balance_Positive", "\"Balance\" >= 0");
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CreditCards");
        }
    }
}
