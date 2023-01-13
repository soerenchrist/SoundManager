
namespace SoundManager.Endpoints.Sound;

public class AddToGroupRequest
{
    public Guid GroupId { get; set; }
    public Guid SoundEffectId { get; set; }
}