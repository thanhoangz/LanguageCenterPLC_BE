using LanguageCenterPLC.Data.Entities;
using LanguageCenterPLC.Infrastructure.Enums;
using LanguageCenterPLC.Utilities.Constants;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LanguageCenterPLC.Data.EF
{
    public class DbInitializer
    {
        private readonly AppDbContext _context;
        private UserManager<AppUser> _userManager;
        private RoleManager<AppRole> _roleManager;

        public DbInitializer(AppDbContext context, UserManager<AppUser> userManager, RoleManager<AppRole> roleManager)
        {
            _context = context;
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public async Task Seed()
        {
            if (!_roleManager.Roles.Any())
            {
                await _roleManager.CreateAsync(new AppRole()
                {
                    Name = "Admin",
                    NormalizedName = "Admin",
                    Description = "Top manager"
                });
                await _roleManager.CreateAsync(new AppRole()
                {
                    Name = "Staff",
                    NormalizedName = "Staff",
                    Description = "Nhân Viên"
                });
                await _roleManager.CreateAsync(new AppRole()
                {
                    Name = "Lecturer",
                    NormalizedName = "Lecturer",
                    Description = "Giảng Viên"
                });
                await _roleManager.CreateAsync(new AppRole()
                {
                    Name = "Customer",
                    NormalizedName = "Customer",
                    Description = "Khách Hàng"
                });
            }

            if (!_userManager.Users.Any())
            {
                await _userManager.CreateAsync(new AppUser()
                {
                    UserName = "admin",
                    FullName = "Administrator",
                    Email = "xuanhoang.ks6@gmail.com",
                    Balance = 0,
                    DateCreated = DateTime.Now,
                    DateModified = DateTime.Now,
                    Status = Status.Active
                }, "123@abcABC");
                var user = await _userManager.FindByNameAsync("admin");
                await _userManager.AddToRoleAsync(user, "Admin");
            }
            if (!_context.Contacts.Any())
            {
                _context.Contacts.Add(new Contact()
                {
                    Id = CommonConstants.DefaultContactId,
                    Address = "Số 97 lô 7C Lê Hồng Phong",
                    Email = "plc.english.vn@gmail.com",
                    Name = "PLC Center",
                    Phone = "083 4862 522",
                    Status = Status.Active,
                    Website = "http://plccenter.com",
                    Lat = 21.0435009,
                    Lng = 105.7894758
                });
            }
            if (_context.Footers.Count(x => x.Id == CommonConstants.DefaultFooterId) == 0)
            {
                string content = "Footer";
                _context.Footers.Add(new Footer()
                {
                    Id = CommonConstants.DefaultFooterId,
                    Content = content
                });
            }

            /* Khóa học */

            if (_context.Courses.Count() == 0)
            {
                List<Course> listCourses = new List<Course>()
                {
                    new Course() { Name="Toiec",Content="", TraingTime = 4, Price = 20000,NumberOfSession = 32, DateCreated = DateTime.Now, DateModified = DateTime.Now, Status = Status.Active,
                        LanguageClasses = new List<LanguageClass>()
                        {
                            new LanguageClass()
                            {
                                Name ="Lớp Toiec 450",
                                StartDay =  new DateTime(2019, 2, 3),
                                EndDay = new DateTime(2019, 5, 5),
                                CourseFee = 20000,
                                LessonFee = 200,
                                MonthlyFee = 6000,
                                Note = "",
                                DateCreated = DateTime.Now,
                                DateModified = DateTime.Now,
                                Status = Status.Active
                            },
                            new LanguageClass()
                            {
                                Name ="Lớp Toiec 600",
                                StartDay =  new DateTime(2019, 2, 3),
                                EndDay = new DateTime(2019, 5, 5),
                                CourseFee = 20000,
                                LessonFee = 200,
                                MonthlyFee = 6000,
                                Note = "",
                                DateCreated = DateTime.Now,
                                DateModified = DateTime.Now,
                                Status = Status.Active
                            },
                            new LanguageClass()
                            {
                                Name ="Lớp Toiec 900",
                                StartDay =  new DateTime(2019, 6, 9),
                                EndDay = new DateTime(2019, 9, 6),
                                CourseFee = 20000,
                                LessonFee = 300,
                                MonthlyFee = 9000,
                                Note = "",
                                DateCreated = DateTime.Now,
                                DateModified = DateTime.Now,
                                Status = Status.Active
                            }
                        }
                    },
                    new Course() { Name="Ielts",Content="", TraingTime = 4, Price = 690000,NumberOfSession = 32, DateCreated = DateTime.Now, DateModified = DateTime.Now, Status = Status.Active},
                    new Course() { Name="Anh văn cơ bản 1",Content="", TraingTime = 4, Price = 208000,NumberOfSession = 32, DateCreated = DateTime.Now, DateModified = DateTime.Now, Status = Status.Active},
                    new Course() { Name="Giao tiếp A2",Content="", TraingTime = 4, Price = 202000,NumberOfSession = 32, DateCreated = DateTime.Now, DateModified = DateTime.Now, Status = Status.Active},

                    new Course() { Name="Toiec 450",Content="", TraingTime = 9, Price = 20000,NumberOfSession = 32, DateCreated = DateTime.Now, DateModified = DateTime.Now, Status = Status.Active},
                    new Course() { Name="Ielts - đọc",Content="", TraingTime = 8, Price = 690000,NumberOfSession = 32, DateCreated = DateTime.Now, DateModified = DateTime.Now, Status = Status.Active},
                    new Course() { Name="Anh văn cơ bản 2",Content="", TraingTime = 4, Price = 700000,NumberOfSession = 32, DateCreated = DateTime.Now, DateModified = DateTime.Now, Status = Status.Active},
                    new Course() { Name="Giao tiếp A5",Content="", TraingTime = 7, Price = 20000,NumberOfSession = 32, DateCreated = DateTime.Now, DateModified = DateTime.Now, Status = Status.Active},

                    new Course() { Name="Toiec 700",Content="", TraingTime = 4, Price = 20000,NumberOfSession = 32, DateCreated = DateTime.Now, DateModified = DateTime.Now, Status = Status.Active},
                    new Course() { Name="Ielts - viết",Content="", TraingTime = 4, Price = 690000,NumberOfSession = 32, DateCreated = DateTime.Now, DateModified = DateTime.Now, Status = Status.Active},
                    new Course() { Name="Anh văn cơ bản 3",Content="", TraingTime = 6, Price = 600000,NumberOfSession = 32, DateCreated = DateTime.Now, DateModified = DateTime.Now, Status = Status.Active},
                    new Course() { Name="Giao tiếp B1",Content="", TraingTime = 6, Price = 200020,NumberOfSession = 32, DateCreated = DateTime.Now, DateModified = DateTime.Now, Status = Status.Active},
                };
                _context.Courses.AddRange(listCourses);
            }

            /* Phòng học */

            if (_context.Classrooms.Count() == 0)
            {
                List<Classroom> listClassrooms = new List<Classroom>()
                {
                    new Classroom() { Name="Phòng A1", DateCreated = DateTime.Now, DateModified = DateTime.Now, Status = Status.Active, },
                    new Classroom() { Name="Phòng A2", DateCreated = DateTime.Now, DateModified = DateTime.Now, Status = Status.Active},
                    new Classroom() { Name="Phòng A3", DateCreated = DateTime.Now, DateModified = DateTime.Now, Status = Status.Active},
                    new Classroom() { Name="Phòng A4", DateCreated = DateTime.Now, DateModified = DateTime.Now, Status = Status.Active},
                    new Classroom() { Name="Phòng A5", DateCreated = DateTime.Now, DateModified = DateTime.Now, Status = Status.Active},
                    new Classroom() { Name="Phòng A6", DateCreated = DateTime.Now, DateModified = DateTime.Now, Status = Status.Active},
                    new Classroom() { Name="Phòng A7", DateCreated = DateTime.Now, DateModified = DateTime.Now, Status = Status.Active},
                    new Classroom() { Name="Phòng A8", DateCreated = DateTime.Now, DateModified = DateTime.Now, Status = Status.Active},
                    new Classroom() { Name="Phòng A9", DateCreated = DateTime.Now, DateModified = DateTime.Now, Status = Status.Active},
                    new Classroom() { Name="Phòng B1", DateCreated = DateTime.Now, DateModified = DateTime.Now, Status = Status.Active},
                    new Classroom() { Name="Phòng B2", DateCreated = DateTime.Now, DateModified = DateTime.Now, Status = Status.Active},
                    new Classroom() { Name="Phòng B3", DateCreated = DateTime.Now, DateModified = DateTime.Now, Status = Status.Active},
                    new Classroom() { Name="Phòng C4", DateCreated = DateTime.Now, DateModified = DateTime.Now, Status = Status.Active},
                    new Classroom() { Name="Phòng C5", DateCreated = DateTime.Now, DateModified = DateTime.Now, Status = Status.Active},
                    new Classroom() { Name="Phòng C6", DateCreated = DateTime.Now, DateModified = DateTime.Now, Status = Status.Active},
                    new Classroom() { Name="Phòng C7", DateCreated = DateTime.Now, DateModified = DateTime.Now, Status = Status.Active},

                };
                _context.Classrooms.AddRange(listClassrooms);
            }

            if (_context.GuestTypes.Count() == 0)
            {
                List<GuestType> listGuest = new List<GuestType>()
                {
                    new GuestType() { Name="Sinh viên", DateCreated = DateTime.Now, DateModified = DateTime.Now, Status = Status.Active},
                    new GuestType() { Name="Người đi làm", DateCreated = DateTime.Now, DateModified = DateTime.Now, Status = Status.Active},
                    new GuestType() { Name="Công nhân", DateCreated = DateTime.Now, DateModified = DateTime.Now, Status = Status.Active},
                    new GuestType() { Name="Đối tượng tiềm năng", DateCreated = DateTime.Now, DateModified = DateTime.Now, Status = Status.Active},
                    new GuestType() { Name="Hẹn đi học", DateCreated = DateTime.Now, DateModified = DateTime.Now, Status = Status.Active},

                };
                _context.GuestTypes.AddRange(listGuest);
            }
        }
    }
}
