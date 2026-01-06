using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace DatabaseProvider.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AccessLevels",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Level = table.Column<int>(type: "integer", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("AccessLevels_pkey", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "BaseBelts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Settings = table.Column<string>(type: "jsonb", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("BaseBelts_pkey", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "BaseNecklines",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Settings = table.Column<string>(type: "jsonb", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("BaseNecklines_pkey", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "BasePants",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Settings = table.Column<string>(type: "jsonb", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("BasePants_pkey", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "BasePantsCuffs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Settings = table.Column<string>(type: "jsonb", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("BasePantsCuffs_pkey", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "BaseSleeveCuffs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Settings = table.Column<string>(type: "jsonb", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("BaseSleeveCuffs_pkey", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "BaseSleeves",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Settings = table.Column<string>(type: "jsonb", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("BaseSleeves_pkey", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "BaseSweaters",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Settings = table.Column<string>(type: "jsonb", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("BaseSweaters_pkey", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Colors",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    ImageData = table.Column<byte[]>(type: "bytea", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("Colors_pkey", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Countries",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("Countries_pkey", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Currencies",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("Currencies_pkey", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Languages",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    FullName = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    Abbreviation = table.Column<string>(type: "character varying(5)", maxLength: 5, nullable: false),
                    Description = table.Column<string>(type: "text", nullable: false),
                    DateFormat = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false),
                    TimeFormat = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("Languages_pkey", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "OrderStatuses",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Status = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("OrderStatuses_pkey", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PaymentMethods",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Method = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PaymentMethods_pkey", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PaymentStatuses",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Status = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PaymentStatuses_pkey", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SizeOptions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Size = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("SizeOptions_pkey", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "UserRoles",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    FkAccessLevels = table.Column<int>(type: "integer", nullable: false),
                    Name = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("UserRoles_pkey", x => x.Id);
                    table.ForeignKey(
                        name: "UserRoles_FkAccessLevels_fkey",
                        column: x => x.FkAccessLevels,
                        principalTable: "AccessLevels",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CustomBelts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    FkBaseBelts = table.Column<int>(type: "integer", nullable: false),
                    CustomSettings = table.Column<string>(type: "jsonb", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("CustomBelts_pkey", x => x.Id);
                    table.ForeignKey(
                        name: "CustomBelts_FkBaseBelts_fkey",
                        column: x => x.FkBaseBelts,
                        principalTable: "BaseBelts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CustomNecklines",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    FkBaseNecklines = table.Column<int>(type: "integer", nullable: false),
                    CustomSettings = table.Column<string>(type: "jsonb", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("CustomNecklines_pkey", x => x.Id);
                    table.ForeignKey(
                        name: "CustomNecklines_FkBaseNecklines_fkey",
                        column: x => x.FkBaseNecklines,
                        principalTable: "BaseNecklines",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CustomPants",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    FkBasePants = table.Column<int>(type: "integer", nullable: false),
                    CustomSettings = table.Column<string>(type: "jsonb", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("CustomPants_pkey", x => x.Id);
                    table.ForeignKey(
                        name: "CustomPants_FkBasePants_fkey",
                        column: x => x.FkBasePants,
                        principalTable: "BasePants",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CustomPantsCuffs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    FkBasePantCuffs = table.Column<int>(type: "integer", nullable: false),
                    CustomSettings = table.Column<string>(type: "jsonb", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("CustomPantsCuffs_pkey", x => x.Id);
                    table.ForeignKey(
                        name: "CustomPantsCuffs_FkBasePantCuffs_fkey",
                        column: x => x.FkBasePantCuffs,
                        principalTable: "BasePantsCuffs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CustomSleeveCuffs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    FkBaseSleeveCuffs = table.Column<int>(type: "integer", nullable: false),
                    CustomSettings = table.Column<string>(type: "jsonb", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("CustomSleeveCuffs_pkey", x => x.Id);
                    table.ForeignKey(
                        name: "CustomSleeveCuffs_FkBaseSleeveCuffs_fkey",
                        column: x => x.FkBaseSleeveCuffs,
                        principalTable: "BaseSleeveCuffs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CustomSleeves",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    FkBaseSleeves = table.Column<int>(type: "integer", nullable: false),
                    CustomSettings = table.Column<string>(type: "jsonb", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("CustomSleeves_pkey", x => x.Id);
                    table.ForeignKey(
                        name: "CustomSleeves_FkBaseSleeves_fkey",
                        column: x => x.FkBaseSleeves,
                        principalTable: "BaseSleeves",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "BaseSportSuit",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    FkBaseNecklines = table.Column<int>(type: "integer", nullable: true),
                    FkBaseSweaters = table.Column<int>(type: "integer", nullable: true),
                    FkBaseSleeves = table.Column<int>(type: "integer", nullable: true),
                    FkBaseSleeveCuffsLeft = table.Column<int>(type: "integer", nullable: true),
                    FkBaseSleeveCuffsRight = table.Column<int>(type: "integer", nullable: true),
                    FkBaseBelts = table.Column<int>(type: "integer", nullable: true),
                    FkBasePants = table.Column<int>(type: "integer", nullable: true),
                    FkBasePantsCuffsLeft = table.Column<int>(type: "integer", nullable: true),
                    FkBasePantsCuffsRight = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("BaseSportSuit_pkey", x => x.Id);
                    table.ForeignKey(
                        name: "BaseSportSuit_FkBaseBelts_fkey",
                        column: x => x.FkBaseBelts,
                        principalTable: "BaseBelts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "BaseSportSuit_FkBaseNecklines_fkey",
                        column: x => x.FkBaseNecklines,
                        principalTable: "BaseNecklines",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "BaseSportSuit_FkBasePantsCuffsLeft_fkey",
                        column: x => x.FkBasePantsCuffsLeft,
                        principalTable: "BasePantsCuffs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "BaseSportSuit_FkBasePantsCuffsRight_fkey",
                        column: x => x.FkBasePantsCuffsRight,
                        principalTable: "BasePantsCuffs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "BaseSportSuit_FkBasePants_fkey",
                        column: x => x.FkBasePants,
                        principalTable: "BasePants",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "BaseSportSuit_FkBaseSleeveCuffsLeft_fkey",
                        column: x => x.FkBaseSleeveCuffsLeft,
                        principalTable: "BaseSleeveCuffs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "BaseSportSuit_FkBaseSleeveCuffsRight_fkey",
                        column: x => x.FkBaseSleeveCuffsRight,
                        principalTable: "BaseSleeveCuffs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "BaseSportSuit_FkBaseSleeves_fkey",
                        column: x => x.FkBaseSleeves,
                        principalTable: "BaseSleeves",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "BaseSportSuit_FkBaseSweaters_fkey",
                        column: x => x.FkBaseSweaters,
                        principalTable: "BaseSweaters",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CustomSweaters",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    FkBaseSweaters = table.Column<int>(type: "integer", nullable: false),
                    CustomSettings = table.Column<string>(type: "jsonb", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("CustomSweaters_pkey", x => x.Id);
                    table.ForeignKey(
                        name: "CustomSweaters_FkBaseSweaters_fkey",
                        column: x => x.FkBaseSweaters,
                        principalTable: "BaseSweaters",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ShippingAddresses",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    FkCountries = table.Column<int>(type: "integer", nullable: false),
                    Address = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    City = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    ZipPostCode = table.Column<string>(type: "character varying(10)", maxLength: 10, nullable: false),
                    StateProvince = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    Email = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("ShippingAddresses_pkey", x => x.Id);
                    table.ForeignKey(
                        name: "ShippingAddresses_FkCountries_fkey",
                        column: x => x.FkCountries,
                        principalTable: "Countries",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "FabricTypes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    FkCurrencies = table.Column<int>(type: "integer", nullable: false),
                    Name = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    Description = table.Column<string>(type: "text", nullable: true),
                    Price = table.Column<decimal>(type: "numeric(10,2)", precision: 10, scale: 2, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("FabricTypes_pkey", x => x.Id);
                    table.ForeignKey(
                        name: "FabricTypes_FkCurrencies_fkey",
                        column: x => x.FkCurrencies,
                        principalTable: "Currencies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "About",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    FkLanguages = table.Column<int>(type: "integer", nullable: false),
                    Header = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    Text = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("About_pkey", x => x.Id);
                    table.ForeignKey(
                        name: "About_FkLanguages_fkey",
                        column: x => x.FkLanguages,
                        principalTable: "Languages",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    FkLanguages = table.Column<int>(type: "integer", nullable: false),
                    Name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    Description = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("Categories_pkey", x => x.Id);
                    table.ForeignKey(
                        name: "Categories_FkLanguages_fkey",
                        column: x => x.FkLanguages,
                        principalTable: "Languages",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Contacts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    FkLanguages = table.Column<int>(type: "integer", nullable: false),
                    Address = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    Phone = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    Email = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("Contacts_pkey", x => x.Id);
                    table.ForeignKey(
                        name: "Contacts_FkLanguages_fkey",
                        column: x => x.FkLanguages,
                        principalTable: "Languages",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Payments",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    FkPaymentMethods = table.Column<int>(type: "integer", nullable: false),
                    FkCurrencies = table.Column<int>(type: "integer", nullable: false),
                    FkPaymentStatuses = table.Column<int>(type: "integer", nullable: false),
                    Price = table.Column<decimal>(type: "numeric(10,2)", precision: 10, scale: 2, nullable: false),
                    PaymentDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP"),
                    PaymentNumber = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("Payments_pkey", x => x.Id);
                    table.ForeignKey(
                        name: "Payments_FkCurrencies_fkey",
                        column: x => x.FkCurrencies,
                        principalTable: "Currencies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "Payments_FkPaymentMethods_fkey",
                        column: x => x.FkPaymentMethods,
                        principalTable: "PaymentMethods",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "Payments_FkPaymentStatuses_fkey",
                        column: x => x.FkPaymentStatuses,
                        principalTable: "PaymentStatuses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    FkUserRoles = table.Column<int>(type: "integer", nullable: false),
                    Login = table.Column<string>(type: "character varying(30)", maxLength: 30, nullable: false),
                    Email = table.Column<string>(type: "character varying(60)", maxLength: 60, nullable: false),
                    Password = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    ConfirmEmail = table.Column<int>(type: "integer", nullable: false),
                    Hash = table.Column<string>(type: "text", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp without time zone", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP"),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp without time zone", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP")
                },
                constraints: table =>
                {
                    table.PrimaryKey("Users_pkey", x => x.Id);
                    table.ForeignKey(
                        name: "Users_FkUserRoles_fkey",
                        column: x => x.FkUserRoles,
                        principalTable: "UserRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CustomSportSuits",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    FkCustomNecklines = table.Column<int>(type: "integer", nullable: true),
                    FkCustomSweaters = table.Column<int>(type: "integer", nullable: true),
                    FkCustomSleeves = table.Column<int>(type: "integer", nullable: true),
                    FkCustomSleeveCuffsLeft = table.Column<int>(type: "integer", nullable: true),
                    FkCustomSleeveCuffsRight = table.Column<int>(type: "integer", nullable: true),
                    FkCustomBelts = table.Column<int>(type: "integer", nullable: true),
                    FkCustomPants = table.Column<int>(type: "integer", nullable: true),
                    FkCustomPantsCuffsLeft = table.Column<int>(type: "integer", nullable: true),
                    FkCustomPantsCuffsRight = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("CustomSportSuits_pkey", x => x.Id);
                    table.ForeignKey(
                        name: "CustomSportSuits_FkCustomBelts_fkey",
                        column: x => x.FkCustomBelts,
                        principalTable: "CustomBelts",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "CustomSportSuits_FkCustomNecklines_fkey",
                        column: x => x.FkCustomNecklines,
                        principalTable: "CustomNecklines",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "CustomSportSuits_FkCustomPantsCuffsLeft_fkey",
                        column: x => x.FkCustomPantsCuffsLeft,
                        principalTable: "CustomPantsCuffs",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "CustomSportSuits_FkCustomPantsCuffsRight_fkey",
                        column: x => x.FkCustomPantsCuffsRight,
                        principalTable: "CustomPantsCuffs",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "CustomSportSuits_FkCustomPants_fkey",
                        column: x => x.FkCustomPants,
                        principalTable: "CustomPants",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "CustomSportSuits_FkCustomSleeveCuffsLeft_fkey",
                        column: x => x.FkCustomSleeveCuffsLeft,
                        principalTable: "CustomSleeveCuffs",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "CustomSportSuits_FkCustomSleeveCuffsRight_fkey",
                        column: x => x.FkCustomSleeveCuffsRight,
                        principalTable: "CustomSleeveCuffs",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "CustomSportSuits_FkCustomSleeves_fkey",
                        column: x => x.FkCustomSleeves,
                        principalTable: "CustomSleeves",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "CustomSportSuits_FkCustomSweaters_fkey",
                        column: x => x.FkCustomSweaters,
                        principalTable: "CustomSweaters",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    FkFabricTypes = table.Column<int>(type: "integer", nullable: false),
                    FkColors = table.Column<int>(type: "integer", nullable: false),
                    FkSizeOptions = table.Column<int>(type: "integer", nullable: false),
                    FkCurrencies = table.Column<int>(type: "integer", nullable: false),
                    Price = table.Column<decimal>(type: "numeric(10,2)", precision: 10, scale: 2, nullable: false),
                    StockQuantity = table.Column<int>(type: "integer", nullable: false),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false, defaultValue: true),
                    IsCustomizable = table.Column<bool>(type: "boolean", nullable: false, defaultValue: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp without time zone", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP"),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp without time zone", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP")
                },
                constraints: table =>
                {
                    table.PrimaryKey("Products_pkey", x => x.Id);
                    table.ForeignKey(
                        name: "Products_FkColors_fkey",
                        column: x => x.FkColors,
                        principalTable: "Colors",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "Products_FkCurrencies_fkey",
                        column: x => x.FkCurrencies,
                        principalTable: "Currencies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "Products_FkFabricTypes_fkey",
                        column: x => x.FkFabricTypes,
                        principalTable: "FabricTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "Products_FkSizeOptions_fkey",
                        column: x => x.FkSizeOptions,
                        principalTable: "SizeOptions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CategoryHierarchy",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ParentId = table.Column<int>(type: "integer", nullable: true),
                    FkCategories = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("CategoryHierarchy_pkey", x => x.Id);
                    table.ForeignKey(
                        name: "CategoryHierarchy_FkCategories_fkey",
                        column: x => x.FkCategories,
                        principalTable: "Categories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "CategoryHierarchy_ParentId_fkey",
                        column: x => x.ParentId,
                        principalTable: "CategoryHierarchy",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserProfiles",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    FkUsers = table.Column<int>(type: "integer", nullable: false),
                    FirstName = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    LastName = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    MiddleName = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    Address = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true),
                    City = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    ZipPostCode = table.Column<string>(type: "character varying(10)", maxLength: 10, nullable: true),
                    StateProvince = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    Country = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    Phone = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: true),
                    Avatar = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "timestamp without time zone", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP"),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp without time zone", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP")
                },
                constraints: table =>
                {
                    table.PrimaryKey("UserProfiles_pkey", x => x.Id);
                    table.ForeignKey(
                        name: "UserProfiles_FkUsers_fkey",
                        column: x => x.FkUsers,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CustomizableProducts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    FkCustomSportSuits = table.Column<int>(type: "integer", nullable: false),
                    FkFabricTypes = table.Column<int>(type: "integer", nullable: false),
                    FkSizeOptions = table.Column<int>(type: "integer", nullable: false),
                    Price = table.Column<decimal>(type: "numeric(10,2)", precision: 10, scale: 2, nullable: false),
                    CustomizationDetails = table.Column<string>(type: "text", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "timestamp without time zone", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP"),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp without time zone", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP")
                },
                constraints: table =>
                {
                    table.PrimaryKey("CustomizableProducts_pkey", x => x.Id);
                    table.ForeignKey(
                        name: "CustomizableProducts_FkCustomSportSuits_fkey",
                        column: x => x.FkCustomSportSuits,
                        principalTable: "CustomSportSuits",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "CustomizableProducts_FkFabricTypes_fkey",
                        column: x => x.FkFabricTypes,
                        principalTable: "FabricTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "CustomizableProducts_FkSizeOptions_fkey",
                        column: x => x.FkSizeOptions,
                        principalTable: "SizeOptions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProductImages",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    FkProducts = table.Column<int>(type: "integer", nullable: false),
                    ImageData = table.Column<byte[]>(type: "bytea", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("ProductImages_pkey", x => x.Id);
                    table.ForeignKey(
                        name: "ProductImages_FkProducts_fkey",
                        column: x => x.FkProducts,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProductTranslations",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    FkLanguages = table.Column<int>(type: "integer", nullable: false),
                    FkProducts = table.Column<int>(type: "integer", nullable: false),
                    FkCategories = table.Column<int>(type: "integer", nullable: false),
                    Name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    Description = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("ProductTranslations_pkey", x => x.Id);
                    table.ForeignKey(
                        name: "ProductTranslations_FkCategories_fkey",
                        column: x => x.FkCategories,
                        principalTable: "Categories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "ProductTranslations_FkLanguages_fkey",
                        column: x => x.FkLanguages,
                        principalTable: "Languages",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "ProductTranslations_FkProducts_fkey",
                        column: x => x.FkProducts,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Reviews",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    FkProducts = table.Column<int>(type: "integer", nullable: false),
                    FkUsers = table.Column<int>(type: "integer", nullable: false),
                    Rating = table.Column<int>(type: "integer", nullable: false),
                    Comment = table.Column<string>(type: "text", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "timestamp without time zone", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP")
                },
                constraints: table =>
                {
                    table.PrimaryKey("Reviews_pkey", x => x.Id);
                    table.ForeignKey(
                        name: "Reviews_FkProducts_fkey",
                        column: x => x.FkProducts,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "Reviews_FkUsers_fkey",
                        column: x => x.FkUsers,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProductOrders",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    FkProducts = table.Column<int>(type: "integer", nullable: true),
                    FkCustomizableProducts = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("ProductOrders_pkey", x => x.Id);
                    table.ForeignKey(
                        name: "ProductOrders_FkCustomizableProducts_fkey",
                        column: x => x.FkCustomizableProducts,
                        principalTable: "CustomizableProducts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "ProductOrders_FkProducts_fkey",
                        column: x => x.FkProducts,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Cart",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    FkProductOrders = table.Column<int>(type: "integer", nullable: false),
                    FkCurrencies = table.Column<int>(type: "integer", nullable: false),
                    Price = table.Column<decimal>(type: "numeric(10,2)", precision: 10, scale: 2, nullable: false),
                    Amount = table.Column<int>(type: "integer", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp without time zone", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP")
                },
                constraints: table =>
                {
                    table.PrimaryKey("Cart_pkey", x => x.Id);
                    table.ForeignKey(
                        name: "Cart_FkCurrencies_fkey",
                        column: x => x.FkCurrencies,
                        principalTable: "Currencies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "Cart_FkProductOrders_fkey",
                        column: x => x.FkProductOrders,
                        principalTable: "ProductOrders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Orders",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    FkProductOrders = table.Column<int>(type: "integer", nullable: false),
                    FkOrderStatus = table.Column<int>(type: "integer", nullable: false),
                    FkPayments = table.Column<int>(type: "integer", nullable: false),
                    FkShippingAddresses = table.Column<int>(type: "integer", nullable: false),
                    Price = table.Column<decimal>(type: "numeric(10,2)", precision: 10, scale: 2, nullable: false),
                    Amount = table.Column<int>(type: "integer", nullable: false),
                    OrderDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP"),
                    CreatedAt = table.Column<DateTime>(type: "timestamp without time zone", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP"),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp without time zone", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP")
                },
                constraints: table =>
                {
                    table.PrimaryKey("Orders_pkey", x => x.Id);
                    table.ForeignKey(
                        name: "Orders_FkOrderStatus_fkey",
                        column: x => x.FkOrderStatus,
                        principalTable: "OrderStatuses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "Orders_FkPayments_fkey",
                        column: x => x.FkPayments,
                        principalTable: "Payments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "Orders_FkProductOrders_fkey",
                        column: x => x.FkProductOrders,
                        principalTable: "ProductOrders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "Orders_FkShippingAddresses_fkey",
                        column: x => x.FkShippingAddresses,
                        principalTable: "ShippingAddresses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "OrderHistory",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    FkOrders = table.Column<int>(type: "integer", nullable: false),
                    FkOrderStatuses = table.Column<int>(type: "integer", nullable: false),
                    ChangedAt = table.Column<DateTime>(type: "timestamp without time zone", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP")
                },
                constraints: table =>
                {
                    table.PrimaryKey("OrderHistory_pkey", x => x.Id);
                    table.ForeignKey(
                        name: "OrderHistory_FkOrderStatuses_fkey",
                        column: x => x.FkOrderStatuses,
                        principalTable: "OrderStatuses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "OrderHistory_FkOrders_fkey",
                        column: x => x.FkOrders,
                        principalTable: "Orders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserOrderHistory",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    FkOrders = table.Column<int>(type: "integer", nullable: false),
                    FkUsers = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("UserOrderHistory_pkey", x => x.Id);
                    table.ForeignKey(
                        name: "UserOrderHistory_FkOrders_fkey",
                        column: x => x.FkOrders,
                        principalTable: "Orders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "UserOrderHistory_FkUsers_fkey",
                        column: x => x.FkUsers,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_About_FkLanguages",
                table: "About",
                column: "FkLanguages");

            migrationBuilder.CreateIndex(
                name: "IX_BaseSportSuit_FkBaseBelts",
                table: "BaseSportSuit",
                column: "FkBaseBelts");

            migrationBuilder.CreateIndex(
                name: "IX_BaseSportSuit_FkBaseNecklines",
                table: "BaseSportSuit",
                column: "FkBaseNecklines");

            migrationBuilder.CreateIndex(
                name: "IX_BaseSportSuit_FkBasePants",
                table: "BaseSportSuit",
                column: "FkBasePants");

            migrationBuilder.CreateIndex(
                name: "IX_BaseSportSuit_FkBasePantsCuffsLeft",
                table: "BaseSportSuit",
                column: "FkBasePantsCuffsLeft");

            migrationBuilder.CreateIndex(
                name: "IX_BaseSportSuit_FkBasePantsCuffsRight",
                table: "BaseSportSuit",
                column: "FkBasePantsCuffsRight");

            migrationBuilder.CreateIndex(
                name: "IX_BaseSportSuit_FkBaseSleeveCuffsLeft",
                table: "BaseSportSuit",
                column: "FkBaseSleeveCuffsLeft");

            migrationBuilder.CreateIndex(
                name: "IX_BaseSportSuit_FkBaseSleeveCuffsRight",
                table: "BaseSportSuit",
                column: "FkBaseSleeveCuffsRight");

            migrationBuilder.CreateIndex(
                name: "IX_BaseSportSuit_FkBaseSleeves",
                table: "BaseSportSuit",
                column: "FkBaseSleeves");

            migrationBuilder.CreateIndex(
                name: "IX_BaseSportSuit_FkBaseSweaters",
                table: "BaseSportSuit",
                column: "FkBaseSweaters");

            migrationBuilder.CreateIndex(
                name: "IX_Cart_FkCurrencies",
                table: "Cart",
                column: "FkCurrencies");

            migrationBuilder.CreateIndex(
                name: "IX_Cart_FkProductOrders",
                table: "Cart",
                column: "FkProductOrders");

            migrationBuilder.CreateIndex(
                name: "IX_Categories_FkLanguages",
                table: "Categories",
                column: "FkLanguages");

            migrationBuilder.CreateIndex(
                name: "IX_CategoryHierarchy_FkCategories",
                table: "CategoryHierarchy",
                column: "FkCategories");

            migrationBuilder.CreateIndex(
                name: "IX_CategoryHierarchy_ParentId",
                table: "CategoryHierarchy",
                column: "ParentId");

            migrationBuilder.CreateIndex(
                name: "IX_Contacts_FkLanguages",
                table: "Contacts",
                column: "FkLanguages");

            migrationBuilder.CreateIndex(
                name: "IX_CustomBelts_FkBaseBelts",
                table: "CustomBelts",
                column: "FkBaseBelts");

            migrationBuilder.CreateIndex(
                name: "IX_CustomNecklines_FkBaseNecklines",
                table: "CustomNecklines",
                column: "FkBaseNecklines");

            migrationBuilder.CreateIndex(
                name: "IX_CustomPants_FkBasePants",
                table: "CustomPants",
                column: "FkBasePants");

            migrationBuilder.CreateIndex(
                name: "IX_CustomPantsCuffs_FkBasePantCuffs",
                table: "CustomPantsCuffs",
                column: "FkBasePantCuffs");

            migrationBuilder.CreateIndex(
                name: "IX_CustomSleeveCuffs_FkBaseSleeveCuffs",
                table: "CustomSleeveCuffs",
                column: "FkBaseSleeveCuffs");

            migrationBuilder.CreateIndex(
                name: "IX_CustomSleeves_FkBaseSleeves",
                table: "CustomSleeves",
                column: "FkBaseSleeves");

            migrationBuilder.CreateIndex(
                name: "IX_CustomSportSuits_FkCustomBelts",
                table: "CustomSportSuits",
                column: "FkCustomBelts");

            migrationBuilder.CreateIndex(
                name: "IX_CustomSportSuits_FkCustomNecklines",
                table: "CustomSportSuits",
                column: "FkCustomNecklines");

            migrationBuilder.CreateIndex(
                name: "IX_CustomSportSuits_FkCustomPants",
                table: "CustomSportSuits",
                column: "FkCustomPants");

            migrationBuilder.CreateIndex(
                name: "IX_CustomSportSuits_FkCustomPantsCuffsLeft",
                table: "CustomSportSuits",
                column: "FkCustomPantsCuffsLeft");

            migrationBuilder.CreateIndex(
                name: "IX_CustomSportSuits_FkCustomPantsCuffsRight",
                table: "CustomSportSuits",
                column: "FkCustomPantsCuffsRight");

            migrationBuilder.CreateIndex(
                name: "IX_CustomSportSuits_FkCustomSleeveCuffsLeft",
                table: "CustomSportSuits",
                column: "FkCustomSleeveCuffsLeft");

            migrationBuilder.CreateIndex(
                name: "IX_CustomSportSuits_FkCustomSleeveCuffsRight",
                table: "CustomSportSuits",
                column: "FkCustomSleeveCuffsRight");

            migrationBuilder.CreateIndex(
                name: "IX_CustomSportSuits_FkCustomSleeves",
                table: "CustomSportSuits",
                column: "FkCustomSleeves");

            migrationBuilder.CreateIndex(
                name: "IX_CustomSportSuits_FkCustomSweaters",
                table: "CustomSportSuits",
                column: "FkCustomSweaters");

            migrationBuilder.CreateIndex(
                name: "IX_CustomSweaters_FkBaseSweaters",
                table: "CustomSweaters",
                column: "FkBaseSweaters");

            migrationBuilder.CreateIndex(
                name: "IX_CustomizableProducts_FkCustomSportSuits",
                table: "CustomizableProducts",
                column: "FkCustomSportSuits");

            migrationBuilder.CreateIndex(
                name: "IX_CustomizableProducts_FkFabricTypes",
                table: "CustomizableProducts",
                column: "FkFabricTypes");

            migrationBuilder.CreateIndex(
                name: "IX_CustomizableProducts_FkSizeOptions",
                table: "CustomizableProducts",
                column: "FkSizeOptions");

            migrationBuilder.CreateIndex(
                name: "IX_FabricTypes_FkCurrencies",
                table: "FabricTypes",
                column: "FkCurrencies");

            migrationBuilder.CreateIndex(
                name: "IX_OrderHistory_FkOrderStatuses",
                table: "OrderHistory",
                column: "FkOrderStatuses");

            migrationBuilder.CreateIndex(
                name: "IX_OrderHistory_FkOrders",
                table: "OrderHistory",
                column: "FkOrders");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_FkOrderStatus",
                table: "Orders",
                column: "FkOrderStatus");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_FkPayments",
                table: "Orders",
                column: "FkPayments");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_FkProductOrders",
                table: "Orders",
                column: "FkProductOrders");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_FkShippingAddresses",
                table: "Orders",
                column: "FkShippingAddresses");

            migrationBuilder.CreateIndex(
                name: "IX_Payments_FkCurrencies",
                table: "Payments",
                column: "FkCurrencies");

            migrationBuilder.CreateIndex(
                name: "IX_Payments_FkPaymentMethods",
                table: "Payments",
                column: "FkPaymentMethods");

            migrationBuilder.CreateIndex(
                name: "IX_Payments_FkPaymentStatuses",
                table: "Payments",
                column: "FkPaymentStatuses");

            migrationBuilder.CreateIndex(
                name: "IX_ProductImages_FkProducts",
                table: "ProductImages",
                column: "FkProducts");

            migrationBuilder.CreateIndex(
                name: "IX_ProductOrders_FkCustomizableProducts",
                table: "ProductOrders",
                column: "FkCustomizableProducts");

            migrationBuilder.CreateIndex(
                name: "IX_ProductOrders_FkProducts",
                table: "ProductOrders",
                column: "FkProducts");

            migrationBuilder.CreateIndex(
                name: "IX_ProductTranslations_FkCategories",
                table: "ProductTranslations",
                column: "FkCategories");

            migrationBuilder.CreateIndex(
                name: "IX_ProductTranslations_FkLanguages",
                table: "ProductTranslations",
                column: "FkLanguages");

            migrationBuilder.CreateIndex(
                name: "IX_ProductTranslations_FkProducts",
                table: "ProductTranslations",
                column: "FkProducts");

            migrationBuilder.CreateIndex(
                name: "IX_Products_FkColors",
                table: "Products",
                column: "FkColors");

            migrationBuilder.CreateIndex(
                name: "IX_Products_FkCurrencies",
                table: "Products",
                column: "FkCurrencies");

            migrationBuilder.CreateIndex(
                name: "IX_Products_FkFabricTypes",
                table: "Products",
                column: "FkFabricTypes");

            migrationBuilder.CreateIndex(
                name: "IX_Products_FkSizeOptions",
                table: "Products",
                column: "FkSizeOptions");

            migrationBuilder.CreateIndex(
                name: "IX_Reviews_FkProducts",
                table: "Reviews",
                column: "FkProducts");

            migrationBuilder.CreateIndex(
                name: "IX_Reviews_FkUsers",
                table: "Reviews",
                column: "FkUsers");

            migrationBuilder.CreateIndex(
                name: "IX_ShippingAddresses_FkCountries",
                table: "ShippingAddresses",
                column: "FkCountries");

            migrationBuilder.CreateIndex(
                name: "IX_UserOrderHistory_FkOrders",
                table: "UserOrderHistory",
                column: "FkOrders");

            migrationBuilder.CreateIndex(
                name: "IX_UserOrderHistory_FkUsers",
                table: "UserOrderHistory",
                column: "FkUsers");

            migrationBuilder.CreateIndex(
                name: "IX_UserProfiles_FkUsers",
                table: "UserProfiles",
                column: "FkUsers");

            migrationBuilder.CreateIndex(
                name: "IX_UserRoles_FkAccessLevels",
                table: "UserRoles",
                column: "FkAccessLevels");

            migrationBuilder.CreateIndex(
                name: "IX_Users_FkUserRoles",
                table: "Users",
                column: "FkUserRoles");

            migrationBuilder.CreateIndex(
                name: "Users_Email_key",
                table: "Users",
                column: "Email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "Users_Login_key",
                table: "Users",
                column: "Login",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "About");

            migrationBuilder.DropTable(
                name: "BaseSportSuit");

            migrationBuilder.DropTable(
                name: "Cart");

            migrationBuilder.DropTable(
                name: "CategoryHierarchy");

            migrationBuilder.DropTable(
                name: "Contacts");

            migrationBuilder.DropTable(
                name: "OrderHistory");

            migrationBuilder.DropTable(
                name: "ProductImages");

            migrationBuilder.DropTable(
                name: "ProductTranslations");

            migrationBuilder.DropTable(
                name: "Reviews");

            migrationBuilder.DropTable(
                name: "UserOrderHistory");

            migrationBuilder.DropTable(
                name: "UserProfiles");

            migrationBuilder.DropTable(
                name: "Categories");

            migrationBuilder.DropTable(
                name: "Orders");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Languages");

            migrationBuilder.DropTable(
                name: "OrderStatuses");

            migrationBuilder.DropTable(
                name: "Payments");

            migrationBuilder.DropTable(
                name: "ProductOrders");

            migrationBuilder.DropTable(
                name: "ShippingAddresses");

            migrationBuilder.DropTable(
                name: "UserRoles");

            migrationBuilder.DropTable(
                name: "PaymentMethods");

            migrationBuilder.DropTable(
                name: "PaymentStatuses");

            migrationBuilder.DropTable(
                name: "CustomizableProducts");

            migrationBuilder.DropTable(
                name: "Products");

            migrationBuilder.DropTable(
                name: "Countries");

            migrationBuilder.DropTable(
                name: "AccessLevels");

            migrationBuilder.DropTable(
                name: "CustomSportSuits");

            migrationBuilder.DropTable(
                name: "Colors");

            migrationBuilder.DropTable(
                name: "FabricTypes");

            migrationBuilder.DropTable(
                name: "SizeOptions");

            migrationBuilder.DropTable(
                name: "CustomBelts");

            migrationBuilder.DropTable(
                name: "CustomNecklines");

            migrationBuilder.DropTable(
                name: "CustomPantsCuffs");

            migrationBuilder.DropTable(
                name: "CustomPants");

            migrationBuilder.DropTable(
                name: "CustomSleeveCuffs");

            migrationBuilder.DropTable(
                name: "CustomSleeves");

            migrationBuilder.DropTable(
                name: "CustomSweaters");

            migrationBuilder.DropTable(
                name: "Currencies");

            migrationBuilder.DropTable(
                name: "BaseBelts");

            migrationBuilder.DropTable(
                name: "BaseNecklines");

            migrationBuilder.DropTable(
                name: "BasePantsCuffs");

            migrationBuilder.DropTable(
                name: "BasePants");

            migrationBuilder.DropTable(
                name: "BaseSleeveCuffs");

            migrationBuilder.DropTable(
                name: "BaseSleeves");

            migrationBuilder.DropTable(
                name: "BaseSweaters");
        }
    }
}
