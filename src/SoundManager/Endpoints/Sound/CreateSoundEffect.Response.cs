namespace SoundManager.Endpoints.Sound;

public class CreateSoundEffectResponse
{
    public required Guid Id { get; set; }
    public required int TotalMilliseconds { get; set; }
    public required string Name { get; set; }
    public required double VolumePercent { get; set; }
}