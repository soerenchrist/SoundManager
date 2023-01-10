namespace SoundManager.Dtos;

public class SoundEffectDto
{
    public required string Name { get; set; }
    public double VolumePercent { get; set; }

    public int TotalMilliseconds { get; set; }
    public int Offset { get; set; }
    public Guid Id { get; set; }
}