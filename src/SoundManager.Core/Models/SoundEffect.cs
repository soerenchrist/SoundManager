namespace SoundManager.Core.Models;

public class SoundEffect
{
    private double _volumePercent = 1f;
    public required string FilePath { get; set; }
    public required string Name { get; set; }
    public List<Group> Groups { get; set; } = new();
    public double VolumePercent
    {
        get => _volumePercent;
        set
        {
            value = value switch
            {
                > 1 => 1,
                < 0 => 0,
                _ => value
            };
            _volumePercent = value;
        }
    }

    public int TotalMilliseconds { get; set; }
    public int Offset { get; set; }
    public Guid Id { get; set; }
}