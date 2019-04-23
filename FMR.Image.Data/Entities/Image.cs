using FMR.Image.Data.Base;
using System.ComponentModel.DataAnnotations;

namespace FMR.Image.Data.Entities
{
    public class Image : BaseEntity
    {
        [Required]
        public byte[] File { get; set; }

        [Required]
        public string FileExtention { get; set; }

        [Required]
        [MaxLength(80)]
        public string Name { get; set; }

        [Required]
        [MaxLength(200)]
        public string Description  { get; set; }
    }
}
