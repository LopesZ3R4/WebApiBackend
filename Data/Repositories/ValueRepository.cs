// File Path: ./Data/Repositories/ValueRepository.cs
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

public class ValueRepository
{
    private readonly ApplicationDbContext _context;

    public ValueRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<List<Value>> GetAllValuesAsync()
    {
        return await _context.Values.ToListAsync();
    }
    public async Task AddValueAsync(Value value)
    {
        _context.Values.Add(value);
        await _context.SaveChangesAsync();
    }
}