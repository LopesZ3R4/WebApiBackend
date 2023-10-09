using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

public class MachineRepository
{
    private readonly ApplicationDbContext _context;

    public MachineRepository(ApplicationDbContext context)
    {
        _context = context;
    }
    public bool Exists(int Id)
    {
        return _context.Machines.Any(u => u.Id == Id);
    }
    public async Task<List<Machine>> GetAllMachinesAsync()
    {
        return await _context.Machines.ToListAsync();
    }

    public async Task AddMachineAsync(Machine Machine)
    {
        _context.Machines.Add(Machine);
        await _context.SaveChangesAsync();
    }

    public async Task<(List<Machine>, bool hasMore)> GetAllMachinesAsync(int pageNumber = 1, int pageSize = 10)
    {
        var query = _context.Machines.AsQueryable();

        var totalCount = await query.CountAsync();

        query = query.Skip((pageNumber - 1) * pageSize).Take(pageSize);

        var hasMore = (pageNumber * pageSize) < totalCount;

        var Machines = await query.ToListAsync();

        return (Machines, hasMore);
    }
}