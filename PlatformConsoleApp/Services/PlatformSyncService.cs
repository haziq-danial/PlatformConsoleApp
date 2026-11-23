using Microsoft.EntityFrameworkCore;
using PlatformConsoleApp.Data;
using PlatformConsoleApp.DTOs;
using PlatformConsoleApp.Models;

namespace PlatformConsoleApp.Services;

public class PlatformSyncService
{
    private readonly AppDbContext _db;

    public PlatformSyncService(AppDbContext db)
    {
        _db = db;
    }

    public async Task SyncPlatformAsync(PlatformDto dto)
    {
        // Find existing platform
        var platform = await _db.Platforms
            .Include(p => p.Wells)
            .FirstOrDefaultAsync(p => p.Id == dto.Id);

        // Insert or update platform
        if (platform == null)
        {
            platform = new Platform
            {
                Id = dto.Id
            };

            _db.Platforms.Add(platform);
        }

        platform.UniqueName = dto.UniqueName;
        platform.Latitude = dto.Latitude;
        platform.Longitude = dto.Longitude;
        platform.LastUpdate = dto.LastUpdate;

        // Insert or update wells
        foreach (var wellDto in dto.Well ?? new List<WellDto>())
        {
            var well = platform.Wells
                .FirstOrDefault(w => w.Id == wellDto.Id);

            if (well == null)
            {
                well = new Well
                {
                    Id = wellDto.Id,
                    PlatformId = dto.Id
                };

                platform.Wells.Add(well);
            }

            well.UniqueName = wellDto.UniqueName;
            well.Latitude = wellDto.Latitude;
            well.Longitude = wellDto.Longitude;
            well.LastUpdate = wellDto.LastUpdate;
        }

        await _db.SaveChangesAsync();
    }
}