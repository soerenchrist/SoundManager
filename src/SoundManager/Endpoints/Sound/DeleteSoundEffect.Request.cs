using Microsoft.AspNetCore.Mvc;

namespace SoundManager.Endpoints.Sound;

public class DeleteSoundEffectRequest
{
   [FromRoute]
   public Guid Id { get; set; }
}