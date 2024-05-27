using Curriculum.Entities;
using Curriculum.Models;
using Curriculum.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Curriculum.Controllers;

[Authorize (Roles = "Administrator")]
public class SubjectsController : Controller
{
    private readonly SubjectRepository _subjectRepository;
    private readonly CourseRepository _courseRepository;
    
    public SubjectsController(SubjectRepository subjectRepository, CourseRepository courseRepository)
    {
        _subjectRepository = subjectRepository;
        _courseRepository = courseRepository;
    }
    
    public ActionResult Index()
    {
        var subjects = _subjectRepository.GetAllAsync().GetAwaiter().GetResult();
        var bsubjects = subjects.Select(g => new BSubject
        {
            id = g.id,
            subject_name = g.subject_name,
            course_name = _courseRepository.GetByIdAsync(g.course_id).GetAwaiter().GetResult().course_name
        });
        return View(bsubjects);
    }
    
    [HttpGet]
    public ActionResult Create()
    {
        var courses = _courseRepository.GetAllAsync().GetAwaiter().GetResult();
        ViewBag.Courses = courses;
        return View(new Subject());
    }

    [HttpPost]
    public ActionResult Create(Subject subject)
    {
        if(!_subjectRepository.ExistsByNameAsync(subject.subject_name).GetAwaiter().GetResult())
        {
            _subjectRepository.AddAsync(subject).GetAwaiter().GetResult();
            return RedirectToAction("Index");
        }
        
        return RedirectToAction("Index");
    }
    
    [HttpGet]
    public ActionResult Edit(Guid id)
    {
        var subject = _subjectRepository.GetByIdAsync(id).GetAwaiter().GetResult();
        if (subject == null)
        {
            return NotFound();
        }
        var courses = _courseRepository.GetAllAsync().GetAwaiter().GetResult();
        ViewBag.Courses = courses;
        return View(subject);
    }

    [HttpPost]
    public ActionResult EditConfirmed(Guid id, Subject subject)
    {
        var existingSubject = _subjectRepository.GetByIdAsync(id).GetAwaiter().GetResult();
        if (existingSubject != null)
        {
            existingSubject.subject_name = subject.subject_name;
            existingSubject.course_id = subject.course_id;
            _subjectRepository.UpdateAsync(existingSubject).GetAwaiter().GetResult();
            return RedirectToAction("Index");
        }
        return RedirectToAction("Index");
    }
    
    [HttpGet]
    public ActionResult Delete(Guid id)
    {
        var subject = _subjectRepository.GetByIdAsync(id).GetAwaiter().GetResult();
        if (subject == null)
        {
            return NotFound();
        }
        var course = _courseRepository.GetByIdAsync(subject.course_id).GetAwaiter().GetResult();
        ViewBag.CourseName = course.course_name;
        return View(subject);
    }

    [HttpPost]
    public ActionResult DeleteConfirmed(Guid id)
    {
        var subject = _subjectRepository.GetByIdAsync(id).GetAwaiter().GetResult();
        if (subject != null)
        {
            _subjectRepository.DeleteAsync(id).GetAwaiter().GetResult();
        }
        return RedirectToAction("Index");
    }
    
    public JsonResult GetSubjectsByCourseId(Guid courseId)
    {
        var subjects = _subjectRepository.GetByCourseIdAsync(courseId).GetAwaiter().GetResult();
        return Json(subjects);
    }
}