using AutoMapper;
using LanguageCenterPLC.Application.Interfaces;
using LanguageCenterPLC.Application.ViewModels.Studies;
using LanguageCenterPLC.Data.Entities;
using LanguageCenterPLC.Infrastructure.Enums;
using LanguageCenterPLC.Infrastructure.Interfaces;
using LanguageCenterPLC.Utilities.Dtos;
using System.Collections.Generic;
using System;
using System.Linq;
using LanguageCenterPLC.Data.EF;
using LanguageCenterPLC.Application.ViewModels.Finances;
using LanguageCenterPLC.Data;

namespace LanguageCenterPLC.Application.Implementation
{
    public class StudyProcessService : IStudyProcessService
    {
        private readonly IRepository<StudyProcess, int> _studyProcessRepository;
        private readonly IRepository<LanguageClass, string> _languageClassRepository;
        private readonly IRepository<Learner, string> _learnerRepository;
        private readonly IRepository<ReceiptDetail, int> _receiptDetailRepository;
        private readonly IRepository<Receipt, string> _receiptRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly AppDbContext _context;

        public StudyProcessService(IRepository<StudyProcess, int> studyProcessRepository, IRepository<LanguageClass, string> languageClassRepository,
            IRepository<Learner, string> learnerRepository, IRepository<ReceiptDetail, int> receiptDetailRepository,
            IRepository<Receipt, string> receiptRepository, IUnitOfWork unitOfWork, AppDbContext context)
        {
            _studyProcessRepository = studyProcessRepository;
            _languageClassRepository = languageClassRepository;
            _learnerRepository = learnerRepository;
            _receiptDetailRepository = receiptDetailRepository;
            _receiptRepository = receiptRepository;
            _unitOfWork = unitOfWork;
            _context = context;
        }


        public bool Add(StudyProcessViewModel studyProcessVm)
        {
            try
            {
                var studyProcess = Mapper.Map<StudyProcessViewModel, StudyProcess>(studyProcessVm);


                _studyProcessRepository.Add(studyProcess);
                SaveChanges();

                var countInClass = _studyProcessRepository.FindAll().Where(x => x.LanguageClassId == studyProcess.LanguageClassId && x.Status == Status.Active).ToList().Count;
                var languageClass = _languageClassRepository.FindById(studyProcess.LanguageClassId);
                int max = languageClass.MaxNumber;

                if (countInClass >= max)
                {
                    languageClass.Status = Status.Pause;
                    _languageClassRepository.Update(languageClass);
                }

                // lấy tên học viên
                var hocvien = _context.Learners.Find(studyProcess.LearnerId);
                var tenhocvien = hocvien.FirstName + hocvien.LastName;
                // log cho xếp lớp
                var log = new LogSystem();
                log.IsStudyProcessLog = true;
                log.DateCreated = DateTime.Now;
                log.Content = "Xếp lớp học viên : " + tenhocvien;
                log.UserId = Const.tempUserId;
                log.LearnerId = studyProcess.LearnerId;
                log.StudyProcessId = studyProcess.Id;
                log.ClassId = studyProcess.LanguageClassId;
                _context.LogSystems.Add(log); 

                return true;
            }
            catch
            {
                return false;
            }
        }

        //public bool Add(StudyProcessViewModel studyProcessVm)
        //{
        //    throw new NotImplementedException();
        //}

