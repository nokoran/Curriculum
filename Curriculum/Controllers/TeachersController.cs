using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Curriculum.Models;
using Curriculum.Data;
using System.Threading.Tasks;
using System.Linq;
using Curriculum.Entities;
using Curriculum.Repositories;
using Microsoft.AspNetCore.Authorization;

namespace Curriculum.Controllers
{
    [Authorize (Roles = "Administrator")]
    public class TeachersController : Controller
    {
        private readonly TeacherRepository _teacherRepository;
        
        public TeachersController(TeacherRepository teacherRepository)
        {
            _teacherRepository = teacherRepository;
        }
        

        // GET: CoursesController
        public ActionResult Index()
        {
            var teachers = _teacherRepository.GetAllAsync().GetAwaiter().GetResult();
            return View(teachers);
        }
        
        // GET: RolesController
        [HttpGet]
        public ActionResult Create()
        {
            return View(new Teacher());
        }

        // POST: RolesController
        [HttpPost]
        public ActionResult Create(Teacher teacher)
        {
            if(!_teacherRepository.ExistsByNameAsync(teacher.full_name).GetAwaiter().GetResult())
            {
                _teacherRepository.AddAsync(teacher).GetAwaiter().GetResult();
                return RedirectToAction("Index");
            }
            
            return RedirectToAction("Index");
        }
        
        // GET: RolesController/Edit/5
        [HttpGet]
        public ActionResult Edit(Guid id)
        {
            var course = _teacherRepository.GetByIdAsync(id).GetAwaiter().GetResult();
            if (course == null)
            {
                return NotFound();
            }
            return View(course);
        }

        // POST: RolesController/EditConfirmed/5
        [HttpPost]
        public ActionResult EditConfirmed(Guid id, Teacher teacher)
        {
            var existingTeacher = _teacherRepository.GetByIdAsync(id).GetAwaiter().GetResult();
            if (existingTeacher != null)
            {
                existingTeacher.full_name = teacher.full_name;
                existingTeacher.desc = teacher.desc;
                _teacherRepository.UpdateAsync(existingTeacher).GetAwaiter().GetResult();
                return RedirectToAction("Index");
            }
            return RedirectToAction("Index");
        }
        
        // GET: RolesController/Delete/5
        [HttpGet]
        public ActionResult Delete(Guid id)
        {
            var teacher = _teacherRepository.GetByIdAsync(id).GetAwaiter().GetResult();
            if (teacher == null)
            {
                return NotFound();
            }
            return View(teacher);
        }

        // POST: RolesController/DeleteConfirmed/5
        [HttpPost]
        public ActionResult DeleteConfirmed(Guid id)
        {
            var teacher = _teacherRepository.GetByIdAsync(id).GetAwaiter().GetResult();
            if (teacher != null)
            {
                _teacherRepository.DeleteAsync(teacher.id).GetAwaiter().GetResult();
                return RedirectToAction("Index");
            }
            return RedirectToAction("Index");
        }
    }
}
