using SoundManager.Core.Models;

namespace SoundManager.UseCases.Interfaces;

public interface IUploadSoundEffectUseCase
{
    Task<SoundEffect> AddSoundEffectAsync(Stream file, string name, double volumePercent, int offset);
}