// File Path: ./Data/Repositories/DefinitionLinkRepository.cs
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

public class DefinitionLinkRepository
{
    private readonly ApplicationDbContext _context;

    public DefinitionLinkRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<List<DefinitionLink>> GetAllDefinitionLinksAsync()
    {
        return await _context.DefinitionLinks.ToListAsync();
    }

    public async Task AddDefinitionLinkAsync(DefinitionLink definitionLink)
    {
        _context.DefinitionLinks.Add(definitionLink);
        await _context.SaveChangesAsync();
    }
}