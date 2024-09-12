namespace ToolRentPro.API.Dto.Auth;

public class AuthResponseDto
{
    public string? Token { get; set; }
    public string? Message { get; set; }
    public bool IsSuccess { get; set; }
}
