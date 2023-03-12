namespace PVP_Projektas_API.Models
{
    public class UserDto
    {
        public string Name { get; set; } = null!;
        public string Lastname { get; set; } = null!;

        public string Email { get; set; } = null!;

        public string Password { get; set; } = null!;
    }
}
