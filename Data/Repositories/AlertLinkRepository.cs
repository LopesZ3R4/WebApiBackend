// File Path: ./Data/Repositories/AlertLinkRepository.cs
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

public class AlertLinkRepository
{
    private readonly ApplicationDbContext _context;

    public AlertLinkRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<List<AlertLink>> GetAllAlertLinksAsync()
    {
        return await _context.AlertLinks.ToListAsync();
    }

    public async Task AddAlertLinkAsync(AlertLink alertLink)
    {
        _context.AlertLinks.Add(alertLink);
        await _context.SaveChangesAsync();
    }
}