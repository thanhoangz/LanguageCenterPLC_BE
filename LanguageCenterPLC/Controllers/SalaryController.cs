using LanguageCenterPLC.Application.Interfaces;
using LanguageCenterPLC.Data.EF;
using LanguageCenterPLC.Data.Entities;
using LanguageCenterPLC.Infrastructure.Enums;
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
            var salaryPaies = _context.SalaryPays.Where(x => x.Month == month && x.Year == year && !string.IsNullOrEmpty(x.PersonnelId));
            var timeSheets = _context.Timesheets.Where(x => x.Month == month && x.Year == year && x.Status == Status.Active);
            var timeSheetNotInSalary = new List<Timesheet>();
            foreach (var salary in salaryPaies)
            {
                var temp = timeSheets.Where(x => x.PersonnelId == salary.PersonnelId).ToList();
                if (temp.Count != 0)
                {
                    timeSheetNotInSalary.Add(temp[0]);
                }
            }

            var resultList = new List<Object>();
            foreach (var item in timeSheetNotInSalary)
            {
                var personnel = _context.Personnels.Find(item.PersonnelId);
                var temp = new
                {
                    TotalBasicSalary = item.Salary,
                    TotalSalaryOfDay = item.SalaryOfDay,
                    TotalAllowance = item.Allowance,
                    TotalBonus = item.Bonus,
                    TotalInsurancePremium = item.InsurancePremiums,
                    TotalWorkdays = item.TotalWorkday,
                    TotalTheoreticalAmount = item.SalaryOfDay * Convert.ToDecimal(item.TotalWorkday),
                    TotalRealityAmount = item.SalaryOfDay * Convert.ToDecimal(item.TotalWorkday) + item.Allowance + item.Bonus - item.InsurancePremiums - item.AdvancePayment,
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
                        salaryPay.TotalInsurancePremium = timeSheet.InsurancePremiums;

                        salaryPay.TotalSalaryOfDay = timeSheet.SalaryOfDay;
                        salaryPay.TotalWorkdays = timeSheet.TotalWorkday;
                        salaryPay.TotalTheoreticalAmount = timeSheet.SalaryOfDay * Convert.ToDecimal(timeSheet.TotalWorkday);
                        salaryPay.TotalRealityAmount = salaryPay.TotalSalaryOfDay * Convert.ToDecimal(salaryPay.TotalWorkdays) + salaryPay.TotalAllowance + salaryPay.TotalBonus - salaryPay.TotalInsurancePremium;
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


    }
}