using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.models.Products_Models
{
    public class md_NewProduct
    {
        public string ProductName { get; set; }
        public string? ProductCode { get; set; }
        public double ProductPrice { get; set; }
        public byte[]? ProductImage { get; set; }
        public int CategoryId { get; set; }
        public int UnitId { get; set; }
        public int ByUser { get; set; }
        public int CompanyId { get; set; }

        public md_NewProduct(string productName, string? productCode, double productPrice, byte[]? productImage,
            int categoryId, int unitId, int byUser, int companyId)
        {
            ProductName = productName;
            ProductCode = productCode;
            ProductPrice = productPrice;
            ProductImage = productImage;
            CategoryId = categoryId;
            UnitId = unitId;
            ByUser = byUser;
            CompanyId = companyId;
        }
    }
}
