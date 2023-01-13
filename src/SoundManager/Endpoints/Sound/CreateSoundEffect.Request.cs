namespace SoundManager.Endpoints.Sound;

public class CreateSoundEffectRequest
{
    public IFormFile File { get; set; } = null!;
    public string Name { get; set; } = null!;
    public double VolumePercent { get; set; } = 1.0;
    public int Offset { get; set; }
}
