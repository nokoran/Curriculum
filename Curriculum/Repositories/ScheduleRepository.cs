using Curriculum.Data;
using Curriculum.Entities;
using Microsoft.EntityFrameworkCore;

namespace Curriculum.Repositories;

public class ScheduleRepository : IRepository<Schedule>
{
    private readonly ApplicationDbContext _context;
    
    public ScheduleRepository(ApplicationDbContext context)
    {
        _context = context;
    }
    
    public async Task<Schedule> GetByIdAsync(Guid id)
    {
        return await _context.Schedules.FindAsync(id);
    }

    public async Task<IEnumerable<Schedule>> GetAllAsync()
    {
        return await _context.Schedules.ToListAsync();
    }
    
    public async Task<IEnumerable<Schedule>> GetByGroupIdAsync(Guid group_id)
    {
        return await _context.Schedules.Where(e => e.group_id == group_id).ToListAsync();
    }
    
    public async Task<IEnumerable<Schedule>> GetByTeacherIdAsync(Guid teacher_id)
    {
        return await _context.Schedules.Where(e => e.teacher_id == teacher_id).ToListAsync();
    }
    
    public async Task<IEnumerable<Schedule>> GetBySubjectIdAsync(Guid subject_id)
    {
        return await _context.Schedules.Where(e => e.subject_id == subject_id).ToListAsync();
    }

    public async Task<Schedule> AddAsync(Schedule entity)
    {
        await _context.Schedules.AddAsync(entity);
        await _context.SaveChangesAsync();
        return entity;
    }

    public async Task<Schedule> UpdateAsync(Schedule entity)
    {
        _context.Entry(entity).State = EntityState.Modified;
        await _context.SaveChangesAsync();
        return entity;
    }

    public async Task<Schedule> DeleteAsync(Guid id)
    {
        var entity = await GetByIdAsync(id);
        _context.Schedules.Remove(entity);
        await _context.SaveChangesAsync();
        return entity;
    }

    public async Task<bool> ExistsByIdAsync(Guid id)
    {
        return await _context.Schedules.AnyAsync(e => e.id == id);
    }

    public async Task<bool> ExistsByNameAsync(string name)
    {
        // cannot be implemented
        return false;
    }
}