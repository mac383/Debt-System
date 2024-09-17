using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.models.Products_Models
{
    public class md_Product
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public string ProductCode { get; set; }
        public double ProductPrice { get; set; }
        public byte[]? ProductImage { get; set; }
        public int CategoryId { get; set; }
        public int UnitId { get; set; }
        public int ByUser { get; set; }
        public int CompanyId { get; set; }

        public md_Product(int productid, string productname, string productcode, double productPrice, byte[]? productimage,
            int categoryId, int unitId, int byUser, int companyId)
        {
            this.ProductId = productid;
            this.ProductName = productname;
            this.ProductCode = productcode;
            this.ProductPrice = productPrice;
            this.ProductImage = productimage;
            this.CategoryId = categoryId;
            this.UnitId = unitId;
            this.ByUser = byUser;
            this.CompanyId = companyId;
        }
    }
}
