using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MainAppShop.Domain.Entities.User
{
    public class Product
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Требуется поле Name.")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Требуется поле Artist.")]
        public string Artist { get; set; }
        [Required(ErrorMessage = "Требуется поле ImageUrl.")]
        public string ImageUrl { get; set; }
        [Required(ErrorMessage = "Требуется поле Price.")]
        [Range(0.01, double.MaxValue, ErrorMessage = "Цена должна быть больше 0.")]
        public decimal Price { get; set; }
        [Required(ErrorMessage = "Требуется поле Description.")]
        public string Description { get; set; }
    }
}
