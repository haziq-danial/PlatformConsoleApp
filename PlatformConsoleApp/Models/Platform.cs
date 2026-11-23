using System;
namespace PlatformConsoleApp.Models;
public class Platform
{
    public int Id { get; set; }
    public string UniqueName { get; set; } = string.Empty;
    public decimal? Latitude { get; set; }
    public decimal? Longitude { get; set; }
    public DateTime? LastUpdate { get; set; }

    // Navigation property
    public ICollection<Well> Wells { get; set; } = new List<Well>();
    public Platform()
	{
	}
}
