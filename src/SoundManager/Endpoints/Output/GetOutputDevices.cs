using Microsoft.AspNetCore.Mvc;
using SoundManager.UseCases.Interfaces;

namespace SoundManager.Endpoints.Output;

public class GetOutputDevices : EndpointBaseAsync.WithoutRequest.WithResult<GetOutputDevicesResponse>
{
    private readonly IGetOutputDevicesUseCase _getOutputDevicesUseCase;

    public GetOutputDevices(IGetOutputDevicesUseCase getOutputDevicesUseCase)
    {
        _getOutputDevicesUseCase = getOutputDevicesUseCase;
    }

    [HttpGet("api/v1/devices")]
    public override Task<GetOutputDevicesResponse> HandleAsync(CancellationToken ct = default)
    {
        var devices = _getOutputDevicesUseCase.GetOutputDevices();
        return Task.FromResult(new GetOutputDevicesResponse(devices));
    }
}