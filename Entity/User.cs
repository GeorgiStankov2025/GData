using GData.Enums;

namespace GData.Entity
{
    public class User
    {
        public Guid Id { get; set; }

        public required string Username { get; set; }=string.Empty;

        public string PasswordHash {  get; set; }=string.Empty;

        public required string Email { get; set; } =string.Empty;

        public required string Firstname {  get; set; }=string.Empty;

        public required string Lastname { get; set;}=string.Empty;

        public UserRole UserRole { get; set; } = UserRole.User;

        public bool IsEmailConfirmed { get; set; } = false;

        public int VerificationCode { get; set; } = 0;

        public DateTime DateCreated { get; set; }

        public DateTime DateModified { get; set; }

    }
}
