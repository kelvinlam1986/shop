﻿using Shop.Model.Abstract;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Model.Models
{
    [Table("Slides")]
    public class Slide : Switchable
    {
        [Key]     
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }

        [Required]
        [MaxLength(256)]
        public string Name { get; set; }
        public string Description { get; set; }

        [Required]
        [MaxLength(256)]
        public string Image { get; set; }

        [Required]
        [MaxLength(256)]
        public string Url { get; set; }
        public int? DisplayOrder { get; set; }
    }
}
