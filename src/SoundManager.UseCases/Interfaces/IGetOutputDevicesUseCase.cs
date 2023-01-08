using SoundManager.Core.Models;

namespace SoundManager.UseCases.Interfaces;

public interface IGetOutputDevicesUseCase
{
    List<OutputDevice> GetOutputDevices();
}