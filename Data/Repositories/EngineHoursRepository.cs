// File Path: ./Data/Repositories/EngineHoursRepository.cs
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

public class EngineHoursRepository
{
    private readonly ApplicationDbContext _context;

    public EngineHoursRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<List<EngineHours>> GetAllEngineHoursAsync()
    {
        return await _context.EngineHours.ToListAsync();
    }

    public async Task AddEngineHoursAsync(EngineHours engineHours)
    {
        _context.EngineHours.Add(engineHours);
        await _context.SaveChangesAsync();
    }
}