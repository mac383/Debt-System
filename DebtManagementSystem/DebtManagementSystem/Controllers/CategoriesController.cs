using BusinessLayer.classes;
using DataAccessLayer.models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using static System.Net.Mime.MediaTypeNames;
using System.ComponentModel.Design;

namespace API_Layer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {

        // Completed Testing.
        [HttpGet("IsExist/{categoryName}/{companyId}", Name = "IsCategoryExist")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<bool>> IsCategoryExist(string categoryName, int companyId)
        {
            if (string.IsNullOrEmpty(categoryName))
                return BadRequest("Invalid category name.");

            if (companyId <= 0)
                return BadRequest($"Invalid company ID {companyId}.");

            try
            {
                return Ok(await cls_Categories.IsCategoryExistAsync(categoryName, companyId));
            }
            catch
            {
                return StatusCode(500, "An error occurred while processing your request.");
            }
        }

        // Completed Testing.
        [HttpGet("IsExist/{categoryId}/{categoryName}/{companyId}", Name = "IsCategoryExistWithOutCurrentCategory")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<bool>> IsCategoryExistWithOutCurrentCategory(int categoryId, string categoryName, int companyId)
        {
            if (categoryId <= 0)
                return BadRequest($"Invalid category ID {categoryId}.");

            if (string.IsNullOrEmpty(categoryName))
                return BadRequest($"Invalid category name.");

            if (companyId <= 0)
                return BadRequest($"Invalid company ID {companyId}.");

            try
            {
                return Ok(await cls_Categories.IsCategoryExistWithOutCurrentCategoryAsync(categoryId, categoryName, companyId));
            }
            catch
            {
                return StatusCode(500, "An error occurred while processing your request.");
            }
        }

        // Completed Testing.
        [HttpGet("Relations", Name = "IsCategoryHasRelations")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<bool>> IsCategoryExist(int categoryId, int companyId)
        {
            if (categoryId <= 0)
                return BadRequest($"Invalid category ID {categoryId}.");

            if (companyId <= 0)
                return BadRequest($"Invalid company ID {companyId}.");

            try
            {
                return Ok(await cls_Categories.IsCategoryHasRelationsAsync(categoryId, companyId));
            }
            catch
            {
                return StatusCode(500, "An error occurred while processing your request.");
            }
        }

        // Completed Testing.
        [HttpGet("Find", Name = "GetCategoryById")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<md_Category>> GetCategoryById(int categoryId, int companyId)
        {
            if (categoryId <= 0) 
                return BadRequest($"Invalid category ID {categoryId}.");

            if (companyId <= 0) 
                return BadRequest($"Invalid company ID {companyId}.");

            try
            {
                var category = await cls_Categories.GetCategoryByIdAsync(categoryId, companyId);

                if (category == null)
                    return NotFound($"Category with ID {categoryId} and company ID {companyId} not found.");

                return Ok
                    (
                        new md_Category
                        (
                            category.CategoryId, category.CategoryName, category.Image, category.ByUser, category.CompanyId
                        )
                    );
            }
            catch
            {
                return StatusCode(500, "An error occurred while processing your request.");
            }
        }

        // Completed Testing.
        [HttpGet("GetAll", Name = "GetAllCategories")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<IEnumerable<md_Categories>>> GetCategories(int companyId)
        {
            if (companyId <= 0)
                return BadRequest($"Invalid company ID {companyId}.");

            try
            {
                List<md_Categories>? categories = await cls_Categories.GetCategoriesAsync(companyId);

                if (categories == null)
                    return NotFound($"No categories found for the given company ID: {companyId}");

                return Ok(categories);
            }
            catch
            {
                return StatusCode(500, "An error occurred while processing your request.");
            }
        }

        // Completed Testing.
        [HttpDelete("Delete", Name = "DeleteCategory")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> DeleteCategory(int categoryId, int companyId)
        {
            if (categoryId <= 0)
                return BadRequest($"Invalid category ID {categoryId}.");

            if (companyId <= 0)
                return BadRequest($"Invalid company ID {companyId}.");

            try
            {
                if (await cls_Categories.DeleteCategoryAsync(categoryId, companyId))
                    return Ok($"Category with ID {categoryId} has been deleted.");
                else
                    return NotFound($"Category with ID {categoryId} not found. no rows deleted.");
            }
            catch
            {
                return StatusCode(500, "An error occurred while processing your request.");
            }
        }

        // Completed Testing.
        [HttpGet("Count", Name = "GetCategoriesCount")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<int>> GetCategoriesCount(int companyId)
        {
            if (companyId <= 0)
                return BadRequest($"Invalid company ID {companyId}.");

            try
            {
                return Ok(await cls_Categories.GetCategoriesCountAsync(companyId));
            }
            catch
            {
                return StatusCode(500, "An error occurred while processing your request.");
            }
        }

        // Completed Testing.
        [HttpPost("New", Name = "InsertNewCategory")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<md_Category>> NewCategory(md_Category category)
        {
            try
            {
                var categoryEntity = new cls_Categories
                {
                    CategoryId = category.CategoryID,
                    CategoryName = category.CategoryName,
                    Image = category.Image,
                    ByUser = category.ByUser,
                    CompanyId = category.CompanyId
                };

                if (!categoryEntity.ValidateCategoryObject())
                    return BadRequest(new { message = "Invalid category data.", status = false, category = category });

                if (await categoryEntity.SaveAsync())
                {
                    category.CategoryID = categoryEntity.CategoryId;

                    return CreatedAtRoute
                        (
                            nameof(GetCategoryById),
                            new
                            {
                                categoryId = category.CategoryID,
                                companyId = category.CompanyId
                            },
                            new
                            {
                                message = "Inserted new category successfully.",
                                status = true,
                                category = category
                            }
                        );
                }
                else
                    return BadRequest(new { status = false, message = "Failed to insert new category." });
            }
            catch
            {
                return StatusCode(500, new { status = false, message = "An error occurred while processing your request." });
            }
        }

        // Completed Testing.
        [HttpPut("Update", Name = "UpdateCategory")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<md_Category>> UpdateCategory(md_Category category)
        {
            try
            {
                var categoryEntity = new cls_Categories
                    (
                        category.CategoryID,
                        category.CategoryName,
                        category.Image,
                        category.ByUser,
                        category.CompanyId
                    );

                if (!categoryEntity.ValidateCategoryObject())
                    return BadRequest(new { message = "Invalid category data.", status = false, category = category });

                if (await categoryEntity.SaveAsync())
                {
                    return Ok
                        (
                            new
                            {
                                message = "Updated category successfully.",
                                status = true,
                                category = category
                            }
                        );
                }
                else
                    return BadRequest(new { status = false, message = "Failed to update category." });
            }
            catch
            {
                return StatusCode(500, new { status = false, message = "An error occurred while processing your request." });
            }
        }

    }
}
