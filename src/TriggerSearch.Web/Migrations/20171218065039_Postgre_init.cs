using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace TriggerSearch.Web.Migrations
{
    public partial class Postgre_init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Groups",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uuid", nullable: false),
                    Created = table.Column<DateTime>(type: "timestamp", nullable: false, defaultValueSql: "now()"),
                    CreaterID = table.Column<Guid>(type: "uuid", nullable: true),
                    IsDefault = table.Column<bool>(type: "bool", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bool", nullable: true),
                    Modified = table.Column<DateTime>(type: "timestamp", nullable: true),
                    ModifierID = table.Column<Guid>(type: "uuid", nullable: true),
                    Note = table.Column<string>(type: "text", nullable: true),
                    Title = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Groups", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Roles",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uuid", nullable: false),
                    Created = table.Column<DateTime>(type: "timestamp", nullable: false, defaultValueSql: "now()"),
                    CreaterID = table.Column<Guid>(type: "uuid", nullable: true),
                    IsDefault = table.Column<bool>(type: "bool", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bool", nullable: true),
                    Modified = table.Column<DateTime>(type: "timestamp", nullable: true),
                    ModifierID = table.Column<Guid>(type: "uuid", nullable: true),
                    Note = table.Column<string>(type: "text", nullable: true),
                    Title = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Roles", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "TypeObjects",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uuid", nullable: false),
                    Created = table.Column<DateTime>(type: "timestamp", nullable: false, defaultValueSql: "now()"),
                    CreaterID = table.Column<Guid>(type: "uuid", nullable: true),
                    Name = table.Column<string>(type: "text", nullable: true),
                    Title = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TypeObjects", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uuid", nullable: false),
                    AvatarURL = table.Column<string>(type: "text", nullable: true),
                    Created = table.Column<DateTime>(type: "timestamp", nullable: false, defaultValueSql: "now()"),
                    CreaterID = table.Column<Guid>(type: "uuid", nullable: true),
                    Email = table.Column<string>(type: "text", nullable: true),
                    FirstName = table.Column<string>(type: "text", nullable: true),
                    IsDefault = table.Column<bool>(type: "bool", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bool", nullable: true),
                    IsLock = table.Column<bool>(type: "bool", nullable: true),
                    LastLogin = table.Column<DateTime>(type: "timestamp", nullable: true),
                    LastName = table.Column<string>(type: "text", nullable: true),
                    Modified = table.Column<DateTime>(type: "timestamp", nullable: true),
                    ModifierID = table.Column<Guid>(type: "uuid", nullable: true),
                    PhoneNumber = table.Column<string>(type: "text", nullable: true),
                    UserName = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "GroupRoles",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uuid", nullable: false),
                    Created = table.Column<DateTime>(type: "timestamp", nullable: false, defaultValueSql: "now()"),
                    CreaterID = table.Column<Guid>(type: "uuid", nullable: true),
                    GroupID = table.Column<Guid>(type: "uuid", nullable: false),
                    RoleID = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GroupRoles", x => x.ID);
                    table.ForeignKey(
                        name: "FK_GroupRoles_Groups_GroupID",
                        column: x => x.GroupID,
                        principalTable: "Groups",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_GroupRoles_Roles_RoleID",
                        column: x => x.RoleID,
                        principalTable: "Roles",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Permits",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uuid", nullable: false),
                    Created = table.Column<DateTime>(type: "timestamp", nullable: false, defaultValueSql: "now()"),
                    CreaterID = table.Column<Guid>(type: "uuid", nullable: true),
                    Name = table.Column<string>(type: "text", nullable: true),
                    Note = table.Column<string>(type: "text", nullable: true),
                    Title = table.Column<string>(type: "text", nullable: true),
                    TypeObjectID = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Permits", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Permits_TypeObjects_TypeObjectID",
                        column: x => x.TypeObjectID,
                        principalTable: "TypeObjects",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "GroupUsers",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uuid", nullable: false),
                    Created = table.Column<DateTime>(type: "timestamp", nullable: false, defaultValueSql: "now()"),
                    CreaterID = table.Column<Guid>(type: "uuid", nullable: true),
                    GroupID = table.Column<Guid>(type: "uuid", nullable: false),
                    UserID = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GroupUsers", x => x.ID);
                    table.ForeignKey(
                        name: "FK_GroupUsers_Groups_GroupID",
                        column: x => x.GroupID,
                        principalTable: "Groups",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_GroupUsers_Users_UserID",
                        column: x => x.UserID,
                        principalTable: "Users",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserRoles",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uuid", nullable: false),
                    Created = table.Column<DateTime>(type: "timestamp", nullable: false, defaultValueSql: "now()"),
                    CreaterID = table.Column<Guid>(type: "uuid", nullable: true),
                    RoleID = table.Column<Guid>(type: "uuid", nullable: false),
                    UserID = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserRoles", x => x.ID);
                    table.ForeignKey(
                        name: "FK_UserRoles_Roles_RoleID",
                        column: x => x.RoleID,
                        principalTable: "Roles",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserRoles_Users_UserID",
                        column: x => x.UserID,
                        principalTable: "Users",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ObjectShareUsers",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uuid", nullable: false),
                    Created = table.Column<DateTime>(type: "timestamp", nullable: false, defaultValueSql: "now()"),
                    CreaterID = table.Column<Guid>(type: "uuid", nullable: true),
                    PermitID = table.Column<Guid>(type: "uuid", nullable: false),
                    TypeObjectID = table.Column<Guid>(type: "uuid", nullable: false),
                    UserID = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ObjectShareUsers", x => x.ID);
                    table.ForeignKey(
                        name: "FK_ObjectShareUsers_Permits_PermitID",
                        column: x => x.PermitID,
                        principalTable: "Permits",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ObjectShareUsers_TypeObjects_TypeObjectID",
                        column: x => x.TypeObjectID,
                        principalTable: "TypeObjects",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ObjectShareUsers_Users_UserID",
                        column: x => x.UserID,
                        principalTable: "Users",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RolePermits",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uuid", nullable: false),
                    Created = table.Column<DateTime>(type: "timestamp", nullable: false, defaultValueSql: "now()"),
                    CreaterID = table.Column<Guid>(type: "uuid", nullable: true),
                    PermitID = table.Column<Guid>(type: "uuid", nullable: false),
                    RoleID = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RolePermits", x => x.ID);
                    table.ForeignKey(
                        name: "FK_RolePermits_Permits_PermitID",
                        column: x => x.PermitID,
                        principalTable: "Permits",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RolePermits_Roles_RoleID",
                        column: x => x.RoleID,
                        principalTable: "Roles",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_GroupRoles_GroupID",
                table: "GroupRoles",
                column: "GroupID");

            migrationBuilder.CreateIndex(
                name: "IX_GroupRoles_RoleID",
                table: "GroupRoles",
                column: "RoleID");

            migrationBuilder.CreateIndex(
                name: "IX_GroupUsers_GroupID",
                table: "GroupUsers",
                column: "GroupID");

            migrationBuilder.CreateIndex(
                name: "IX_GroupUsers_UserID",
                table: "GroupUsers",
                column: "UserID");

            migrationBuilder.CreateIndex(
                name: "IX_ObjectShareUsers_PermitID",
                table: "ObjectShareUsers",
                column: "PermitID");

            migrationBuilder.CreateIndex(
                name: "IX_ObjectShareUsers_TypeObjectID",
                table: "ObjectShareUsers",
                column: "TypeObjectID");

            migrationBuilder.CreateIndex(
                name: "IX_ObjectShareUsers_UserID",
                table: "ObjectShareUsers",
                column: "UserID");

            migrationBuilder.CreateIndex(
                name: "IX_Permits_TypeObjectID",
                table: "Permits",
                column: "TypeObjectID");

            migrationBuilder.CreateIndex(
                name: "IX_RolePermits_PermitID",
                table: "RolePermits",
                column: "PermitID");

            migrationBuilder.CreateIndex(
                name: "IX_RolePermits_RoleID",
                table: "RolePermits",
                column: "RoleID");

            migrationBuilder.CreateIndex(
                name: "IX_UserRoles_RoleID",
                table: "UserRoles",
                column: "RoleID");

            migrationBuilder.CreateIndex(
                name: "IX_UserRoles_UserID",
                table: "UserRoles",
                column: "UserID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "GroupRoles");

            migrationBuilder.DropTable(
                name: "GroupUsers");

            migrationBuilder.DropTable(
                name: "ObjectShareUsers");

            migrationBuilder.DropTable(
                name: "RolePermits");

            migrationBuilder.DropTable(
                name: "UserRoles");

            migrationBuilder.DropTable(
                name: "Groups");

            migrationBuilder.DropTable(
                name: "Permits");

            migrationBuilder.DropTable(
                name: "Roles");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "TypeObjects");
        }
    }
}
