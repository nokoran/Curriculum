using Curriculum.Data;
using Curriculum.Entities;
using Microsoft.EntityFrameworkCore;

namespace Curriculum.Repositories;

public class RoomRepository : IRepository<Room>
{
    private ApplicationDbContext _context;
    
    public RoomRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Room> GetByIdAsync(Guid id)
    {
        return await _context.Rooms.FindAsync(id);
    }

    public async Task<IEnumerable<Room>> GetAllAsync()
    {
        return await _context.Rooms.ToListAsync();
    }

    public async Task<Room> AddAsync(Room entity)
    {
        var result = await _context.Rooms.AddAsync(entity);
        await _context.SaveChangesAsync();
        return result.Entity;
    }

    public async Task<Room> UpdateAsync(Room entity)
    {
        var result = _context.Rooms.Update(entity);
        await _context.SaveChangesAsync();
        return result.Entity;
    }

    public async Task<Room> DeleteAsync(Guid id)
    {
        var entity = await GetByIdAsync(id);
        if (entity != null)
        {
            var result = _context.Rooms.Remove(entity);
            await _context.SaveChangesAsync();
            return result.Entity;
        }

        return null;
    }

    public async Task<bool> ExistsByIdAsync(Guid id)
    {
        return await _context.Rooms.AnyAsync(e => e.id == id);
    }

    public async Task<bool> ExistsByNameAsync(string name)
    {
        return await _context.Rooms.AnyAsync(e => e.name == name);
    }
}