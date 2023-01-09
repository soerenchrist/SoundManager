using SoundManager.Core.Models;

namespace SoundManager.Core.Interfaces;

public interface ISoundPlayer
{
    SoundPlayResult PlaySound(SoundEffect soundEffect);
    Task<bool> StopSound(Guid token, int fadeDurationMillis = -1);
}