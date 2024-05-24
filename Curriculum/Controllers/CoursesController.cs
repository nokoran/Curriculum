using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Curriculum.Models;
using Curriculum.Data;
using System.Threading.Tasks;
using System.Linq;
using Curriculum.Entities;
using Curriculum.Repositories;

namespace Curriculum.Controllers
{
    public class CoursesController : Controller
    {
        private readonly CourseRepository _courseRepository;
        private readonly GroupRepository _groupRepository;
        
        public CoursesController(CourseRepository courseRepository, GroupRepository groupRepository)
        {
            _courseRepository = courseRepository;
            _groupRepository = groupRepository;
        }

        // GET: CoursesController
        public ActionResult Index()
        {
            var courses = _courseRepository.GetAllAsync().GetAwaiter().GetResult();
            return View(courses);
        }
        
        // GET: RolesController
        [HttpGet]
        public ActionResult Create()
        {
            return View(new Course());
        }

        // POST: RolesController
        [HttpPost]
        public ActionResult Create(Course course)
        {
            if(!_courseRepository.ExistsByNameAsync(course.course_name).GetAwaiter().GetResult())
            {
                _courseRepository.AddAsync(course).GetAwaiter().GetResult();
                return RedirectToAction("Index");
            }
            
            return RedirectToAction("Index");
        }
        
        // GET: RolesController/Edit/5
        [HttpGet]
        public ActionResult Edit(Guid id)
        {
            var course = _courseRepository.GetByIdAsync(id).GetAwaiter().GetResult();
            if (course == null)
            {
                return NotFound();
            }
            return View(course);
        }

        // POST: RolesController/EditConfirmed/5
        [HttpPost]
        public ActionResult EditConfirmed(Guid id, Course course)
        {
            var existingCourse = _courseRepository.GetByIdAsync(id).GetAwaiter().GetResult();
            if (existingCourse != null)
            {
                existingCourse.course_name = course.course_name;
                existingCourse.desc = course.desc;
                existingCourse.credit_hours = course.credit_hours;
                _courseRepository.UpdateAsync(existingCourse).GetAwaiter().GetResult();
                return RedirectToAction("Index");
            }
            return RedirectToAction("Index");
        }
        
        // GET: RolesController/Delete/5
        [HttpGet]
        public ActionResult Delete(Guid id)
        {
            var course = _courseRepository.GetByIdAsync(id).GetAwaiter().GetResult();
            if (course == null)
            {
                return NotFound();
            }
            return View(course);
        }

        // POST: RolesController/DeleteConfirmed/5
        [HttpPost]
        public ActionResult DeleteConfirmed(Guid id)
        {
            var course = _courseRepository.GetByIdAsync(id).GetAwaiter().GetResult();
            if (course != null)
            {
                var groups = _groupRepository.GetByCourseIdAsync(course.id).GetAwaiter().GetResult();
                foreach (var group in groups)
                {
                    _groupRepository.DeleteAsync(group.id).GetAwaiter().GetResult();
                }
                _courseRepository.DeleteAsync(id).GetAwaiter().GetResult();
                return RedirectToAction("Index");
            }
            return RedirectToAction("Index");
        }
    }
}
