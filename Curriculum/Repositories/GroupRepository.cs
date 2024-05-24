using Curriculum.Data;
using Curriculum.Entities;
using Curriculum.Models;
using Microsoft.EntityFrameworkCore;

namespace Curriculum.Repositories;

public class GroupRepository : IRepository<Group>
{
    private ApplicationDbContext _context;
    
    public GroupRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Group> GetByIdAsync(Guid id)
    {
        return await _context.Groups.FindAsync(id);
    }
    
    public async Task<IEnumerable<Group>> GetByCourseIdAsync(Guid id)
    {
        return await _context.Groups.Where(g => g.course_id == id).ToListAsync();
    }
    
    public async Task<IEnumerable<Group>> GetAllAsync()
    {
        return await _context.Groups.ToListAsync();
    }
    
    public async Task<Group> AddAsync(Group entity)
    {
        _context.Groups.Add(entity);
        await _context.SaveChangesAsync();
        return entity;
    }
    
    public async Task<Group> UpdateAsync(Group entity)
    {
        _context.Entry(entity).State = EntityState.Modified;
        await _context.SaveChangesAsync();
        return entity;
    }
    
    public async Task<Group> DeleteAsync(Guid id)
    {
        var group = await _context.Groups.FindAsync(id);
        if (group == null)
        {
            return null;
        }
        
        _context.Groups.Remove(group);
        await _context.SaveChangesAsync();
        return group;
    }
    
    public async Task<bool> ExistsByNameAsync(string name)
    {
        return await _context.Groups.AnyAsync(e => e.group_name == name);
    }
    
    public async Task<bool> ExistsByIdAsync(Guid id)
    {
        return await _context.Groups.AnyAsync(e => e.id == id);
    }
    
}