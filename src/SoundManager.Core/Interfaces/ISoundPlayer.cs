using SoundManager.Core.Models;

namespace SoundManager.Core.Interfaces;

public interface ISoundPlayer
{
    SoundPlayResult PlaySound(SoundEffect soundEffect);
    bool StopSound(Guid token);
}