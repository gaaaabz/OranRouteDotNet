using System.ComponentModel.DataAnnotations;

namespace FiapOrangeRoute.Auth.DTOs;

public class LoginDTO
{
    [Required]
    [EmailAddress]
    public string Email { get; set; } = string.Empty;

    [Required]
    public string Senha { get; set; } = string.Empty;
}