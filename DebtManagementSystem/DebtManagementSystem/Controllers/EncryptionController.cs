using BusinessLayer.classes;
using BusinessLayer.classes.encryption;
using DataAccessLayer.models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API_Layer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EncryptionController : ControllerBase
    {
        //Completed Testing
        [HttpGet("Encrypt", Name = "Encrypt")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<string> Encrypt(string plainText)
        {
            if (string.IsNullOrEmpty(plainText))
                return BadRequest($"Plain text should not be null or empty.");

            try
            {
                return Ok(cls_encryption.HashEncryption(plainText));
            }
            catch
            {
                return StatusCode(500, "An error occurred while processing your request.");
            }
        }
    }
}
