using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FMR.Image.Mvc.ViewModels
{
    public class ImageViewModel
    {
        [Required]
        [Display(Name = "Nome")]
        [StringLength(80)]
        public string Name { get; set; }

        [Required]
        [Display(Name = "Descrição")]
        [StringLength(200)]
        public string Description { get; set; }

        public List<Data.Entities.Image> Images { get; set; }
    }
}
