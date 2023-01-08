namespace SoundManager.Core.Models;

public class Group
{
    public required Guid Id { get; set; }
    public required string Name { get; set; }
    private readonly List<SoundEffect> _soundEffects = new();
    public IReadOnlyList<SoundEffect> SoundEffects => _soundEffects.AsReadOnly();
    
    public void AddSoundEffect(SoundEffect soundEffect)
    {
        _soundEffects.Add(soundEffect);
    }
    
    public void RemoveSoundEffect(SoundEffect soundEffect)
    {
        _soundEffects.Remove(soundEffect);
    }
}