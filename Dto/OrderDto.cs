﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TourMarket.Models;

namespace TourMarket.Dto
{
    public class OrderDto
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public Tour Tour { get; set; }
        public Manager Manager { get; set; }
        public Tourist Tourist { get; set; }
    }
}
