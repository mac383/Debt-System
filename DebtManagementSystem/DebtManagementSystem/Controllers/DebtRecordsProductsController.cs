using BusinessLayer.classes;
using DataAccessLayer.models.Customers;
using DataAccessLayer.models.DebtRecordsProducts_models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API_Layer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DebtRecordsProductsController : ControllerBase
    {
        // Completed Testing.
        [HttpGet("GetAll", Name = "GetDebtRecordsProducts")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<IEnumerable<md_DebtRecordsProducts>>> GetDebtRecordsProducts(int debtRecordId, int companyId)
        {
            if (debtRecordId <= 0)
                return BadRequest($"Invalid debt record ID {debtRecordId}.");

            if (companyId <= 0)
                return BadRequest($"Invalid company ID {companyId}.");

            try
            {
                List<md_DebtRecordsProducts>? debtRecords = await cls_DebtRecordsProducts.GetDebtRecordsProductsAsync(debtRecordId, companyId);

                if (debtRecords == null)
                    debtRecords = new List<md_DebtRecordsProducts>();

                return Ok(debtRecords);
            }
            catch
            {
                return StatusCode(500, "An error occurred while processing your request.");
            }
        }
    }
}
