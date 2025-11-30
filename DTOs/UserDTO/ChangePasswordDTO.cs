namespace GData.DTOs.UserDTO
{
    public class ChangePasswordDTO
    {

        public required string Username { get; set; }

        public required string Password { get; set; }

        public required string NewPassword { get; set; }

        public required string Email { get; set; }

    }
}
