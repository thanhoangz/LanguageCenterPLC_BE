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
            @"http://media.doisongphapluat.com/602/2019/8/16/loat-anh-mong-manh-suong-khoi-cua-tan-sinh-vien-dai-hoc-luat-3.jpeg",
            @"https://tokhoe.com/wp-content/uploads/2017/08/nu-dong-phuc2.jpg",
            @"https://photo-3-baomoi.zadn.vn/w700_r1/2018_11_17_329_28624679/ec30eb00ab41421f1b50.jpg",
            @"https://icdn.dantri.com.vn/2018/12/19/a-khoi-sinh-vien-viet-nam-13-1545233052369478376074.jpg",
            @"https://znews-photo.zadn.vn/w660/Uploaded/kcwvouvs/2017_03_23/3.jpg",
            @"https://dotobjyajpegd.cloudfront.net/photo/5ca5d137a449d212f32262d1",
            @"https://cdn.shopify.com/s/files/1/0074/9774/4451/products/DPG008_1_2048x.jpg?v=1547179896",
            @"http://vinhuni.edu.vn/data/1/upload/796/images//2017/05/thu_khoa_van_6.jpg",
            @"https://znews-photo.zadn.vn/w660/Uploaded/qjyyf/2014_02_10/534023_308934209250475_2140461783_n.jpg",
            @"https://sohanews.sohacdn.com/2017/photo-10-1505955382266.jpg",
            @"https://media.tintuc.vn/uploads/medias/2017/06/22/550x500/linh-ka-8-bb-baaadNmV5f.jpg",
            @"https://photo-3-baomoi.zadn.vn/w700_r1/2019_11_08_304_32869877/a6462976b2365b680227.jpg",

            @"http://sinhvienusa.org/wp-content/uploads/2016/02/img20160221230548033.jpg",
            @"https://we25.vn/media/images/nam-sinh-9x-dep-trai-khoa-fb(2).jpg",
            @"http://mcnews3.media.netnews.vn/tiin//archive/images/20180816/102607_hue2.jpg",
            @"https://quanghunggroup.com/wp-content/uploads/2019/06/6-kieu-toc-nam-dep-phu-hop-cho-hoc-sinh-5.jpg",
            @"http://www.qtv.vn/dataimages/201806//original/images1067062_mon_23.jpg",
            @"http://kenh14cdn.com/crop/640_360/2019/3/13/photo-1-1552412377302291295915-crop-1552412442632202196017.jpg",

            @"https://imgraovat.vnecdn.net/images/1280_768/2018/09/19/aff747265bac97b66ebd740a97657bf4.jpeg",
            @"https://dinhat.com/wp-content/uploads/2018/05/anh-the-di-xkld-nhat-ban.jpg",
            @"https://miro.medium.com/max/1336/0*pPSYwu08e3xZqiMF",
            @"https://learns.vn/wp-content/uploads/avatars/6/59d10bb9ab5db-bpfull.jpg",
            @"https://dichvucaptoc.com/wp-content/uploads/2019/04/hi%CC%80nh-a%CC%89nh-xin-visa-trung-quo%CC%82%CC%81c.jpg",
            @"http://file.hstatic.net/1000202498/file/cach-chup-anh-the-dep-cho-nam-2_grande.jpg",
            @"https://ssl.latcdn.com/img/aQrHfc1pM-anh-ho-so-dung-cho-don-xin-viec-word.jpg",
            @"https://scontent.fhan5-2.fna.fbcdn.net/v/t1.0-9/37078089_1981317955511560_2319863427440312320_n.jpg?_nc_cat=110&_nc_oc=AQlgH85LjOm5nWwKS2pDVxwXik_yX9kuGSysGVUjyVdMyXwWBYYi1xFBQaLykwDRa7XKSt8k_flVrEw1qwZQoTvE&_nc_ht=scontent.fhan5-2.fna&oh=172454f381e17b1ad066fc5d0648686c&oe=5E2B2C43",

        };

        public List<string> _national = new List<string>()
        {
            "Việt Nam","Mỹ","Úc","Canada","Pháp","Nhật","Trung quốc"
        };


        public List<string> firstName = new List<string>()
        {
            "Nguyễn","Vũ","Đào","Lê","Trần","Bá","Tôn","Triệu","Khinh","Vương","Lý","Lưu","Hoàng","Ngô","Cao","Bác","Kiến"
        };

        public List<string> midNameFemale = new List<string>()
        {
            "Ái","Lan","Ngọc","Như","Quỳnh","Thảo","Thu","Thủy","Trâm","Hạ","Gia"
        };

        public List<string> midNameMale = new List<string>()
        {
            "Nhược","Hải","Tịnh","Kỳ","Y","Đức","Cao","Vĩ","Khải","Bằng","Dương","Hạ","Gia","Lịch"
        };

        public List<string> lastNameMale = new List<string>()
        {
            "Lãng","Hiên","Cường","Kỳ","Kiệt","Đức","Cao","Vĩ","Khải","Bằng","Dương","Hạc","Gia","Lịch"
        };
        public List<string> lastNameFemale = new List<string>()
        {
            "Duệ","Lệ","Vũ","Tú","Quỳnh","Tuyết","Trân","Liên","Di","Nguyệt","Kha","Hi","Giai","Dao"
        };

        #endregion
        [HttpGet]
        [Route("CreateSample_Category")]
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
                    Avatar = "https://scontent.fhan5-5.fna.fbcdn.net/v/t1.0-9/52082273_1989923987978968" +
                    "_1959613751179083776_n.jpg?_nc_cat=108&_nc_oc=AQk93G0qSS8w6k_AgIwyP1r2vIEhf2a5fis-pX8JAzlSIZ-Gl5YRvfOoWAwXoMvb7" +
                    "2WwiMscWvXq0ZUFf5u0PULR&_nc_ht=scontent.fhan5-5.fna&oh=8a4cab9d2f3c88697ff99f10b5ba3d9c&oe=5E53E27B",
                    DateCreated = DateTime.Now,
                    DateModified = DateTime.Now,
                    Status = Status.Active
                }, "123@abcABC");
                var user = await _userManager.FindByNameAsync("admin");
                await _userManager.AddToRoleAsync(user, "Admin");

                await _userManager.CreateAsync(new AppUser()
                {
                    UserName = "staff",
                    FullName = "Hoàng Tiến Dũng",
                    Email = "dungbo.97@gmail.com",
                    Balance = 0,
                    Avatar = "https://scontent.fhan5-6.fna.fbcdn.net/v/t31.0-8/22104773_1960221297599474_6856562694427714577_o.jpg?_nc_c" +
                    "at=105&_nc_oc=AQmDSn9Z89MfHotI2z6EFYe_2T1UQnS1FcT_wpFakXGcIpquCDhvPK_9oTU_7ytcvZUMxrA8q1MZ1UVNp5JurNAJ&_nc_ht=scontent.fhan5-6" +
                    ".fna&oh=54b7c2470c85e8e043ba916459043189&oe=5E257381",
                    DateCreated = DateTime.Now,
                    DateModified = DateTime.Now,
                    Status = Status.Active
                }, "123@abcABC");
                var user1 = await _userManager.FindByNameAsync("staff");
                await _userManager.AddToRoleAsync(user1, "Staff");

                await _userManager.CreateAsync(new AppUser()
                {
                    UserName = "lecturer",
                    FullName = "Nguyễn Viết Phương",
                    Email = "nguyenvietphuong10@gmail.com",
                    Balance = 0,
                    Avatar = "https://scontent.fhan5-4.fna.fbcdn.net/v/t1.0-9/57016879_2299835780304689_8351403043366895616_o.jpg" +
                    "?_nc_cat=104&_nc_oc=AQnKozlDuU5L-b49v_WCOOGTcpADQyh87YA8Wis6jhGtoL63mdtJ_LjBnAr7bJO43NEF4qLB9b51HvNw5eGFbxNE&_nc_ht=scontent.fhan5-4." +
                    "fna&oh=0be678dc839a72de1542e8c16476bfc6&oe=5E5EC3BF",
                    DateCreated = DateTime.Now,
                    DateModified = DateTime.Now,
                    Status = Status.Active
                }, "123@abcABC");
                var user2 = await _userManager.FindByNameAsync("lecturer");
                await _userManager.AddToRoleAsync(user2, "Lecturer");
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
                        new Course() { Name="Anh văn cơ bản",Content="", TraingTime = 4, Price = 208000,NumberOfSession = 32, DateCreated = DateTime.Now, DateModified = DateTime.Now, Status = Status.Active},
                        new Course() { Name="Giao tiếp",Content="", TraingTime = 4, Price = 202000,NumberOfSession = 32, DateCreated = DateTime.Now, DateModified = DateTime.Now, Status = Status.Active},
                        new Course() { Name="Luyện phát âm",Content="", TraingTime = 9, Price = 20000,NumberOfSession = 32, DateCreated = DateTime.Now, DateModified = DateTime.Now, Status = Status.Active},
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

            await _context.SaveChangesAsync();

            return Ok("Đã tạo dữ liệu thành công!");
        }
        [HttpGet]
        [Route("CreateSample_Category_2")]
        public async Task<Object> CreateSampleData_2()
        {
            /* Lớp học */
            if (_context.LanguageClasses.Count() == 0)
            {

                List<LanguageClass> languageClasses = new List<LanguageClass>();
                List<Course> Course = _context.Courses.ToList();

                foreach (var item in Course)
                {
                    for (int j = 0; j < 5; j++)
                    {
                        LanguageClass _class = new LanguageClass()
                        {
                            Id = TextHelper.RandomString(10),
                            Name = item.Name + " " + j.ToString(),
                            StartDay = new DateTime(2019, 6, 9),
                            EndDay = new DateTime(2019, 9, 6),
                            CourseFee = 20000,
                            LessonFee = 300,
                            MonthlyFee = 9000,
                            Note = "",
                            DateCreated = DateTime.Now,
                            DateModified = DateTime.Now,
                            Status = Status.Active,
                            CourseId = item.Id,
                            MaxNumber = 15
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
                ReceiptType receiptType = new ReceiptType
                {
                    Name = "Thu học phí",
                    Note = "",
                    DateCreated = DateTime.Now,
                    DateModified = DateTime.Now,
                    Status = Status.Active
                };

                ReceiptType receiptTypeA = new ReceiptType
                {
                    Name = "Thu quỹ lớp",
                    Note = "",
                    DateCreated = DateTime.Now,
                    DateModified = DateTime.Now,
                    Status = Status.Active
                };

                ReceiptType receiptTypeB = new ReceiptType
                {
                    Name = "Thu khác",
                    Note = "",
                    DateCreated = DateTime.Now,
                    DateModified = DateTime.Now,
                    Status = Status.Active
                };

                listReceiptTypes.Add(receiptType);
                listReceiptTypes.Add(receiptTypeA);
                listReceiptTypes.Add(receiptTypeB);
                _context.ReceiptTypes.AddRange(listReceiptTypes);
            }

            /* Loại phiếu chi */
            if (_context.PaySlipTypes.Count() == 0)
            {

                List<PaySlipType> paySlipTypes = new List<PaySlipType>();
                PaySlipType paySlip = new PaySlipType()
                {
                    Name = "Tạm ứng lương",
                    Note = "",
                    DateCreated = DateTime.Now,
                    DateModified = DateTime.Now,
                    Status = Status.Active
                };
                paySlipTypes.Add(paySlip);

                PaySlipType paySlipA = new PaySlipType()
                {
                    Name = "Chi phí điện nước",
                    Note = "",
                    DateCreated = DateTime.Now,
                    DateModified = DateTime.Now,
                    Status = Status.Active
                };
                paySlipTypes.Add(paySlipA);

                PaySlipType paySlipD = new PaySlipType()
                {
                    Name = "Chi phí mua CSVC",
                    Note = "",
                    DateCreated = DateTime.Now,
                    DateModified = DateTime.Now,
                    Status = Status.Active
                };
                paySlipTypes.Add(paySlipA);

                PaySlipType paySlipB = new PaySlipType()
                {
                    Name = "Chi phí internet",
                    Note = "",
                    DateCreated = DateTime.Now,
                    DateModified = DateTime.Now,
                    Status = Status.Active
                };
                paySlipTypes.Add(paySlipB);


                PaySlipType paySlipC = new PaySlipType()
                {
                    Name = "Chi phí khác",
                    Note = "",
                    DateCreated = DateTime.Now,
                    DateModified = DateTime.Now,
                    Status = Status.Active
                };
                paySlipTypes.Add(paySlipC);
                _context.PaySlipTypes.AddRange(paySlipTypes);
            }



            #endregion
            /* Người học */
            if (_context.Learners.Count() == 0)
            {

                List<Learner> learners = new List<Learner>();
                for (int j = 0; j < 50; j++)
                {
                    Learner learner = new Learner()
                    {
                        CardId = TextHelper.RandomNumber(10)
                    };

                    learner.Id = TextHelper.RandomString(10);
                    learner.CardId = "HV" + "000" + ((j < 10) ? "000" + j.ToString() : (j < 100) ? "00" + j.ToString() : (j < 1000) ? "0" + j.ToString() : j.ToString());
                    Random gen = new Random();
                    bool result = gen.Next(100) < 50 ? true : false;
                    learner.Sex = result;

                    if (learner.Sex)
                    {
                        Random ranName = new Random();
                        learner.FirstName = firstName[ranName.Next(16)] + " " + midNameMale[ranName.Next(14)];
                        learner.LastName = lastNameMale[ranName.Next(14)];

                        int im = ranName.Next(12, 17);
                        learner.Image = _images[im];
                    }
                    else
                    {
                        Random ranName = new Random();
                        learner.FirstName = firstName[ranName.Next(16)] + " " + midNameFemale[ranName.Next(11)];
                        learner.LastName = lastNameFemale[ranName.Next(14)];

                        int im = ranName.Next(1, 11);
                        learner.Image = _images[im];
                    }
                    Random r = new Random();
                    DateTime rDate = new DateTime(r.Next(1990, 2010), r.Next(1, 12), r.Next(1, 28));

                    learner.Birthday = rDate;
                    learner.Address = "Số 4c26, Khu 1, Lê Hồng Phong, Ngô Quyền, Hải Phòng";
                    learner.Email = TextHelper.EmailAddress(16);
                    learner.Facebook = @"https://www.facebook.com/" + TextHelper.RandomString(10);
                    learner.Phone = TextHelper.RandomNumber(10);
                    learner.ParentFullName = firstName[r.Next(16)] + " " + midNameMale[r.Next(14)] + " " + lastNameMale[r.Next(14)];
                    learner.ParentPhone = TextHelper.RandomNumber(10);

                    Random rnd = new Random();
                    int temp;
                    temp = rnd.Next(1, 7);
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
                for (int j = 0; j < 15; j++)
                {
                    Lecturer lecturer = new Lecturer()
                    {
                        CardId = TextHelper.RandomNumber(10)
                    };

                    Random gen = new Random();
                    bool result = gen.Next(100) < 50 ? true : false;
                    lecturer.Sex = result;

                    if (lecturer.Sex)
                    {
                        Random ranName = new Random();
                        lecturer.FirstName = firstName[ranName.Next(16)] + " " + midNameMale[ranName.Next(14)];
                        lecturer.LastName = lastNameMale[ranName.Next(14)];
                    
                    }
                    else
                    {
                        Random ranName = new Random();
                        lecturer.FirstName = firstName[ranName.Next(16)] + " " + midNameFemale[ranName.Next(11)];
                        lecturer.LastName = lastNameFemale[ranName.Next(14)];
                    }

                    lecturer.CardId = "GV" + "000" + ((j < 10) ? "000" + j.ToString() : (j < 100) ? "00" + j.ToString() : (j < 1000) ? "0" + j.ToString() : j.ToString());
                    Random r = new Random();
                    DateTime rDate = new DateTime(r.Next(1900, 1985), r.Next(1, 12), r.Next(1, 28));

                    lecturer.Birthday = rDate;
                    lecturer.Address = "Số 4c26, Khu 1, Lê Hồng Phong, Ngô Quyền, Hải Phòng";
                    lecturer.Email = TextHelper.EmailAddress(16);
                    lecturer.Facebook = @"https://www.facebook.com/" + TextHelper.RandomString(10);
                    lecturer.Phone = TextHelper.RandomNumber(10);

                    Random rnd = new Random();
                    int temp = rnd.Next(18, 25);
                    lecturer.Image = _images[temp];

                    temp = rnd.Next(1, 7);
                    lecturer.Nationality = _national[temp];
                    lecturer.MarritalStatus = gen.Next(100) < 50 ? 1 : 0;
                    lecturer.ExperienceRecord = "";
                    lecturer.Position = "Giáo viên";
                    lecturer.Certificate = "";

                    temp = rnd.Next(1, 30);
                    lecturer.BasicSalary = 12500000;
                    lecturer.Allowance = 500000;
                    lecturer.Bonus = 500000;
                    lecturer.InsurancePremium = 0;
                    lecturer.WageOfLecturer = 500000;
                    lecturer.WageOfTutor = 200000;
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
                for (int j = 0; j < 17; j++)
                {
                    Personnel personnel = new Personnel()
                    {
                        CardId = TextHelper.RandomNumber(10)
                    };

                    personnel.Id = TextHelper.RandomString(10);
                    personnel.CardId = "NV" + "000" + ((j < 10) ? "000" + j.ToString() : (j < 100) ? "00" + j.ToString() : (j < 1000) ? "0" + j.ToString() : j.ToString());
                    Random gen = new Random();
                    bool result = gen.Next(100) < 50 ? true : false;
                    personnel.Sex = result;

                    if (personnel.Sex)
                    {
                        Random ranName = new Random();
                        personnel.FirstName = firstName[ranName.Next(16)] + " " + midNameMale[ranName.Next(14)];
                        personnel.LastName = lastNameMale[ranName.Next(14)];
                    }
                    else
                    {
                        Random ranName = new Random();
                        personnel.FirstName = firstName[ranName.Next(16)] + " " + midNameFemale[ranName.Next(11)];
                        personnel.LastName = lastNameFemale[ranName.Next(14)];
                    }

                    Random r = new Random();
                    DateTime rDate = new DateTime(r.Next(1900, 2010), r.Next(1, 12), r.Next(1, 28));

                    personnel.Birthday = rDate;
                    personnel.Address = "Số 4c26, Khu 1, Lê Hồng Phong, Ngô Quyền, Hải Phòng";
                    personnel.Email = TextHelper.EmailAddress(16);
                    personnel.Facebook = @"https://www.facebook.com/" + TextHelper.RandomString(10);
                    personnel.Phone = TextHelper.RandomNumber(10);

                    Random rnd = new Random();
                    int temp = rnd.Next(1, 25);
                    personnel.Image = _images[temp];
                    temp = rnd.Next(1, 7);
                    personnel.Nationality = _national[temp];
                    personnel.SalaryOfDay = 300000;
                    personnel.MarritalStatus = gen.Next(100) < 50 ? 1 : 0;
                    personnel.ExperienceRecord = "";
                    personnel.Position = "Nhân viên";
                    personnel.Certificate = "";
                    temp = rnd.Next(1, 30);
                    personnel.BasicSalary =7500000;
                    personnel.Allowance = 500000;
                    personnel.Bonus = 500000;
                    personnel.InsurancePremium = 0;


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


        [HttpGet]
        [Route("CreateSample_Category_3")]
        public async Task<Object> CreateSampleData_3()
        {
            if (_context.Functions.Count() == 0)
            {
                List<Function> functions = new List<Function>()
                {
                    new Function()
                    {
                        Id = "7rjHufjhgg",
                        Name = "Danh sách lớp",
                        URL = "admin/class-list",
                        SortOrder = 1,
                        Status = Status.Active,
                    },
                    new Function()
                    {
                        Id = "owqkazmakauwmajlop",
                        Name = "Danh sách lớp",
                        URL = "",
                        SortOrder = 35,
                        Status = Status.Active,
                        ParentId = "7rjHufjhgg",
                    },
                    new Function()
                    {
                        Id = "82fxsSM76d",
                        Name = "Cấu hình trung tâm",
                        URL = "",
                        SortOrder = 2,
                        Status = Status.Active,
                    },
                    new Function()
                    {
                        Id = "cZ5kTRruS4",
                        Name = "Lớp học",
                        URL = "admin/language-classes",
                        SortOrder = 3,
                        Status = Status.Active,
                        ParentId = "82fxsSM76d",

                    },
                    new Function()
                    {
                        Id = "uZh6mCgxL7",
                        Name = "Phòng học",
                        URL = "admin/classroom",
                        SortOrder = 4,
                        Status = Status.Active,
                        ParentId = "82fxsSM76d",
                    },
                    new Function()
                    {
                        Id = "fgfghtyu5654fgj",
                        Name = "Đối tượng",
                        URL = "admin/guest-type",
                        SortOrder = 5,
                        Status = Status.Active,
                        ParentId = "82fxsSM76d",
                    },
                    new Function()
                    {
                        Id = "dfgfu67y867745jkghhdfg",
                        Name = "Khóa học",
                        URL = "admin/course",
                        SortOrder = 6,
                        Status = Status.Active,
                        ParentId = "82fxsSM76d",
                    },
                    new Function()
                    {
                        Id = "9658741253",
                        Name = "Loại phiếu thu",
                        URL = "admin/course",
                        SortOrder = 7,
                        Status = Status.Active,
                        ParentId = "82fxsSM76d",
                    },
                    new Function()
                    {
                        Id = "8745895142dfg",
                        Name = "Loại phiếu chi",
                        URL = "admin/course",
                        SortOrder = 8,
                        Status = Status.Active,
                        ParentId = "82fxsSM76d",
                    },
                    new Function()
                    {
                        Id = "dkjfhreij5h234598ufdfh",
                        Name = "Quản lý học tập",
                        URL = "",
                        SortOrder = 9,
                        Status = Status.Active,
                    },
                    new Function()
                    {
                        Id = "dfgfkfglkjet6hgu",
                        Name = "Xếp lịch học",
                        URL = "",
                        SortOrder = 10,
                        Status = Status.Active,
                        ParentId = "dkjfhreij5h234598ufdfh"
                    },
                    new Function()
                    {
                        Id = "lkjdfgjk4j5456kj",
                        Name = "Quản lý điểm định kỳ",
                        URL = "",
                        SortOrder = 11,
                        Status = Status.Active,
                        ParentId = "dkjfhreij5h234598ufdfh"
                    },
                     new Function()
                    {
                        Id = "lkjdfgjk4j90807",
                        Name = "Quản lý điểm cuối khóa",
                        URL = "",
                        SortOrder = 12,
                        Status = Status.Active,
                        ParentId = "dkjfhreij5h234598ufdfh"
                    },
                     new Function()
                    {
                        Id = "klasldksai69",
                        Name = "Quản lý học viên",
                        URL = "",
                        SortOrder = 13,
                        Status = Status.Active
                    },
                      new Function()
                    {
                        Id = "ljxkkojk4j90807",
                        Name = "Học viên",
                        URL = "",
                        SortOrder = 14,
                        Status = Status.Active,
                        ParentId = "klasldksai69"
                    },
                       new Function()
                    {
                        Id = "ooiusilxla686",
                        Name = "Thêm mới học viên",
                        URL = "",
                        SortOrder = 15,
                        Status = Status.Active,
                        ParentId = "klasldksai69"
                    },
                        new Function()
                    {
                        Id = "mlakuowzgakbkaoppq799",
                        Name = "Điểm danh",
                        URL = "",
                        SortOrder = 16,
                        Status = Status.Active,
                        ParentId = "klasldksai69"
                    },
                        new Function()
                    {
                        Id = "zmzjakuqooznueu01919",
                        Name = "Xếp lớp",
                        URL = "",
                        SortOrder = 17,
                        Status = Status.Active,
                        ParentId = "klasldksai69"
                    },
                         new Function()
                    {
                        Id = "nzmgayoruynzeql1235678",
                        Name = "Quá trình học tập",
                        URL = "",
                        SortOrder = 18,
                        Status = Status.Active,
                        ParentId = "klasldksai69"
                    },
                          new Function()
                    {
                        Id = "uqoozlimabocmhvgq98",
                        Name = "Quản lý nhân sự",
                        URL = "",
                        SortOrder = 19,
                        Status = Status.Active
                    },
                          new Function()
                    {
                        Id = "vbadsamdlaa3218as",
                        Name = "Giáo viên",
                        URL = "",
                        SortOrder = 20,
                        Status = Status.Active,
                        ParentId = "uqoozlimabocmhvgq98"
                    },
                           new Function()
                    {
                        Id = "cxaxquoamzhohwu52525nvnv",
                        Name = "Nhân viên",
                        URL = "",
                        SortOrder = 21,
                        Status = Status.Active,
                        ParentId = "uqoozlimabocmhvgq98"
                    },
                           new Function()
                    {
                        Id = "mkzmhakhzhchamcongnv",
                        Name = "Chấm công nhân viên",
                        URL = "",
                        SortOrder = 22,
                        Status = Status.Active,
                        ParentId = "uqoozlimabocmhvgq98"
                    },
                            new Function()
                    {
                        Id = "opanmjkzwotuzngtw128nsdh3",
                        Name = "Tính lương",
                        URL = "",
                        SortOrder = 23,
                        Status = Status.Active,
                        ParentId = "uqoozlimabocmhvgq98"
                    },
                             new Function()
                    {
                        Id = "qumnzjaqyqo731asdnahthuchi",
                        Name = "Quản lý thu chi",
                        URL = "",
                        SortOrder = 24,
                        Status = Status.Active
                    },
                              new Function()
                    {
                        Id = "qpqnamzyoqmzh25628mzk",
                        Name = "Phiếu thu",
                        URL = "",
                        SortOrder = 25,
                        Status = Status.Active,
                        ParentId = "qumnzjaqyqo731asdnahthuchi"
                    },
                              new Function()
                    {
                        Id = "mzkhakuqjnzgkwghroq68758868",
                        Name = "Phiếu chi",
                        URL = "",
                        SortOrder = 26,
                        Status = Status.Active,
                        ParentId = "qumnzjaqyqo731asdnahthuchi"
                    },
                              new Function()
                    {
                        Id = "bctkquaomzawdkzgaka",
                        Name = "Báo cáo thống kê",
                        URL = "",
                        SortOrder = 27,
                        Status = Status.Active,
                    },
                              new Function()
                    {
                        Id = "bcccnv123456789",
                        Name = "Báo cáo chấm công",
                        URL = "",
                        SortOrder = 28,
                        Status = Status.Active,
                        ParentId = "bctkquaomzawdkzgaka"
                    },

                              new Function()
                    {
                        Id = "bcdck123456789",
                        Name = "Báo cáo điểm cuối khóa",
                        URL = "",
                        SortOrder = 29,
                        Status = Status.Active,
                        ParentId = "bctkquaomzawdkzgaka"

                    },
                              new Function()
                    {
                        Id = "bcddkaiwwianh68",
                        Name = "Báo cáo điểm đình kỳ",
                        URL = "",
                        SortOrder = 30,
                        Status = Status.Active,
                        ParentId = "bctkquaomzawdkzgaka"

                    },
                               new Function()
                    {
                        Id = "bcdonghocphi32u28281",
                        Name = "Báo cáo đóng học phí",
                        URL = "",
                        SortOrder = 31,
                        Status = Status.Active,
                        ParentId = "bctkquaomzawdkzgaka"
                    },
                               new Function()
                    {
                        Id = "bcdiemdanh62121sajdsak",
                        Name = "Báo cáo điểm danh",
                        URL = "",
                        SortOrder = 32,
                        Status = Status.Active,
                        ParentId = "bctkquaomzawdkzgaka"
                    },
                               new Function()
                    {
                        Id = "bcchuadonghocphi",
                        Name = "Báo cáo chưa đóng học phí",
                        URL = "",
                        SortOrder = 33,
                        Status = Status.Active,
                        ParentId = "bctkquaomzawdkzgaka"
                    },
                               new Function()
                    {
                        Id = "bcdanhsachlop",
                        Name = "Báo cáo danh sách lớp",
                        URL = "",
                        SortOrder = 34,
                        Status = Status.Active,
                        ParentId = "bctkquaomzawdkzgaka"
                    },
                };
                _context.Functions.AddRange(functions);
            }

            if (_context.Permissions.Count() == 0)
            {
                List<Permission> permissions = new List<Permission>();

                var functionc = _context.Functions;
                var users = _context.AppUsers;
                foreach (var item in users)
                {
                    foreach (var fun in functionc)
                    {
                        Permission permission = new Permission();
                        permission.AppUserId = item.Id;
                        permission.FunctionId = fun.Id;
                        permission.CanCreate = true;
                        permission.CanDelete = true;
                        permission.CanRead = true;
                        permission.CanUpdate = true;
                        permission.Status = Status.Active;
                        permissions.Add(permission);
                    }
                }
                _context.Permissions.AddRange(permissions);
            }

            if (_context.TimeShifts.Count() == 0)
            {
                List<TimeShift> timeShifts = new List<TimeShift>()
                {
                    new TimeShift(){ Name = "Ca 1", FromTime = new TimeSpan(7, 0, 0), ToTime = new TimeSpan(9, 0, 0)},
                    new TimeShift(){ Name = "Ca 2", FromTime = new TimeSpan(8, 0, 0), ToTime = new TimeSpan(10, 0, 0)},
                    new TimeShift(){ Name = "Ca 3", FromTime = new TimeSpan(9, 0, 0), ToTime = new TimeSpan(11, 0, 0)},
                    new TimeShift(){ Name = "Ca 4", FromTime = new TimeSpan(14, 0, 0), ToTime = new TimeSpan(16, 0, 0)},
                    new TimeShift(){ Name = "Ca 5", FromTime = new TimeSpan(15, 0, 0), ToTime = new TimeSpan(17, 0, 0)},
                    new TimeShift(){ Name = "Ca 6", FromTime = new TimeSpan(16, 0, 0), ToTime = new TimeSpan(18, 0, 0)},
                    new TimeShift(){ Name = "Ca 7", FromTime = new TimeSpan(19, 0, 0), ToTime = new TimeSpan(21, 0, 0)},
                };

            
                _context.TimeShifts.AddRange(timeShifts);
            }

            await _context.SaveChangesAsync();

            return Ok("Đã tạo dữ liệu thành công!");

        }



        [HttpGet]
        [Route("CreateSample_Category_4")]
        public async Task<Object> CreateSampleData_4()
        {

            if (_context.TeachingSchedules.Count() == 0)
            {
                List<TeachingSchedule> teachingSchedules = new List<TeachingSchedule>();

                for (int i = 0; i < 10; i++)
                {
                    TeachingSchedule teachingSchedule = new TeachingSchedule();
                    teachingSchedule.FromDate = DateTime.Now;

                    Random rnd = new Random();
                    int temp = rnd.Next(1, 60);
                    teachingSchedule.ToDate = teachingSchedule.FromDate.AddDays(temp);
                    temp = rnd.Next(1, 4);
                    teachingSchedule.DaysOfWeek = temp;
                    teachingSchedule.Status = Status.Pause;
                    teachingSchedule.DateCreated = DateTime.Now;
                    teachingSchedule.DateModified = DateTime.Now;
                    temp = rnd.Next(1, 14);
                    teachingSchedule.LecturerId = _context.Lecturers.ToList()[temp].Id;
                    temp = rnd.Next(1, 15);
                    teachingSchedule.ClassroomId = _context.Classrooms.ToList()[temp].Id;
                    teachingSchedule.LanguageClassId = _context.LanguageClasses.ToList()[i].Id;

                    teachingSchedules.Add(teachingSchedule);
                }
                _context.TeachingSchedules.AddRange(teachingSchedules);
            }



            if (_context.ClassSessions.Count() == 0)
            {
                List<ClassSession> classSessions = new List<ClassSession>();
                var teachingSchedules = _context.TeachingSchedules.ToList();
                foreach (var item in teachingSchedules)
                {

                    TimeSpan Time = item.ToDate - item.FromDate;
                    int totalDay = Time.Days;
                    Random rnd = new Random();
                    int temp = rnd.Next(1, 3);
                    var learnDate = item.FromDate;
                    int timeshiftTemp = rnd.Next(0, 6);
                    var timeshift = _context.TimeShifts.ToList()[timeshiftTemp];
                    for (int i = 1; i <= totalDay; i++)
                    {

                        if (temp == 1)
                        {
                            if (
                                learnDate.DayOfWeek.ToString() == "Monday"
                                || learnDate.DayOfWeek.ToString() == "Wednesday"
                                || learnDate.DayOfWeek.ToString() == "Friday"
                                )
                            {
                                ClassSession classSession1 = new ClassSession();
                                classSession1.Date = learnDate;
                                classSession1.FromTime = timeshift.FromTime;
                                classSession1.ToTime = timeshift.ToTime;
                                classSession1.TeachingScheduleId = item.Id;
                                classSessions.Add(classSession1);
                            }

                        }
                        else
                        {
                            if (
                                learnDate.DayOfWeek.ToString() == "Tuesday"
                                || learnDate.DayOfWeek.ToString() == "Thursday"
                                || learnDate.DayOfWeek.ToString() == "Saturday"
                                )
                            {
                                ClassSession classSession1 = new ClassSession();
                                classSession1.Date = learnDate;
                                classSession1.FromTime = timeshift.FromTime;
                                classSession1.ToTime = timeshift.ToTime;
                                classSession1.TeachingScheduleId = item.Id;
                                classSessions.Add(classSession1);
                            }
                        }
                        learnDate = learnDate.AddDays(1);
                    }
                }


                _context.ClassSessions.AddRange(classSessions);
            }


            await _context.SaveChangesAsync();

            return Ok("Đã tạo dữ liệu thành công!");

        }
    }
}