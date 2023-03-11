#nullable enable

namespace PVP_Projektas_API.Models;

public class User
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public string Lastname { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string Password { get; set; } = null!;
    public List<Shelf> Shelves { get; set; } = new List<Shelf>(); // user WILL have atleast one shelf
}