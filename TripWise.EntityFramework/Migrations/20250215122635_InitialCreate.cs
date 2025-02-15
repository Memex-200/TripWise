using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TripWise.EntityFramework.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Agents",
                columns: table => new
                {
                    AgentCode = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Active = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Agents", x => x.AgentCode);
                });

            migrationBuilder.CreateTable(
                name: "CompanyTypes",
                columns: table => new
                {
                    CompanyTypeId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TypeName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CompanyTypes", x => x.CompanyTypeId);
                });

            migrationBuilder.CreateTable(
                name: "Country",
                columns: table => new
                {
                    CountryCode = table.Column<string>(type: "nvarchar(3)", maxLength: 3, nullable: false),
                    CountryName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Country", x => x.CountryCode);
                });

            migrationBuilder.CreateTable(
                name: "Customers",
                columns: table => new
                {
                    CustomerId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Address = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Details = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Phone = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Mobile = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    CustomerFrom = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Customers", x => x.CustomerId);
                });

            migrationBuilder.CreateTable(
                name: "RoomTypes",
                columns: table => new
                {
                    RoomTypeId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TypeName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RoomTypes", x => x.RoomTypeId);
                });

            migrationBuilder.CreateTable(
                name: "TicketTypes",
                columns: table => new
                {
                    TicketTypeId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TypeName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TicketTypes", x => x.TicketTypeId);
                });

            migrationBuilder.CreateTable(
                name: "PromoOffers",
                columns: table => new
                {
                    PromoOfferCode = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PromoOfferName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    ActiveFrom = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ActiveTo = table.Column<DateTime>(type: "datetime2", nullable: false),
                    AgentCode = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PromoOffers", x => x.PromoOfferCode);
                    table.ForeignKey(
                        name: "FK_PromoOffers_Agents_AgentCode",
                        column: x => x.AgentCode,
                        principalTable: "Agents",
                        principalColumn: "AgentCode");
                });

            migrationBuilder.CreateTable(
                name: "City",
                columns: table => new
                {
                    CityId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CityName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    CountryCode = table.Column<string>(type: "nvarchar(3)", maxLength: 3, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_City", x => x.CityId);
                    table.ForeignKey(
                        name: "FK_City_Country_CountryCode",
                        column: x => x.CountryCode,
                        principalTable: "Country",
                        principalColumn: "CountryCode",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Offers",
                columns: table => new
                {
                    OfferCode = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OfferName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    TimeCreated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ActiveFrom = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ActiveTo = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TimeAccepted = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Accepted = table.Column<bool>(type: "bit", nullable: false),
                    PromoOfferId = table.Column<int>(type: "int", nullable: true),
                    AgentId = table.Column<int>(type: "int", nullable: false),
                    CustomerId = table.Column<int>(type: "int", nullable: false),
                    AgentCode = table.Column<int>(type: "int", nullable: true),
                    CustomerId1 = table.Column<int>(type: "int", nullable: true),
                    PromoOfferCode = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Offers", x => x.OfferCode);
                    table.ForeignKey(
                        name: "FK_Offers_Agents_AgentCode",
                        column: x => x.AgentCode,
                        principalTable: "Agents",
                        principalColumn: "AgentCode");
                    table.ForeignKey(
                        name: "FK_Offers_Agents_AgentId",
                        column: x => x.AgentId,
                        principalTable: "Agents",
                        principalColumn: "AgentCode",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Offers_Customers_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "Customers",
                        principalColumn: "CustomerId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Offers_Customers_CustomerId1",
                        column: x => x.CustomerId1,
                        principalTable: "Customers",
                        principalColumn: "CustomerId");
                    table.ForeignKey(
                        name: "FK_Offers_PromoOffers_PromoOfferCode",
                        column: x => x.PromoOfferCode,
                        principalTable: "PromoOffers",
                        principalColumn: "PromoOfferCode");
                    table.ForeignKey(
                        name: "FK_Offers_PromoOffers_PromoOfferId",
                        column: x => x.PromoOfferId,
                        principalTable: "PromoOffers",
                        principalColumn: "PromoOfferCode");
                });

            migrationBuilder.CreateTable(
                name: "Hotels",
                columns: table => new
                {
                    HotelId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    HotelName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    HotelAddress = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Details = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsPartner = table.Column<bool>(type: "bit", nullable: false),
                    Active = table.Column<bool>(type: "bit", nullable: false),
                    CityId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Hotels", x => x.HotelId);
                    table.ForeignKey(
                        name: "FK_Hotels_City_CityId",
                        column: x => x.CityId,
                        principalTable: "City",
                        principalColumn: "CityId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Transport_Company",
                columns: table => new
                {
                    CompanyId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CompanyName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    HQAddress = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsPartner = table.Column<bool>(type: "bit", nullable: false),
                    Active = table.Column<bool>(type: "bit", nullable: false),
                    CityId = table.Column<int>(type: "int", nullable: false),
                    CompanyTypeId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Transport_Company", x => x.CompanyId);
                    table.ForeignKey(
                        name: "FK_Transport_Company_City_CityId",
                        column: x => x.CityId,
                        principalTable: "City",
                        principalColumn: "CityId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Transport_Company_CompanyTypes_CompanyTypeId",
                        column: x => x.CompanyTypeId,
                        principalTable: "CompanyTypes",
                        principalColumn: "CompanyTypeId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Contracts",
                columns: table => new
                {
                    ContractCode = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CustomerId = table.Column<int>(type: "int", nullable: false),
                    AgentId = table.Column<int>(type: "int", nullable: false),
                    OfferCode = table.Column<int>(type: "int", nullable: false),
                    TimeSigned = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TotalPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    PaymentDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    PaymentTime = table.Column<TimeSpan>(type: "time", nullable: false),
                    PaymentAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Refunded = table.Column<bool>(type: "bit", nullable: false),
                    RefundedTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    RefundedAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    AgentCode = table.Column<int>(type: "int", nullable: true),
                    CustomerId1 = table.Column<int>(type: "int", nullable: true),
                    OfferCode1 = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Contracts", x => x.ContractCode);
                    table.ForeignKey(
                        name: "FK_Contracts_Agents_AgentCode",
                        column: x => x.AgentCode,
                        principalTable: "Agents",
                        principalColumn: "AgentCode");
                    table.ForeignKey(
                        name: "FK_Contracts_Agents_AgentId",
                        column: x => x.AgentId,
                        principalTable: "Agents",
                        principalColumn: "AgentCode",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Contracts_Customers_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "Customers",
                        principalColumn: "CustomerId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Contracts_Customers_CustomerId1",
                        column: x => x.CustomerId1,
                        principalTable: "Customers",
                        principalColumn: "CustomerId");
                    table.ForeignKey(
                        name: "FK_Contracts_Offers_OfferCode",
                        column: x => x.OfferCode,
                        principalTable: "Offers",
                        principalColumn: "OfferCode");
                    table.ForeignKey(
                        name: "FK_Contracts_Offers_OfferCode1",
                        column: x => x.OfferCode1,
                        principalTable: "Offers",
                        principalColumn: "OfferCode");
                });

            migrationBuilder.CreateTable(
                name: "Hotel_Service",
                columns: table => new
                {
                    HotelServiceId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    HotelId = table.Column<int>(type: "int", nullable: false),
                    RoomTypeId = table.Column<int>(type: "int", nullable: false),
                    ServicePrice = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Active = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Hotel_Service", x => x.HotelServiceId);
                    table.ForeignKey(
                        name: "FK_Hotel_Service_Hotels_HotelId",
                        column: x => x.HotelId,
                        principalTable: "Hotels",
                        principalColumn: "HotelId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Hotel_Service_RoomTypes_RoomTypeId",
                        column: x => x.RoomTypeId,
                        principalTable: "RoomTypes",
                        principalColumn: "RoomTypeId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Transport_Service",
                columns: table => new
                {
                    TransportServiceId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TransportCompanyId = table.Column<int>(type: "int", nullable: false),
                    TicketTypeId = table.Column<int>(type: "int", nullable: false),
                    FromCityId = table.Column<int>(type: "int", nullable: false),
                    ToCityId = table.Column<int>(type: "int", nullable: false),
                    ServicePrice = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Active = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Transport_Service", x => x.TransportServiceId);
                    table.ForeignKey(
                        name: "FK_Transport_Service_City_FromCityId",
                        column: x => x.FromCityId,
                        principalTable: "City",
                        principalColumn: "CityId");
                    table.ForeignKey(
                        name: "FK_Transport_Service_City_ToCityId",
                        column: x => x.ToCityId,
                        principalTable: "City",
                        principalColumn: "CityId");
                    table.ForeignKey(
                        name: "FK_Transport_Service_TicketTypes_TicketTypeId",
                        column: x => x.TicketTypeId,
                        principalTable: "TicketTypes",
                        principalColumn: "TicketTypeId");
                    table.ForeignKey(
                        name: "FK_Transport_Service_Transport_Company_TransportCompanyId",
                        column: x => x.TransportCompanyId,
                        principalTable: "Transport_Company",
                        principalColumn: "CompanyId");
                });

            migrationBuilder.CreateTable(
                name: "OfferHotelServices",
                columns: table => new
                {
                    OfferCode = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    HotelServiceId = table.Column<int>(type: "int", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    DiscountPercent = table.Column<int>(type: "int", nullable: false),
                    FinalServicePrice = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    OfferCode1 = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OfferHotelServices", x => x.OfferCode);
                    table.ForeignKey(
                        name: "FK_OfferHotelServices_Hotel_Service_HotelServiceId",
                        column: x => x.HotelServiceId,
                        principalTable: "Hotel_Service",
                        principalColumn: "HotelServiceId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_OfferHotelServices_Offers_OfferCode1",
                        column: x => x.OfferCode1,
                        principalTable: "Offers",
                        principalColumn: "OfferCode",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PromoOfferHotelServices",
                columns: table => new
                {
                    PromoOfferId = table.Column<int>(type: "int", nullable: false),
                    HotelServiceId = table.Column<int>(type: "int", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    DiscountPercent = table.Column<int>(type: "int", nullable: false),
                    FinalServicePrice = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PromoOfferHotelServices", x => new { x.PromoOfferId, x.HotelServiceId });
                    table.ForeignKey(
                        name: "FK_PromoOfferHotelServices_Hotel_Service_HotelServiceId",
                        column: x => x.HotelServiceId,
                        principalTable: "Hotel_Service",
                        principalColumn: "HotelServiceId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PromoOfferHotelServices_PromoOffers_PromoOfferId",
                        column: x => x.PromoOfferId,
                        principalTable: "PromoOffers",
                        principalColumn: "PromoOfferCode",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "OfferTransportServices",
                columns: table => new
                {
                    OfferCode = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TransportServiceId = table.Column<int>(type: "int", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    DiscountPercent = table.Column<int>(type: "int", nullable: false),
                    FinalServicePrice = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    OfferCode1 = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OfferTransportServices", x => x.OfferCode);
                    table.ForeignKey(
                        name: "FK_OfferTransportServices_Offers_OfferCode1",
                        column: x => x.OfferCode1,
                        principalTable: "Offers",
                        principalColumn: "OfferCode",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_OfferTransportServices_Transport_Service_TransportServiceId",
                        column: x => x.TransportServiceId,
                        principalTable: "Transport_Service",
                        principalColumn: "TransportServiceId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PromoOfferTransportServices",
                columns: table => new
                {
                    PromoOfferId = table.Column<int>(type: "int", nullable: false),
                    TransportServiceId = table.Column<int>(type: "int", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    DiscountPercent = table.Column<int>(type: "int", nullable: false),
                    FinalServicePrice = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TransportServiceId1 = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PromoOfferTransportServices", x => new { x.PromoOfferId, x.TransportServiceId });
                    table.ForeignKey(
                        name: "FK_PromoOfferTransportServices_PromoOffers_PromoOfferId",
                        column: x => x.PromoOfferId,
                        principalTable: "PromoOffers",
                        principalColumn: "PromoOfferCode",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PromoOfferTransportServices_Transport_Service_TransportServiceId",
                        column: x => x.TransportServiceId,
                        principalTable: "Transport_Service",
                        principalColumn: "TransportServiceId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PromoOfferTransportServices_Transport_Service_TransportServiceId1",
                        column: x => x.TransportServiceId1,
                        principalTable: "Transport_Service",
                        principalColumn: "TransportServiceId");
                });

            migrationBuilder.CreateIndex(
                name: "IX_City_CountryCode",
                table: "City",
                column: "CountryCode");

            migrationBuilder.CreateIndex(
                name: "IX_Contracts_AgentCode",
                table: "Contracts",
                column: "AgentCode");

            migrationBuilder.CreateIndex(
                name: "IX_Contracts_AgentId",
                table: "Contracts",
                column: "AgentId");

            migrationBuilder.CreateIndex(
                name: "IX_Contracts_CustomerId",
                table: "Contracts",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_Contracts_CustomerId1",
                table: "Contracts",
                column: "CustomerId1");

            migrationBuilder.CreateIndex(
                name: "IX_Contracts_OfferCode",
                table: "Contracts",
                column: "OfferCode");

            migrationBuilder.CreateIndex(
                name: "IX_Contracts_OfferCode1",
                table: "Contracts",
                column: "OfferCode1");

            migrationBuilder.CreateIndex(
                name: "IX_Hotel_Service_HotelId",
                table: "Hotel_Service",
                column: "HotelId");

            migrationBuilder.CreateIndex(
                name: "IX_Hotel_Service_RoomTypeId",
                table: "Hotel_Service",
                column: "RoomTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Hotels_CityId",
                table: "Hotels",
                column: "CityId");

            migrationBuilder.CreateIndex(
                name: "IX_OfferHotelServices_HotelServiceId",
                table: "OfferHotelServices",
                column: "HotelServiceId");

            migrationBuilder.CreateIndex(
                name: "IX_OfferHotelServices_OfferCode1",
                table: "OfferHotelServices",
                column: "OfferCode1");

            migrationBuilder.CreateIndex(
                name: "IX_Offers_AgentCode",
                table: "Offers",
                column: "AgentCode");

            migrationBuilder.CreateIndex(
                name: "IX_Offers_AgentId",
                table: "Offers",
                column: "AgentId");

            migrationBuilder.CreateIndex(
                name: "IX_Offers_CustomerId",
                table: "Offers",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_Offers_CustomerId1",
                table: "Offers",
                column: "CustomerId1");

            migrationBuilder.CreateIndex(
                name: "IX_Offers_PromoOfferCode",
                table: "Offers",
                column: "PromoOfferCode");

            migrationBuilder.CreateIndex(
                name: "IX_Offers_PromoOfferId",
                table: "Offers",
                column: "PromoOfferId");

            migrationBuilder.CreateIndex(
                name: "IX_OfferTransportServices_OfferCode1",
                table: "OfferTransportServices",
                column: "OfferCode1");

            migrationBuilder.CreateIndex(
                name: "IX_OfferTransportServices_TransportServiceId",
                table: "OfferTransportServices",
                column: "TransportServiceId");

            migrationBuilder.CreateIndex(
                name: "IX_PromoOfferHotelServices_HotelServiceId",
                table: "PromoOfferHotelServices",
                column: "HotelServiceId");

            migrationBuilder.CreateIndex(
                name: "IX_PromoOffers_AgentCode",
                table: "PromoOffers",
                column: "AgentCode");

            migrationBuilder.CreateIndex(
                name: "IX_PromoOfferTransportServices_TransportServiceId",
                table: "PromoOfferTransportServices",
                column: "TransportServiceId");

            migrationBuilder.CreateIndex(
                name: "IX_PromoOfferTransportServices_TransportServiceId1",
                table: "PromoOfferTransportServices",
                column: "TransportServiceId1");

            migrationBuilder.CreateIndex(
                name: "IX_Transport_Company_CityId",
                table: "Transport_Company",
                column: "CityId");

            migrationBuilder.CreateIndex(
                name: "IX_Transport_Company_CompanyTypeId",
                table: "Transport_Company",
                column: "CompanyTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Transport_Service_FromCityId",
                table: "Transport_Service",
                column: "FromCityId");

            migrationBuilder.CreateIndex(
                name: "IX_Transport_Service_TicketTypeId",
                table: "Transport_Service",
                column: "TicketTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Transport_Service_ToCityId",
                table: "Transport_Service",
                column: "ToCityId");

            migrationBuilder.CreateIndex(
                name: "IX_Transport_Service_TransportCompanyId",
                table: "Transport_Service",
                column: "TransportCompanyId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Contracts");

            migrationBuilder.DropTable(
                name: "OfferHotelServices");

            migrationBuilder.DropTable(
                name: "OfferTransportServices");

            migrationBuilder.DropTable(
                name: "PromoOfferHotelServices");

            migrationBuilder.DropTable(
                name: "PromoOfferTransportServices");

            migrationBuilder.DropTable(
                name: "Offers");

            migrationBuilder.DropTable(
                name: "Hotel_Service");

            migrationBuilder.DropTable(
                name: "Transport_Service");

            migrationBuilder.DropTable(
                name: "Customers");

            migrationBuilder.DropTable(
                name: "PromoOffers");

            migrationBuilder.DropTable(
                name: "Hotels");

            migrationBuilder.DropTable(
                name: "RoomTypes");

            migrationBuilder.DropTable(
                name: "TicketTypes");

            migrationBuilder.DropTable(
                name: "Transport_Company");

            migrationBuilder.DropTable(
                name: "Agents");

            migrationBuilder.DropTable(
                name: "City");

            migrationBuilder.DropTable(
                name: "CompanyTypes");

            migrationBuilder.DropTable(
                name: "Country");
        }
    }
}
