using BusinessLayer.classes;
using DataAccessLayer.models;
using DataAccessLayer.models.Company_models;
using DataAccessLayer.models.Customers;
using DataAccessLayer.models.Customers_models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API_Layer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomersController : ControllerBase
    {

        // Completed Testing.
        [HttpGet("GetAll", Name = "GetCustomers")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<IEnumerable<md_Customers>>> GetCustomers(int companyId)
        {

            if (companyId <= 0)
                return BadRequest($"Invalid company ID {companyId}.");

            try
            {
                List<md_Customers>? customers = await cls_Customers.GetCustomersAsync(companyId);

                if (customers == null)
                    customers = new List<md_Customers>();

                return Ok(customers);
            }
            catch
            {
                return StatusCode(500, "An error occurred while processing your request.");
            }
        }

        // Completed Testing.
        [HttpGet("GetActive", Name = "GetActiveCustomers")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<IEnumerable<md_Customers>>> GetActiveCustomers(int companyId)
        {

            if (companyId <= 0)
                return BadRequest($"Invalid company ID {companyId}.");

            try
            {
                List<md_Customers>? customers = await cls_Customers.GetActiveCustomersAsync(companyId);

                if (customers == null)
                    customers = new List<md_Customers>();

                return Ok(customers);
            }
            catch
            {
                return StatusCode(500, "An error occurred while processing your request.");
            }
        }

        // Completed Testing.
        [HttpGet("GetInActive", Name = "GetInActiveCustomers")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<IEnumerable<md_Customers>>> GetInActiveCustomers(int companyId)
        {
            if (companyId <= 0)
                return BadRequest($"Invalid company ID {companyId}.");

            try
            {
                List<md_Customers>? customers = await cls_Customers.GetInActiveCustomersAsync(companyId);

                if (customers == null)
                    customers = new List<md_Customers>();

                return Ok(customers);
            }
            catch
            {
                return StatusCode(500, "An error occurred while processing your request.");
            }
        }

        // Completed Testing.
        [HttpGet("Find/{customerId}/{companyId}", Name = "GetCustomerById")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<md_Customer>> GetCustomerById(int customerId, int companyId)
        {
            if (customerId <= 0)
                return BadRequest($"Invalid customer ID {customerId}.");

            if (companyId <= 0)
                return BadRequest($"Invalid company ID {companyId}.");

            try
            {
                var customer = await cls_Customers.GetCustomerByIdAsync(customerId, companyId);

                if (customer == null)
                    return NotFound($"No customer found.");

                return Ok
                    (
                        new md_Customer
                        (
                            customer.CustomerId, customer.FullName, customer.Phone1, customer.Phone2, customer.CustomerCode, customer.Address,
                            Convert.ToBoolean(customer.CustomerStatus), customer.TelegramId, customer.ByUser, customer.CompanyId
                        )
                    );
            }
            catch
            {
                return StatusCode(500, "An error occurred while processing your request.");
            }
        }

        // Completed Testing.
        [HttpGet("Activation", Name = "SetCustomerAsActive")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<bool>> SetCustomerAsActive(int customerId, int companyId)
        {
            if (customerId <= 0)
                return BadRequest($"Invalid customer ID {customerId}");

            if (companyId <= 0)
                return BadRequest($"Invalid company ID {companyId}");

            try
            {
                bool result = await cls_Customers.SetCustomerAsActiveAsync(customerId, companyId);

                return result ?
                Ok(new { status = true, message = $"Customer account {customerId} has been successfully activated." })
                :
                BadRequest(new { status = false, message = $"The account {customerId} could not be activated. Please try again later." });
            }
            catch
            {
                return StatusCode(500, "An error occurred while processing your request.");
            }
        }

        // Completed Testing.
        [HttpGet("Deactivation", Name = "SetCustomerAsInActive")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<bool>> SetCustomerAsInActive(int customerId, int companyId)
        {
            if (customerId <= 0)
                return BadRequest($"Invalid customer ID {customerId}");

            if (companyId <= 0)
                return BadRequest($"Invalid company ID {companyId}");

            try
            {
                bool result = await cls_Customers.SetCustomerAsInActiveAsync(customerId, companyId);

                return result ?
                Ok(new { status = true, message = $"Customer account {customerId} has been successfully deactivated." })
                :
                BadRequest(new { status = false, message = $"The account {customerId} could not be deactivated. Please try again later." });
            }
            catch
            {
                return StatusCode(500, "An error occurred while processing your request.");
            }
        }

        // Completed Testing.
        [HttpGet("IsCodeExist", Name = "IsCustomerCodeExist")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<bool>> IsCustomerCodeExist(string customerCode)
        {
            if (string.IsNullOrEmpty(customerCode))
                return BadRequest("Invalid customer code.");

            try
            {
                return Ok(await cls_Customers.IsCustomerCodeExistAsync(customerCode));
            }
            catch
            {
                return StatusCode(500, "An error occurred while processing your request.");
            }
        }

        // Completed Testing.
        [HttpGet("Count", Name = "GetCustomersCount")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<int>> GetCustomersCount(int companyId)
        {
            try
            {
                return Ok(await cls_Customers.GetCustomersCountAsync(companyId));
            }
            catch
            {
                return StatusCode(500, "An error occurred while processing your request.");
            }
        }

        // Completed Testing.
        [HttpGet("Relations", Name = "IsCustomerHasRelations")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<bool>> IsCustomerHasRelations(int customerId, int companyId)
        {
            if (customerId <= 0)
                return BadRequest($"Invalid customer ID {customerId}.");

            if (companyId <= 0)
                return BadRequest($"Invalid company ID {companyId}.");

            try
            {
                return Ok(await cls_Customers.IsCustomerHasRelationsAsync(customerId, companyId));
            }
            catch
            {
                return StatusCode(500, "An error occurred while processing your request.");
            }
        }

        // Completed Testing.
        [HttpDelete("Delete", Name = "DeleteCustomer")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> DeleteCustomer(int customerId, int companyId)
        {
            if (customerId <= 0)
                return BadRequest($"Invalid customer ID {customerId}.");

            if (companyId <= 0)
                return BadRequest($"Invalid company ID {companyId}.");

            try
            {
                if (await cls_Customers.DeleteCustomerAsync(customerId, companyId))
                    return Ok(new { status = true, message = $"Customer with ID {customerId} has been deleted." });
                else
                    return NotFound(new { status = false, message = $"Customer with ID {customerId} not found. no rows deleted." });
            }
            catch
            {
                return StatusCode(500, "An error occurred while processing your request.");
            }
        }

        // Completed Testing.
        [HttpPost("New", Name = "NewCustomer")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> NewCustomer(md_NewCustomer customer)
        {
            try
            {
                cls_Customers? customerEntity = new cls_Customers
                {
                    Address = customer.Address,
                    FullName = customer.FullName,
                    Phone1 = customer.Phone1,
                    Phone2 = customer.Phone2,
                    TelegramId = customer.TelegramID,
                    CompanyId = customer.CompanyId,
                    ByUser = customer.ByUser
                };

                if (!customerEntity.ValidateCustomerObject())
                    return BadRequest(new { message = "Invalid customer data.", status = false, customer = customer });

                if (await customerEntity.SaveAsync())
                {
                    int insertedCustomerId = customerEntity.CustomerId;
                    int companyId = customerEntity.CompanyId;
                    customerEntity = await cls_Customers.GetCustomerByIdAsync(insertedCustomerId, companyId);

                    if (customerEntity == null)
                        return CreatedAtRoute
                        (
                            nameof(GetCustomerById),
                            new
                            {
                                customerId = insertedCustomerId,
                                companyId = companyId
                            },
                            new
                            {
                                message = "Inserted new customer successfully.",
                                status = true
                            }
                        );

                    md_Customer insertedCustomer = new md_Customer
                        (
                            customerEntity.CustomerId, customerEntity.FullName, customerEntity.Phone1, customerEntity.Phone2, customerEntity.CustomerCode,
                            customerEntity.Address, Convert.ToBoolean(customerEntity.CustomerStatus), customerEntity.TelegramId, customerEntity.ByUser, customerEntity.CompanyId
                        );

                    return CreatedAtRoute
                        (
                            nameof(GetCustomerById),
                            new
                            {
                                customerId = insertedCustomer.CustomerId,
                                companyId = insertedCustomer.CompanyId
                            },
                            new
                            {
                                message = "Inserted new customer successfully.",
                                status = true,
                                Customer = insertedCustomer
                            }
                        );
                }
                else
                    return BadRequest(new { status = false, message = "Failed to insert new customer.", customer = customer });
            }
            catch
            {
                return StatusCode(500, new { status = false, message = "An error occurred while processing your request." });
            }
        }

        // Completed Testing.
        [HttpPut("Update", Name = "UpdateCustomer")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> UpdateCustomer(md_UpdateCustomer customer)
        {

            if (customer.CustomerId <= 0)
                return BadRequest($"Invalid customer ID {customer.CustomerId}.");

            if (customer.CompanyId <= 0)
                return BadRequest($"Invalid company ID {customer.CompanyId}.");

            try
            {
                cls_Customers? customerEntity = new cls_Customers
                    (
                        customer.CustomerId, customer.FullName, customer.Phone1, customer.Phone2, customer.Address, customer.CustomerStatus,
                        customer.TelegramID, customer.ByUser, customer.CompanyId
                    );

                if (!customerEntity.ValidateCustomerObject())
                    return BadRequest(new { message = "Invalid customer data.", status = false, customer = customer });

                if (await customerEntity.SaveAsync())
                {
                    return Ok
                        (
                            new
                            {
                                message = "Updated customer successfully.",
                                status = true,
                                company = customer
                            }
                        );
                }
                else
                    return BadRequest(new { status = false, message = "Failed to update customer." });
            }
            catch
            {
                return StatusCode(500, new { status = false, message = "An error occurred while processing your request." });
            }
        }

    }
}
