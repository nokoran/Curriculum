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
    public class GroupsController : Controller
    {
        private readonly GroupRepository _groupRepository;
        private readonly CourseRepository _courseRepository;
        
        public GroupsController(GroupRepository groupRepository, CourseRepository courseRepository)
        {
            _groupRepository = groupRepository;
            _courseRepository = courseRepository;
        }

        // GET: CoursesController
        public ActionResult Index()
        {
            var groups = _groupRepository.GetAllAsync().GetAwaiter().GetResult();
            var bgroups = groups.Select(g => new BGroup
            {
                id = g.id,
                group_name = g.group_name,
                course_name = _courseRepository.GetByIdAsync(g.course_id).GetAwaiter().GetResult().course_name
            });
            return View(bgroups);
        }
        
        // GET: RolesController
        [HttpGet]
        public ActionResult Create()
        {
            var courses = _courseRepository.GetAllAsync().GetAwaiter().GetResult();
            ViewBag.Courses = courses;
            return View(new Group());
        }

        // POST: RolesController
        [HttpPost]
        public ActionResult Create(Group group)
        {
            if(!_groupRepository.ExistsByNameAsync(group.group_name).GetAwaiter().GetResult())
            {
                _groupRepository.AddAsync(group).GetAwaiter().GetResult();
                return RedirectToAction("Index");
            }
            
            return RedirectToAction("Index");
        }
        
        // GET: RolesController/Edit/5
        [HttpGet]
        public ActionResult Edit(Guid id)
        {
            var group = _groupRepository.GetByIdAsync(id).GetAwaiter().GetResult();
            if (group == null)
            {
                return NotFound();
            }
            var courses = _courseRepository.GetAllAsync().GetAwaiter().GetResult();
            ViewBag.Courses = courses;
            return View(group);
        }

        // POST: RolesController/EditConfirmed/5
        [HttpPost]
        public ActionResult EditConfirmed(Guid id, Group group)
        {
            var existingGroup = _groupRepository.GetByIdAsync(id).GetAwaiter().GetResult();
            if (existingGroup != null)
            {
                existingGroup.group_name = group.group_name;
                existingGroup.course_id = group.course_id;
                _groupRepository.UpdateAsync(existingGroup).GetAwaiter().GetResult();
                return RedirectToAction("Index");
            }
            return RedirectToAction("Index");
        }
        
        // GET: RolesController/Delete/5
        [HttpGet]
        public ActionResult Delete(Guid id)
        {
            var group = _groupRepository.GetByIdAsync(id).GetAwaiter().GetResult();
            if (group == null)
            {
                return NotFound();
            }
            var course = _courseRepository.GetByIdAsync(group.course_id).GetAwaiter().GetResult();
            ViewBag.CourseName = course.course_name;
            return View(group);
        }

        // POST: RolesController/DeleteConfirmed/5
        [HttpPost]
        public ActionResult DeleteConfirmed(Guid id)
        {
            var group = _groupRepository.GetByIdAsync(id).GetAwaiter().GetResult();
            if (group != null)
            {
                _groupRepository.DeleteAsync(id).GetAwaiter().GetResult();
            }
            return RedirectToAction("Index");
        }
        
        [HttpGet]
        public JsonResult GetGroupsByCourseId(Guid courseId)
        {
            var groups = _groupRepository.GetByCourseIdAsync(courseId).GetAwaiter().GetResult();
            return Json(groups);
        }
    }
}
