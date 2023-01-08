using NAudio.Wave;
using SoundManager.Core.Models;
using SoundManager.UseCases.Interfaces;

namespace SoundManager.UseCases.Output;

public class GetOutputDevicesUseCase : IGetOutputDevicesUseCase
{
    public List<OutputDevice> GetOutputDevices()
    {
        var outputDevices = new List<OutputDevice>();
        foreach (var device in DirectSoundOut.Devices)
        {
            outputDevices.Add(new OutputDevice
            {
                Id = device.Guid,
                Name = device.Description
            });
        }

        return outputDevices;
    }
}