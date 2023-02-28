﻿#nullable enable

namespace PVP_Projektas_API.Models;

public class User
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public string Lastname { get; set; } = null!;
    public List<Shelf>? Shelves { get; set; } // user may or may not have a shelf
}