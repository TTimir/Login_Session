﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Login_Session.Models;

public partial class TblUser
{
    public int Id { get; set; }
    [Required]
    public string Name { get; set; } = null!;
    [Required]
    public string Gender { get; set; } = null!;
    [Required]
    public int? Age { get; set; }
    [Required]
    public string Email { get; set; } = null!;
    [Required]
    [DataType(DataType.Password)]
    public string Password { get; set; } = null!;
}
