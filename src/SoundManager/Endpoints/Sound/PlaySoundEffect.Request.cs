using Microsoft.AspNetCore.Mvc;

namespace SoundManager.Endpoints.Sound;

public class PlaySoundEffectRequest
{
    [FromRoute]
    public Guid Id { get; set; }
}