using AutoMapper;
using DinkToPdf;
using IronPdf;
using LanguageCenterPLC.Application.Interfaces;
using LanguageCenterPLC.Application.ViewModels.Studies;
using LanguageCenterPLC.Data.EF;
using LanguageCenterPLC.Data.Entities;
using LanguageCenterPLC.Infrastructure.Enums;
using LanguageCenterPLC.Utilities.Helpers;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LanguageCenterPLC.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SalaryController : ControllerBase
    {
        private readonly ITimesheetService _timesheetService;
        private readonly AppDbContext _context;
        public SalaryController(AppDbContext context, ITimesheetService timesheetService)
        {
            _context = context;
            _timesheetService = timesheetService;
        }

        [HttpPost]
        [Route("paied-roll-personnels")]
        public async Task<List<Object>> PaiedPersonnels(int month, int year)
        {
            var salaryPaies = _context.SalaryPays.Where(x => x.Month == month && x.Year == year && !string.IsNullOrEmpty(x.PersonnelId)).ToList();
            var resultList = new List<Object>();
            foreach (var item in salaryPaies)
            {
                var personnel = _context.Personnels.Find(item.PersonnelId);
                var paiedPersonnel = new
                {
                    PerSonnel = personnel,
                    Salary = item
                };
                resultList.Add(paiedPersonnel);
            }
            return await Task.FromResult(resultList);
        }

        [HttpPost]
        [Route("not-paied-roll-personnels")]
        public async Task<List<Object>> NotPaiedPersonnels(int month, int year)
        {
            var salaryPaies = _context.SalaryPays.Where(x => x.Month == month && x.Year == year && !string.IsNullOrEmpty(x.PersonnelId)).ToList();
            var timeSheets = _context.Timesheets.Where(x => x.Month == month && x.Year == year && x.Status == Status.Active).ToList();

            var timeSheetInSalary = new List<Timesheet>();
            if (salaryPaies.Count != 0)
            {
                foreach (var salary in salaryPaies)
                {
                    var temp = timeSheets.Where(x => x.PersonnelId == salary.PersonnelId).SingleOrDefault();
                    if (temp != null)
                    {
                        timeSheetInSalary.Add(temp);
                    }
                }
            }

            var timeSheetNotInSalary = new List<Timesheet>();
            foreach (var item in timeSheets)
            {
                if (!timeSheetInSalary.Contains(item))
                {
                    timeSheetNotInSalary.Add(item);
                }
            }

            var resultList = new List<Object>();
            foreach (var item in timeSheetNotInSalary)
            {
                var personnel = _context.Personnels.Find(item.PersonnelId);
                var temp = new
                {
                    id = item.Id,
                    TotalBasicSalary = item.Salary,         // lương cơ bản
                    TotalSalaryOfDay = item.SalaryOfDay,    // luong theo ngày
                    TotalAllowance = item.Allowance,
                    TotalAdvancePayment = item.AdvancePayment,  // tạm ứng
                    TotalBonus = item.Bonus,
                    TotalWorkdays = item.TotalWorkday,          // số công
                    TotalTheoreticalAmount = item.SalaryOfDay * Convert.ToDecimal(item.TotalWorkday) + item.Allowance + item.Bonus,   // tổng lương   
                    TotalInsurancePremium = ((item.SalaryOfDay * Convert.ToDecimal(item.TotalWorkday) + item.Allowance + item.Bonus) * 8) / 100,
                    TotalRealityAmount = item.SalaryOfDay * Convert.ToDecimal(item.TotalWorkday) + item.Allowance + item.Bonus - (((item.SalaryOfDay * Convert.ToDecimal(item.TotalWorkday) + item.Allowance + item.Bonus) * 8) / 100) - item.AdvancePayment,  // tiền nhận đc
                    item.Month,
                    item.Year
                };

                var paiedPersonnel = new
                {
                    PerSonnel = personnel,
                    Salary = temp
                };
                resultList.Add(paiedPersonnel);
            }
            return await Task.FromResult(resultList);
        }

        [HttpPost]
        [Route("payroll-approval-staff")]
        public async Task<Object> PayrollApprovalStaff(List<int> timeSheetList)
        {
            try
            {
                await Task.Run(() =>
                {
                    List<SalaryPay> salaryPays = new List<SalaryPay>();
                    foreach (var item in timeSheetList)
                    {
                        SalaryPay salaryPay = new SalaryPay();
                        var timeSheet = _timesheetService.GetById(item);
                        salaryPay.PersonnelId = timeSheet.PersonnelId;
                        salaryPay.TotalBasicSalary = timeSheet.Salary;
                        salaryPay.TotalAllowance = timeSheet.Allowance;
                        salaryPay.TotalBonus = timeSheet.Bonus;
                        salaryPay.TotalAdvancePayment = timeSheet.AdvancePayment;
                        salaryPay.TotalSalaryOfDay = timeSheet.SalaryOfDay;
                        salaryPay.TotalWorkdays = timeSheet.TotalWorkday;
                        salaryPay.TotalTheoreticalAmount = timeSheet.SalaryOfDay * Convert.ToDecimal(timeSheet.TotalWorkday) + timeSheet.Allowance + timeSheet.Bonus;     // tổng lương    
                        salaryPay.TotalInsurancePremium = ((timeSheet.SalaryOfDay * Convert.ToDecimal(timeSheet.TotalWorkday) + timeSheet.Allowance + timeSheet.Bonus) * 8) / 100;
                        // nhận đc bên dưới
                        salaryPay.TotalRealityAmount = salaryPay.TotalSalaryOfDay * Convert.ToDecimal(salaryPay.TotalWorkdays) + salaryPay.TotalAllowance + salaryPay.TotalBonus - salaryPay.TotalInsurancePremium - salaryPay.TotalAdvancePayment;
                        salaryPay.Month = timeSheet.Month;
                        salaryPay.Year = timeSheet.Year;
                        salaryPays.Add(salaryPay);
                    }

                    _context.SalaryPays.AddRange(salaryPays);
                    _context.SaveChanges();
                });
            }
            catch
            {
                throw new Exception(string.Format("Có lỗi xảy ra !"));
            }

            return Ok();
        }

        // POST: api/TeachingSchedules
        [HttpPost]
        [Route("add-lecturer-payroll")]
        public async Task<Object> AddListLecture(List<SalaryPay> listGV)
        {
            try
            {
                await Task.Run(() =>
                {

                    _context.SalaryPays.AddRange(listGV);
                    _context.SaveChanges();
                });
            }
            catch
            {
                throw new Exception(string.Format("Có lỗi xảy ra !"));
            }

            return Ok();
        }

        [HttpPost]
        [Route("paied-roll-lecturers")]
        public async Task<List<Object>> PaiedLecturers(int month, int year)
        {
            var salaryPaies = _context.SalaryPays.Where(x => x.Month == month && x.Year == year && x.LecturerId != null && x.LecturerId != 0).ToList();

            var resultList = new List<Object>();
            foreach (var item in salaryPaies)
            {
                var lecturer = _context.Lecturers.Find(item.LecturerId);
                var paiedLecturer = new
                {
                    Lecturer = lecturer,
                    Salary = item
                };
                resultList.Add(paiedLecturer);
            }
            return await Task.FromResult(resultList);
        }



        [HttpPost]
        [Route("not-paied-roll-lecturers")]
        public async Task<List<Object>> NotPaiedLecturers(int month, int year)
        {
            var SalaryPaies = _context.SalaryPays.Where(x => x.Month == month && x.Year == year && x.LecturerId != null && x.LecturerId != 0).ToList();
            if (SalaryPaies.Count != 0)
            {
                List<Lecturer> lecturerInSalaryPaies = new List<Lecturer>();
                foreach (var lecturer in _context.Lecturers)
                {
                    foreach (var salaryPay in SalaryPaies)
                    {
                        if (lecturer.Id == salaryPay.LecturerId)
                        {
                            lecturerInSalaryPaies.Add(lecturer);
                        }
                    }
                }

                List<Lecturer> lecturerOutSalaryPaies = new List<Lecturer>();
                foreach (var item in _context.Lecturers)
                {
                    if (!lecturerInSalaryPaies.Contains(item))
                    {
                        lecturerOutSalaryPaies.Add(item);
                    }
                }
                var resultList = new List<Object>();

                foreach (var item in lecturerOutSalaryPaies)
                {

                    var attendanceSheets = _context.AttendanceSheets.Where(x => x.LecturerId == item.Id && x.Date.Month == month && x.Date.Year == year && x.Status == Status.Active)
                        .OrderByDescending(x => x.Date);

                    float countWorkDays = attendanceSheets.ToList().Count;
                    var paySlipsOfLecturer = _context.PaySlips.Where(x => x.Date.Month == month && x.Date.Year == year && x.Status == Status.Active && x.ReceiveLecturerId == item.Id).ToList();
                    decimal totalAdvancePayment = 0;
                    foreach (var pay in paySlipsOfLecturer)
                    {
                        totalAdvancePayment += pay.Total;
                    }

                    decimal totalAmount = 0;
                    foreach (var attendance in attendanceSheets)
                    {
                        totalAmount += attendance.WageOfLecturer;
                    }
                    var temp = new
                    {
                        TotalBasicSalary = item.BasicSalary,         // lương cơ bản
                        TotalSalaryOfDay = item.WageOfLecturer,    // luong theo ngày
                        TotalAllowance = item.Allowance,
                        TotalAdvancePayment = totalAdvancePayment,  // tạm ứng
                        TotalBonus = item.Bonus,
                        TotalWorkdays = countWorkDays,          // số công
                        TotalGiangday = totalAmount,
                        TotalTheoreticalAmount = totalAmount + item.Allowance + item.Bonus,   // tổng lương  
                        TotalInsurancePremium = ((totalAmount + item.Allowance + item.Bonus) * 8) / 100,   // bảo hiểm
                        TotalRealityAmount = totalAmount + item.Allowance + item.Bonus - (((totalAmount + item.Allowance + item.Bonus) * 8) / 100) - totalAdvancePayment,  // tiền nhận đc
                        Month = month,
                        Year = year,
                        LecturerId = item.Id
                    };

                    var lecturerVm = Mapper.Map<LecturerViewModel>(item);
                    var paiedLecturer = new
                    {
                        Lecturer = lecturerVm,
                        Salary = temp
                    };
                    resultList.Add(paiedLecturer);
                }
                return await Task.FromResult(resultList);
            }
            else
            {
                var resultList = new List<Object>();

                foreach (var item in _context.Lecturers)
                {

                    var attendanceSheets = _context.AttendanceSheets.Where(x => x.LecturerId == item.Id && x.Date.Month == month && x.Date.Year == year && x.Status == Status.Active)
                        .OrderByDescending(x => x.Date);

                    float countWorkDays = attendanceSheets.ToList().Count;
                    var paySlipsOfLecturer = _context.PaySlips.Where(x => x.Date.Month == month && x.Date.Year == year && x.Status == Status.Active && x.ReceiveLecturerId == item.Id).ToList();
                    decimal totalAdvancePayment = 0;
                    foreach (var pay in paySlipsOfLecturer)
                    {
                        totalAdvancePayment += pay.Total;
                    }

                    decimal totalAmount = 0;
                    foreach (var attendance in attendanceSheets)
                    {
                        totalAmount += attendance.WageOfLecturer;
                    }

                    var temp = new
                    {
                        TotalBasicSalary = item.BasicSalary,         // lương cơ bản
                        TotalSalaryOfDay = item.WageOfLecturer,    // luong theo ngày
                        TotalAllowance = item.Allowance,
                        TotalAdvancePayment = totalAdvancePayment,  // tạm ứng
                        TotalBonus = item.Bonus,
                        TotalWorkdays = countWorkDays,          // số công
                        TotalGiangday = totalAmount,
                        TotalTheoreticalAmount = totalAmount + item.Allowance + item.Bonus,   // tổng lương  
                        TotalInsurancePremium = ((totalAmount + item.Allowance + item.Bonus) * 8) / 100,
                        TotalRealityAmount = totalAmount + item.Allowance + item.Bonus - (((totalAmount + item.Allowance + item.Bonus) * 8) / 100) - totalAdvancePayment,  // tiền nhận đc
                        Month = month,
                        Year = year,
                        LecturerId = item.Id
                    };

                    var lecturerVm = Mapper.Map<LecturerViewModel>(item);
                    var paiedLecturer = new
                    {
                        Lecturer = lecturerVm,
                        Salary = temp
                    };
                    resultList.Add(paiedLecturer);
                }
                return await Task.FromResult(resultList);
            }
        }



        [HttpPost]
        [Route("time-sheet-of-lecturer")]
        public async Task<List<Object>> GetTimeSheetOfLecturers(int month, int year)
        {
            var lecturers = _context.Lecturers.Where(x => x.Status == Status.Active);
            IEnumerable<AttendanceSheet> attendanceSheets = _context.AttendanceSheets.Where(x => x.Date.Month == month && x.Date.Year == year).ToList();
            attendanceSheets = attendanceSheets.Select(e => { e.Date = DateTime.Parse(e.Date.ToShortDateString()); return e; });
            var data = from e in attendanceSheets
                       group e by new { e.LecturerId, e.Date } into p
                       select new { Id = p.Key.LecturerId, p.Key.Date, Number = p.Count() };

            List<Object> result = new List<object>();
            int index = 1;
            foreach (var lecturer in lecturers)
            {
                var objectOfLecturer = data.Where(x => x.Id == lecturer.Id);


                if (objectOfLecturer.ToList().Count != 0)
                {

                    var monthList = new List<int>();
                    for (int i = 1; i <= DateTime.DaysInMonth(year, month); i++)
                    {
                        var number = objectOfLecturer.Where(x => x.Date.Day == i).SingleOrDefault();
                        if (number != null)
                        {
                            monthList.Add(number.Number);
                            continue;
                        }
                        monthList.Add(0);
                    }

                    var newRow = new
                    {
                        Index = index,
                        LecturerName = lecturer.FirstName + " " + lecturer.LastName,
                        PhoneNumber = " ",
                        Days = monthList,
                        Total = monthList.Sum()
                    };

                    result.Add(newRow);
                    index++;
                }
                else
                {
                    var monthList = new List<int>();
                    for (int i = 1; i <= DateTime.DaysInMonth(year, month); i++)
                    {
                        monthList.Add(0);
                    }
                    var newRow = new
                    {
                        Index = index,
                        LecturerName = lecturer.FirstName + " " + lecturer.LastName,
                        PhoneNumber = " ",
                        Days = monthList,
                        Total = monthList.Sum()
                    };
                    result.Add(newRow);
                    index++;
                }
            }
            return await Task.FromResult(result);
        }


        [HttpPost]
        [Route("attendance-of-learner")]
        public async Task<List<Object>> GetAttendanceSheetOfLearners(int month, int year, string _class)
        {
            var learners = (from l in _context.Learners
                            join st in _context.StudyProcesses on l.Id equals st.LearnerId
                            where st.LanguageClassId == _class && l.Status == Status.Active && st.Status == Status.Active
                            select l).ToList();
            var infoClass = _context.LanguageClasses.Where(x => x.Id == _class).SingleOrDefault();
            IEnumerable<AttendanceSheetDetail> attendanceSheetsDetails = _context.AttendanceSheetDetails.Where(x => x.DateCreated.Month == month && x.DateCreated.Year == year && x.LanguageClassId == _class).ToList();
            attendanceSheetsDetails = attendanceSheetsDetails.Select(e => { e.DateCreated = DateTime.Parse(e.DateCreated.ToShortDateString()); return e; });
            var data = from e in attendanceSheetsDetails
                       group e by new { e.LearnerId, e.DateCreated } into p
                       select new { Id = p.Key.LearnerId, p.Key.DateCreated, Number = p.Count() };

            List<Object> result = new List<object>();
            int index = 1;

            foreach (var learner in learners)
            {
                var objectOfLearner = data.Where(x => x.Id == learner.Id);


                if (objectOfLearner.ToList().Count != 0)
                {

                    var monthList = new List<int>();
                    for (int i = 1; i <= DateTime.DaysInMonth(year, month); i++)
                    {
                        var number = objectOfLearner.Where(x => x.DateCreated.Day == i).SingleOrDefault();
                        if (number != null)
                        {
                            monthList.Add(number.Number);
                            continue;
                        }
                        monthList.Add(0);
                    }

                    var newRow = new
                    {
                        Index = index,
                        FullName = learner.FirstName + " " + learner.LastName,
                        BirthYear = learner.Birthday.Year,
                        Class = infoClass.Name,
                        Days = monthList,
                        Total = monthList.Sum()
                    };

                    result.Add(newRow);
                    index++;
                }
                else
                {
                    var monthList = new List<int>();
                    for (int i = 1; i <= DateTime.DaysInMonth(year, month); i++)
                    {
                        monthList.Add(0);
                    }
                    var newRow = new
                    {
                        Index = index,
                        FullName = learner.FirstName + " " + learner.LastName,
                        BirthYear = learner.Birthday.Year,
                        Class = infoClass.Name,
                        Days = monthList,
                        Total = monthList.Sum()
                    };
                    result.Add(newRow);
                    index++;
                }
            }
            return await Task.FromResult(result);
        }


    }
}