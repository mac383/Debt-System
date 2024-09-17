using BusinessLayer.classes.keys;
using BusinessLayer.classes.validation;
using DataAccessLayer.models.Products_Models;
using DataAccessLayer.repositories;
using Microsoft.Identity.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.classes
{
    public class cls_Products
    {
        enum EN_Mode { New = 1, Update = 2 }
        private EN_Mode _Mode;

        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public string ProductCode { get; set; }
        public double ProductPrice { get; set; }
        public byte[]? ProductImage { get; set; }
        public int CategoryId { get; set; }
        public int UnitId { get; set; }
        public int ByUser { get; set; }
        public int CompanyId { get; set; }

        public cls_Products()
        {
            ProductId = -1;
            ProductName = string.Empty;
            ProductCode = string.Empty;
            ProductPrice = -1;
            ProductImage = null;
            CategoryId = -1;
            UnitId = -1;
            ByUser = -1;
            CompanyId = -1;

            _Mode = EN_Mode.New;
        }

        public cls_Products(int productId, string productName, string productCode, double productPrice, byte[]? productImage,
            int categoryId, int unitId, int byUser, int companyId)
        {
            ProductId = productId;
            ProductName = productName;
            ProductCode = productCode;
            ProductPrice = productPrice;
            ProductImage = productImage;
            CategoryId = categoryId;
            UnitId = unitId;
            ByUser = byUser;
            CompanyId = companyId;

            _Mode = EN_Mode.Update;
        }

        //Completed Testing
        public bool ValidateProductObj()
        {
            if (!cls_validation.CheckLength(1, 100, ProductName))
                return false;

            if (!cls_validation.IsFloat(ProductPrice.ToString()))
                return false;

            // جميع التحقق مر بنجاح
            return true;
        }

        //Completed Testing
        public static async Task<cls_Products?> GetProductByCodeAsync(string productCode, int companyId)
        {
            md_Product? product = await cls_Products_D.GetProductByCodeAsync(productCode, companyId);

            if (product == null)
                return null;

            return new cls_Products
                (
                    product.ProductId, product.ProductName, product.ProductCode, product.ProductPrice,
                    product.ProductImage, product.CategoryId, product.UnitId, product.ByUser, product.CompanyId
                );
        }

        //Completed Testing
        public static async Task<cls_Products?> GetProductByIdAsync(int productId, int companyId)
        {
            md_Product? product = await cls_Products_D.GetProductByByIdAsync(productId, companyId);

            if (product == null)
                return null;

            return new cls_Products
                (
                    product.ProductId, product.ProductName, product.ProductCode, product.ProductPrice,
                    product.ProductImage, product.CategoryId, product.UnitId, product.ByUser, product.CompanyId
                );
        }

        //Completed Testing
        public static async Task<List<md_Products>?> GetProductsAsync(int companyId)
        {
            return await cls_Products_D.GetProductsAsync(companyId);
        }

        //Completed Testing
        public static async Task<List<md_Products>?> GetProductsByCategoryId(int categoryId, int companyId)
        {
            return await cls_Products_D.GetProductsByCategoryId(categoryId, companyId);
        }

        //Completed Testing
        public static async Task<int> GetProductsCountAsync(int companyId)
        {
            return await cls_Products_D.GetProductsCountAsync(companyId);
        }

        //Completed Testing
        public static async Task<bool> IsProductCodeExistAsync(string productCode)
        {
            return await cls_Products_D.IsProductCodeExistAsync(productCode);
        }

        //Completed Testing
        public static async Task<bool> IsProductHasRelationsAsync(int productId, int companyId)
        {
            return await cls_Products_D.IsProductHasRelationsAsync(productId, companyId);
        }

        //Completed Testing
        public static async Task<bool> DeleteProductAsync(int productId, int companyId)
        {
            return await cls_Products_D.DeleteProductAsync(productId, companyId);
        }

        //Completed Testing
        private async Task<bool> _NewProductAsync()
        {
            if (!ValidateProductObj())
                return false;

            md_NewProduct product = new md_NewProduct
                (
                    ProductName, ProductCode, ProductPrice, ProductImage, CategoryId, UnitId, ByUser, CompanyId
                );

            int insertedId = await cls_Products_D.NewProductAsync(product);
            
            ProductId = insertedId > 0 ? insertedId : -1;
            return ProductId > 0;
        }

        //Completed Testing
        private async Task<bool> _UpdateProductAsync()
        {
            if (!ValidateProductObj())
                return false;

            md_UpdateProduct product = new md_UpdateProduct
                (
                    ProductId, ProductName, ProductPrice, ProductImage, CategoryId, UnitId, ByUser, CompanyId
                );

            return await cls_Products_D.UpdateProductAsync(product);
        }

        //Completed Testing
        public async Task<bool> SaveAsync()
        {
            switch (_Mode)
            {
                case EN_Mode.New:
                    if (await _NewProductAsync())
                    {
                        _Mode = EN_Mode.Update;
                        return true;
                    }
                    break;

                case EN_Mode.Update:
                    if (await _UpdateProductAsync())
                        return true;
                    break;
            }
            return false;
        }

    }
}
