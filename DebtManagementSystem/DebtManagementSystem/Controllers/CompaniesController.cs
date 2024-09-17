using BusinessLayer.classes;
using DataAccessLayer.models;
using DataAccessLayer.models.Company_models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages.Infrastructure;

namespace API_Layer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CompaniesController : ControllerBase
    {

        // Completed Testing.
        [HttpGet("IsCodeExist/{code}", Name = "IsCompanyCodeExist")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<bool>> IsCompanyCodeExist(string code)
        {
            if (string.IsNullOrEmpty(code))
                return BadRequest("Invalid company code.");

            try
            {
                return Ok(await cls_Companies.IsCompanyCodeExistAsync(code));
            }
            catch
            {
                return StatusCode(500, "An error occurred while processing your request.");
            }
        }

        // Completed Testing.
        [HttpGet("IsCodeExist/{Id}/{code}", Name = "IsCompanyCodeExistWithOutCurrentCompany")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<bool>> IsCompanyCodeExistWithOutCurrentCompany(int Id, string code)
        {
            if (Id <= 0)
                return BadRequest($"Invalid company ID {Id}.");

            if (string.IsNullOrEmpty(code))
                return BadRequest($"Invalid company code.");

            try
            {
                return Ok(await cls_Companies.IsCompanyCodeExistWithOutCurrentCompanyAsync(Id, code));
            }
            catch
            {
                return StatusCode(500, "An error occurred while processing your request.");
            }
        }

        // Completed Testing.
        [HttpGet("IsPhoneExist/{phone}", Name = "IsPhoneExist")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<bool>> IsPhoneExist(string phone)
        {
            if (string.IsNullOrEmpty(phone))
                return BadRequest("Invalid company phone.");

            try
            {
                return Ok(await cls_Companies.IsCompanyPhoneExistAsync(phone));
            }
            catch
            {
                return StatusCode(500, "An error occurred while processing your request.");
            }
        }

        // Completed Testing.
        [HttpGet("IsPhoneExist/{Id}/{phone}", Name = "IsPhoneExistWithOutCurrentCompany")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<bool>> IsPhoneExistWithOutCurrentCompany(int Id, string phone)
        {
            if (Id <= 0)
                return BadRequest($"Invalid company ID {Id}.");

            if (string.IsNullOrEmpty(phone))
                return BadRequest($"Invalid company phone.");

            try
            {
                return Ok(await cls_Companies.IsCompanyPhoneExistWithOutCurrentCompanyAsync(Id, phone));
            }
            catch
            {
                return StatusCode(500, "An error occurred while processing your request.");
            }
        }

        // Completed Testing.
        [HttpGet("Count", Name = "GetCompaniesCount")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<int>> GetCompaniesCount()
        {
            try
            {
                return Ok(await cls_Companies.GetCompaniesCountAsync());
            }
            catch
            {
                return StatusCode(500, "An error occurred while processing your request.");
            }
        }

        // Completed Testing.
        [HttpGet("IsSubscriptionActive/{companyId}", Name = "IsSubscriptionActive")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<bool>> IsSubscriptionActive(int companyId)
        {
            if (companyId <= 0)
                return BadRequest($"Invalid company ID {companyId}");

            try
            {
                return Ok(await cls_Companies.IsSubscriptionActiveAsync(companyId));
            }
            catch
            {
                return StatusCode(500, "An error occurred while processing your request.");
            }
        }

        // Completed Testing.
        [HttpGet("GetAll", Name = "GetCompanies")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<IEnumerable<md_Companies>>> GetCompanies()
        {
            try
            {
                List<md_Companies>? companies = await cls_Companies.GetCompaniesAsync();

                if (companies == null)
                    companies = new List<md_Companies>();

                return Ok(companies);
            }
            catch
            {
                return StatusCode(500, "An error occurred while processing your request.");
            }
        }

        // Completed Testing.
        [HttpGet("Find/{companyId}", Name = "GetCompanyById")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<md_Company>> GetCompanyById(int companyId)
        {
            if (companyId <= 0)
                return BadRequest($"Invalid company ID {companyId}.");

            try
            {
                var company = await cls_Companies.GetCompanyByIdAsync(companyId);

                if (company == null)
                    return NotFound($"No company found.");

                return Ok
                    (
                        new md_Company
                        (
                            company.CompanyId, company.ManagerFullName, company.CompanyName, company.CompanyCode, company.CompanyImage,
                            company.Phone1, company.Phone2, company.Address, company.SubscriptionFee, company.Currency, company.RegistrationDate,
                            Convert.ToBoolean(company.SubscriptionStatus), company.SubscriptionStartDate, company.SubscriptionEndDate,
                            company.RemainingSubscriptionDays, company.Description, Convert.ToBoolean(company.IsPaid), company.ByAdmin, company.Action
                        )
                    );
            }
            catch
            {
                return StatusCode(500, "An error occurred while processing your request.");
            }
        }

        // Completed Testing.
        [HttpGet("GetActive", Name = "GetActiveCompanies")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<IEnumerable<md_Companies>>> GetActiveCompanies()
        {
            try
            {
                List<md_Companies>? companies = await cls_Companies.GetActiveCompaniesAsync();

                if (companies == null)
                    companies = new List<md_Companies>();

                return Ok(companies);
            }
            catch
            {
                return StatusCode(500, "An error occurred while processing your request.");
            }
        }

        // Completed Testing.
        [HttpGet("GetInActive", Name = "GetInActiveCompanies")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<IEnumerable<md_Companies>>> GetInActiveCompanies()
        {
            try
            {
                List<md_Companies>? companies = await cls_Companies.GetInActiveCompaniesAsync();

                if (companies == null)
                    companies = new List<md_Companies>();

                return Ok(companies);
            }
            catch
            {
                return StatusCode(500, "An error occurred while processing your request.");
            }
        }

        // Completed Testing.
        [HttpGet("GetByAdmin/{adminId}", Name = "GetCompaniesByAdmin")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<IEnumerable<md_Companies>>> GetCompaniesByAdmin(int adminId)
        {
            if (adminId <= 0)
                return BadRequest($"Invalid admin ID {adminId}");

            try
            {
                List<md_Companies>? companies = await cls_Companies.GetCompaniesByAdminAsync(adminId);

                if (companies == null)
                    companies = new List<md_Companies>();

                return Ok(companies);
            }
            catch
            {
                return StatusCode(500, "An error occurred while processing your request.");
            }
        }

        // Completed Testing.
        [HttpGet("Activation", Name = "SetCompanyAsActive")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<bool>> SetCompanyAsActive(int companyId, int adminId)
        {
            if (companyId <= 0)
                return BadRequest($"Invalid company ID {companyId}");

            if (adminId <= 0)
                return BadRequest($"Invalid admin ID {adminId}");

            try
            {
                bool result = await cls_Companies.SetCompanyAsActiveAsync(companyId, adminId);

                return result ?
                Ok(new { status = true, message = $"Company account {companyId} has been successfully activated." })
                :
                BadRequest(new { status = false, message = $"The account {companyId} could not be activated. Please try again later." });
            }
            catch
            {
                return StatusCode(500, "An error occurred while processing your request.");
            }
        }

        // Completed Testing.
        [HttpGet("Deactivation", Name = "SetCompanyAsInActive")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<bool>> SetCompanyAsInActive(int companyId, int adminId)
        {
            if (companyId <= 0)
                return BadRequest($"Invalid company ID {companyId}");

            if (adminId <= 0)
                return BadRequest($"Invalid admin ID {adminId}");

            try
            {
                bool result = await cls_Companies.SetCompanyAsInActiveAsync(companyId, adminId);

                return result ?
                Ok(new { status = true, message = $"Company account {companyId} has been successfully deactivated." })
                :
                BadRequest(new { status = false, message = $"The account {companyId} could not be deactivated. Please try again later." });
            }
            catch
            {
                return StatusCode(500, "An error occurred while processing your request.");
            }
        }

        // Completed Testing.
        [HttpGet("markAsPaid", Name = "SetPaidStatusAsPaid")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<bool>> SetPaidStatusAsPaid(int companyId, int adminId)
        {
            if (companyId <= 0)
                return BadRequest($"Invalid company ID {companyId}");

            if (adminId <= 0)
                return BadRequest($"Invalid admin ID {adminId}");

            try
            {
                bool result = await cls_Companies.SetPaidStatusAsPaidAsync(companyId, adminId);

                return result ?
                Ok(new { status = true, message = $"Payment status has been changed to Paid." })
                :
                BadRequest(new { status = false, message = $"Payment status has not been changed to Paid." });
            }
            catch
            {
                return StatusCode(500, "An error occurred while processing your request.");
            }
        }

        // Completed Testing.
        [HttpGet("markAsUnPaid", Name = "SetPaidStatusAsUnPaid")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<bool>> SetPaidStatusAsUnPaid(int companyId, int adminId)
        {
            if (companyId <= 0)
                return BadRequest($"Invalid company ID {companyId}");

            if (adminId <= 0)
                return BadRequest($"Invalid admin ID {adminId}");

            try
            {
                bool result = await cls_Companies.SetPaidStatusAsUnPaidAsync(companyId, adminId);

                return result ?
                Ok(new { status = true, message = $"Payment status has been changed to UnPaid." })
                :
                BadRequest(new { status = false, message = $"Payment status has not been changed to UnPaid." });
            }
            catch
            {
                return StatusCode(500, "An error occurred while processing your request.");
            }
        }

        // Completed Testing.
        [HttpPost("New", Name = "NewCompany")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<md_Company>> NewCompany(md_NewCompany company)
        {
            try
            {
                cls_Companies? companyEntity = new cls_Companies
                {
                    ManagerFullName = company.ManagerFullName,
                    CompanyName = company.CompanyName,
                    CompanyImage = company.CompanyImage,
                    Phone1 = company.Phone1,
                    Phone2 = company.Phone2,
                    Address = company.Address,
                    SubscriptionFee = company.SubscriptionFee,
                    Currency = company.Currency,
                    SubscriptionStatus = company.SubscriptionStatus ? cls_Companies.EN_SubscriptionStatus.Active : cls_Companies.EN_SubscriptionStatus.InActive,
                    SubscriptionStartDate = company.SubscriptionStartDate,
                    SubscriptionEndDate = company.SubscriptionEndDate,
                    Description = company.Description,
                    IsPaid = company.IsPaid ? cls_Companies.EN_IsPaid.Paid : cls_Companies.EN_IsPaid.UnPaid,
                    ByAdmin = company.ByAdmin
                };

                if (!companyEntity.ValidateCompanyObj())
                    return BadRequest(new { message = "Invalid company data.", status = false, company = company });

                if (await companyEntity.SaveAsync())
                {
                    int insertedId = companyEntity.CompanyId;
                    companyEntity = await cls_Companies.GetCompanyByIdAsync(companyEntity.CompanyId);

                    if (companyEntity == null)
                        return CreatedAtRoute
                        (
                            nameof(GetCompanyById),
                            new
                            {
                                insertedId
                            },
                            new
                            {
                                message = "Inserted new company successfully.",
                                status = true
                            }
                        );

                    md_Company insertedCompany = new md_Company
                        (
                            companyEntity.CompanyId, companyEntity.ManagerFullName, companyEntity.CompanyName, companyEntity.CompanyCode,
                            companyEntity.CompanyImage, companyEntity.Phone1, companyEntity.Phone2, companyEntity.Address, companyEntity.SubscriptionFee,
                            companyEntity.Currency, companyEntity.RegistrationDate, Convert.ToBoolean(companyEntity.SubscriptionStatus),
                            companyEntity.SubscriptionStartDate, companyEntity.SubscriptionEndDate, companyEntity.RemainingSubscriptionDays,
                            companyEntity.Description, Convert.ToBoolean(companyEntity.IsPaid), companyEntity.ByAdmin, companyEntity.Action
                        );

                    return CreatedAtRoute
                        (
                            nameof(GetCompanyById),
                            new
                            {
                                companyId = insertedCompany.CompanyId
                            },
                            new
                            {
                                message = "Inserted new company successfully.",
                                status = true,
                                company = insertedCompany
                            }
                        );
                }
                else
                    return BadRequest(new { status = false, message = "Failed to insert new company.", company = company });
            }
            catch
            {
                return StatusCode(500, new { status = false, message = "An error occurred while processing your request." });
            }
        }

        // Completed Testing.
        [HttpPut("Update", Name = "UpdateCompany")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<md_UpdateCompany>> UpdateCompany(md_UpdateCompany company)
        {

            if (company.CompanyId <= 0)
                return BadRequest($"Invalid company ID {company.CompanyId}.");

            try
            {
                cls_Companies? companyEntity = new cls_Companies
                    (
                        company.CompanyId, company.ManagerFullName, company.CompanyName, company.CompanyImage, company.Phone1,
                        company.Phone2, company.Address, company.SubscriptionFee, company.Currency, company.SubscriptionStatus,
                        company.SubscriptionStartDate, company.SubscriptionEndDate, company.Description, company.IsPaid, company.ByAdmin
                    );

                if (!companyEntity.ValidateCompanyObj())
                    return BadRequest(new { message = "Invalid company data.", status = false, company = company });

                if (await companyEntity.SaveAsync())
                {
                    return Ok
                        (
                            new
                            {
                                message = "Updated company successfully.",
                                status = true,
                                company = company
                            }
                        );
                }
                else
                    return BadRequest(new { status = false, message = "Failed to update company." });
            }
            catch
            {
                return StatusCode(500, new { status = false, message = "An error occurred while processing your request." });
            }
        }

        // Completed Testing.
        [HttpDelete("Delete", Name = "DeleteCompany")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> DeleteCategory(int companyId)
        {
            if (companyId <= 0)
                return BadRequest($"Invalid company ID {companyId}.");

            try
            {
                if (await cls_Companies.DeleteCompanyAsync(companyId))
                    return Ok(new { status = true, message = $"Company with ID {companyId} has been deleted." });
                else
                    return NotFound(new { status = false, message = $"Company with ID {companyId} not found. no rows deleted." });
            }
            catch
            {
                return StatusCode(500, "An error occurred while processing your request.");
            }
        }
    }
}

