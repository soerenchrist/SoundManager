using SoundManager.Core.Models;

namespace SoundManager.Endpoints.Output;

public record GetOutputDevicesResponse(List<OutputDevice> Devices);