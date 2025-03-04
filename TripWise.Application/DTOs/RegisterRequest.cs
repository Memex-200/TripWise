using System.ComponentModel.DataAnnotations;

public class RegisterRequest
{
    [Required]
    public string FirstName { get; set; }

    [Required]
    public string LastName { get; set; }

    [Required, EmailAddress]
    public string Email { get; set; }

    [Required]
    public string Password { get; set; }

    public string Phone { get; set; }

    public string Mobile { get; set; }

    public string Address { get; set; }

    public string Details { get; set; }
}
