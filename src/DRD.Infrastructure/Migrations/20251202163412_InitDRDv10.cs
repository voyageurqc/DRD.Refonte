using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DRD.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class InitDRDv10 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AccessType",
                columns: table => new
                {
                    AccessTypeCode = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    DescriptionFr = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    DescriptionEn = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    CreationDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ModificationDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AccessType", x => x.AccessTypeCode);
                });

            migrationBuilder.CreateTable(
                name: "ApplicationView",
                columns: table => new
                {
                    ViewCode = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Controller = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Action = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    DescriptionFr = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    DescriptionEn = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    CreationDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ModificationDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ApplicationView", x => x.ViewCode);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Client",
                columns: table => new
                {
                    ClientNumber = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ClientName1 = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    ClientName2 = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: true),
                    Address1 = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Address2 = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    City = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    CountryCode = table.Column<string>(type: "nvarchar(5)", maxLength: 5, nullable: false),
                    ProvinceCode = table.Column<string>(type: "nvarchar(5)", maxLength: 5, nullable: false),
                    PostalCode = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: false),
                    ContactName = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    Phone1 = table.Column<string>(type: "nvarchar(25)", maxLength: 25, nullable: false),
                    Phone2 = table.Column<string>(type: "nvarchar(25)", maxLength: 25, nullable: true),
                    Email = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    OrganizationType = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    OrganizationNumber = table.Column<int>(type: "int", nullable: true),
                    EmployeeNumber = table.Column<int>(type: "int", nullable: true),
                    EmployeeName = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: true),
                    EntryOrder = table.Column<string>(type: "nvarchar(5)", maxLength: 5, nullable: false),
                    McpaCode = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    TransferMode = table.Column<string>(type: "nvarchar(5)", maxLength: 5, nullable: false),
                    AccountType = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    ReportType = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    ServerLocation = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    CultureCode = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    ClientTypeDrd = table.Column<int>(type: "int", nullable: false),
                    CreationDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ModificationDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Client", x => x.ClientNumber);
                });

            migrationBuilder.CreateTable(
                name: "CodeSet",
                columns: table => new
                {
                    TypeCode = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Code = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    DescriptionFr = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    DescriptionEn = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreationDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ModificationDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CodeSet", x => new { x.TypeCode, x.Code });
                });

            migrationBuilder.CreateTable(
                name: "DatabaseTable",
                columns: table => new
                {
                    TableName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    RowCount = table.Column<long>(type: "bigint", nullable: false),
                    CreationDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ModificationDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DatabaseTable", x => x.TableName);
                });

            migrationBuilder.CreateTable(
                name: "Institution",
                columns: table => new
                {
                    InstitutionNumber = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    Name = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    CreationDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ModificationDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Institution", x => x.InstitutionNumber);
                });

            migrationBuilder.CreateTable(
                name: "WebMessage",
                columns: table => new
                {
                    MessageNumber = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TitleFr = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    TitleEn = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    ContentFr = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ContentEn = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MessageType = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    IsMandatory = table.Column<bool>(type: "bit", nullable: false),
                    RequiresConfirmation = table.Column<bool>(type: "bit", nullable: false),
                    StartDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    EndDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreationDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ModificationDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WebMessage", x => x.MessageNumber);
                });

            migrationBuilder.CreateTable(
                name: "ApplicationUser",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    AddressLine = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    City = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Province = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    PostalCode = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: true),
                    DefaultPrinter = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    LaserPrinter = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    SectorCode = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    AccessTypeCode = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    MenuCode = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    CreationDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    ModificationDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedBy = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    UserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    Email = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SecurityStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "bit", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "bit", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ApplicationUser", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ApplicationUser_AccessType_AccessTypeCode",
                        column: x => x.AccessTypeCode,
                        principalTable: "AccessType",
                        principalColumn: "AccessTypeCode",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ClientDetail",
                columns: table => new
                {
                    ClientNumber = table.Column<int>(type: "int", nullable: false),
                    DrdNumber = table.Column<int>(type: "int", nullable: false),
                    TransactionType = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    TransactionCode = table.Column<int>(type: "int", nullable: false),
                    FrequencyCode = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    DelayCode = table.Column<int>(type: "int", nullable: false),
                    TransitNumber = table.Column<long>(type: "bigint", nullable: false),
                    TransitId = table.Column<int>(type: "int", nullable: false),
                    Transit = table.Column<int>(type: "int", nullable: false),
                    FolioNumber = table.Column<long>(type: "bigint", nullable: false),
                    CheckDigit = table.Column<int>(type: "int", nullable: false),
                    DrdFileNumber = table.Column<int>(type: "int", nullable: false),
                    IndividualNumber = table.Column<int>(type: "int", nullable: false),
                    LastTransactionDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IntegrationDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CompletedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastInvoiceDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    NextInvoiceDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ContactName = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    Phone1 = table.Column<long>(type: "bigint", nullable: false),
                    Phone2 = table.Column<long>(type: "bigint", nullable: false),
                    AutonomousFlag = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CurrencyCode = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FileFormat = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CompanyLimitAmount = table.Column<int>(type: "int", nullable: false),
                    IndividualLimitAmount = table.Column<int>(type: "int", nullable: false),
                    NextFileNumberToValidate = table.Column<int>(type: "int", nullable: false),
                    DepartureReason = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    TransitFeeNumber = table.Column<long>(type: "bigint", nullable: false),
                    FolioFeeNumber = table.Column<long>(type: "bigint", nullable: false),
                    InvoiceCalculationMode = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    InvoiceFrequencyCode = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    GLAccount = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    InvoiceNumber = table.Column<int>(type: "int", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    InternalUseOnly = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    TransmissionInProgress = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    DepositWithdrawalCode = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    SecondaryDrdNumber = table.Column<int>(type: "int", nullable: false),
                    MasterClientFlag = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SignatureCount = table.Column<int>(type: "int", nullable: false),
                    AuthorizationLevel = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    PreDepositDelay = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    PayingClient = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    CreationDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ModificationDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClientDetail", x => new { x.ClientNumber, x.DrdNumber });
                    table.ForeignKey(
                        name: "FK_ClientDetail_Client_ClientNumber",
                        column: x => x.ClientNumber,
                        principalTable: "Client",
                        principalColumn: "ClientNumber",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Branch",
                columns: table => new
                {
                    InstitutionNumber = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    BranchNumber = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    BranchName = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: true),
                    AddressLine = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    City = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    ProvinceCode = table.Column<string>(type: "nvarchar(5)", maxLength: 5, nullable: false),
                    PostalCode = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: false),
                    CreationDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ModificationDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Branch", x => new { x.InstitutionNumber, x.BranchNumber });
                    table.ForeignKey(
                        name: "FK_Branch_Institution_InstitutionNumber",
                        column: x => x.InstitutionNumber,
                        principalTable: "Institution",
                        principalColumn: "InstitutionNumber",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "WebMessageLink",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    WebMessageId = table.Column<int>(type: "int", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Url = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    CreationDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ModificationDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WebMessageLink", x => x.Id);
                    table.ForeignKey(
                        name: "FK_WebMessageLink_WebMessage_WebMessageId",
                        column: x => x.WebMessageId,
                        principalTable: "WebMessage",
                        principalColumn: "MessageNumber",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "WebMessageUser",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    WebMessageId = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    ReadDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ActionDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Accepted = table.Column<bool>(type: "bit", nullable: true),
                    CreationDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ModificationDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WebMessageUser", x => x.Id);
                    table.ForeignKey(
                        name: "FK_WebMessageUser_WebMessage_WebMessageId",
                        column: x => x.WebMessageId,
                        principalTable: "WebMessage",
                        principalColumn: "MessageNumber",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUserClaims_ApplicationUser_UserId",
                        column: x => x.UserId,
                        principalTable: "ApplicationUser",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderKey = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderDisplayName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_AspNetUserLogins_ApplicationUser_UserId",
                        column: x => x.UserId,
                        principalTable: "ApplicationUser",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_ApplicationUser_UserId",
                        column: x => x.UserId,
                        principalTable: "ApplicationUser",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_AspNetUserTokens_ApplicationUser_UserId",
                        column: x => x.UserId,
                        principalTable: "ApplicationUser",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserViewAccess",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", maxLength: 450, nullable: false),
                    ViewCode = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    PrivilegeCode = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    CreationDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ModificationDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserViewAccess", x => new { x.UserId, x.ViewCode });
                    table.ForeignKey(
                        name: "FK_UserViewAccess_ApplicationUser_UserId",
                        column: x => x.UserId,
                        principalTable: "ApplicationUser",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserViewAccess_ApplicationView_ViewCode",
                        column: x => x.ViewCode,
                        principalTable: "ApplicationView",
                        principalColumn: "ViewCode",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Individual",
                columns: table => new
                {
                    ClientNumber = table.Column<int>(type: "int", nullable: false),
                    DrdNumber = table.Column<int>(type: "int", nullable: false),
                    IndividualNumber = table.Column<int>(type: "int", nullable: false),
                    IndividualName = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    IndividualName1 = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: true),
                    IndividualName2 = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: true),
                    Address1 = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    Address2 = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    City = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    CountryCode = table.Column<string>(type: "nvarchar(5)", maxLength: 5, nullable: true),
                    ProvinceCode = table.Column<string>(type: "nvarchar(5)", maxLength: 5, nullable: true),
                    PostalCode = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: true),
                    Email = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: true),
                    Phone1 = table.Column<string>(type: "nvarchar(25)", maxLength: 25, nullable: true),
                    Phone2 = table.Column<string>(type: "nvarchar(25)", maxLength: 25, nullable: true),
                    LanguageCode = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true),
                    TransitNumber = table.Column<long>(type: "bigint", nullable: true),
                    TransitId = table.Column<int>(type: "int", nullable: true),
                    Transit = table.Column<int>(type: "int", nullable: true),
                    FolioNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CheckDigit = table.Column<int>(type: "int", nullable: true),
                    TransactionCode = table.Column<int>(type: "int", nullable: true),
                    RepeatCount = table.Column<int>(type: "int", nullable: true),
                    FrequencyCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TransferAmount = table.Column<int>(type: "int", nullable: true),
                    PreviousTransferAmount = table.Column<int>(type: "int", nullable: true),
                    MaxAmount = table.Column<int>(type: "int", nullable: true),
                    TotalAmount = table.Column<int>(type: "int", nullable: true),
                    EndDate = table.Column<int>(type: "int", nullable: true),
                    NextTransactionDate = table.Column<long>(type: "bigint", nullable: true),
                    LastTransactionDate = table.Column<int>(type: "int", nullable: true),
                    Reference = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    TransferDay = table.Column<int>(type: "int", nullable: true),
                    AutoTransitNumber = table.Column<long>(type: "bigint", nullable: true),
                    AutoTransitId = table.Column<int>(type: "int", nullable: true),
                    AutoTransit = table.Column<int>(type: "int", nullable: true),
                    AutoFolioNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AutoCheckDigit = table.Column<int>(type: "int", nullable: true),
                    StartDate = table.Column<int>(type: "int", nullable: true),
                    KeyName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    TransferLimitAmount = table.Column<int>(type: "int", nullable: true),
                    CreationDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ModificationDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Individual", x => new { x.ClientNumber, x.DrdNumber, x.IndividualNumber });
                    table.ForeignKey(
                        name: "FK_Individual_ClientDetail_ClientNumber_DrdNumber",
                        columns: x => new { x.ClientNumber, x.DrdNumber },
                        principalTable: "ClientDetail",
                        principalColumns: new[] { "ClientNumber", "DrdNumber" },
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "ApplicationUser",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "IX_ApplicationUser_AccessTypeCode",
                table: "ApplicationUser",
                column: "AccessTypeCode");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "ApplicationUser",
                column: "NormalizedUserName",
                unique: true,
                filter: "[NormalizedUserName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true,
                filter: "[NormalizedName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserClaims_UserId",
                table: "AspNetUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserLogins_UserId",
                table: "AspNetUserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_UserViewAccess_ViewCode",
                table: "UserViewAccess",
                column: "ViewCode");

            migrationBuilder.CreateIndex(
                name: "IX_WebMessageLink_WebMessageId",
                table: "WebMessageLink",
                column: "WebMessageId");

            migrationBuilder.CreateIndex(
                name: "IX_WebMessageUser_WebMessageId",
                table: "WebMessageUser",
                column: "WebMessageId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AspNetRoleClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins");

            migrationBuilder.DropTable(
                name: "AspNetUserRoles");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens");

            migrationBuilder.DropTable(
                name: "Branch");

            migrationBuilder.DropTable(
                name: "CodeSet");

            migrationBuilder.DropTable(
                name: "DatabaseTable");

            migrationBuilder.DropTable(
                name: "Individual");

            migrationBuilder.DropTable(
                name: "UserViewAccess");

            migrationBuilder.DropTable(
                name: "WebMessageLink");

            migrationBuilder.DropTable(
                name: "WebMessageUser");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "Institution");

            migrationBuilder.DropTable(
                name: "ClientDetail");

            migrationBuilder.DropTable(
                name: "ApplicationUser");

            migrationBuilder.DropTable(
                name: "ApplicationView");

            migrationBuilder.DropTable(
                name: "WebMessage");

            migrationBuilder.DropTable(
                name: "Client");

            migrationBuilder.DropTable(
                name: "AccessType");
        }
    }
}
