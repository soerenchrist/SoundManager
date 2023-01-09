using Microsoft.AspNetCore.Mvc;

namespace SoundManager.Endpoints.Sound;

public class StopSoundEffectRequest
{
    [FromRoute]
    public Guid Token { get; set; }

    [FromQuery]
    public int FadeDurationMillis { get; set; } = -1;
}