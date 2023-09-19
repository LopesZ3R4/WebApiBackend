// File Path: ./Data/Repositories/AlertRepository.cs
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

public class AlertRepository
{
    private readonly ApplicationDbContext _context;

    public AlertRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<List<Alert>> GetAllAlertsAsync()
    {
        return await _context.Alerts.ToListAsync();
    }

    public async Task AddAlertAsync(Alert alert)
    {
        _context.Alerts.Add(alert);
        await _context.SaveChangesAsync();
    }
}