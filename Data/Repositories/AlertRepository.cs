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
    public async Task<(List<Alert>,bool hasMore)> GetAllAlertsAsync(int pageNumber = 1, int pageSize = 10, string? type = null, string? color = null, string? severity = null, DateTime? startDate = null, DateTime? endDate = null, String? machineType = null)
    {
        endDate ??= DateTime.Today;
        var query = _context.Alerts
                        .Include(a => a.Machine)
                        .ThenInclude(m => m.Client)
                        .AsNoTracking();

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

        if (startDate.HasValue && endDate.HasValue)
        {
            query = query.Where(a => a.Time.HasValue && a.Time.Value.Date >= startDate.Value.Date && a.Time.Value.Date <= endDate.Value.Date);
        }
        if (!string.IsNullOrEmpty(machineType))
        {
            query = query.Where(a => a.Machine.Type == machineType);
        }

        var totalCount = await query.CountAsync();

        query = query.Skip((pageNumber - 1) * pageSize).Take(pageSize);

        var hasMore = (pageNumber * pageSize) < totalCount;
        
        var alerts = await query.ToListAsync();

        return (alerts, hasMore);
    }
    public async Task UpdateAlertSentStatus(int alertId)
    {
        var alert = await _context.Alerts.FindAsync(alertId);
        if (alert != null)
        {
            alert.Sent = true;
            await _context.SaveChangesAsync();
        }
        else
        {
            throw new Exception("Alert not found");
        }
    }
}