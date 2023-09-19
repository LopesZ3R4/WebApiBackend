// File Path: ./Data/Repositories/DurationRepository.cs
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

public class DurationRepository
{
    private readonly ApplicationDbContext _context;

    public DurationRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<List<Duration>> GetAllDurationsAsync()
    {
        return await _context.Durations.ToListAsync();
    }

    public async Task AddDurationAsync(Duration duration)
    {
        _context.Durations.Add(duration);
        await _context.SaveChangesAsync();
    }
}