        public bool Delete(int id)
        {
            try
            {
                var studyProcess = _studyProcessRepository.FindById(id);
                _studyProcessRepository.Remove(studyProcess);

                var countInClass = _studyProcessRepository.FindAll().Where(x => x.LanguageClassId == studyProcess.LanguageClassId && x.Status == Status.Active).ToList().Count;
                var languageClass = _languageClassRepository.FindById(studyProcess.LanguageClassId);
                int max = languageClass.MaxNumber;

                if (countInClass < max)
                {
                    languageClass.Status = Status.Active;
                    _languageClassRepository.Update(languageClass);
                }

                // xóa log khi xếp nhầm
                var log = _context.LogSystems.Where(x => x.IsStudyProcessLog == true && x.StudyProcessId == id).SingleOrDefault();
                if(log != null)
                {
                    _context.LogSystems.Remove(log);
                    _context.SaveChanges();
                }

                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool DeleteByLearner(string languageClassId, string LearnerId)
        {
            try
            {
                var id = GetStudyProByLearner(languageClassId, LearnerId);
                var studyProcess = _studyProcessRepository.FindById(id);

                _studyProcessRepository.Remove(studyProcess);
                SaveChanges();
                var countInClass = _studyProcessRepository.FindAll().Where(x => x.LanguageClassId == studyProcess.LanguageClassId && x.Status == Status.Active).ToList().Count;
                var languageClass = _languageClassRepository.FindById(studyProcess.LanguageClassId);
                int max = languageClass.MaxNumber;

                if (countInClass < max)
                {
                    languageClass.Status = Status.Active;
                    _languageClassRepository.Update(languageClass);
                }

                // xóa log khi xếp nhầm
                var log = _context.LogSystems.Where(x => x.IsStudyProcessLog == true && x.StudyProcessId == studyProcess.Id).SingleOrDefault();
                if (log != null)
                {
                    _context.LogSystems.Remove(log);
                    _context.SaveChanges();
                }

                return true;

            }
            catch
            {
                return false;
            }
        }


        public List<StudyProcessViewModel> GetAll()
        {
            var languageClass = _languageClassRepository.FindAll().ToList();

            var studyProcess = _studyProcessRepository.FindAll().ToList();

            var studyProcessViewModel = Mapper.Map<List<StudyProcessViewModel>>(studyProcess);

            foreach (var item in studyProcessViewModel)
            {
                string name = _languageClassRepository.FindById(item.LanguageClassId).Name;
                item.LanguageClassName = name;
            }

            return studyProcessViewModel;
        }

        public PagedResult<StudyProcessViewModel> GetAllPaging(string keyword, int status, int pageSize, int pageIndex)
        {
            var query = _studyProcessRepository.FindAll();
            if (!string.IsNullOrEmpty(keyword))
            {
                query = query.Where(x => x.LanguageClassId.Contains(keyword));
            }

            Status _status = (Status)status;

            query = query.Where(x => x.Status == _status);

            var totalRow = query.Count();
            var data = query.OrderByDescending(x => x.LanguageClassId)
                .Skip((pageIndex - 1) * pageSize)
                .Take(pageSize);
            var resultPaging = Mapper.Map<List<StudyProcessViewModel>>(data);

            return new PagedResult<StudyProcessViewModel>()
            {
                CurrentPage = pageIndex,
                PageSize = pageSize,
                Results = resultPaging,
                RowCount = totalRow
            };
        }

        public List<StudyProcessViewModel> GetAllWithConditions(string languageClassId, string LearnerId, int status)
        {
            var query = _studyProcessRepository.FindAll();

            if (!string.IsNullOrEmpty(languageClassId))
            {
                query = query.Where(x => x.LanguageClassId.Contains(languageClassId));
            }

            if (!string.IsNullOrEmpty(LearnerId))
            {
                query = query.Where(x => x.LearnerId.Contains(LearnerId));
            }

            Status _status = (Status)status;
            if (_status == Status.Active || _status == Status.InActive || _status == Status.Pause)
            {
                query = query.Where(x => x.Status == _status).OrderBy(x => x.DateCreated);
            }

            var studyProcessViewModel = Mapper.Map<List<StudyProcessViewModel>>(query);

            return studyProcessViewModel;
        }

        public int GetStudyProByLearner(string languageClassId, string LearnerId)
        {
            var query = _studyProcessRepository.FindAll();

            if (!string.IsNullOrEmpty(languageClassId))
            {
                query = query.Where(x => x.LanguageClassId == languageClassId);
            }

            if (!string.IsNullOrEmpty(LearnerId))
            {
                query = query.Where(x => x.LearnerId == LearnerId);
            }

            var studyProcessViewModel = Mapper.Map<StudyProcessViewModel>(query.FirstOrDefault());

            return studyProcessViewModel.Id;
        }

        public StudyProcessViewModel GetByClassLearner(string classId, string learnerId)
        {
            var query = _studyProcessRepository.FindAll();

            if (!string.IsNullOrEmpty(classId))
            {
                query = query.Where(x => x.LanguageClassId == classId);
            }

            if (!string.IsNullOrEmpty(learnerId))
            {
                query = query.Where(x => x.LearnerId == learnerId);
            }

            var studyProcessViewModel = Mapper.Map<StudyProcessViewModel>(query.FirstOrDefault());

            return studyProcessViewModel;
        }

        public StudyProcessViewModel GetById(int id)
        {
            var studyProcess = _studyProcessRepository.FindById(id);
            var studyProcessViewModel = Mapper.Map<StudyProcessViewModel>(studyProcess);
            //////// tìm kiếm thêm học viên
            var item = studyProcessViewModel;

            var learner = _learnerRepository.FindById(item.LearnerId);
            var learnerViewModel = new LearnerViewModel();
            learnerViewModel.Id = learner.Id;
            learnerViewModel.FirstName = learner.FirstName;
            learnerViewModel.LastName = learner.LastName;
            learnerViewModel.Sex = learner.Sex;
            learnerViewModel.Birthday = learner.Birthday;
            learnerViewModel.Address = learner.Address;
            item.Learner = learnerViewModel;


            return studyProcessViewModel;
        }

        public List<StudyProcessViewModel> GetStudyProcessByClassId(string languageClassId, int status)
        {
            var query = _studyProcessRepository.FindAll();

            if (!string.IsNullOrEmpty(languageClassId))
            {
                query = query.Where(x => x.LanguageClassId.Contains(languageClassId));
            }

            Status _status = (Status)status;
            if (_status == Status.Active || _status == Status.InActive || _status == Status.Pause)
            {
                query = query.Where(x => x.Status == _status).OrderBy(x => x.DateCreated);
            }

            var studyProcessViewModel = Mapper.Map<List<StudyProcessViewModel>>(query);

            return studyProcessViewModel;
        }

        // chỗ này là của thằng Bò múa cấm động vào :V
        public List<StudyProcessViewModel> GetAllClassOfLearner(string learnerId)
        {
            var languageClassOfLearner = _studyProcessRepository.FindAll().Where(x => x.LearnerId == learnerId && x.Status == Status.Active);

            var studyProcessViewModel = Mapper.Map<List<StudyProcessViewModel>>(languageClassOfLearner);
            foreach (var item in studyProcessViewModel)
            {
                string className = _languageClassRepository.FindById(item.LanguageClassId).Name;
                item.LanguageClassName = className;
                item.MonthlyFee = _languageClassRepository.FindById(item.LanguageClassId).MonthlyFee;
                item.CourseFee = _languageClassRepository.FindById(item.LanguageClassId).CourseFee;

            }

            return studyProcessViewModel;
        }
        public List<StudyProcessViewModel> GetLearnerForReceipt()
        {
            var languageClassOfLearner = (from learner in _learnerRepository.FindAll().Where(x => x.Status == Status.Active)
                                          join
study in _studyProcessRepository.FindAll().Where(y => y.Status == Status.Active)
on learner.Id equals study.LearnerId
                                          select study).Distinct();

            var studyProcessViewModel = Mapper.Map<List<StudyProcessViewModel>>(languageClassOfLearner);
            foreach (var item in studyProcessViewModel)
            {
                item.LearnerName = _learnerRepository.FindById(item.LearnerId).FirstName + " " + _learnerRepository.FindById(item.LearnerId).LastName;

            }

            return studyProcessViewModel;
        }


        public bool IsExists(int id)
        {
            var studyProcess = _studyProcessRepository.FindById(id);
            return (studyProcess == null) ? false : true;
        }

        public void SaveChanges()
        {
            _unitOfWork.Commit();
        }

        public bool Update(StudyProcessViewModel studyProcessVm)
        {
            try
            {
                var studyProcess = Mapper.Map<StudyProcessViewModel, StudyProcess>(studyProcessVm);

                _studyProcessRepository.Update(studyProcess);
                SaveChanges();

                var countInClass = _studyProcessRepository.FindAll().Where(x => x.LanguageClassId == studyProcess.LanguageClassId && x.Status == Status.Active).ToList().Count;
                var languageClass = _languageClassRepository.FindById(studyProcess.LanguageClassId);
                int max = languageClass.MaxNumber;

                if (countInClass < max)
                {
                    languageClass.Status = Status.Active;
                    _languageClassRepository.Update(languageClass);
                }
                else
                {
                    languageClass.Status = Status.Pause;
                    _languageClassRepository.Update(languageClass);
                }

                if (studyProcess.Status == Status.InActive)
                {
                    // lấy tên học viên
                    var hocvien = _context.Learners.Find(studyProcess.LearnerId);
                    var tenhocvien = hocvien.FirstName + hocvien.LastName;
                    // log cho nghỉ học
                    var log = new LogSystem();
                    log.IsStudyProcessLog = true;
                    log.DateCreated = DateTime.Now;
                    log.Content = "Nghỉ học : " + tenhocvien;
                    log.UserId = Const.tempUserId;
                    log.LearnerId = studyProcess.LearnerId;
                    log.StudyProcessId = studyProcess.Id;
                    log.ClassId = studyProcess.LanguageClassId;
                    _context.LogSystems.Add(log);
                }             

                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool UpdateStatus(int studyProcessId, Status status)
        {
            try
            {
                var studyProcess = _studyProcessRepository.FindById(studyProcessId);
                studyProcess.Status = status;

                if (status == Status.Pause)
                {
                    // lấy tên học viên
                    var hocvien = _context.Learners.Find(studyProcess.LearnerId);
                    var tenhocvien = hocvien.FirstName + " " + hocvien.LastName;
                    // log mới cho chuyển lớp
                    var log = new LogSystem();
                    log.IsStudyProcessLog = true;
                    log.DateCreated = DateTime.Now;
                    log.Content = "Chuyển lớp học viên : " + tenhocvien;
                    log.UserId = Const.tempUserId;
                    log.LearnerId = studyProcess.LearnerId;
                    log.StudyProcessId = studyProcess.Id;
                    log.ClassId = studyProcess.LanguageClassId;
                    _context.LogSystems.Add(log);
                }
                return true;
            }
            catch
            {
                return false;
            }
        }


        public List<StudyProcessViewModel> GetAllInClass(string classId, int status)
        {
            var studyprocess = _studyProcessRepository.FindAll().Where(x => x.LanguageClassId == classId);

            Status _status = (Status)status;    //
            if (_status == Status.Active || _status == Status.InActive || _status == Status.Pause)
            {
                studyprocess = studyprocess.Where(x => x.Status == _status).OrderBy(x => x.Learner.LastName);
            }

            var studyprocessViewModel = Mapper.Map<List<StudyProcessViewModel>>(studyprocess);

            foreach (var item in studyprocessViewModel)
            {
                var learner = _learnerRepository.FindById(item.LearnerId);
                var learnerViewModel = new LearnerViewModel();
                learnerViewModel.Id = learner.Id;
                learnerViewModel.CardId = learner.CardId;
                learnerViewModel.FirstName = learner.FirstName;
                learnerViewModel.LastName = learner.LastName;
                learnerViewModel.Sex = learner.Sex;
                learnerViewModel.Birthday = learner.Birthday;
                learnerViewModel.Address = learner.Address;
                learnerViewModel.Phone = learner.Phone;
                item.Learner = learnerViewModel;

            }
            return studyprocessViewModel;
        }

        // Bò viết cho báo cáo chưa đóng học phí
        public List<StudyProcessViewModel> GetLearNotPaidTuiTion(int month, int year, string classId)
        {
            List<StudyProcess> learnerNotPaid = new List<StudyProcess>();
            var leanerInClass = _studyProcessRepository.FindAll().Where(x => x.LanguageClassId == classId && x.Status == Status.Active).ToList();
            var test = (from lear in _learnerRepository.FindAll(x => x.Status == Status.Active)
                        join study in _studyProcessRepository.FindAll().Where(x => x.LanguageClassId == classId && x.Status == Status.Active)
                        on lear.Id equals study.LearnerId
                        orderby lear.LastName ascending
                        select lear).ToList();
            var receiptDetail = _receiptDetailRepository.FindAll().Where(x => x.Status == Status.Active & x.LanguageClassId == classId && x.Month ==month && x.Year == year).ToList();
            var receiptDetailViewModel = Mapper.Map<List<ReceiptDetailViewModel>>(receiptDetail);
            foreach (var item in receiptDetailViewModel)
            {
                item.LearnerId = _receiptRepository.FindById(item.ReceiptId).LearnerId;           
            }
            for (int i = 0; i < leanerInClass.Count(); i++)
            {
                for (int j = 0; j < receiptDetailViewModel.Count(); j++)
                {
                    if (leanerInClass[i].LearnerId == receiptDetailViewModel[j].LearnerId)
                    {
                        learnerNotPaid.Add(leanerInClass[i]);
                    }
                }
            }

            // 2 cái này như nhau k hiểu lắm nhưng dùng đúng thì quất thôi
            var learnerSelect = leanerInClass.Except(learnerNotPaid);

            //var difList = leanerInClass.Where(a => !learnerNotPaid.Any(a1 => a1.LearnerId == a.LearnerId))
            //.Union(learnerNotPaid.Where(a => !leanerInClass.Any(a1 => a1.LearnerId == a.LearnerId)));
            //////////////////////////////////

            var learNotPaidViewModel = Mapper.Map<List<StudyProcessViewModel>>(learnerSelect);
            foreach (var item in learNotPaidViewModel)
            {
                item.LearnerName = _learnerRepository.FindById(item.LearnerId).FirstName + " " + _learnerRepository.FindById(item.LearnerId).LastName;
                item.LearnerAdress = _learnerRepository.FindById(item.LearnerId).Address;
                item.LearnerBriday = _learnerRepository.FindById(item.LearnerId).Birthday;
                item.LearnerPhone = _learnerRepository.FindById(item.LearnerId).Phone;
                item.LearnerSex = _learnerRepository.FindById(item.LearnerId).Sex;
                item.LearnerNameOrderBy = _learnerRepository.FindById(item.LearnerId).LastName;
            }
            return learNotPaidViewModel;


        }
    }
}
