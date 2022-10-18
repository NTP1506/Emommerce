﻿using System;
using System.Collections.Generic;

#nullable disable

namespace Share_Models
{
    public partial class Category
    {
        public int CatId { get; set; }
        public string CartName { get; set; }
        public string Descriptions { get; set; }
        public int? ParentId { get; set; }
        public int? Levels { get; set; }
        public int? Ordering { get; set; }
        public bool Published { get; set; }
        public string Thump { get; set; }
        public string Title { get; set; }
        public string Alias { get; set; }
        public string MetaDesc { get; set; }
        public string MetaKey { get; set; }
        public string Cover { get; set; }
        public string SchemaMarkup { get; set; }
    }
}