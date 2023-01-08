using FastEndpoints;
using FluentValidation;

namespace SoundManager.Endpoints.Sound;

public class CreateSoundEffectRequest
{
    public IFormFile File { get; set; } = null!;
    public string Name { get; set; } = null!;
    public double VolumePercent { get; set; } = 1.0;
    public int Offset { get; set; }
}

public class CreateSoundEffectRequestValidator : Validator<CreateSoundEffectRequest>
{
    public CreateSoundEffectRequestValidator()
    {
        RuleFor(x => x.File).NotNull();
        RuleFor(x => x.Name).NotNull();
        RuleFor(x => x.VolumePercent).InclusiveBetween(0, 1);
        RuleFor(x => x.Offset).GreaterThanOrEqualTo(0);
    }
}