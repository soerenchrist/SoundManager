using FastEndpoints;
using FluentValidation;

namespace SoundManager.Endpoints.Groups;

public class CreateGroupRequest
{
    public string Name { get; set; } = string.Empty; 
}

public class CreateGroupRequestValidator : Validator<CreateGroupRequest>
{
    public CreateGroupRequestValidator()
    {
        RuleFor(x => x.Name).NotEmpty();
    }
}