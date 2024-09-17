using BusinessLayer.classes;
using DataAccessLayer.models;
using DataAccessLayer.models.Settings_models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API_Layer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SettingsController : ControllerBase
    {
        // Completed Testing.
        [HttpGet("Find", Name = "GetSettings")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<md_Setting>> GetSettings(int companyId)
        {
            if (companyId <= 0)
                return BadRequest($"Invalid company ID {companyId}.");

            try
            {
                var settings = await cls_Settings.GetSettingsAsync(companyId);

                if (settings == null)
                    return NotFound($"Settings with company ID {companyId} not found.");

                return Ok
                    (
                        new md_Setting
                        (
                            settings.SettingId, settings.CompanyName, settings.Description, settings.Logo,
                            settings.Currency, settings.PaymentRequestMessage, settings.CompanyId
                        )
                    );
            }
            catch
            {
                return StatusCode(500, "An error occurred while processing your request.");
            }
        }

        // Completed Testing.
        [HttpPut("Update", Name = "UpdateSetting")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<md_Setting>> UpdateSetting(md_UpdateSetting setting)
        {
            if (setting.CompanyId <= 0)
                return BadRequest($"Invalid company ID {setting.CompanyId}");

            try
            {
                var settingEntity = new cls_Settings
                    (
                        setting.CompanyName, setting.Description, setting.Logo, setting.Currency, setting.PaymentRequestMessage, setting.CompanyId
                    );

                if (!settingEntity.ValidateSettingObject())
                    return BadRequest(new { message = "Invalid setting data.", status = false, setting = setting });

                if (await settingEntity.UpdateSettingsAsync())
                {
                    return Ok
                        (
                            new
                            {
                                message = "Updated setting successfully.",
                                status = true,
                                setting = setting
                            }
                        );
                }
                else
                    return BadRequest(new { status = false, message = "Failed to update setting." });
            }
            catch
            {
                return StatusCode(500, new { status = false, message = "An error occurred while processing your request." });
            }
        }

    }
}
