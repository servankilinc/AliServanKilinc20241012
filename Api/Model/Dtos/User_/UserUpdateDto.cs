﻿using Core.Model;

namespace Model.Dtos.User_;

public class UserUpdateDto : IDto
{
    public string? Id { get; set; }
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public string? Email { get; set; }
}
