namespace App.Core.Dtos.UserDtos.Telegrams;

public class UserTelegramUpdateDto 
{
    public string? Username { get; set; } = string.Empty;
    public string? FirstName { get; set; } = string.Empty;
    public string? LastName { get; set; } = string.Empty;
    public string? Phone { get; set; } = string.Empty;
    public string? Language { get; set; } = string.Empty;
}
