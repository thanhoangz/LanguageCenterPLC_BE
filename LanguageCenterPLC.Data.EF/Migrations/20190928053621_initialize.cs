using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace LanguageCenterPLC.Data.EF.Migrations
{
    public partial class initialize : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    Name = table.Column<string>(maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    UserName = table.Column<string>(maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(maxLength: 256, nullable: true),
                    Email = table.Column<string>(maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(nullable: false),
                    PasswordHash = table.Column<string>(nullable: true),
                    SecurityStamp = table.Column<string>(nullable: true),
                    ConcurrencyStamp = table.Column<string>(nullable: true),
                    PhoneNumber = table.Column<string>(nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(nullable: false),
                    TwoFactorEnabled = table.Column<bool>(nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(nullable: true),
                    LockoutEnabled = table.Column<bool>(nullable: false),
                    AccessFailedCount = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ClassRooms",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(maxLength: 500, nullable: false),
                    Status = table.Column<int>(nullable: false),
                    DateCreated = table.Column<DateTime>(nullable: false),
                    DateModified = table.Column<DateTime>(nullable: false),
                    Note = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClassRooms", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ContactDetails",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    Name = table.Column<string>(maxLength: 250, nullable: false),
                    Phone = table.Column<string>(maxLength: 50, nullable: true),
                    Email = table.Column<string>(maxLength: 250, nullable: true),
                    Website = table.Column<string>(maxLength: 250, nullable: true),
                    Address = table.Column<string>(maxLength: 250, nullable: true),
                    Other = table.Column<string>(nullable: true),
                    Lat = table.Column<double>(nullable: true),
                    Lng = table.Column<double>(nullable: true),
                    Status = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ContactDetails", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Courses",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(maxLength: 500, nullable: false),
                    Price = table.Column<decimal>(nullable: false),
                    Content = table.Column<string>(nullable: false),
                    TraingTime = table.Column<int>(nullable: false),
                    NumberOfSession = table.Column<int>(nullable: false),
                    DateCreated = table.Column<DateTime>(nullable: false),
                    DateModified = table.Column<DateTime>(nullable: false),
                    Status = table.Column<int>(nullable: false),
                    Note = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Courses", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Feedbacks",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(maxLength: 250, nullable: false),
                    Email = table.Column<string>(maxLength: 250, nullable: true),
                    Message = table.Column<string>(maxLength: 500, nullable: true),
                    Status = table.Column<int>(nullable: false),
                    DateCreated = table.Column<DateTime>(nullable: false),
                    DateModified = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Feedbacks", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Footers",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    Content = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Footers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "GuestTypes",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(maxLength: 250, nullable: false),
                    DateCreated = table.Column<DateTime>(nullable: false),
                    DateModified = table.Column<DateTime>(nullable: false),
                    Status = table.Column<int>(nullable: false),
                    Note = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GuestTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Lecturers",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CardId = table.Column<string>(maxLength: 100, nullable: true),
                    FirstName = table.Column<string>(maxLength: 50, nullable: false),
                    LastName = table.Column<string>(maxLength: 50, nullable: false),
                    Sex = table.Column<bool>(nullable: false),
                    Birthday = table.Column<DateTime>(nullable: false),
                    Address = table.Column<string>(maxLength: 500, nullable: false),
                    Nationality = table.Column<string>(maxLength: 100, nullable: false),
                    MarritalStatus = table.Column<int>(nullable: false),
                    ExperienceRecord = table.Column<string>(nullable: true),
                    Email = table.Column<string>(maxLength: 200, nullable: true),
                    Facebook = table.Column<string>(maxLength: 200, nullable: true),
                    Phone = table.Column<string>(type: "VARCHAR(16)", nullable: true),
                    Position = table.Column<string>(nullable: false),
                    Certificate = table.Column<string>(nullable: false),
                    Image = table.Column<string>(nullable: false),
                    BasicSalary = table.Column<decimal>(nullable: false),
                    Allowance = table.Column<decimal>(nullable: false),
                    Bonus = table.Column<decimal>(nullable: false),
                    InsurancePremium = table.Column<decimal>(nullable: false),
                    DateCreated = table.Column<DateTime>(nullable: false),
                    DateModified = table.Column<DateTime>(nullable: false),
                    WageOfLecturer = table.Column<decimal>(nullable: false),
                    WageOfTutor = table.Column<decimal>(nullable: false),
                    IsVisitingLecturer = table.Column<bool>(nullable: false),
                    IsTutor = table.Column<bool>(nullable: false),
                    Status = table.Column<int>(nullable: false),
                    Note = table.Column<string>(nullable: true),
                    QuitWorkDay = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Lecturers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PaySlipTypes",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: false),
                    Status = table.Column<int>(nullable: false),
                    DateCreated = table.Column<DateTime>(nullable: false),
                    DateModified = table.Column<DateTime>(nullable: false),
                    Note = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PaySlipTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Personnels",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    CardId = table.Column<string>(maxLength: 100, nullable: true),
                    FirstName = table.Column<string>(maxLength: 50, nullable: false),
                    LastName = table.Column<string>(maxLength: 50, nullable: false),
                    Sex = table.Column<bool>(nullable: false),
                    Birthday = table.Column<DateTime>(nullable: false),
                    Address = table.Column<string>(maxLength: 500, nullable: false),
                    Nationality = table.Column<string>(maxLength: 100, nullable: false),
                    MarritalStatus = table.Column<int>(nullable: false),
                    ExperienceRecord = table.Column<string>(nullable: true),
                    Email = table.Column<string>(maxLength: 200, nullable: true),
                    Facebook = table.Column<string>(maxLength: 200, nullable: true),
                    Phone = table.Column<string>(type: "VARCHAR(16)", nullable: true),
                    Position = table.Column<string>(nullable: false),
                    Certificate = table.Column<string>(nullable: false),
                    Image = table.Column<string>(nullable: false),
                    BasicSalary = table.Column<decimal>(nullable: false),
                    Allowance = table.Column<decimal>(nullable: false),
                    Bonus = table.Column<decimal>(nullable: false),
                    InsurancePremium = table.Column<decimal>(nullable: false),
                    DateCreated = table.Column<DateTime>(nullable: false),
                    DateModified = table.Column<DateTime>(nullable: false),
                    Status = table.Column<int>(nullable: false),
                    Note = table.Column<string>(nullable: true),
                    QuitWorkDay = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Personnels", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ReceiptTypes",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(maxLength: 200, nullable: false),
                    Status = table.Column<int>(nullable: false),
                    DateCreated = table.Column<DateTime>(nullable: false),
                    DateModified = table.Column<DateTime>(nullable: false),
                    Note = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReceiptTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SystemConfigs",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    Name = table.Column<string>(maxLength: 128, nullable: false),
                    Value1 = table.Column<string>(nullable: true),
                    Value2 = table.Column<int>(nullable: true),
                    Value3 = table.Column<bool>(nullable: true),
                    Value4 = table.Column<DateTime>(nullable: true),
                    Value5 = table.Column<decimal>(nullable: true),
                    Status = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SystemConfigs", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FullName = table.Column<string>(nullable: false),
                    Username = table.Column<string>(nullable: false),
                    Password = table.Column<string>(nullable: false),
                    Status = table.Column<int>(nullable: false),
                    DateCreated = table.Column<DateTime>(nullable: false),
                    DateModified = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleId = table.Column<string>(nullable: false),
                    ClaimType = table.Column<string>(nullable: true),
                    ClaimValue = table.Column<string>(nullable: true)
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
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(nullable: false),
                    ClaimType = table.Column<string>(nullable: true),
                    ClaimValue = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(nullable: false),
                    ProviderKey = table.Column<string>(nullable: false),
                    ProviderDisplayName = table.Column<string>(nullable: true),
                    UserId = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                columns: table => new
                {
                    UserId = table.Column<string>(nullable: false),
                    RoleId = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                columns: table => new
                {
                    UserId = table.Column<string>(nullable: false),
                    LoginProvider = table.Column<string>(nullable: false),
                    Name = table.Column<string>(nullable: false),
                    Value = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "LanguageClasses",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    Name = table.Column<string>(maxLength: 200, nullable: false),
                    CourseFee = table.Column<decimal>(nullable: false),
                    MonthlyFee = table.Column<decimal>(nullable: false),
                    LessonFee = table.Column<decimal>(nullable: false),
                    StartDay = table.Column<DateTime>(nullable: false),
                    EndDay = table.Column<DateTime>(nullable: false),
                    Status = table.Column<int>(nullable: false),
                    DateCreated = table.Column<DateTime>(nullable: false),
                    DateModified = table.Column<DateTime>(nullable: false),
                    Note = table.Column<string>(nullable: true),
                    CourseId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LanguageClasses", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LanguageClasses_Courses_CourseId",
                        column: x => x.CourseId,
                        principalTable: "Courses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Learners",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    CardId = table.Column<string>(maxLength: 100, nullable: true),
                    FirstName = table.Column<string>(maxLength: 50, nullable: false),
                    LastName = table.Column<string>(maxLength: 50, nullable: false),
                    Sex = table.Column<bool>(nullable: false),
                    Birthday = table.Column<DateTime>(nullable: false),
                    Address = table.Column<string>(maxLength: 500, nullable: false),
                    Email = table.Column<string>(maxLength: 200, nullable: true),
                    Facebook = table.Column<string>(maxLength: 200, nullable: true),
                    Phone = table.Column<string>(type: "VARCHAR(16)", nullable: true),
                    ParentFullName = table.Column<string>(maxLength: 100, nullable: false),
                    ParentPhone = table.Column<string>(type: "VARCHAR(16)", nullable: false),
                    Image = table.Column<string>(nullable: true),
                    Status = table.Column<int>(nullable: false),
                    Note = table.Column<string>(nullable: true),
                    DateCreated = table.Column<DateTime>(nullable: false),
                    DateModified = table.Column<DateTime>(nullable: false),
                    GuestTypeId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Learners", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Learners_GuestTypes_GuestTypeId",
                        column: x => x.GuestTypeId,
                        principalTable: "GuestTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PaySlips",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    Content = table.Column<string>(nullable: false),
                    Total = table.Column<decimal>(nullable: false),
                    Date = table.Column<DateTime>(nullable: false),
                    Receiver = table.Column<string>(maxLength: 200, nullable: true),
                    Status = table.Column<int>(nullable: false),
                    DateCreated = table.Column<DateTime>(nullable: false),
                    DateModified = table.Column<DateTime>(nullable: false),
                    Note = table.Column<string>(nullable: true),
                    PaySlipTypeId = table.Column<int>(nullable: false),
                    PersonnelId = table.Column<string>(nullable: false),
                    SendPersonnelId = table.Column<string>(nullable: true),
                    UserId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PaySlips", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PaySlips_PaySlipTypes_PaySlipTypeId",
                        column: x => x.PaySlipTypeId,
                        principalTable: "PaySlipTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PaySlips_Personnels_PersonnelId",
                        column: x => x.PersonnelId,
                        principalTable: "Personnels",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PaySlips_Personnels_SendPersonnelId",
                        column: x => x.SendPersonnelId,
                        principalTable: "Personnels",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PaySlips_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Timesheets",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Month = table.Column<int>(nullable: false),
                    Year = table.Column<int>(nullable: false),
                    Day_1 = table.Column<float>(nullable: false),
                    Day_2 = table.Column<float>(nullable: false),
                    Day_3 = table.Column<float>(nullable: false),
                    Day_4 = table.Column<float>(nullable: false),
                    Day_5 = table.Column<float>(nullable: false),
                    Day_6 = table.Column<float>(nullable: false),
                    Day_7 = table.Column<float>(nullable: false),
                    Day_8 = table.Column<float>(nullable: false),
                    Day_9 = table.Column<float>(nullable: false),
                    Day_10 = table.Column<float>(nullable: false),
                    Day_11 = table.Column<float>(nullable: false),
                    Day_12 = table.Column<float>(nullable: false),
                    Day_13 = table.Column<float>(nullable: false),
                    Day_14 = table.Column<float>(nullable: false),
                    Day_15 = table.Column<float>(nullable: false),
                    Day_16 = table.Column<float>(nullable: false),
                    Day_17 = table.Column<float>(nullable: false),
                    Day_18 = table.Column<float>(nullable: false),
                    Day_19 = table.Column<float>(nullable: false),
                    Day_20 = table.Column<float>(nullable: false),
                    Day_21 = table.Column<float>(nullable: false),
                    Day_22 = table.Column<float>(nullable: false),
                    Day_23 = table.Column<float>(nullable: false),
                    Day_24 = table.Column<float>(nullable: false),
                    Day_25 = table.Column<float>(nullable: false),
                    Day_26 = table.Column<float>(nullable: false),
                    Day_27 = table.Column<float>(nullable: false),
                    Day_28 = table.Column<float>(nullable: false),
                    Day_29 = table.Column<float>(nullable: false),
                    Day_30 = table.Column<float>(nullable: false),
                    Day_31 = table.Column<float>(nullable: false),
                    TotalWorkday = table.Column<int>(nullable: false),
                    Salary = table.Column<decimal>(nullable: false),
                    Allowance = table.Column<decimal>(nullable: false),
                    Bonus = table.Column<decimal>(nullable: false),
                    TotalSalary = table.Column<decimal>(nullable: false),
                    InsurancePremiums = table.Column<decimal>(nullable: false),
                    AdvancePayment = table.Column<decimal>(nullable: false),
                    TotalActualSalary = table.Column<decimal>(nullable: false),
                    Status = table.Column<int>(nullable: false),
                    DateCreated = table.Column<DateTime>(nullable: false),
                    DateModified = table.Column<DateTime>(nullable: false),
                    Note = table.Column<string>(nullable: true),
                    UserId = table.Column<int>(nullable: false),
                    PersonnelId = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Timesheets", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Timesheets_Personnels_PersonnelId",
                        column: x => x.PersonnelId,
                        principalTable: "Personnels",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Timesheets_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AttendanceSheets",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    WageOfLecturer = table.Column<decimal>(nullable: false),
                    WageOfTutor = table.Column<decimal>(nullable: false),
                    Date = table.Column<DateTime>(nullable: false),
                    Status = table.Column<int>(nullable: false),
                    DateCreated = table.Column<DateTime>(nullable: false),
                    DateModified = table.Column<DateTime>(nullable: false),
                    Note = table.Column<string>(nullable: true),
                    LanguageClassId = table.Column<string>(nullable: false),
                    LecturerId = table.Column<int>(nullable: false),
                    TutorId = table.Column<int>(nullable: true),
                    UserId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AttendanceSheets", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AttendanceSheets_LanguageClasses_LanguageClassId",
                        column: x => x.LanguageClassId,
                        principalTable: "LanguageClasses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AttendanceSheets_Lecturers_LecturerId",
                        column: x => x.LecturerId,
                        principalTable: "Lecturers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AttendanceSheets_Lecturers_TutorId",
                        column: x => x.TutorId,
                        principalTable: "Lecturers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AttendanceSheets_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "EndingCoursePoints",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DateOnPoint = table.Column<DateTime>(nullable: false),
                    ExaminationDate = table.Column<DateTime>(nullable: false),
                    Status = table.Column<int>(nullable: false),
                    DateCreated = table.Column<DateTime>(nullable: false),
                    DateModified = table.Column<DateTime>(nullable: false),
                    Note = table.Column<string>(nullable: true),
                    LanguageClassId = table.Column<string>(nullable: false),
                    LecturerId = table.Column<int>(nullable: false),
                    UserId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EndingCoursePoints", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EndingCoursePoints_LanguageClasses_LanguageClassId",
                        column: x => x.LanguageClassId,
                        principalTable: "LanguageClasses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EndingCoursePoints_Lecturers_LecturerId",
                        column: x => x.LecturerId,
                        principalTable: "Lecturers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EndingCoursePoints_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PeriodicPoints",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DateOnPoint = table.Column<DateTime>(nullable: false),
                    ExaminationDate = table.Column<DateTime>(nullable: false),
                    Week = table.Column<int>(nullable: false),
                    Status = table.Column<int>(nullable: false),
                    DateCreated = table.Column<DateTime>(nullable: false),
                    DateModified = table.Column<DateTime>(nullable: false),
                    Note = table.Column<string>(nullable: true),
                    LanguageClassId = table.Column<string>(nullable: false),
                    LecturerId = table.Column<int>(nullable: false),
                    UserId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PeriodicPoints", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PeriodicPoints_LanguageClasses_LanguageClassId",
                        column: x => x.LanguageClassId,
                        principalTable: "LanguageClasses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PeriodicPoints_Lecturers_LecturerId",
                        column: x => x.LecturerId,
                        principalTable: "Lecturers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PeriodicPoints_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Receipts",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    NameOfPaymentApplicant = table.Column<string>(maxLength: 100, nullable: false),
                    ForReason = table.Column<string>(nullable: false),
                    CollectionDate = table.Column<DateTime>(nullable: false),
                    TotalAmount = table.Column<decimal>(nullable: false),
                    Status = table.Column<int>(nullable: false),
                    DateCreated = table.Column<DateTime>(nullable: false),
                    DateModified = table.Column<DateTime>(nullable: false),
                    Note = table.Column<string>(nullable: true),
                    ReceiptTypeId = table.Column<int>(nullable: false),
                    PersonnelId = table.Column<string>(nullable: false),
                    UserId = table.Column<int>(nullable: false),
                    LearnerId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Receipts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Receipts_Learners_LearnerId",
                        column: x => x.LearnerId,
                        principalTable: "Learners",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Receipts_Personnels_PersonnelId",
                        column: x => x.PersonnelId,
                        principalTable: "Personnels",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Receipts_ReceiptTypes_ReceiptTypeId",
                        column: x => x.ReceiptTypeId,
                        principalTable: "ReceiptTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Receipts_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "StudyProcess",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OutDate = table.Column<DateTime>(nullable: false),
                    InDate = table.Column<DateTime>(nullable: false),
                    Status = table.Column<int>(nullable: false),
                    DateCreated = table.Column<DateTime>(nullable: false),
                    DateModified = table.Column<DateTime>(nullable: false),
                    Note = table.Column<string>(nullable: true),
                    LanguageClassId = table.Column<string>(nullable: false),
                    LearnerId = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StudyProcess", x => x.Id);
                    table.ForeignKey(
                        name: "FK_StudyProcess_LanguageClasses_LanguageClassId",
                        column: x => x.LanguageClassId,
                        principalTable: "LanguageClasses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_StudyProcess_Learners_LearnerId",
                        column: x => x.LearnerId,
                        principalTable: "Learners",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TeachingSchedules",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FromDate = table.Column<DateTime>(nullable: false),
                    ToDate = table.Column<DateTime>(nullable: false),
                    TimeShift = table.Column<string>(maxLength: 500, nullable: true),
                    DaysOfWeek = table.Column<string>(maxLength: 500, nullable: false),
                    Status = table.Column<int>(nullable: false),
                    DateCreated = table.Column<DateTime>(nullable: false),
                    DateModified = table.Column<DateTime>(nullable: false),
                    Note = table.Column<string>(nullable: true),
                    LecturerId = table.Column<int>(nullable: false),
                    ClassRoomId = table.Column<int>(nullable: false),
                    LanguageClassId = table.Column<string>(nullable: false),
                    LearnerId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TeachingSchedules", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TeachingSchedules_ClassRooms_ClassRoomId",
                        column: x => x.ClassRoomId,
                        principalTable: "ClassRooms",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TeachingSchedules_LanguageClasses_LanguageClassId",
                        column: x => x.LanguageClassId,
                        principalTable: "LanguageClasses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TeachingSchedules_Learners_LearnerId",
                        column: x => x.LearnerId,
                        principalTable: "Learners",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TeachingSchedules_Lecturers_LecturerId",
                        column: x => x.LecturerId,
                        principalTable: "Lecturers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AttendanceSheetDetails",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Status = table.Column<int>(nullable: false),
                    DateCreated = table.Column<DateTime>(nullable: false),
                    DateModified = table.Column<DateTime>(nullable: false),
                    Note = table.Column<string>(nullable: true),
                    LearnerId = table.Column<string>(nullable: false),
                    LanguageClassId = table.Column<string>(nullable: true),
                    AttendanceSheetId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AttendanceSheetDetails", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AttendanceSheetDetails_AttendanceSheets_AttendanceSheetId",
                        column: x => x.AttendanceSheetId,
                        principalTable: "AttendanceSheets",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AttendanceSheetDetails_LanguageClasses_LanguageClassId",
                        column: x => x.LanguageClassId,
                        principalTable: "LanguageClasses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AttendanceSheetDetails_Learners_LearnerId",
                        column: x => x.LearnerId,
                        principalTable: "Learners",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "EndingCoursePointDetails",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ListeningPoint = table.Column<decimal>(nullable: false),
                    SayingPoint = table.Column<decimal>(nullable: false),
                    WritingPoint = table.Column<decimal>(nullable: false),
                    ReadingPoint = table.Column<decimal>(nullable: false),
                    TotalPoint = table.Column<decimal>(nullable: false),
                    AveragePoint = table.Column<decimal>(nullable: false),
                    Status = table.Column<int>(nullable: false),
                    DateCreated = table.Column<DateTime>(nullable: false),
                    DateModified = table.Column<DateTime>(nullable: false),
                    Note = table.Column<string>(nullable: true),
                    SortOrder = table.Column<int>(nullable: false),
                    LearnerId = table.Column<string>(nullable: false),
                    EndingCoursePointId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EndingCoursePointDetails", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EndingCoursePointDetails_EndingCoursePoints_EndingCoursePointId",
                        column: x => x.EndingCoursePointId,
                        principalTable: "EndingCoursePoints",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EndingCoursePointDetails_Learners_LearnerId",
                        column: x => x.LearnerId,
                        principalTable: "Learners",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PeriodicPointDetails",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Point = table.Column<decimal>(nullable: false),
                    AveragePoint = table.Column<decimal>(nullable: false),
                    SortedByAveragePoint = table.Column<decimal>(nullable: false),
                    SortedByPoint = table.Column<decimal>(nullable: false),
                    Status = table.Column<int>(nullable: false),
                    DateCreated = table.Column<DateTime>(nullable: false),
                    DateModified = table.Column<DateTime>(nullable: false),
                    Note = table.Column<string>(nullable: true),
                    LearnerId = table.Column<string>(nullable: false),
                    PeriodicPointId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PeriodicPointDetails", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PeriodicPointDetails_Learners_LearnerId",
                        column: x => x.LearnerId,
                        principalTable: "Learners",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PeriodicPointDetails_PeriodicPoints_PeriodicPointId",
                        column: x => x.PeriodicPointId,
                        principalTable: "PeriodicPoints",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ReceiptDetails",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Month = table.Column<int>(nullable: false),
                    Year = table.Column<int>(nullable: false),
                    Tuition = table.Column<decimal>(nullable: false),
                    FundMoney = table.Column<decimal>(nullable: false),
                    InfrastructureMoney = table.Column<decimal>(nullable: false),
                    OtherMoney = table.Column<decimal>(nullable: false),
                    TotalMoney = table.Column<decimal>(nullable: false),
                    Status = table.Column<int>(nullable: false),
                    DateCreated = table.Column<DateTime>(nullable: false),
                    DateModified = table.Column<DateTime>(nullable: false),
                    Note = table.Column<string>(nullable: true),
                    LanguageClassId = table.Column<string>(nullable: false),
                    ReceiptId = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReceiptDetails", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ReceiptDetails_LanguageClasses_LanguageClassId",
                        column: x => x.LanguageClassId,
                        principalTable: "LanguageClasses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ReceiptDetails_Receipts_ReceiptId",
                        column: x => x.ReceiptId,
                        principalTable: "Receipts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

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
                name: "EmailIndex",
                table: "AspNetUsers",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true,
                filter: "[NormalizedUserName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_AttendanceSheetDetails_AttendanceSheetId",
                table: "AttendanceSheetDetails",
                column: "AttendanceSheetId");

            migrationBuilder.CreateIndex(
                name: "IX_AttendanceSheetDetails_LanguageClassId",
                table: "AttendanceSheetDetails",
                column: "LanguageClassId");

            migrationBuilder.CreateIndex(
                name: "IX_AttendanceSheetDetails_LearnerId",
                table: "AttendanceSheetDetails",
                column: "LearnerId");

            migrationBuilder.CreateIndex(
                name: "IX_AttendanceSheets_LanguageClassId",
                table: "AttendanceSheets",
                column: "LanguageClassId");

            migrationBuilder.CreateIndex(
                name: "IX_AttendanceSheets_LecturerId",
                table: "AttendanceSheets",
                column: "LecturerId");

            migrationBuilder.CreateIndex(
                name: "IX_AttendanceSheets_TutorId",
                table: "AttendanceSheets",
                column: "TutorId");

            migrationBuilder.CreateIndex(
                name: "IX_AttendanceSheets_UserId",
                table: "AttendanceSheets",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_EndingCoursePointDetails_EndingCoursePointId",
                table: "EndingCoursePointDetails",
                column: "EndingCoursePointId");

            migrationBuilder.CreateIndex(
                name: "IX_EndingCoursePointDetails_LearnerId",
                table: "EndingCoursePointDetails",
                column: "LearnerId");

            migrationBuilder.CreateIndex(
                name: "IX_EndingCoursePoints_LanguageClassId",
                table: "EndingCoursePoints",
                column: "LanguageClassId");

            migrationBuilder.CreateIndex(
                name: "IX_EndingCoursePoints_LecturerId",
                table: "EndingCoursePoints",
                column: "LecturerId");

            migrationBuilder.CreateIndex(
                name: "IX_EndingCoursePoints_UserId",
                table: "EndingCoursePoints",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_LanguageClasses_CourseId",
                table: "LanguageClasses",
                column: "CourseId");

            migrationBuilder.CreateIndex(
                name: "IX_Learners_GuestTypeId",
                table: "Learners",
                column: "GuestTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_PaySlips_PaySlipTypeId",
                table: "PaySlips",
                column: "PaySlipTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_PaySlips_PersonnelId",
                table: "PaySlips",
                column: "PersonnelId");

            migrationBuilder.CreateIndex(
                name: "IX_PaySlips_SendPersonnelId",
                table: "PaySlips",
                column: "SendPersonnelId");

            migrationBuilder.CreateIndex(
                name: "IX_PaySlips_UserId",
                table: "PaySlips",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_PeriodicPointDetails_LearnerId",
                table: "PeriodicPointDetails",
                column: "LearnerId");

            migrationBuilder.CreateIndex(
                name: "IX_PeriodicPointDetails_PeriodicPointId",
                table: "PeriodicPointDetails",
                column: "PeriodicPointId");

            migrationBuilder.CreateIndex(
                name: "IX_PeriodicPoints_LanguageClassId",
                table: "PeriodicPoints",
                column: "LanguageClassId");

            migrationBuilder.CreateIndex(
                name: "IX_PeriodicPoints_LecturerId",
                table: "PeriodicPoints",
                column: "LecturerId");

            migrationBuilder.CreateIndex(
                name: "IX_PeriodicPoints_UserId",
                table: "PeriodicPoints",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_ReceiptDetails_LanguageClassId",
                table: "ReceiptDetails",
                column: "LanguageClassId");

            migrationBuilder.CreateIndex(
                name: "IX_ReceiptDetails_ReceiptId",
                table: "ReceiptDetails",
                column: "ReceiptId");

            migrationBuilder.CreateIndex(
                name: "IX_Receipts_LearnerId",
                table: "Receipts",
                column: "LearnerId");

            migrationBuilder.CreateIndex(
                name: "IX_Receipts_PersonnelId",
                table: "Receipts",
                column: "PersonnelId");

            migrationBuilder.CreateIndex(
                name: "IX_Receipts_ReceiptTypeId",
                table: "Receipts",
                column: "ReceiptTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Receipts_UserId",
                table: "Receipts",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_StudyProcess_LanguageClassId",
                table: "StudyProcess",
                column: "LanguageClassId");

            migrationBuilder.CreateIndex(
                name: "IX_StudyProcess_LearnerId",
                table: "StudyProcess",
                column: "LearnerId");

            migrationBuilder.CreateIndex(
                name: "IX_TeachingSchedules_ClassRoomId",
                table: "TeachingSchedules",
                column: "ClassRoomId");

            migrationBuilder.CreateIndex(
                name: "IX_TeachingSchedules_LanguageClassId",
                table: "TeachingSchedules",
                column: "LanguageClassId");

            migrationBuilder.CreateIndex(
                name: "IX_TeachingSchedules_LearnerId",
                table: "TeachingSchedules",
                column: "LearnerId");

            migrationBuilder.CreateIndex(
                name: "IX_TeachingSchedules_LecturerId",
                table: "TeachingSchedules",
                column: "LecturerId");

            migrationBuilder.CreateIndex(
                name: "IX_Timesheets_PersonnelId",
                table: "Timesheets",
                column: "PersonnelId");

            migrationBuilder.CreateIndex(
                name: "IX_Timesheets_UserId",
                table: "Timesheets",
                column: "UserId");
        }

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
                name: "AttendanceSheetDetails");

            migrationBuilder.DropTable(
                name: "ContactDetails");

            migrationBuilder.DropTable(
                name: "EndingCoursePointDetails");

            migrationBuilder.DropTable(
                name: "Feedbacks");

            migrationBuilder.DropTable(
                name: "Footers");

            migrationBuilder.DropTable(
                name: "PaySlips");

            migrationBuilder.DropTable(
                name: "PeriodicPointDetails");

            migrationBuilder.DropTable(
                name: "ReceiptDetails");

            migrationBuilder.DropTable(
                name: "StudyProcess");

            migrationBuilder.DropTable(
                name: "SystemConfigs");

            migrationBuilder.DropTable(
                name: "TeachingSchedules");

            migrationBuilder.DropTable(
                name: "Timesheets");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "AttendanceSheets");

            migrationBuilder.DropTable(
                name: "EndingCoursePoints");

            migrationBuilder.DropTable(
                name: "PaySlipTypes");

            migrationBuilder.DropTable(
                name: "PeriodicPoints");

            migrationBuilder.DropTable(
                name: "Receipts");

            migrationBuilder.DropTable(
                name: "ClassRooms");

            migrationBuilder.DropTable(
                name: "LanguageClasses");

            migrationBuilder.DropTable(
                name: "Lecturers");

            migrationBuilder.DropTable(
                name: "Learners");

            migrationBuilder.DropTable(
                name: "Personnels");

            migrationBuilder.DropTable(
                name: "ReceiptTypes");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Courses");

            migrationBuilder.DropTable(
                name: "GuestTypes");
        }
    }
}
