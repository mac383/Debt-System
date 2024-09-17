using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.models.Products_Models
{
    public class md_Products
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public string ProductCode { get; set; }
        public double ProductPrice { get; set; }
        public byte[]? ProductImage { get; set; }
        public string CategoryName { get; set; }
        public string UnitName { get; set; }
        public string UserName { get; set; }

        public md_Products(int productid, string productname, string productcode, double productPrice, byte[]? productimage,
            string categoryname, string unitname, string username)
        {
            this.ProductId = productid;
            this.ProductName = productname;
            this.ProductCode = productcode;
            this.ProductPrice = productPrice;
            this.ProductImage = productimage;
            this.CategoryName = categoryname;
            this.UnitName = unitname;
            this.UserName = username;
        }
    }
}
