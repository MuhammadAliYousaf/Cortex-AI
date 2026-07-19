namespace CortexAI.Infrastructure.Identity;

public sealed class DefaultAdminOptions
{
    public const string SectionName = "DefaultAdmin";

    public string Email { get; init; } = string.Empty;
    public string Password { get; init; } = string.Empty;
}
