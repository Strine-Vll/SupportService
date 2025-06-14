﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Dtos.UserDtos;

public class UserPreviewDto
{
    public int Id { get; set; }

    public string Name { get; set; }

    public string Email { get; set; }

    public bool IsDeactivated { get; set; }
}
