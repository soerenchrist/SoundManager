using FastEndpoints;
using SoundManager.UseCases.Interfaces;

namespace SoundManager.Endpoints.Output;

public class GetOutputDevices : EndpointWithoutRequest<GetOutputDevicesResponse>
{
    private readonly IGetOutputDevicesUseCase _getOutputDevicesUseCase;

    public GetOutputDevices(IGetOutputDevicesUseCase getOutputDevicesUseCase)
    {
        _getOutputDevicesUseCase = getOutputDevicesUseCase;
    }

    public override void Configure()
    {
        Get("devices");
        AllowAnonymous();
    }

    public override async Task HandleAsync(CancellationToken ct)
    {
        var devices = _getOutputDevicesUseCase.GetOutputDevices();
        await SendAsync(new GetOutputDevicesResponse(devices), cancellation: ct);
    }
}