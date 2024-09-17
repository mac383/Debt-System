using BusinessLayer.classes;
using BusinessLayer.classes.keys;
using DataAccessLayer.models.Products_Models;
using DataAccessLayer.models.User_models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API_Layer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        //Completed Testing
        [HttpGet("GetProductByCode/{productCode}/{companyId}", Name = "GetProductByCode")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<md_Product>> GetProductByCode(string productCode, int companyId)
        {
            if (string.IsNullOrEmpty(productCode))
                return BadRequest($"Invalid product code.");

            if (companyId <= 0)
                return BadRequest($"Invalid company ID {companyId}.");

            try
            {
                var product = await cls_Products.GetProductByCodeAsync(productCode, companyId);

                if (product == null)
                    return NotFound($"No product found.");

                return Ok
                    (
                        new md_Product
                        (
                            product.ProductId, product.ProductName, product.ProductCode, product.ProductPrice, product.ProductImage,
                            product.CategoryId, product.UnitId, product.ByUser, product.CompanyId
                        )
                    );
            }
            catch
            {
                return StatusCode(500, "An error occurred while processing your request.");
            }
        }

        //Completed Testing
        [HttpGet("GetProductById/{productId}/{companyId}", Name = "GetProductById")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<md_Product>> GetProductById(int productId, int companyId)
        {
            if (productId <= 0)
                return BadRequest($"Invalid product ID {productId}.");

            if (companyId <= 0)
                return BadRequest($"Invalid company ID {companyId}.");

            try
            {
                var product = await cls_Products.GetProductByIdAsync(productId, companyId);

                if (product == null)
                    return NotFound($"No product found.");

                return Ok
                    (
                        new md_Product
                        (
                            product.ProductId, product.ProductName, product.ProductCode, product.ProductPrice, product.ProductImage,
                            product.CategoryId, product.UnitId, product.ByUser, product.CompanyId
                        )
                    );
            }
            catch
            {
                return StatusCode(500, "An error occurred while processing your request.");
            }
        }

        //Completed Testing
        [HttpGet("GetAll", Name = "GetAllProducts")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<IEnumerable<md_Products>>> GetAllProducts(int companyId)
        {
            if (companyId <= 0)
                return BadRequest($"Invalid company ID {companyId}.");

            try
            {
                List<md_Products>? products = await cls_Products.GetProductsAsync(companyId);

                if (products == null)
                    products = new List<md_Products>();

                return Ok(products);
            }
            catch
            {
                return StatusCode(500, "An error occurred while processing your request.");
            }
        }

        //Completed Testing
        [HttpGet("GetByCategoryId/{categoryId}/{companyId}", Name = "GetByCategoryId")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<IEnumerable<md_Products>>> GetByCategoryId(int categoryId, int companyId)
        {
            if (categoryId <= 0)
                return BadRequest($"Invalid category ID {categoryId}.");

            if (companyId <= 0)
                return BadRequest($"Invalid company ID {companyId}.");

            try
            {
                List<md_Products>? products = await cls_Products.GetProductsByCategoryId(categoryId, companyId);

                if (products == null)
                    products = new List<md_Products>();

                return Ok(products);
            }
            catch
            {
                return StatusCode(500, "An error occurred while processing your request.");
            }
        }

        //Completed Testing
        [HttpGet("Count", Name = "GetProductsCount")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<int>> GetProductsCount(int companyId)
        {
            if (companyId <= 0)
                return BadRequest($"Invalid company ID {companyId}.");

            try
            {
                return Ok(await cls_Products.GetProductsCountAsync(companyId));
            }
            catch
            {
                return StatusCode(500, "An error occurred while processing your request.");
            }
        }

        //Completed Testing
        [HttpGet("IsProductCodeExist/{productCode}", Name = "IsProductCodeExist")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<bool>> IsProductCodeExist(string productCode)
        {
            if (string.IsNullOrEmpty(productCode))
                return BadRequest("Invalid  product code.");

            try
            {
                return Ok(await cls_Products.IsProductCodeExistAsync(productCode));
            }
            catch
            {
                return StatusCode(500, "An error occurred while processing your request.");
            }
        }

        //Completed Testing
        [HttpGet("IsProductHasRelations/{productId}/{companyId}", Name = "IsProductHasRelations")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<bool>> IsProductHasRelations(int productId, int companyId)
        {
            if (productId <= 0)
                return BadRequest($"Invalid  product ID {productId}.");

            if (companyId <= 0)
                return BadRequest($"Invalid  company ID {companyId}.");

            try
            {
                return Ok(await cls_Products.IsProductHasRelationsAsync(productId, companyId));
            }
            catch
            {
                return StatusCode(500, "An error occurred while processing your request.");
            }
        }

        //Completed Testing
        [HttpDelete("Delete", Name = "DeleteProduct")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> DeleteProduct(int productId, int companyId)
        {
            if (productId <= 0)
                return BadRequest($"Invalid product ID {productId}.");

            if (companyId <= 0)
                return BadRequest($"Invalid company ID {companyId}.");

            try
            {
                if (await cls_Products.DeleteProductAsync(productId, companyId))
                    return Ok(new { status = true, message = $"Product with ID {productId} has been deleted successfully." });
                else
                    return NotFound(new { status = false, message = $"Product with ID {productId} not found. no rows deleted." });
            }
            catch
            {
                return StatusCode(500, "An error occurred while processing your request.");
            }
        }

        //Completed Testing
        [HttpPost("New", Name = "NewProduct")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<md_NewProduct>> NewProduct(md_NewProduct product)
        {
            do
            {

                product.ProductCode = cls_Keys.GetKey(10, 1, cls_Keys.EN_KeyType.NumbersLetters);

            } while (await cls_Products.IsProductCodeExistAsync(product.ProductCode));

            try
            {
                cls_Products? productEntity = new cls_Products
                {
                    ProductName = product.ProductName,
                    ProductCode = product.ProductCode,
                    ProductPrice = product.ProductPrice,
                    ProductImage = product.ProductImage,
                    CategoryId = product.CategoryId,
                    UnitId = product.UnitId,
                    ByUser = product.ByUser,
                    CompanyId = product.CompanyId
                };

                if (!productEntity.ValidateProductObj())
                    return BadRequest(new { message = "Invalid product data.", status = false, product = product });

                if (await productEntity.SaveAsync())
                {
                    int insertedId = productEntity.ProductId;
                    int companyId = productEntity.CompanyId;
                    productEntity = await cls_Products.GetProductByIdAsync(productEntity.ProductId, productEntity.CompanyId);

                    if (productEntity == null)
                        return CreatedAtRoute
                        (
                            nameof(GetProductById),
                            new
                            {
                                insertedId,
                                companyId
                            },
                            new
                            {
                                message = "Inserted new product successfully.",
                                status = true
                            }
                        );

                    md_Product insertedProduct = new md_Product
                        (
                            productEntity.ProductId, productEntity.ProductName, productEntity.ProductCode, productEntity.ProductPrice,
                            productEntity.ProductImage, productEntity.CategoryId, productEntity.UnitId, productEntity.ByUser, productEntity.CompanyId
                        );

                    return CreatedAtRoute
                        (
                            nameof(GetProductById),
                            new
                            {

                                insertedProduct.ProductId,
                                insertedProduct.CompanyId
                            },
                            new
                            {
                                message = "Inserted new product successfully.",
                                status = true,
                                product = insertedProduct
                            }
                        );
                }
                else
                    return BadRequest(new { status = false, message = "Failed to insert new product.", product = product });
            }
            catch
            {
                return StatusCode(500, new { status = false, message = "An error occurred while processing your request." });
            }
        }

        //Completed Testing
        [HttpPut("UpdateProduct", Name = "UpdateProduct")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<md_UpdateProduct>> UpdateProduct(md_UpdateProduct product)
        {
            if (product.ProductId <= 0)
                return BadRequest($"Invalid product ID {product.ProductId}.");

            if (product.CompanyId <= 0)
                return BadRequest($"Invalid company ID {product.CompanyId}.");

            try
            {
                cls_Products? productEntity = new cls_Products
                    (
                        product.ProductId, product.ProductName, string.Empty, product.ProductPrice, product.ProductImage,
                        product.CategoryId, product.UnitId, product.ByUser, product.CompanyId
                    );

                if (!productEntity.ValidateProductObj())
                    return BadRequest(new { message = "Invalid product data.", status = false, product = product });

                if (await productEntity.SaveAsync())
                {
                    return Ok
                        (
                            new
                            {
                                message = "Updated product successfully.",
                                status = true,
                                product = product
                            }
                        );
                }
                else
                    return BadRequest(new { status = false, message = "Failed to update product." });
            }
            catch
            {
                return StatusCode(500, new { status = false, message = "An error occurred while processing your request." });
            }
        }

    }
}
