// File Path: ./Data/Repositories/LinkRepository.cs
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

public class LinkRepository
{
    private readonly ApplicationDbContext _context;

    public LinkRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<List<Link>> GetAllLinksAsync()
    {
        return await _context.Links.ToListAsync();
    }
    public async Task AddLinkAsync(Link link)
    {
        _context.Links.Add(link);
        await _context.SaveChangesAsync();
    }
}