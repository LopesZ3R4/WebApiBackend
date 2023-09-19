// File Path: ./Data/Repositories/LocationRepository.cs
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

public class LocationRepository
{
    private readonly ApplicationDbContext _context;

    public LocationRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<List<Location>> GetAllLocationsAsync()
    {
        return await _context.Locations.ToListAsync();
    }

    public async Task AddLocationAsync(Location location)
    {
        _context.Locations.Add(location);
        await _context.SaveChangesAsync();
    }}