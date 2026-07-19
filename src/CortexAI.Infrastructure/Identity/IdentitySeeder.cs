using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.DependencyInjection;

namespace CortexAI.Infrastructure.Identity;

public static class IdentitySeeder
{
    public const string AdministratorRole = "Administrator";

    public static async Task SeedAsync(IServiceProvider services, CancellationToken cancellationToken = default)
    {
        using var scope = services.CreateScope();
        var userManager = scope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();
        var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
        var options = scope.ServiceProvider.GetRequiredService<IOptions<DefaultAdminOptions>>().Value;
        var logger = scope.ServiceProvider.GetRequiredService<ILoggerFactory>().CreateLogger("IdentitySeeder");

        if (string.IsNullOrWhiteSpace(options.Email) || string.IsNullOrWhiteSpace(options.Password))
        {
            throw new InvalidOperationException(
                "Default admin credentials are required. Set DefaultAdmin__Email and DefaultAdmin__Password.");
        }

        if (!await roleManager.RoleExistsAsync(AdministratorRole))
        {
            var roleResult = await roleManager.CreateAsync(new IdentityRole(AdministratorRole));
            EnsureSucceeded(roleResult, "create the Administrator role");
        }

        var user = await userManager.FindByEmailAsync(options.Email);
        if (user is null)
        {
            user = new ApplicationUser
            {
                UserName = options.Email,
                Email = options.Email,
                EmailConfirmed = true
            };

            var userResult = await userManager.CreateAsync(user, options.Password);
            EnsureSucceeded(userResult, "create the default administrator");
            logger.LogInformation("Created default administrator account {AdminEmail}", options.Email);
        }

        if (!await userManager.IsInRoleAsync(user, AdministratorRole))
        {
            var roleResult = await userManager.AddToRoleAsync(user, AdministratorRole);
            EnsureSucceeded(roleResult, "assign the Administrator role");
        }
    }

    private static void EnsureSucceeded(IdentityResult result, string operation)
    {
        if (!result.Succeeded)
        {
            throw new InvalidOperationException($"Could not {operation}: {string.Join("; ", result.Errors.Select(error => error.Description))}");
        }
    }
}
