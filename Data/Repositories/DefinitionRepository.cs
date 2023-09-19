// File Path: ./Data/Repositories/DefinitionRepository.cs
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

public class DefinitionRepository
{
    private readonly ApplicationDbContext _context;

    public DefinitionRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<List<Definition>> GetAllDefinitionsAsync()
    {
        return await _context.Definitions.ToListAsync();
    }

    public async Task AddDefinitionAsync(Definition definition)
    {
        _context.Definitions.Add(definition);
        await _context.SaveChangesAsync();
    }
}