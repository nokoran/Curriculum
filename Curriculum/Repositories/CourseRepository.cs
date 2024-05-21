using Curriculum.Data;
using Curriculum.Enitities;
using Curriculum.Models;
using Microsoft.EntityFrameworkCore;

namespace Curriculum.Repositories;

public class CourseRepository : IRepository<Course>
{
    private ApplicationDbContext _context;
    
    public CourseRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Course> GetByIdAsync(Guid id)
    {
        return await _context.Courses.FindAsync(id);
    }
    
    public async Task<IEnumerable<Course>> GetAllAsync()
    {
        return await _context.Courses.ToListAsync();
    }
    
    public async Task<Course> AddAsync(Course entity)
    {
        _context.Courses.Add(entity);
        await _context.SaveChangesAsync();
        return entity;
    }
    
    public async Task<Course> UpdateAsync(Course entity)
    {
        _context.Entry(entity).State = EntityState.Modified;
        await _context.SaveChangesAsync();
        return entity;
    }
    
    public async Task<Course> DeleteAsync(Guid id)
    {
        var course = await _context.Courses.FindAsync(id);
        if (course == null)
        {
            return null;
        }
        
        _context.Courses.Remove(course);
        await _context.SaveChangesAsync();
        return course;
    }
    
    public async Task<bool> ExistsByNameAsync(string name)
    {
        return await _context.Courses.AnyAsync(e => e.course_name == name);
    }
    
    public async Task<bool> ExistsByIdAsync(Guid id)
    {
        return await _context.Courses.AnyAsync(e => e.id == id);
    }
    
}