﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Dtos.ServiceRequestStatsDtos;

public class FilterStatDto
{
    public DateTime? StartDate { get; set; }

    public DateTime? EndDate { get; set; }

    public int? UserId { get; set; }
}
