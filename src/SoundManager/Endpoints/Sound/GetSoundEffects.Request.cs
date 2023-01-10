using Microsoft.AspNetCore.Mvc;

namespace SoundManager.Endpoints.Sound;

public class GetSoundEffectsRequest
{
    [FromQuery]
    public Guid? GroupId { get; set; }
}