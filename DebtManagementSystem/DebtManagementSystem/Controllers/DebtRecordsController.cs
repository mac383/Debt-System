using BusinessLayer.classes;
using DataAccessLayer.models;
using DataAccessLayer.models.Customers;
using DataAccessLayer.models.DebtRecords_model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API_Layer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DebtRecordsController : ControllerBase
    {
        [HttpGet("Find", Name = "GetDebtRecordById")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<md_DebtRecord>> GetDebtRecordById(int debtRecordId, int companyId)
        {
            if (debtRecordId <= 0)
                return BadRequest($"Invalid debt record ID {debtRecordId}.");

            if (companyId <= 0)
                return BadRequest($"Invalid company ID {companyId}.");

            try
            {
                var debtRecord = await cls_DebtRecords.GetDebtRecordByIdAsync(debtRecordId, companyId);

                if (debtRecord == null)
                    return NotFound($"Debt with ID {debtRecordId} and company ID {companyId} not found.");

                return Ok
                    (
                        new md_DebtRecord
                        (
                            debtRecord.DebtRecordId, debtRecord.CustomerObj.CustomerId, debtRecord.TotalPrice, debtRecord.RemainingAmount, 
                            Convert.ToBoolean(debtRecord.IsPaid), debtRecord.RegistrationDate, debtRecord.Description,
                            debtRecord.UserObj.UserId, debtRecord.CompanyId
                        )
                    );
            }
            catch
            {
                return StatusCode(500, "An error occurred while processing your request.");
            }
        }

        [HttpGet("GetAll", Name = "GetAllDebtRecords")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<IEnumerable<md_DebtRecords>>> GetAllDebtRecords(int companyId)
        {
            if (companyId <= 0)
                return BadRequest($"Invalid company ID {companyId}.");

            try
            {
                List<md_DebtRecords>? debtRecords = await cls_DebtRecords.GetDebtRecordsAsync(companyId);

                if (debtRecords == null)
                    debtRecords = new List<md_DebtRecords>();

                return Ok(debtRecords);
            }
            catch
            {
                return StatusCode(500, "An error occurred while processing your request.");
            }
        }

        [HttpGet("GetPaid", Name = "GetPaidDebtRecords")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<IEnumerable<md_DebtRecords>>> GetPaidDebtRecords(int companyId)
        {
            if (companyId <= 0)
                return BadRequest($"Invalid company ID {companyId}.");

            try
            {
                List<md_DebtRecords>? debtRecords = await cls_DebtRecords.GetPaidDebtRecordsAsync(companyId);

                if (debtRecords == null)
                    debtRecords = new List<md_DebtRecords>();

                return Ok(debtRecords);
            }
            catch
            {
                return StatusCode(500, "An error occurred while processing your request.");
            }
        }

        [HttpGet("GetUnPaid", Name = "GetUnPaidDebtRecords")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<IEnumerable<md_DebtRecords>>> GetUnPaidDebtRecords(int companyId)
        {
            if (companyId <= 0)
                return BadRequest($"Invalid company ID {companyId}.");

            try
            {
                List<md_DebtRecords>? debtRecords = await cls_DebtRecords.GetUnPaidDebtRecordsAsync(companyId);

                if (debtRecords == null)
                    debtRecords = new List<md_DebtRecords>();

                return Ok(debtRecords);
            }
            catch
            {
                return StatusCode(500, "An error occurred while processing your request.");
            }
        }

        [HttpGet("Count", Name = "GetDebtRecordsCount")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<int>> GetDebtRecordsCount(int companyId)
        {
            if (companyId <= 0)
                return BadRequest($"Invalid company ID {companyId}.");

            try
            {
                return Ok(await cls_DebtRecords.GetDebtRecordsCountAsync(companyId));
            }
            catch
            {
                return StatusCode(500, "An error occurred while processing your request.");
            }
        }

        [HttpGet("TotalDebt", Name = "GetTotalDebt")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<double>> GetTotalDebt(int companyId)
        {
            if (companyId <= 0)
                return BadRequest($"Invalid company ID {companyId}.");

            try
            {
                return Ok(await cls_DebtRecords.GetTotalDebtAsync(companyId));
            }
            catch
            {
                return StatusCode(500, "An error occurred while processing your request.");
            }
        }

        [HttpGet("Relations", Name = "IsDebtRecordHasRelations")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<bool>> IsDebtRecordHasRelations(int debtRecordId, int companyId)
        {
            if (debtRecordId <= 0)
                return BadRequest($"Invalid debt record ID {debtRecordId}.");

            if (companyId <= 0)
                return BadRequest($"Invalid company ID {companyId}.");

            try
            {
                return Ok(await cls_DebtRecords.IsDebtRecordHasRelationsAsync(debtRecordId, companyId));
            }
            catch
            {
                return StatusCode(500, "An error occurred while processing your request.");
            }
        }

        [HttpDelete("Delete", Name = "DeleteDebtRecord")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> DeleteDebtRecord(int debtRecordId, int companyId)
        {
            if (debtRecordId <= 0)
                return BadRequest($"Invalid debt record ID {debtRecordId}.");

            if (companyId <= 0)
                return BadRequest($"Invalid company ID {companyId}.");

            try
            {
                if (await cls_DebtRecords.DeleteDebtRecordAsync(debtRecordId, companyId))
                    return Ok($"Debt record with ID {debtRecordId} has been deleted.");
                else
                    return NotFound($"Debt record with ID {debtRecordId} not found. no rows deleted.");
            }
            catch
            {
                return StatusCode(500, "An error occurred while processing your request.");
            }
        }

        [HttpPost("New", Name = "NewDebtRecord")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> NewDebtRecord(md_NewDebtRecord debtRecord)
        {
            try
            {
                cls_DebtRecords? record = new cls_DebtRecords();
                record.CustomerObj.CustomerId = debtRecord.CustomerId;
                record.Description = debtRecord.Description;
                record.UserObj.ByUser = debtRecord.ByUser;
                record.CompanyId = debtRecord.CompanyId;

                if (debtRecord.DebtRecords_Products.Rows.Count <= 0)
                    return BadRequest(new { state = false, message = "Debt records products data is empty." });

                if (await record.SaveAsync(debtRecord.DebtRecords_Products))
                {
                    int insertedDebtRecordId = record.DebtRecordId;
                    int companyId = record.CompanyId;
                    record = await cls_DebtRecords.GetDebtRecordByIdAsync(insertedDebtRecordId, companyId);

                    if (record == null)
                        return CreatedAtRoute
                        (
                            nameof(GetDebtRecordById),
                            new
                            {
                                debtRecordId = insertedDebtRecordId,
                                companyId = companyId
                            },
                            new
                            {
                                message = "Inserted new debt record successfully.",
                                status = true
                            }
                        );

                    md_DebtRecord insertedDebtRecord = new md_DebtRecord
                        (
                            record.DebtRecordId, record.CustomerObj.CustomerId, record.TotalPrice, record.RemainingAmount, Convert.ToBoolean(record.IsPaid),
                            record.RegistrationDate, record.Description, record.UserObj.UserId, record.CompanyId
                        );

                    return CreatedAtRoute
                        (
                            nameof(GetDebtRecordById),
                            new
                            {
                                debtRecordId = insertedDebtRecordId,
                                companyId = companyId
                            },
                            new
                            {
                                message = "Inserted new debt record successfully.",
                                status = true,
                                debtRecord = insertedDebtRecord
                            }
                        );
                }
                else
                    return BadRequest(new { status = false, message = "Failed to insert new debt record.", debtRecord = debtRecord });
            }
            catch
            {
                return StatusCode(500, new { status = false, message = "An error occurred while processing your request." });
            }
        }

        [HttpPut("Update", Name = "UpdateDebtRecord")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> UpdateDebtRecord(md_UpdateDebtRecord debtRecord)
        {

            if (debtRecord.DebtRecordId <= 0)
                return BadRequest($"Invalid debt record ID {debtRecord.DebtRecordId}.");

            if (debtRecord.CompanyId <= 0)
                return BadRequest($"Invalid company ID {debtRecord.CompanyId}.");

            try
            {
                cls_DebtRecords? customerEntity = new cls_DebtRecords
                    (
                        debtRecord.DebtRecordId, debtRecord.CustomerId, debtRecord.Description, debtRecord.ByUser, debtRecord.CompanyId
                    );

                if (await customerEntity.SaveAsync(debtRecord.DebtRecord_Products))
                {
                    return Ok
                        (
                            new
                            {
                                message = "Updated debt record successfully.",
                                status = true,
                                company = debtRecord
                            }
                        );
                }
                else
                    return BadRequest(new { status = false, message = "Failed to update debt record." });
            }
            catch
            {
                return StatusCode(500, new { status = false, message = "An error occurred while processing your request." });
            }
        }

    }
}
