﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Models
{
    public class MoviePurchaseReportModel
    {
        public int MovieId { get; set; }
        public string Title { get; set; }
        public int TotalPurchases { get; set; }
    }
}
