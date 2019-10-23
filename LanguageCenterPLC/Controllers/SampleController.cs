using LanguageCenterPLC.Data.EF;
using LanguageCenterPLC.Data.Entities;
using LanguageCenterPLC.Infrastructure.Enums;
using LanguageCenterPLC.Utilities.Constants;
using LanguageCenterPLC.Utilities.Helpers;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LanguageCenterPLC.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SampleController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly UserManager<AppUser> _userManager;
        private readonly RoleManager<AppRole> _roleManager;

        public SampleController(AppDbContext context, UserManager<AppUser> userManager, RoleManager<AppRole> roleManager)
        {
            _context = context;
            _userManager = userManager;
            _roleManager = roleManager;
        }

        #region dữ liệu mẫu 
        public List<string> _images = new List<string>()
        {
            "data:image/jpeg;base64,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",
            @"https://tokhoe.com/wp-content/uploads/2017/08/nu-dong-phuc2.jpg",
            "data:image/jpeg;base64,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",
            @"https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcRnQ3wNGN-nGytwXjV0LVCjYyOu1Z2zeTVLDa5g1fE8B_TmvCXpyQ",
            @"https://znews-photo.zadn.vn/w660/Uploaded/zxgovb/2015_09_05/10418914_1362754997106290_688380908058332734_n.jpg",
            @"https://we25.vn/media2018/Img_News/2019/03/28/nu-sinh-nam-nhat-dh-ngoai-thuong-nhan-bao-like-tu-buc-anh-hoc-quan-su-lo-danh-tinh-la-hoc-sinh-gioi-2_20190328105256.jpg",
            @"http://file.vforum.vn/hinh/2016/08/hinh-anh-nhung-girl-xinh-tu-suong-dep-nhat-hot-girl-selfie-facebook-2.jpg",
            @"https://s16815.pcdn.co/wp-content/uploads/2017/06/iStock-609683672-studying.jpg",
            @"https://www.exlibrisgroup.com/wp-content/uploads/2018/01/Untitled-design-6-1.png",
            @"https://www.tokyoteenies.com/media/thumbs/5/7/f/3/8/57f38eedf3b7e/236x355/15.jpg",
            @"https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcS44xEqEqoGADcwXd1n9Sjc1_Mqnmv5nQh-U7r7K8P_4KYQ6Svo",
            @"http://saoleloi.com/images/baiviet/jav/rola-takizawa.jpg",
            @"https://media.ohay.tv/v1/upload/content/2017-11/14/1205-6bc660b69abadcc4d1f756a382bf7673.jpg",
            @"https://scontent.fhan5-7.fna.fbcdn.net/v/t1.0-9/10376137_259358934251656_3320677344743473884_n.jpg?_nc_cat=103&_nc_oc=AQlU-9t7WVO8o3bZiijPeWgIJaf6QVKOBgZDA7oGv8DE3i8eUo1dzjn5n4TqmSOD7fQcf06o3jitA65-Sits2ygc&_nc_ht=scontent.fhan5-7.fna&oh=5564226197eb744ded8c0b343c010e8c&oe=5E566C59",
            @"https://scontent.fhan5-2.fna.fbcdn.net/v/t1.0-1/c0.0.2011.2011a/73482633_2434565420199083_6011195756004769792_o.jpg?_nc_cat=110&_nc_oc=AQn7ZW3F6Pr_Gp-GRhUeQthzoQCfejtjv84DTnjSHIoNF-i0vjSaaBcc-hHjgjkyDExYONrT2BTvX6eht2YzUC8H&_nc_ht=scontent.fhan5-2.fna&oh=d0b71a4aeddfe12e36116f32e8ad32c6&oe=5E1752D9",
            @"https://scontent.fhan5-7.fna.fbcdn.net/v/t1.0-9/45576457_1162598560562736_4853310025117990912_o.jpg?_nc_cat=103&_nc_oc=AQmDuUKRMe7pz-Adwhe-kAtPz8jd9FEDJCqfWQV9ph8GEzxI02uP25WQGoBUIT6SbsrC7283yP4ns7jsXCRPCEHm&_nc_ht=scontent.fhan5-7.fna&oh=beebd78aaa5d6e37ba2051ce9acdd1a8&oe=5E29D8B5",
            @"https://scontent.fhan5-4.fna.fbcdn.net/v/t1.0-9/67544944_2371080639846869_6344308029298573312_o.jpg?_nc_cat=104&_nc_oc=AQluIhWM6-plXrNquDxnEMUxkErK47JAaaO_35A7lzs94BzTIekx9vBA1xQHzjbcPwu374_jyuiapE_1RjrmYOTu&_nc_ht=scontent.fhan5-4.fna&oh=3d547e5cd92b588ab92bb1aa95afbf21&oe=5E55AE20",
            @"https://scontent.fhan5-7.fna.fbcdn.net/v/t31.0-8/22291241_1916922411963389_1635466374862426939_o.jpg?_nc_cat=103&_nc_oc=AQnMzH3HeVKBSuU7iLTXofDKgtNbnWktUzN-B-p3FRYzIHVuQ68YOUO_cCxQU9frmU8L6AuFl4LsI1NmKyS7Tjur&_nc_ht=scontent.fhan5-7.fna&oh=40556514aaaa9058d2fb3afbb6f4315f&oe=5E64230A",
            @"https://scontent.fhan5-6.fna.fbcdn.net/v/t31.0-8/18216799_813067862182804_8260177825729180537_o.jpg?_nc_cat=107&_nc_oc=AQmtc5cjtKxxNZygKkbCGb2RuSO5qRgftGxU32pOiKwwQtDew54c_hYolTq5JTWwpbRuj42OGrLJAY0TPuvmrwDf&_nc_ht=scontent.fhan5-6.fna&oh=c1efe19980cd1def52ed5a0079e30fdc&oe=5E2CE7FD",
            @"https://cafebiz.cafebizcdn.vn/thumb_w/600/2016/photo-0-1477883902802-crop-1477977528098.jpg",
            @"https://www.docxem.com/wp-content/uploads/2015/10/%C3%A26.jpg",
            @"http://static2.yan.vn/YanNews/2167221/201808/4a2bc43d00f3db0613138c5f5e637a95-98df37fe.png",
            @"https://scontent.fhan5-5.fna.fbcdn.net/v/t1.0-9/12417652_1099631016754811_4117657579030545024_n.jpg?_nc_cat=108&_nc_oc=AQm1zJXBYd2rjvHkZRdLcHj5fZ7UMLnL1eSs9QJ_KgxEs2NCGLmcBHw-WcD7U3agD3lH5DoU--tRDukZjPX5fzXy&_nc_ht=scontent.fhan5-5.fna&oh=edb3ca547bb3d2c1c5baf757a8180d1b&oe=5E61CF2F",
            @"https://scontent.fhan5-5.fna.fbcdn.net/v/t1.0-9/51304465_1078810395654064_5375734273452015616_n.jpg?_nc_cat=108&_nc_oc=AQnDIimSONRWVw6zlDhDqDMGIoR3ewUgP3kvv3XNG11uB4HqdafDXl7GZdpDRl0gOFxA9eN2zYJfaqPwosb99jbD&_nc_ht=scontent.fhan5-5.fna&oh=6c3d25664f7c384a9f901ea4627a3d29&oe=5E21E92D",
            @"https://scontent.fhan5-4.fna.fbcdn.net/v/t1.0-9/72706304_1002291733479387_560269863763836928_n.jpg?_nc_cat=104&_nc_oc=AQl-sZ70m15c7bpl0kZHUAyzJn8mbHaHwv2qeMksYB42QElZNXA_A8-7Cjv9lg8Z5qfksnJhSIMvysggEPlgelLA&_nc_ht=scontent.fhan5-4.fna&oh=2a6d6c59cbad0a36b2e30fb0efbc0722&oe=5E2CEE27",
            @"https://scontent.fhan5-2.fna.fbcdn.net/v/t1.0-9/37078089_1981317955511560_2319863427440312320_n.jpg?_nc_cat=110&_nc_oc=AQlgH85LjOm5nWwKS2pDVxwXik_yX9kuGSysGVUjyVdMyXwWBYYi1xFBQaLykwDRa7XKSt8k_flVrEw1qwZQoTvE&_nc_ht=scontent.fhan5-2.fna&oh=172454f381e17b1ad066fc5d0648686c&oe=5E2B2C43",

        };

        public List<string> _national = new List<string>()
        {
            "Việt Nam",
            "Mỹ",
            "Úc",
            "Canada",
            "Pháp",
            "Nhật",
            "Trung quốc"
        };

        #endregion
        [HttpGet]
        [Route("CreateSample")]
        public async Task<Object> CreateSampleData()
        {

            #region initialize data
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
                        new Course() { Name="Toiec",Content="", TraingTime = 4, Price = 20000,NumberOfSession = 32, DateCreated = DateTime.Now, DateModified = DateTime.Now, Status = Status.Active},
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

            /* Đối tượng */
            if (_context.GuestTypes.Count() == 0)
            {
                List<GuestType> listGuest = new List<GuestType>()
                    {
                        new GuestType() { Name="Sinh viên", DateCreated = DateTime.Now, DateModified = DateTime.Now, Status = Status.Active},
                        new GuestType() { Name="Người đi làm", DateCreated = DateTime.Now, DateModified = DateTime.Now, Status = Status.Active},
                        new GuestType() { Name="Công nhân", DateCreated = DateTime.Now, DateModified = DateTime.Now, Status = Status.Active},
                        new GuestType() { Name="Đối tượng tiềm năng", DateCreated = DateTime.Now, DateModified = DateTime.Now, Status = Status.Active},
                        new GuestType() { Name="Hẹn đi học", DateCreated = DateTime.Now, DateModified = DateTime.Now, Status = Status.Active},
                        new GuestType() { Name="Học sinh THPT", DateCreated = DateTime.Now, DateModified = DateTime.Now, Status = Status.Active},
                        new GuestType() { Name="Học sinh TH", DateCreated = DateTime.Now, DateModified = DateTime.Now, Status = Status.Active},
                        new GuestType() { Name="Đối tượng 1", DateCreated = DateTime.Now, DateModified = DateTime.Now, Status = Status.Active},
                    };
                _context.GuestTypes.AddRange(listGuest);
            }

            /* Lớp học */
            if (_context.LanguageClasses.Count() == 0)
            {

                List<LanguageClass> languageClasses = new List<LanguageClass>();
                List<Course> Course = _context.Courses.ToList();

                foreach (var item in Course)
                {
                    for (int j = 0; j < 10; j++)
                    {
                        LanguageClass _class = new LanguageClass()
                        {
                            Id = TextHelper.RandomString(50),
                            Name = "Lớp " + item.Name + " " + j.ToString(),
                            StartDay = new DateTime(2019, 6, 9),
                            EndDay = new DateTime(2019, 9, 6),
                            CourseFee = 20000,
                            LessonFee = 300,
                            MonthlyFee = 9000,
                            Note = "",
                            DateCreated = DateTime.Now,
                            DateModified = DateTime.Now,
                            Status = Status.Active,
                            CourseId = item.Id
                        };

                        languageClasses.Add(_class);
                    }
                }
                _context.LanguageClasses.AddRange(languageClasses);
            }

            /* Loại phiếu thu */
            if (_context.ReceiptTypes.Count() == 0)
            {

                List<ReceiptType> listReceiptTypes = new List<ReceiptType>();
                for (int j = 0; j < 10; j++)
                {
                    ReceiptType receiptType = new ReceiptType
                    {
                        Name = "Loại phiếu thu " + j.ToString(),
                        Note = "",
                        DateCreated = DateTime.Now,
                        DateModified = DateTime.Now,
                        Status = Status.Active
                    };

                    listReceiptTypes.Add(receiptType);
                }
                _context.ReceiptTypes.AddRange(listReceiptTypes);
            }

            /* Loại phiếu chi */
            if (_context.PaySlipTypes.Count() == 0)
            {

                List<PaySlipType> paySlipTypes = new List<PaySlipType>();
                for (int j = 0; j < 10; j++)
                {
                    PaySlipType paySlip = new PaySlipType()
                    {
                        Name = "Loại phiếu chi " + j.ToString(),
                        Note = "",
                        DateCreated = DateTime.Now,
                        DateModified = DateTime.Now,
                        Status = Status.Active
                    };

                    paySlipTypes.Add(paySlip);
                }
                _context.PaySlipTypes.AddRange(paySlipTypes);
            }



            #endregion
            /* Người học */
            if (_context.Learners.Count() == 0)
            {

                List<Learner> learners = new List<Learner>();
                for (int j = 0; j < 200; j++)
                {
                    Learner learner = new Learner()
                    {
                        CardId = TextHelper.RandomNumber(10),
                        FirstName = TextHelper.GenerateName(5),
                        LastName = TextHelper.GenerateName(4) + " " + TextHelper.GenerateName(4)
                    };

                    learner.Id = TextHelper.RandomString(50);
                    Random gen = new Random();
                    bool result = gen.Next(100) < 50 ? true : false;
                    learner.Sex = result;

                    Random r = new Random();
                    DateTime rDate = new DateTime(r.Next(1900, 2010), r.Next(1, 12), r.Next(1, 28));

                    learner.Birthday = rDate;
                    learner.Address = "Số 4c26, Khu 1, Lê Hồng Phong, Ngô Quyền, Hải Phòng";
                    learner.Email = TextHelper.EmailAddress(16);
                    learner.Facebook = @"https://www.facebook.com/" + TextHelper.RandomString(10);
                    learner.Phone = TextHelper.RandomNumber(10);
                    learner.ParentFullName = TextHelper.GenerateName(5) + TextHelper.GenerateName(4) + TextHelper.GenerateName(3);
                    learner.ParentPhone = TextHelper.RandomNumber(10);

                    Random rnd = new Random();
                    int temp = rnd.Next(1, 7);
                    learner.Image = _images[temp];
                    learner.GuestTypeId = temp;

                    learner.Note = "";
                    learner.DateCreated = DateTime.Now;
                    learner.DateModified = DateTime.Now;
                    learner.Status = Status.Active;

                    learners.Add(learner);
                }
                _context.Learners.AddRange(learners);
            }

            /* Giảng viên0*/
            if (_context.Lecturers.Count() == 0)
            {

                List<Lecturer> lecturers = new List<Lecturer>();
                for (int j = 0; j < 20; j++)
                {
                    Lecturer lecturer = new Lecturer()
                    {
                        CardId = TextHelper.RandomNumber(10),
                        FirstName = TextHelper.GenerateName(5),
                        LastName = TextHelper.GenerateName(4) + " " + TextHelper.GenerateName(4)
                    };

                    Random gen = new Random();
                    bool result = gen.Next(100) < 50 ? true : false;
                    lecturer.Sex = result;

                    Random r = new Random();
                    DateTime rDate = new DateTime(r.Next(1900, 2010), r.Next(1, 12), r.Next(1, 28));

                    lecturer.Birthday = rDate;
                    lecturer.Address = "Số 4c26, Khu 1, Lê Hồng Phong, Ngô Quyền, Hải Phòng";
                    lecturer.Email = TextHelper.EmailAddress(16);
                    lecturer.Facebook = @"https://www.facebook.com/" + TextHelper.RandomString(10);
                    lecturer.Phone = TextHelper.RandomNumber(10);

                    Random rnd = new Random();
                    int temp = rnd.Next(1, 7);
                    lecturer.Image = _images[temp];
                    lecturer.Nationality = _national[temp];
                    lecturer.MarritalStatus = gen.Next(100) < 50 ? 1 : 0;
                    lecturer.ExperienceRecord = "";
                    lecturer.Position = "Giáo viên";
                    lecturer.Certificate = "";

                    lecturer.BasicSalary = temp * 100000;
                    lecturer.Allowance = 50000;
                    lecturer.Bonus = temp * 200;
                    lecturer.InsurancePremium = temp * 15;
                    lecturer.WageOfLecturer = 30000;
                    lecturer.WageOfTutor = 15000;
                    lecturer.IsTutor = gen.Next(100) < 50 ? true : false;
                    lecturer.IsVisitingLecturer = gen.Next(100) < 50 ? true : false;

                    lecturer.Note = "";
                    lecturer.DateCreated = DateTime.Now;
                    lecturer.DateModified = DateTime.Now;
                    lecturer.Status = Status.Active;

                    lecturers.Add(lecturer);
                }
                _context.Lecturers.AddRange(lecturers);
            }

            /* Nhân viên */
            if (_context.Personnels.Count() == 0)
            {
                List<Personnel> personnels = new List<Personnel>();
                for (int j = 0; j < 20; j++)
                {
                    Personnel personnel = new Personnel()
                    {
                        CardId = TextHelper.RandomNumber(10),
                        FirstName = TextHelper.GenerateName(5),
                        LastName = TextHelper.GenerateName(4) + " " + TextHelper.GenerateName(4)
                    };

                    personnel.Id = TextHelper.RandomString(50);
                    Random gen = new Random();
                    bool result = gen.Next(100) < 50 ? true : false;
                    personnel.Sex = result;

                    Random r = new Random();
                    DateTime rDate = new DateTime(r.Next(1900, 2010), r.Next(1, 12), r.Next(1, 28));

                    personnel.Birthday = rDate;
                    personnel.Address = "Số 4c26, Khu 1, Lê Hồng Phong, Ngô Quyền, Hải Phòng";
                    personnel.Email = TextHelper.EmailAddress(16);
                    personnel.Facebook = @"https://www.facebook.com/" + TextHelper.RandomString(10);
                    personnel.Phone = TextHelper.RandomNumber(10);

                    Random rnd = new Random();
                    int temp = rnd.Next(1, 7);
                    personnel.Image = _images[temp];
                    personnel.Nationality = _national[temp];
                    personnel.MarritalStatus = gen.Next(100) < 50 ? 1 : 0;
                    personnel.ExperienceRecord = "";
                    personnel.Position = "Nhân viên";
                    personnel.Certificate = "";

                    personnel.BasicSalary = temp * 100000;
                    personnel.Allowance = 50000;
                    personnel.Bonus = temp * 200;
                    personnel.InsurancePremium = temp * 15;


                    personnel.Note = "";
                    personnel.DateCreated = DateTime.Now;
                    personnel.DateModified = DateTime.Now;
                    personnel.Status = Status.Active;

                    personnels.Add(personnel);
                }
                _context.Personnels.AddRange(personnels);
            }
            await _context.SaveChangesAsync();

            return Ok("Đã tạo dữ liệu thành công!");
        }


    }
}