﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymSystem.BLL.Specifications
{
    public class SpecPrams
    {
        private const int MaxPageSize = 50;

        public int PageIndex { get; set; } = 1;
        private int pageSize = 5;

        public int PageSize
        {
            get { return pageSize; }
            set { pageSize = value > MaxPageSize ? MaxPageSize : value; }
        }

        public string? Sort { get; set; }
        private string search;

        public string? Search
        {
            get { return search; }
            set { search = value.ToLower(); }
        }
    }
}
