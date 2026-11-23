using System;
namespace PlatformConsoleApp.DTOs;

public class WellDto
{
    public int Id { get; set; }

    public int PlatformId { get; set; }

    public string UniqueName { get; set; } = string.Empty;

    public decimal? Latitude { get; set; }

    public decimal? Longitude { get; set; }

    public DateTime? CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }
}
