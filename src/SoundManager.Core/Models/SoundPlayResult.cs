namespace SoundManager.Core.Models;

public record SoundPlayResult(Guid Token, Guid SoundEffectId, int DurationMillis);