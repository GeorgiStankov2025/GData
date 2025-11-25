namespace GData.DTOs
{
    public class RegisterUserDTO
    {

        public required string Username { get; set; } = string.Empty;

        public required string Password { get; set; } = string.Empty;

        public required string Email { get; set; } = string.Empty;

        public required string Firstname { get; set; } = string.Empty;

        public required string Lastname { get; set; } = string.Empty;

    }
}
