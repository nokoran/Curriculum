using Curriculum.Data;
using Curriculum.Entities;
using Microsoft.EntityFrameworkCore;

namespace Curriculum.Repositories;

public class TeacherRepository : IRepository<Teacher>
{
    private readonly ApplicationDbContext _context;
    
    public TeacherRepository(ApplicationDbContext context)
    {
        _context = context;
    }
    
    public async Task<Teacher> GetByIdAsync(Guid id)
    {
        return await _context.Teachers.FindAsync(id);
    }

    public async Task<IEnumerable<Teacher>> GetAllAsync()
    {
        return await _context.Teachers.ToListAsync();
    }

    public async Task<Teacher> AddAsync(Teacher entity)
    {
        await _context.Teachers.AddAsync(entity);
        await _context.SaveChangesAsync();
        return entity;
    }

    public async Task<Teacher> UpdateAsync(Teacher entity)
    {
        _context.Entry(entity).State = EntityState.Modified;
        await _context.SaveChangesAsync();
        return entity;
    }

    public async Task<Teacher> DeleteAsync(Guid id)
    {
        var entity = await _context.Teachers.FindAsync(id);
        _context.Teachers.Remove(entity);
        await _context.SaveChangesAsync();
        return entity;
    }

    public async Task<bool> ExistsByIdAsync(Guid id)
    {
        return await _context.Teachers.AnyAsync(e => e.id == id);
    }

    public async Task<bool> ExistsByNameAsync(string name)
    {
        return await _context.Teachers.AnyAsync(e => e.full_name == name);
    }
}