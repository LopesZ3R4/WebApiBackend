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
    public bool Exists(int Id)
        {
            return _context.Alerts.Any(u => u.Id == Id);
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
    public async Task<(List<Alert>,bool hasMore)> GetAllAlertsAsync(int pageNumber = 1, int pageSize = 10, string? type = null, string? color = null, string? severity = null, DateTime? date = null)
    {
        var query = _context.Alerts.AsQueryable();

        if (!string.IsNullOrEmpty(type))
        {
            query = query.Where(a => a.Type == type);
        }

        if (!string.IsNullOrEmpty(color))
        {
            query = query.Where(a => a.Color == color);
        }

        if (!string.IsNullOrEmpty(severity))
        {
            query = query.Where(a => a.Severity == severity);
        }

        if (date.HasValue)
        {
            query = query.Where(a => a.Time.HasValue && a.Time.Value.Date == date.Value.Date);
        }
        var totalCount = await query.CountAsync();

        query = query.Skip((pageNumber - 1) * pageSize).Take(pageSize);

        var hasMore = (pageNumber * pageSize) < totalCount;
        
        var alerts = await query.ToListAsync();

        return (alerts, hasMore);
    }
}