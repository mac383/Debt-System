using BusinessLayer.classes;
using DataAccessLayer.models;
using DataAccessLayer.models.Company_models;
using DataAccessLayer.models.PaidRecords_models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API_Layer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaidRecordsController : ControllerBase
    {
        [HttpGet("Count", Name = "GetPaidRecordsCount")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<int>> GetPaidRecordsCount(int debtRecordId, int companyId)
        {
            if (debtRecordId <= 0)
                return BadRequest($"Invalid debt record ID {debtRecordId}.");

            if (companyId <= 0)
                return BadRequest($"Invalid company ID {companyId}.");

            try
            {
                return Ok(await cls_PaidRecords.GetPaidRecordsCountByDebtRecordIdAsync(debtRecordId, companyId));
            }
            catch
            {
                return StatusCode(500, "An error occurred while processing your request.");
            }
        }

        [HttpGet("GetAll", Name = "GetPaidRecords")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<IEnumerable<md_PaidRecords>>> GetPaidRecords(int companyId)
        {
            if (companyId <= 0)
                return BadRequest($"Invalid company ID {companyId}.");
            try
            {
                List<md_PaidRecords>? paidRecords = await cls_PaidRecords.GetPaidRecordsAsync(companyId);

                if (paidRecords == null)
                    paidRecords = new List<md_PaidRecords>();

                return Ok(paidRecords);
            }
            catch
            {
                return StatusCode(500, "An error occurred while processing your request.");
            }
        }

        [HttpGet("Get{debtRecordId}", Name = "GetPaidRecordsByDebtRecordId")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<IEnumerable<md_PaidRecords>>> GetPaidRecordsByDebtRecordId(int debtRecordId, int companyId)
        {
            if (debtRecordId <= 0)
                return BadRequest($"Invalid debt record ID {debtRecordId}.");

            if (companyId <= 0)
                return BadRequest($"Invalid company ID {companyId}.");
            try
            {
                List<md_PaidRecords>? paidRecords = await cls_PaidRecords.GetPaidRecordsByDebtRecordIdAsync(debtRecordId, companyId);

                if (paidRecords == null)
                    paidRecords = new List<md_PaidRecords>();

                return Ok(paidRecords);
            }
            catch
            {
                return StatusCode(500, "An error occurred while processing your request.");
            }
        }

        [HttpPost("New", Name = "NewPaid")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<md_PaidRecords>> NewPaid(md_NewPaid paid)
        {
            try
            {
                cls_PaidRecords? paidEntity = new cls_PaidRecords
                {
                    DebtRecordId = paid.DebtRecordId,
                    PaymentAmount = paid.PaymentAmount,
                    Description = paid.Description,
                    ByUser = paid.ByUser,
                    CompanyId = paid.CompanyId
                };

                if (!paidEntity.ValidatePaidRecordObj())
                    return BadRequest(new { message = "Invalid paid record data.", status = false, paid = paid });

                if (await paidEntity.SaveAsync())
                {
                    int insertedId = paidEntity.PaidRecordId;

                    return Ok
                        (
                            new
                            {
                                message = "Inserted new paid record successfully.",
                                status = true
                            }
                        );
                }
                else
                    return BadRequest(new { status = false, message = "Failed to insert new paid record.", paid = paid });
            }
            catch
            {
                return StatusCode(500, new { status = false, message = "An error occurred while processing your request." });
            }
        }

        [HttpPut("Update", Name = "UpdatePaid")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<md_UpdatePaid>> UpdatePaid(md_UpdatePaid paid)
        {

            if (paid.PaidRecordId <= 0)
                return BadRequest($"Invalid paid record ID {paid.PaidRecordId}.");

            try
            {
                cls_PaidRecords? paidEntity = new cls_PaidRecords
                    (
                        paid.PaidRecordId, paid.PaymentAmount, paid.Description, paid.ByUser, paid.CompanyId
                    );

                if (!paidEntity.ValidatePaidRecordObj())
                    return BadRequest(new { message = "Invalid paid record data.", status = false, paid = paid });

                if (await paidEntity.SaveAsync())
                {
                    return Ok
                        (
                            new
                            {
                                message = "Updated paid record successfully.",
                                status = true,
                                paid = paid
                            }
                        );
                }
                else
                    return BadRequest(new { status = false, message = "Failed to update paid record." });
            }
            catch
            {
                return StatusCode(500, new { status = false, message = "An error occurred while processing your request." });
            }
        }
    }
}
