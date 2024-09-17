using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.models.Products_Models
{
    public class md_UpdateProduct
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public double ProductPrice { get; set; }
        public byte[]? ProductImage { get; set; }
        public int CategoryId { get; set; }
        public int UnitId { get; set; }
        public int ByUser { get; set; }
        public int CompanyId { get; set; }

        public md_UpdateProduct(int productId, string productName, double productPrice, byte[]? productImage,
            int categoryId, int unitId, int byUser, int companyId)
        {
            ProductId = productId;
            ProductName = productName;
            ProductPrice = productPrice;
            ProductImage = productImage;
            CategoryId = categoryId;
            UnitId = unitId;
            ByUser = byUser;
            CompanyId = companyId;
        }
    }
}
