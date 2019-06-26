﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElevenNote.Models
{
    public class CategoryListItem
    {
        [Display(Name ="Category #")]
        public int CategoryId { get; set; }
        [Display(Name = "Category")]
        public string CategoryName { get; set; }
    }
}
