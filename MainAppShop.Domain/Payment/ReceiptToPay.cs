using MainAppShop.Domain.Product;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MainAppShop.Domain
{
    public class ReceiptToPay
    {
        public string Id { get; set; }
        public decimal TotalPrice { get; set; }

        public List<ProductDTO> Products { get; set; }
    }
}