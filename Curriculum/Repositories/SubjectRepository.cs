using Curriculum.Data;
using Curriculum.Entities;
using Microsoft.EntityFrameworkCore;

namespace Curriculum.Repositories;

public class SubjectRepository : IRepository<Subject>
{
    private readonly ApplicationDbContext _context;
    
    public SubjectRepository(ApplicationDbContext context)
    {
        _context = context;
    }
    
    public async Task<Subject> GetByIdAsync(Guid id)
    {
    return await _context.Subjects.FindAsync(id);
    }

    public async Task<IEnumerable<Subject>> GetAllAsync()
    {
        return await _context.Subjects.ToListAsync();
    }

    public async Task<Subject> AddAsync(Subject entity)
    {
        await _context.Subjects.AddAsync(entity);
        await _context.SaveChangesAsync();
        return entity;
    }

    public async Task<Subject> UpdateAsync(Subject entity)
    {
        _context.Entry(entity).State = EntityState.Modified;
        await _context.SaveChangesAsync();
        return entity;
    }

    public async Task<Subject> DeleteAsync(Guid id)
    {
        var entity = await GetByIdAsync(id);
        _context.Subjects.Remove(entity);
        await _context.SaveChangesAsync();
        return entity;
    }

    public async Task<bool> ExistsByIdAsync(Guid id)
    {
        return await _context.Subjects.AnyAsync(e => e.id == id);
    }

    public async Task<bool> ExistsByNameAsync(string name)
    {
        return await _context.Subjects.AnyAsync(e => e.subject_name == name);
    }
}