using BusinessLayer.classes;
using DataAccessLayer.models;
using DataAccessLayer.models.Company_models;
using DataAccessLayer.models.SaleUnit_models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API_Layer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SaleUnitesController : ControllerBase
    {
        //Completed Testing
        [HttpGet("IsExist/{unitName}/{companyId}", Name = "IsSaleUnitExistAsync")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<bool>> IsSaleUnitExist(string unitName, int companyId)
        {
            if (companyId <= 0)
                return BadRequest($"Invalid company ID {companyId}.");

            if (string.IsNullOrEmpty(unitName))
                return BadRequest("Invalid unit name.");

            try
            {
                return Ok(await cls_SaleUnits.IsSaleUnitExistAsync(unitName, companyId));
            }
            catch
            {
                return StatusCode(500, "An error occurred while processing your request.");
            }
        }

        //Completed Testing
        [HttpGet("IsExist/{unitId}/{unitName}/{companyId}", Name = "IsSaleUnitExistWithOutCurrentSaleUnit")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<bool>> IsSaleUnitExistWithOutCurrentSaleUnit(int unitId, string unitName, int companyId)
        {
            if (unitId <= 0)
                return BadRequest($"Invalid unit ID {unitId}.");

            if (companyId <= 0)
                return BadRequest($"Invalid company ID {companyId}.");

            if (string.IsNullOrEmpty(unitName))
                return BadRequest("Invalid unit name.");

            try
            {
                return Ok(await cls_SaleUnits.IsSaleUnitExistWithOutCurrentSaleUnitAsync(unitId, unitName, companyId));
            }
            catch
            {
                return StatusCode(500, "An error occurred while processing your request.");
            }
        }

        //Completed Testing
        [HttpGet("Count", Name = "GetSaleUnitsCount")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<int>> GetSaleUnitsCount(int companyId)
        {
            if (companyId <= 0)
                return BadRequest($"Invalid company ID {companyId}");

            try
            {
                return Ok(await cls_SaleUnits.GetSaleUnitsCountAsync(companyId));
            }
            catch
            {
                return StatusCode(500, "An error occurred while processing your request.");
            }
        }

        //Completed Testing
        [HttpGet("Relations/{unitId}/{companyId}", Name = "IsSaleUnitHasRelations")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<bool>> IsSaleUnitHasRelations(int unitId, int companyId)
        {
            if (unitId <= 0)
                return BadRequest($"Invalid unit ID {unitId}.");

            if (companyId <= 0)
                return BadRequest($"Invalid company ID {companyId}.");

            try
            {
                return Ok(await cls_SaleUnits.IsSaleUnitHasRelationsAsync(unitId, companyId));
            }
            catch
            {
                return StatusCode(500, "An error occurred while processing your request.");
            }
        }

        //Completed Testing
        [HttpGet("GetAll", Name = "GetSaleUnits")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<IEnumerable<md_SaleUnits>>> GetSaleUnits(int companyId)
        {
            if (companyId <= 0)
                return BadRequest($"Invalid company ID {companyId}");

            try
            {
                List<md_SaleUnits>? saleUnits = await cls_SaleUnits.GetSaleUnitsAsync(companyId);

                if (saleUnits == null)
                    saleUnits = new List<md_SaleUnits>();

                return Ok(saleUnits);
            }
            catch
            {
                return StatusCode(500, "An error occurred while processing your request.");
            }
        }

        //Completed Testing
        [HttpGet("Find/{unitId}/{companyId}", Name = "GetSaleUnitById")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<md_SaleUnit>> GetSaleUnitById(int unitId, int companyId)
        {
            if (unitId <= 0)
                return BadRequest($"Invalid unit ID {unitId}.");

            if (companyId <= 0)
                return BadRequest($"Invalid company ID {companyId}.");

            try
            {
                var saleUnit = await cls_SaleUnits.GetSaleUnitByIdAsync(unitId, companyId);

                if (saleUnit == null)
                    return NotFound($"No sale units found.");

                return Ok
                    (
                        new md_SaleUnit(saleUnit.SaleUnitID, saleUnit.UnitName, saleUnit.ByUser, saleUnit.CompanyId)
                    );
            }
            catch
            {
                return StatusCode(500, "An error occurred while processing your request.");
            }
        }

        //Completed Testing
        [HttpDelete("Delete", Name = "DeleteSaleUnit")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> DeleteSaleUnit(int unitId, int companyId)
        {
            if (unitId <= 0)
                return BadRequest($"Invalid unit ID {unitId}.");

            if (companyId <= 0)
                return BadRequest($"Invalid company ID {companyId}.");

            try
            {
                if (await cls_SaleUnits.DeleteSaleUnitAsync(unitId, companyId))
                    return Ok(new { status = true, message = $"Sale unit with ID {unitId} has been deleted." });
                else
                    return NotFound(new { status = false, message = $"Sale unit with ID {unitId} not found. no rows deleted." });
            }
            catch
            {
                return StatusCode(500, "An error occurred while processing your request.");
            }
        }

        //Completed Testing
        [HttpPost("New", Name = "NewSaleUnit")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<md_NewSaleUnit>> NewSaleUnit(md_NewSaleUnit unit)
        {
            try
            {
                cls_SaleUnits? unitEntity = new cls_SaleUnits
                {
                    UnitName = unit.UnitName,
                    ByUser = unit.ByUser,
                    CompanyId = unit.CompanyId
                };

                if (!unitEntity.ValidateCategoryObj())
                    return BadRequest(new { message = "Invalid sale unit data.", status = false, saleUnit = unit });

                if (await unitEntity.SaveAsync())
                {
                    md_SaleUnit insertedUnit = new md_SaleUnit(unitEntity.SaleUnitID, unitEntity.UnitName, unitEntity.ByUser, unitEntity.CompanyId);

                    return Ok
                        (
                            new
                            {
                                message = "Inserted new sale unit successfully.",
                                status = true,
                                saleUnit = insertedUnit
                            }
                        );
                }
                else
                    return BadRequest(new { status = false, message = "Failed to insert new sale unit.", saleUnit = unit });
            }
            catch
            {
                return StatusCode(500, new { status = false, message = "An error occurred while processing your request." });
            }
        }

        //Completed Testing
        [HttpPut("Update", Name = "UpdateSaleUnit")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<md_SaleUnit>> UpdateSaleUnit(md_SaleUnit unit)
        {
            if (unit.SaleUnitID <= 0)
                return BadRequest($"Invalid sale unit ID {unit.SaleUnitID}");

            if (unit.CompanyId <= 0)
                return BadRequest($"Invalid company ID {unit.CompanyId}.");

            try
            {
                cls_SaleUnits? unitEntity = new cls_SaleUnits
                    (
                        unit.SaleUnitID, unit.UnitName, unit.ByUser, unit.CompanyId
                    );

                if (!unitEntity.ValidateCategoryObj())
                    return BadRequest(new { message = "Invalid sale unit data.", status = false, saleUnit = unit });

                if (await unitEntity.SaveAsync())
                {
                    return Ok
                        (
                            new
                            {
                                message = "Updated sale unit successfully.",
                                status = true,
                                saleUnit = unit
                            }
                        );
                }
                else
                    return BadRequest(new { status = false, message = "Failed to update sale unit." });
            }
            catch
            {
                return StatusCode(500, new { status = false, message = "An error occurred while processing your request." });
            }
        }
    }
}
