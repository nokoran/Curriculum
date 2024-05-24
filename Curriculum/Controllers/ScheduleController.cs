using Curriculum.Entities;
using Curriculum.Models;
using Curriculum.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Curriculum.Controllers
{
    [Authorize]
    public class ScheduleController : Controller
    {
        private readonly ScheduleRepository _scheduleRepository;
        private readonly SubjectRepository _subjectRepository;
        private readonly TeacherRepository _teacherRepository;
        private readonly GroupRepository _groupRepository;
        private readonly CourseRepository _courseRepository;
        private readonly UserManager<ApplicationUser> _userManager;

        public ScheduleController(ScheduleRepository scheduleRepository,
            SubjectRepository subjectRepository,
            TeacherRepository teacherRepository,
            GroupRepository groupRepository,
            CourseRepository courseRepository,
            UserManager<ApplicationUser> userManager)
        {
            _scheduleRepository = scheduleRepository;
            _subjectRepository = subjectRepository;
            _teacherRepository = teacherRepository;
            _groupRepository = groupRepository;
            _courseRepository = courseRepository;
            _userManager = userManager;
        }
        
        
        public ActionResult Index()
        {
            var schedules = _scheduleRepository.GetAllAsync().GetAwaiter().GetResult();
            var bschedules = schedules.Select(g => new BSchedule
            {
                id = g.id,
                subject_name = _subjectRepository.GetByIdAsync(g.subject_id).GetAwaiter().GetResult().subject_name,
                course_name = _courseRepository.GetByIdAsync(g.course_id).GetAwaiter().GetResult().course_name,
                group_name = _groupRepository.GetByIdAsync(g.group_id).GetAwaiter().GetResult().group_name,
                place = g.place,
                teacher_name = _teacherRepository.GetByIdAsync(g.teacher_id).GetAwaiter().GetResult().full_name,
                start_time = g.start_time,
                end_time = g.end_time
            });
            var sortedSchedules = bschedules.OrderBy(s => s.start_time);
            if (User.IsInRole("Administrator"))
            {
                return View(sortedSchedules);
            }
            if (User.IsInRole("Student"))
            {
                var user = _userManager.GetUserAsync(User).GetAwaiter().GetResult();
                var group = _groupRepository.GetByIdAsync((Guid)user.GroupId).GetAwaiter().GetResult();
                var studentSchedules = sortedSchedules.Where(s => s.group_name == group.group_name);
                return View(studentSchedules);
            }
            if (User.IsInRole("Teacher"))
            {
                var user = _userManager.GetUserAsync(User).GetAwaiter().GetResult();
                var teacher_name = _teacherRepository.GetByIdAsync((Guid)user.teacher_id).GetAwaiter().GetResult().full_name;
                var teacherSchedules = sortedSchedules.Where(s => s.teacher_name == teacher_name);
                return View(teacherSchedules);
            }
            return View();
        }

        [HttpGet]
        [Authorize(Roles = "Administrator")]
        public ActionResult Create()
        {
            var courses = _courseRepository.GetAllAsync().GetAwaiter().GetResult();
            var subjects = _subjectRepository.GetAllAsync().GetAwaiter().GetResult();
            var teachers = _teacherRepository.GetAllAsync().GetAwaiter().GetResult();
            var groups = _groupRepository.GetAllAsync().GetAwaiter().GetResult();
            ViewBag.Courses = courses;
            ViewBag.Subjects = subjects;
            ViewBag.Teachers = teachers;
            ViewBag.Groups = groups;
            return View();
        }

        [HttpPost]
        [Authorize(Roles = "Administrator")]
        public ActionResult Create(Schedule schedule, DateTime start_date, DateTime end_date, bool is_alternating)
        {
            if (ModelState.IsValid)
            {
                // Calculate the number of weeks between start_date and end_date
                int weeks = (int)(end_date - start_date).TotalDays / 7;

                // Iterate over each week
                for (int i = 0; i <= weeks; i++)
                {
                    if (is_alternating)
                    {
                        if (i % 2 == 0)
                        {
                            Schedule newSchedule = new Schedule
                            {
                                subject_id = schedule.subject_id,
                                teacher_id = schedule.teacher_id,
                                course_id = schedule.course_id,
                                group_id = schedule.group_id,
                                place = schedule.place,
                                start_time = new DateTime(new DateOnly(start_date.Year, start_date.Month, start_date.Day).AddDays(i * 7),
                                    new TimeOnly(schedule.start_time.Hour, schedule.start_time.Minute, schedule.start_time.Second)),
                                end_time = new DateTime(new DateOnly(start_date.Year, start_date.Month, start_date.Day).AddDays(i * 7), new TimeOnly(schedule.end_time.Hour,
                                    schedule.end_time.Minute, schedule.end_time.Second))
                            };

                            _scheduleRepository.AddAsync(newSchedule).GetAwaiter().GetResult();
                        }
                    }
                    else
                    {
                        Schedule newSchedule = new Schedule
                        {
                            subject_id = schedule.subject_id,
                            teacher_id = schedule.teacher_id,
                            course_id = schedule.course_id,
                            group_id = schedule.group_id,
                            place = schedule.place,
                            start_time = new DateTime(new DateOnly(start_date.Year, start_date.Month, start_date.Day).AddDays(i * 7),
                                new TimeOnly(schedule.start_time.Hour, schedule.start_time.Minute, schedule.start_time.Second)),
                            end_time = new DateTime(new DateOnly(start_date.Year, start_date.Month, start_date.Day).AddDays(i * 7), new TimeOnly(schedule.end_time.Hour,
                                schedule.end_time.Minute, schedule.end_time.Second))
                        };

                        _scheduleRepository.AddAsync(newSchedule).GetAwaiter().GetResult();
                    }
                }
            }
            return RedirectToAction("Index");
        }

        [HttpGet]
        [Authorize(Roles = "Administrator")]
        public ActionResult Edit(Guid id)
        {
            var schedule = _scheduleRepository.GetByIdAsync(id).GetAwaiter().GetResult();
            if (schedule == null)
            {
                return NotFound();
            }

            var courses = _courseRepository.GetAllAsync().GetAwaiter().GetResult();
            var subjects = _subjectRepository.GetAllAsync().GetAwaiter().GetResult();
            var teachers = _teacherRepository.GetAllAsync().GetAwaiter().GetResult();
            var groups = _groupRepository.GetAllAsync().GetAwaiter().GetResult();
            ViewBag.Courses = courses;
            ViewBag.Subjects = subjects;
            ViewBag.Teachers = teachers;
            ViewBag.Groups = groups;
            return View(schedule);
        }

        [HttpPost]
        [Authorize(Roles = "Administrator")]
        public ActionResult EditConfirmed(Guid id, Schedule schedule)
        {
            var existingSchedule = _scheduleRepository.GetByIdAsync(id).GetAwaiter().GetResult();
            if (existingSchedule != null)
            {
                existingSchedule.subject_id = schedule.subject_id;
                existingSchedule.teacher_id = schedule.teacher_id;
                existingSchedule.course_id = schedule.course_id;
                existingSchedule.group_id = schedule.group_id;
                existingSchedule.place = schedule.place;
                existingSchedule.start_time = schedule.start_time;
                existingSchedule.end_time = schedule.end_time;
                _scheduleRepository.UpdateAsync(existingSchedule).GetAwaiter().GetResult();
                return RedirectToAction("Index");
            }

            return RedirectToAction("Index");
        }

        [HttpGet]
        [Authorize(Roles = "Administrator")]
        public ActionResult Delete(Guid id)
        {
            //with names for ids
            var schedule = _scheduleRepository.GetByIdAsync(id).GetAwaiter().GetResult();
            if (schedule == null)
            {
                return NotFound();
            }
            var course = _courseRepository.GetByIdAsync(schedule.course_id).GetAwaiter().GetResult();
            var subject = _subjectRepository.GetByIdAsync(schedule.subject_id).GetAwaiter().GetResult();
            var teacher = _teacherRepository.GetByIdAsync(schedule.teacher_id).GetAwaiter().GetResult();
            var group = _groupRepository.GetByIdAsync(schedule.group_id).GetAwaiter().GetResult();
            ViewBag.SubjectName = subject.subject_name;
            ViewBag.TeacherName = teacher.full_name;
            ViewBag.GroupName = group.group_name;
            ViewBag.CourseName = course.course_name;
            
            return View(schedule);
        }

        [HttpPost]
        [Authorize(Roles = "Administrator")]
        public ActionResult DeleteConfirmed(Guid id)
        {
            var subject = _subjectRepository.GetByIdAsync(id).GetAwaiter().GetResult();
            if (subject != null)
            {
                _subjectRepository.DeleteAsync(id).GetAwaiter().GetResult();
            }

            return RedirectToAction("Index");
        }
    }
}
