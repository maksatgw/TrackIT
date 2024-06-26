﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrackIT.Entity.Model
{
    public class Location
    {
        public int LocationId { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }
        public List<Product> Products { get; set; }
    }
}
