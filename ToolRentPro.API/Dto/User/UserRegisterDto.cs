﻿using System.ComponentModel.DataAnnotations;

namespace ToolRentPro.API.Dto.User;

public class UserRegisterDto
{
    [Required]
    [EmailAddress]
    public string Email { get; set; } = string.Empty;
    [Required]
    public string FirstName { get; set; } = string.Empty;
    [Required]
    public string LastName { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
    public List<string>? Roles { get; set; }
}
